using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VET_WebAPI.Entidades;
using VET_WebAPI.LogicaNegocio.Interfaces;

namespace VET_WebAPI.Controllers
{
    [Authorize]                          // Requiere token JWT válido
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly IlnClientes _ln;

        public ClientesController(IlnClientes pLn) => _ln = pLn;

        // GET api/Clientes/obtenerClientes
        [HttpGet("obtenerClientes")]
        public async Task<IActionResult> ObtenerClientes()
        {
            var lLista = await _ln.ObtenerClientes();
            return Ok(lLista);
        }

        // GET api/Clientes/obtenerCliente/5
        [HttpGet("obtenerCliente/{pId}")]
        public async Task<IActionResult> ObtenerCliente(int pId)
        {
            var lCliente = await _ln.ObtenerClientePorId(pId);
            if (lCliente == null) return NotFound();
            return Ok(lCliente);
        }

        // POST api/Clientes/insCliente
        [HttpPost("insCliente")]
        public async Task<IActionResult> InsertarCliente([FromBody] eCliente pCliente)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var lId = await _ln.InsertarCliente(pCliente);
            pCliente.TN_IdCliente = lId;
            return Ok(pCliente);
        }

        // PUT api/Clientes/modCliente
        [HttpPut("modCliente")]
        public async Task<IActionResult> ModificarCliente([FromBody] eCliente pCliente)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _ln.ModificarCliente(pCliente);
            return Ok(pCliente);
        }

        // DELETE api/Clientes/delCliente/5
        [HttpDelete("delCliente/{pId}")]
        public async Task<IActionResult> EliminarCliente(int pId)
        {
            await _ln.EliminarCliente(pId);
            return Ok(new { TN_IdCliente = pId });
        }
    }
}
