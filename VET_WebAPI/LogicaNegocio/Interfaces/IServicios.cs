using VET_WebAPI.Entidades;

namespace VET_WebAPI.LogicaNegocio.Interfaces
{
    // Contrato para el servicio de Clientes
    public interface IlnClientes
    {
        Task<List<eCliente>> ObtenerClientes();
        Task<eCliente?> ObtenerClientePorId(int pId);
        Task<int> InsertarCliente(eCliente pCliente);
        Task<int> ModificarCliente(eCliente pCliente);
        Task<int> EliminarCliente(int pId);
    }

    // Contrato para el servicio de Mascotas
    public interface IlnMascotas
    {
        Task<List<eMascota>> ObtenerMascotas();
        Task<eMascota?> ObtenerMascotaPorId(int pId);
        Task<int> InsertarMascota(eMascota pMascota);
        Task<int> ModificarMascota(eMascota pMascota);
        Task<int> EliminarMascota(int pId);
    }

    // Contrato para el servicio de Detalle Mascota
    public interface IlnDetalleMascota
    {
        Task<eDetalleMascota?> ObtenerDetalleMascota(int pIdMascota);
        Task<int> InsertarDetalle(eDetalleMascota pDetalle);
        Task<int> ModificarDetalle(eDetalleMascota pDetalle);
    }

    // Contrato para el servicio de Citas
    public interface IlnCitas
    {
        Task<List<eCita>> ObtenerCitas();
        Task<eCita?> ObtenerCitaPorId(int pId);
        Task<int> InsertarCita(eCita pCita);
        Task<int> ModificarCita(eCita pCita);
        Task<int> EliminarCita(int pId);
    }

    // Contrato para el servicio de Diagnóstico
    public interface IlnDiagnostico
    {
        Task<List<eDiagnostico>> ObtenerDiagnosticos();
        Task<eDiagnostico?> ObtenerDiagnosticoPorId(int pId);
        Task<int> InsertarDiagnostico(eDiagnostico pDiag);
        Task<int> ModificarDiagnostico(eDiagnostico pDiag);
        Task<int> EliminarDiagnostico(int pId);
    }
}
