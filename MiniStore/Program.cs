using Microsoft.EntityFrameworkCore;
using MiniStore.Data;

var builder = WebApplication.CreateBuilder(args);

// ===== Services =====
builder.Services.AddControllersWithViews(); // MVC + API por atributos

// EF Core (usa "DefaultConnection" de appsettings.Development.json)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// CORS: front Angular por HTTPS (4200 y opcional 4201)
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("ng", p => p
        .WithOrigins("https://localhost:4200", "https://localhost:4201")
        .AllowAnyHeader()
        .AllowAnyMethod()
        // .AllowCredentials() // habilítalo si vas a usar cookies/sesión
    );
});

// Swagger en Development
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ===== Pipeline =====
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCors("ng");

// Si usas autenticación: app.UseAuthentication();
app.UseAuthorization();

// ===== Endpoints =====
app.MapControllers(); // API por atributos
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// ===== Migrar BD (sin semillas) =====
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await db.Database.MigrateAsync(); // <-- comenta esta línea si NO quieres auto-migrate
}

app.Run();
