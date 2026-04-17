using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using VET_MVC.Models;

namespace VET_MVC.Servicios
{
    // ── INTERFAZ PRINCIPAL ───────────────────────────────────────────────────
    // Define todos los métodos que el MVC necesita de la API
    public interface IsrvVeterinaria
    {
        // Login
        Task<string> AutenticarUsuario(mUsuario pUsuario);

        // Clientes
        Task<List<mCliente>>  ObtenerClientes();
        Task<mCliente?>       ObtenerCliente(int pId);
        Task<bool>            InsertarCliente(mCliente pCliente);
        Task<bool>            ModificarCliente(mCliente pCliente);
        Task<bool>            EliminarCliente(int pId);

        // Mascotas
        Task<List<mMascota>>  ObtenerMascotas();
        Task<mMascota?>       ObtenerMascota(int pId);
        Task<bool>            InsertarMascota(mMascota pMascota);
        Task<bool>            ModificarMascota(mMascota pMascota);
        Task<bool>            EliminarMascota(int pId);

        // Detalle Mascota
        Task<mDetalleMascota?> ObtenerDetalleMascota(int pIdMascota);
        Task<bool>             InsertarDetalle(mDetalleMascota pDetalle);
        Task<bool>             ModificarDetalle(mDetalleMascota pDetalle);

        // Citas
        Task<List<mCita>>  ObtenerCitas();
        Task<mCita?>       ObtenerCita(int pId);
        Task<bool>         InsertarCita(mCita pCita);
        Task<bool>         ModificarCita(mCita pCita);
        Task<bool>         EliminarCita(int pId);

        // Diagnóstico
        Task<List<mDiagnostico>>  ObtenerDiagnosticos();
        Task<mDiagnostico?>       ObtenerDiagnostico(int pId);
        Task<bool>                InsertarDiagnostico(mDiagnostico pDiag);
        Task<bool>                ModificarDiagnostico(mDiagnostico pDiag);
        Task<bool>                EliminarDiagnostico(int pId);
    }

    // ── IMPLEMENTACIÓN ───────────────────────────────────────────────────────
    public class srvVeterinaria : IsrvVeterinaria
    {
        private static string gToken = "";   // Token JWT guardado en memoria
        private readonly string gBaseURL;
        private HttpClient CrearCliente()
        {
            var lCliente = new HttpClient();
            lCliente.BaseAddress = new Uri(gBaseURL);
            // Inyecta el token en cada petición
            if (!string.IsNullOrEmpty(gToken))
                lCliente.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", gToken);
            return lCliente;
        }

        public srvVeterinaria()
        {
            var appConfig = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();
            gBaseURL = appConfig.GetSection("rutaAPI:baseURL").Value!;
        }

        // ── LOGIN ─────────────────────────────────────────────────────────────
        public async Task<string> AutenticarUsuario(mUsuario pUsuario)
        {
            try
            {
                using var lCliente = new HttpClient();
                lCliente.BaseAddress = new Uri(gBaseURL);
                var lContenido = new StringContent(
                    JsonConvert.SerializeObject(pUsuario), Encoding.UTF8, "application/json");

                var lResp = await lCliente.PostAsync("api/Autenticacion/validarUsuario", lContenido);
                if (lResp.IsSuccessStatusCode)
                {
                    var lJson = await lResp.Content.ReadAsStringAsync();
                    var lResultado = JsonConvert.DeserializeObject<mResultadoToken>(lJson);
                    gToken = lResultado?.token ?? "";
                }
                else { gToken = ""; }
            }
            catch { gToken = ""; }
            return gToken;
        }

        // ── HELPER: GET ───────────────────────────────────────────────────────
        private async Task<T?> Get<T>(string pRuta)
        {
            using var lCliente = CrearCliente();
            var lResp = await lCliente.GetAsync(pRuta);
            if (!lResp.IsSuccessStatusCode) return default;
            var lJson = await lResp.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(lJson);
        }

        // ── HELPER: POST ──────────────────────────────────────────────────────
        private async Task<bool> Post<T>(string pRuta, T pObj)
        {
            using var lCliente = CrearCliente();
            var lContenido = new StringContent(
                JsonConvert.SerializeObject(pObj), Encoding.UTF8, "application/json");
            var lResp = await lCliente.PostAsync(pRuta, lContenido);
            return lResp.IsSuccessStatusCode;
        }

        // ── HELPER: PUT ───────────────────────────────────────────────────────
        private async Task<bool> Put<T>(string pRuta, T pObj)
        {
            using var lCliente = CrearCliente();
            var lContenido = new StringContent(
                JsonConvert.SerializeObject(pObj), Encoding.UTF8, "application/json");
            var lResp = await lCliente.PutAsync(pRuta, lContenido);
            return lResp.IsSuccessStatusCode;
        }

        // ── HELPER: DELETE ────────────────────────────────────────────────────
        private async Task<bool> Delete(string pRuta)
        {
            using var lCliente = CrearCliente();
            var lResp = await lCliente.DeleteAsync(pRuta);
            return lResp.IsSuccessStatusCode;
        }

        // ── CLIENTES ──────────────────────────────────────────────────────────
        public Task<List<mCliente>>  ObtenerClientes()          => Get<List<mCliente>>("api/Clientes/obtenerClientes")!;
        public Task<mCliente?>       ObtenerCliente(int pId)    => Get<mCliente>($"api/Clientes/obtenerCliente/{pId}");
        public Task<bool>            InsertarCliente(mCliente p) => Post("api/Clientes/insCliente", p);
        public Task<bool>            ModificarCliente(mCliente p)=> Put("api/Clientes/modCliente", p);
        public Task<bool>            EliminarCliente(int pId)   => Delete($"api/Clientes/delCliente/{pId}");

        // ── MASCOTAS ──────────────────────────────────────────────────────────
        public Task<List<mMascota>>  ObtenerMascotas()          => Get<List<mMascota>>("api/Mascotas/obtenerMascotas")!;
        public Task<mMascota?>       ObtenerMascota(int pId)    => Get<mMascota>($"api/Mascotas/obtenerMascota/{pId}");
        public Task<bool>            InsertarMascota(mMascota p) => Post("api/Mascotas/insMascota", p);
        public Task<bool>            ModificarMascota(mMascota p)=> Put("api/Mascotas/modMascota", p);
        public Task<bool>            EliminarMascota(int pId)   => Delete($"api/Mascotas/delMascota/{pId}");

        // ── DETALLE MASCOTA ───────────────────────────────────────────────────
        public Task<mDetalleMascota?> ObtenerDetalleMascota(int pId) => Get<mDetalleMascota>($"api/DetalleMascota/obtenerDetalle/{pId}");
        public Task<bool>             InsertarDetalle(mDetalleMascota p) => Post("api/DetalleMascota/insDetalle", p);
        public Task<bool>             ModificarDetalle(mDetalleMascota p)=> Put("api/DetalleMascota/modDetalle", p);

        // ── CITAS ─────────────────────────────────────────────────────────────
        public Task<List<mCita>>  ObtenerCitas()       => Get<List<mCita>>("api/Citas/obtenerCitas")!;
        public Task<mCita?>       ObtenerCita(int pId) => Get<mCita>($"api/Citas/obtenerCita/{pId}");
        public Task<bool>         InsertarCita(mCita p) => Post("api/Citas/insCita", p);
        public Task<bool>         ModificarCita(mCita p)=> Put("api/Citas/modCita", p);
        public Task<bool>         EliminarCita(int pId) => Delete($"api/Citas/delCita/{pId}");

        // ── DIAGNÓSTICO ───────────────────────────────────────────────────────
        public Task<List<mDiagnostico>>  ObtenerDiagnosticos()          => Get<List<mDiagnostico>>("api/Diagnostico/obtenerDiagnosticos")!;
        public Task<mDiagnostico?>       ObtenerDiagnostico(int pId)    => Get<mDiagnostico>($"api/Diagnostico/obtenerDiagnostico/{pId}");
        public Task<bool>                InsertarDiagnostico(mDiagnostico p) => Post("api/Diagnostico/insDiagnostico", p);
        public Task<bool>                ModificarDiagnostico(mDiagnostico p)=> Put("api/Diagnostico/modDiagnostico", p);
        public Task<bool>                EliminarDiagnostico(int pId)   => Delete($"api/Diagnostico/delDiagnostico/{pId}");
    }
}
