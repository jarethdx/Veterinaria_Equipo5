using VET_WebAPI.AccesoDatos;
using VET_WebAPI.Entidades;
using VET_WebAPI.LogicaNegocio.Interfaces;

namespace VET_WebAPI.LogicaNegocio.Servicios
{
    // ─────────────────────────────────────────────
    // CLIENTES
    // ─────────────────────────────────────────────
    public class lnClientes : IlnClientes
    {
        private readonly adClientes _ad;

        // Recibe la cadena de conexión desde Program.cs vía DI
        public lnClientes(IConfiguration pConfig)
        {
            _ad = new adClientes(pConfig.GetConnectionString("VetCnx")!);
        }

        public Task<List<eCliente>>  ObtenerClientes()               => _ad.ObtenerClientes();
        public Task<eCliente?>       ObtenerClientePorId(int pId)    => _ad.ObtenerClientePorId(pId);
        public Task<int>             InsertarCliente(eCliente p)     => _ad.InsertarCliente(p);
        public Task<int>             ModificarCliente(eCliente p)    => _ad.ModificarCliente(p);
        public Task<int>             EliminarCliente(int pId)        => _ad.EliminarCliente(pId);
    }

    // ─────────────────────────────────────────────
    // MASCOTAS
    // ─────────────────────────────────────────────
    public class lnMascotas : IlnMascotas
    {
        private readonly adMascotas _ad;

        public lnMascotas(IConfiguration pConfig)
        {
            _ad = new adMascotas(pConfig.GetConnectionString("VetCnx")!);
        }

        public Task<List<eMascota>>  ObtenerMascotas()               => _ad.ObtenerMascotas();
        public Task<eMascota?>       ObtenerMascotaPorId(int pId)    => _ad.ObtenerMascotaPorId(pId);
        public Task<int>             InsertarMascota(eMascota p)     => _ad.InsertarMascota(p);
        public Task<int>             ModificarMascota(eMascota p)    => _ad.ModificarMascota(p);
        public Task<int>             EliminarMascota(int pId)        => _ad.EliminarMascota(pId);
    }

    // ─────────────────────────────────────────────
    // DETALLE MASCOTA
    // ─────────────────────────────────────────────
    public class lnDetalleMascota : IlnDetalleMascota
    {
        private readonly adDetalleMascota _ad;

        public lnDetalleMascota(IConfiguration pConfig)
        {
            _ad = new adDetalleMascota(pConfig.GetConnectionString("VetCnx")!);
        }

        public Task<eDetalleMascota?> ObtenerDetalleMascota(int pId) => _ad.ObtenerDetalleMascota(pId);
        public Task<int>              InsertarDetalle(eDetalleMascota p) => _ad.InsertarDetalle(p);
        public Task<int>              ModificarDetalle(eDetalleMascota p) => _ad.ModificarDetalle(p);
    }

    // ─────────────────────────────────────────────
    // CITAS
    // ─────────────────────────────────────────────
    public class lnCitas : IlnCitas
    {
        private readonly adCitas _ad;

        public lnCitas(IConfiguration pConfig)
        {
            _ad = new adCitas(pConfig.GetConnectionString("VetCnx")!);
        }

        public Task<List<eCita>>  ObtenerCitas()              => _ad.ObtenerCitas();
        public Task<eCita?>       ObtenerCitaPorId(int pId)   => _ad.ObtenerCitaPorId(pId);
        public Task<int>          InsertarCita(eCita p)       => _ad.InsertarCita(p);
        public Task<int>          ModificarCita(eCita p)      => _ad.ModificarCita(p);
        public Task<int>          EliminarCita(int pId)       => _ad.EliminarCita(pId);
    }

    // ─────────────────────────────────────────────
    // DIAGNÓSTICO
    // ─────────────────────────────────────────────
    public class lnDiagnostico : IlnDiagnostico
    {
        private readonly adDiagnostico _ad;

        public lnDiagnostico(IConfiguration pConfig)
        {
            _ad = new adDiagnostico(pConfig.GetConnectionString("VetCnx")!);
        }

        public Task<List<eDiagnostico>>  ObtenerDiagnosticos()              => _ad.ObtenerDiagnosticos();
        public Task<eDiagnostico?>       ObtenerDiagnosticoPorId(int pId)   => _ad.ObtenerDiagnosticoPorId(pId);
        public Task<int>                 InsertarDiagnostico(eDiagnostico p) => _ad.InsertarDiagnostico(p);
        public Task<int>                 ModificarDiagnostico(eDiagnostico p)=> _ad.ModificarDiagnostico(p);
        public Task<int>                 EliminarDiagnostico(int pId)        => _ad.EliminarDiagnostico(pId);
    }
}
