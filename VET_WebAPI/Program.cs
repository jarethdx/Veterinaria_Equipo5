using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using VET_WebAPI.Identity;
using VET_WebAPI.LogicaNegocio.Interfaces;
using VET_WebAPI.LogicaNegocio.Servicios;

var builder = WebApplication.CreateBuilder(args);

// ── 1. CONTROLLERS ──────────────────────────────────────────────────────────
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ── 2. BASE DE DATOS IDENTITY (para login/usuarios) ─────────────────────────
var lCnxIdentity = builder.Configuration.GetConnectionString("IdentityCnx");
builder.Services.AddDbContext<VetIdentityDbContext>(op => op.UseSqlServer(lCnxIdentity));

// ── 3. ASP.NET IDENTITY (igual al demo del profesor) ────────────────────────
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequireDigit           = true;
    options.Password.RequireLowercase       = true;
    options.Password.RequireUppercase       = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength         = 8;
    options.SignIn.RequireConfirmedEmail    = false;
    options.Lockout.AllowedForNewUsers      = true;
    options.Lockout.MaxFailedAccessAttempts = 5;
})
.AddDefaultTokenProviders()
.AddEntityFrameworkStores<VetIdentityDbContext>();

// ── 4. AUTENTICACIÓN JWT ─────────────────────────────────────────────────────
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme    = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer           = true,
        ValidateAudience         = true,
        ValidateLifetime         = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer              = builder.Configuration["JWT:Issuer"],
        ValidAudience            = builder.Configuration["JWT:Audience"],
        IssuerSigningKey         = new SymmetricSecurityKey(
                                       Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]!))
    };
});

// ── 5. INYECCIÓN DE DEPENDENCIAS (servicios de negocio) ──────────────────────
builder.Services.AddScoped<IlnClientes,       lnClientes>();
builder.Services.AddScoped<IlnMascotas,       lnMascotas>();
builder.Services.AddScoped<IlnDetalleMascota, lnDetalleMascota>();
builder.Services.AddScoped<IlnCitas,          lnCitas>();
builder.Services.AddScoped<IlnDiagnostico,    lnDiagnostico>();

// ── 6. CORS (permite que el MVC consuma la API) ──────────────────────────────
builder.Services.AddCors(options =>
{
    options.AddPolicy("VetPolicy", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// ── 7. PIPELINE HTTP ─────────────────────────────────────────────────────────
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("VetPolicy");
app.UseAuthentication();   // Primero autenticar
app.UseAuthorization();    // Luego autorizar
app.MapControllers();

app.Run();
