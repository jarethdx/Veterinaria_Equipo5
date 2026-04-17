using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VET_WebAPI.Entidades;
using VET_WebAPI.LogicaNegocio.Interfaces;

namespace VET_WebAPI.Controllers
{
    // ─────────────────────────────────────────────
    // MASCOTAS
    // ─────────────────────────────────────────────
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class MascotasController : ControllerBase
    {
        private readonly IlnMascotas _ln;
        public MascotasController(IlnMascotas pLn) => _ln = pLn;

        [HttpGet("obtenerMascotas")]
        public async Task<IActionResult> ObtenerMascotas()
            => Ok(await _ln.ObtenerMascotas());

        [HttpGet("obtenerMascota/{pId}")]
        public async Task<IActionResult> ObtenerMascota(int pId)
        {
            var lObj = await _ln.ObtenerMascotaPorId(pId);
            if (lObj == null) return NotFound();
            return Ok(lObj);
        }

        [HttpPost("insMascota")]
        public async Task<IActionResult> InsertarMascota([FromBody] eMascota pMascota)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var lId = await _ln.InsertarMascota(pMascota);
            pMascota.TN_IdMascota = lId;
            return Ok(pMascota);
        }

        [HttpPut("modMascota")]
        public async Task<IActionResult> ModificarMascota([FromBody] eMascota pMascota)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _ln.ModificarMascota(pMascota);
            return Ok(pMascota);
        }

        [HttpDelete("delMascota/{pId}")]
        public async Task<IActionResult> EliminarMascota(int pId)
        {
            await _ln.EliminarMascota(pId);
            return Ok(new { TN_IdMascota = pId });
        }
    }

    // ─────────────────────────────────────────────
    // DETALLE MASCOTA
    // ─────────────────────────────────────────────
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DetalleMascotaController : ControllerBase
    {
        private readonly IlnDetalleMascota _ln;
        public DetalleMascotaController(IlnDetalleMascota pLn) => _ln = pLn;

        [HttpGet("obtenerDetalle/{pId}")]
        public async Task<IActionResult> ObtenerDetalle(int pId)
        {
            var lObj = await _ln.ObtenerDetalleMascota(pId);
            if (lObj == null) return NotFound();
            return Ok(lObj);
        }

        [HttpPost("insDetalle")]
        public async Task<IActionResult> InsertarDetalle([FromBody] eDetalleMascota pDetalle)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _ln.InsertarDetalle(pDetalle);
            return Ok(pDetalle);
        }

        [HttpPut("modDetalle")]
        public async Task<IActionResult> ModificarDetalle([FromBody] eDetalleMascota pDetalle)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _ln.ModificarDetalle(pDetalle);
            return Ok(pDetalle);
        }
    }

    // ─────────────────────────────────────────────
    // CITAS
    // ─────────────────────────────────────────────
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CitasController : ControllerBase
    {
        private readonly IlnCitas _ln;
        public CitasController(IlnCitas pLn) => _ln = pLn;

        [HttpGet("obtenerCitas")]
        public async Task<IActionResult> ObtenerCitas()
            => Ok(await _ln.ObtenerCitas());

        [HttpGet("obtenerCita/{pId}")]
        public async Task<IActionResult> ObtenerCita(int pId)
        {
            var lObj = await _ln.ObtenerCitaPorId(pId);
            if (lObj == null) return NotFound();
            return Ok(lObj);
        }

        [HttpPost("insCita")]
        public async Task<IActionResult> InsertarCita([FromBody] eCita pCita)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var lId = await _ln.InsertarCita(pCita);
            pCita.TN_IdCita = lId;
            return Ok(pCita);
        }

        [HttpPut("modCita")]
        public async Task<IActionResult> ModificarCita([FromBody] eCita pCita)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _ln.ModificarCita(pCita);
            return Ok(pCita);
        }

        [HttpDelete("delCita/{pId}")]
        public async Task<IActionResult> EliminarCita(int pId)
        {
            await _ln.EliminarCita(pId);
            return Ok(new { TN_IdCita = pId });
        }
    }

    // ─────────────────────────────────────────────
    // DIAGNÓSTICO
    // ─────────────────────────────────────────────
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DiagnosticoController : ControllerBase
    {
        private readonly IlnDiagnostico _ln;
        public DiagnosticoController(IlnDiagnostico pLn) => _ln = pLn;

        [HttpGet("obtenerDiagnosticos")]
        public async Task<IActionResult> ObtenerDiagnosticos()
            => Ok(await _ln.ObtenerDiagnosticos());

        [HttpGet("obtenerDiagnostico/{pId}")]
        public async Task<IActionResult> ObtenerDiagnostico(int pId)
        {
            var lObj = await _ln.ObtenerDiagnosticoPorId(pId);
            if (lObj == null) return NotFound();
            return Ok(lObj);
        }

        [HttpPost("insDiagnostico")]
        public async Task<IActionResult> InsertarDiagnostico([FromBody] eDiagnostico pDiag)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var lId = await _ln.InsertarDiagnostico(pDiag);
            pDiag.TN_IdDiagnostico = lId;
            return Ok(pDiag);
        }

        [HttpPut("modDiagnostico")]
        public async Task<IActionResult> ModificarDiagnostico([FromBody] eDiagnostico pDiag)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _ln.ModificarDiagnostico(pDiag);
            return Ok(pDiag);
        }

        [HttpDelete("delDiagnostico/{pId}")]
        public async Task<IActionResult> EliminarDiagnostico(int pId)
        {
            await _ln.EliminarDiagnostico(pId);
            return Ok(new { TN_IdDiagnostico = pId });
        }
    }
}
