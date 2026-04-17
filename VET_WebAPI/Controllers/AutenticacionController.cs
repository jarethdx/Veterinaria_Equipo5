using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace VET_WebAPI.Controllers
{
    // Este controller maneja el login y devuelve el token JWT
    // Sigue el mismo patrón del LabS13 del profesor
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class AutenticacionController : ControllerBase
    {
        private readonly UserManager<IdentityUser>  _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration             _config;

        public AutenticacionController(
            UserManager<IdentityUser>  pUserManager,
            SignInManager<IdentityUser> pSignInManager,
            IConfiguration             pConfig)
        {
            _userManager   = pUserManager;
            _signInManager = pSignInManager;
            _config        = pConfig;
        }

        // POST api/Autenticacion/validarUsuario
        // Recibe { "usuario": "admin@vet.com", "contrasena": "Admin123!" }
        // Devuelve { "token": "eyJ..." }
        [HttpPost("validarUsuario")]
        public async Task<IActionResult> ValidarUsuario([FromBody] LoginRequest pLogin)
        {
            // Buscar el usuario por email
            var lUsuario = await _userManager.FindByEmailAsync(pLogin.usuario);
            if (lUsuario == null)
                return Ok(new { token = "" });

            // Verificar contraseña
            var lResultado = await _signInManager.CheckPasswordSignInAsync(lUsuario, pLogin.contrasena, false);
            if (!lResultado.Succeeded)
                return Ok(new { token = "" });

            // Generar el token JWT
            var lToken = GenerarToken(lUsuario);
            return Ok(new { token = lToken });
        }

        // Genera el token JWT con los datos del usuario
        private string GenerarToken(IdentityUser pUsuario)
        {
            var lClaims = new[]
            {
                new Claim(ClaimTypes.Name,  pUsuario.UserName ?? ""),
                new Claim(ClaimTypes.Email, pUsuario.Email    ?? ""),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var lLlave    = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]!));
            var lCredencial = new SigningCredentials(lLlave, SecurityAlgorithms.HmacSha256);

            var lToken = new JwtSecurityToken(
                issuer:   _config["JWT:Issuer"],
                audience: _config["JWT:Audience"],
                claims:   lClaims,
                expires:  DateTime.Now.AddHours(8),
                signingCredentials: lCredencial
            );

            return new JwtSecurityTokenHandler().WriteToken(lToken);
        }
        [HttpPost("registrarUsuario")]
        public async Task<IActionResult> RegistrarUsuario([FromBody] LoginRequest pLogin)
        {
            var lUsuario = new IdentityUser();
            lUsuario.Email = pLogin.usuario;
            lUsuario.UserName = pLogin.usuario;
            var lResultado = await _userManager.CreateAsync(lUsuario, pLogin.contrasena);
            if (lResultado.Succeeded)
                return Ok(new { mensaje = "Usuario creado correctamente" });
            return BadRequest(lResultado.Errors);
        }
    }

    // Modelo del request de login (igual al Usuario del LabS13)
    public class LoginRequest
    {
        public string usuario   { get; set; } = null!;
        public string contrasena { get; set; } = null!;
    }




}
