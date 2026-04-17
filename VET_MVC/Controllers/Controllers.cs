using Microsoft.AspNetCore.Mvc;
using VET_MVC.Models;
using VET_MVC.Servicios;

namespace VET_MVC.Controllers
{
    // ── LOGIN ────────────────────────────────────────────────────────────────
    public class LoginController : Controller
    {
        private readonly IsrvVeterinaria _srv;
        public LoginController(IsrvVeterinaria pSrv) => _srv = pSrv;

        public IActionResult login() => View();

        public IActionResult errorGeneral() => View();

        public async Task<IActionResult> accesoUsuario(mUsuario pUsuario)
        {
            string lToken = await _srv.AutenticarUsuario(pUsuario);
            if (!string.IsNullOrEmpty(lToken))
                return RedirectToAction("listadoClientes", "Clientes");
            return View("errorGeneral");
        }
    }

    // ── CLIENTES ─────────────────────────────────────────────────────────────
    public class ClientesController : Controller
    {
        private readonly IsrvVeterinaria _srv;
        public ClientesController(IsrvVeterinaria pSrv) => _srv = pSrv;

        public async Task<IActionResult> listadoClientes()
            => View(await _srv.ObtenerClientes());

        public IActionResult agregaCliente() => View();

        public async Task<IActionResult> modificaCliente(int pId)
            => View(await _srv.ObtenerCliente(pId));

        public async Task<IActionResult> detalleCliente(int pId)
            => View(await _srv.ObtenerCliente(pId));

        public async Task<IActionResult> eliminaCliente(int pId)
            => View(await _srv.ObtenerCliente(pId));

        // ── ACCIONES ──────────────────────────────────────────────────────────
        public async Task<IActionResult> insCliente(mCliente pCliente)
        {
            await _srv.InsertarCliente(pCliente);
            return View("listadoClientes", await _srv.ObtenerClientes());
        }

        public async Task<IActionResult> modCliente(mCliente pCliente)
        {
            await _srv.ModificarCliente(pCliente);
            return View("listadoClientes", await _srv.ObtenerClientes());
        }

        public async Task<IActionResult> delCliente(mCliente pCliente)
        {
            await _srv.EliminarCliente(pCliente.TN_IdCliente);
            return View("listadoClientes", await _srv.ObtenerClientes());
        }
    }

    // ── MASCOTAS ─────────────────────────────────────────────────────────────
    public class MascotasController : Controller
    {
        private readonly IsrvVeterinaria _srv;
        public MascotasController(IsrvVeterinaria pSrv) => _srv = pSrv;

        public async Task<IActionResult> listadoMascotas()
            => View(await _srv.ObtenerMascotas());

        public async Task<IActionResult> agregaMascota()
        {
            ViewBag.Clientes = await _srv.ObtenerClientes();
            return View();
        }

        public async Task<IActionResult> modificaMascota(int pId)
        {
            ViewBag.Clientes = await _srv.ObtenerClientes();
            return View(await _srv.ObtenerMascota(pId));
        }

        public async Task<IActionResult> detalleMascota(int pId)
            => View(await _srv.ObtenerMascota(pId));

        public async Task<IActionResult> eliminaMascota(int pId)
            => View(await _srv.ObtenerMascota(pId));

        public async Task<IActionResult> insMascota(mMascota pMascota)
        {
            await _srv.InsertarMascota(pMascota);
            return View("listadoMascotas", await _srv.ObtenerMascotas());
        }

        public async Task<IActionResult> modMascota(mMascota pMascota)
        {
            await _srv.ModificarMascota(pMascota);
            return View("listadoMascotas", await _srv.ObtenerMascotas());
        }

        public async Task<IActionResult> delMascota(mMascota pMascota)
        {
            await _srv.EliminarMascota(pMascota.TN_IdMascota);
            return View("listadoMascotas", await _srv.ObtenerMascotas());
        }
    }

    // ── DETALLE MASCOTA ──────────────────────────────────────────────────────
    public class DetalleMascotaController : Controller
    {
        private readonly IsrvVeterinaria _srv;
        public DetalleMascotaController(IsrvVeterinaria pSrv) => _srv = pSrv;

        public async Task<IActionResult> detallesMascota(int pId)
            => View(await _srv.ObtenerDetalleMascota(pId) ?? new mDetalleMascota { TN_IdMascota = pId });

        public async Task<IActionResult> agregaDetalle(int pId)
        {
            ViewBag.IdMascota = pId;
            return View(new mDetalleMascota { TN_IdMascota = pId });
        }

        public async Task<IActionResult> insDetalle(mDetalleMascota pDetalle)
        {
            await _srv.InsertarDetalle(pDetalle);
            return RedirectToAction("listadoMascotas", "Mascotas");
        }

        public async Task<IActionResult> modDetalle(mDetalleMascota pDetalle)
        {
            await _srv.ModificarDetalle(pDetalle);
            return RedirectToAction("listadoMascotas", "Mascotas");
        }
    }

    // ── CITAS ────────────────────────────────────────────────────────────────
    public class CitasController : Controller
    {
        private readonly IsrvVeterinaria _srv;
        public CitasController(IsrvVeterinaria pSrv) => _srv = pSrv;

        public async Task<IActionResult> listadoCitas()
            => View(await _srv.ObtenerCitas());

        public async Task<IActionResult> agregaCita()
        {
            ViewBag.Clientes = await _srv.ObtenerClientes();
            ViewBag.Mascotas = await _srv.ObtenerMascotas();
            return View();
        }

        public async Task<IActionResult> modificaCita(int pId)
        {
            ViewBag.Clientes = await _srv.ObtenerClientes();
            ViewBag.Mascotas = await _srv.ObtenerMascotas();
            return View(await _srv.ObtenerCita(pId));
        }

        public async Task<IActionResult> detalleCita(int pId)
            => View(await _srv.ObtenerCita(pId));

        public async Task<IActionResult> eliminaCita(int pId)
            => View(await _srv.ObtenerCita(pId));

        public async Task<IActionResult> insCita(mCita pCita)
        {
            await _srv.InsertarCita(pCita);
            return View("listadoCitas", await _srv.ObtenerCitas());
        }

        public async Task<IActionResult> modCita(mCita pCita)
        {
            await _srv.ModificarCita(pCita);
            return View("listadoCitas", await _srv.ObtenerCitas());
        }

        public async Task<IActionResult> delCita(mCita pCita)
        {
            await _srv.EliminarCita(pCita.TN_IdCita);
            return View("listadoCitas", await _srv.ObtenerCitas());
        }
    }

    // ── DIAGNÓSTICO ──────────────────────────────────────────────────────────
    public class DiagnosticoController : Controller
    {
        private readonly IsrvVeterinaria _srv;
        public DiagnosticoController(IsrvVeterinaria pSrv) => _srv = pSrv;

        public async Task<IActionResult> listadoDiagnosticos()
            => View(await _srv.ObtenerDiagnosticos());

        public async Task<IActionResult> agregaDiagnostico()
        {
            ViewBag.Citas = await _srv.ObtenerCitas();
            return View();
        }

        public async Task<IActionResult> modificaDiagnostico(int pId)
        {
            ViewBag.Citas = await _srv.ObtenerCitas();
            return View(await _srv.ObtenerDiagnostico(pId));
        }

        public async Task<IActionResult> detalleDiagnostico(int pId)
            => View(await _srv.ObtenerDiagnostico(pId));

        public async Task<IActionResult> eliminaDiagnostico(int pId)
            => View(await _srv.ObtenerDiagnostico(pId));

        public async Task<IActionResult> insDiagnostico(mDiagnostico pDiag)
        {
            await _srv.InsertarDiagnostico(pDiag);
            return View("listadoDiagnosticos", await _srv.ObtenerDiagnosticos());
        }

        public async Task<IActionResult> modDiagnostico(mDiagnostico pDiag)
        {
            await _srv.ModificarDiagnostico(pDiag);
            return View("listadoDiagnosticos", await _srv.ObtenerDiagnosticos());
        }

        public async Task<IActionResult> delDiagnostico(mDiagnostico pDiag)
        {
            await _srv.EliminarDiagnostico(pDiag.TN_IdDiagnostico);
            return View("listadoDiagnosticos", await _srv.ObtenerDiagnosticos());
        }
    }
}
