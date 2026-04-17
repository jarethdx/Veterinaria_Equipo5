using VET_MVC.Servicios;

var builder = WebApplication.CreateBuilder(args);

// ── SERVICIOS ────────────────────────────────────────────────────────────────
builder.Services.AddControllersWithViews();

// Inyección de dependencias del servicio que consume la API
builder.Services.AddScoped<IsrvVeterinaria, srvVeterinaria>();

var app = builder.Build();

// ── PIPELINE ─────────────────────────────────────────────────────────────────
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// Ruta por defecto apunta al Login (igual al LabS13 del profesor)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=login}/{id?}");

app.Run();
