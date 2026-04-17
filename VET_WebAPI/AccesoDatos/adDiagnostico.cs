using Microsoft.Data.SqlClient;
using VET_WebAPI.Entidades;

namespace VET_WebAPI.AccesoDatos
{
    public class adDiagnostico
    {
        private readonly string _cnx;

        public adDiagnostico(string pCnx) => _cnx = pCnx;

        public async Task<List<eDiagnostico>> ObtenerDiagnosticos()
        {
            var lLista = new List<eDiagnostico>();
            using var con = new SqlConnection(_cnx);
            using var cmd = new SqlCommand("SP_ObtenerDiagnosticos", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            await con.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
                lLista.Add(MapearDiagnostico(reader));

            return lLista;
        }

        public async Task<eDiagnostico?> ObtenerDiagnosticoPorId(int pId)
        {
            using var con = new SqlConnection(_cnx);
            using var cmd = new SqlCommand("SP_ObtenerDiagnosticoPorId", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pIdDiagnostico", pId);

            await con.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
                return MapearDiagnostico(reader);

            return null;
        }

        public async Task<int> InsertarDiagnostico(eDiagnostico pDiag)
        {
            using var con = new SqlConnection(_cnx);
            using var cmd = new SqlCommand("SP_InsertarDiagnostico", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pIdCita",           pDiag.TN_IdCita);
            cmd.Parameters.AddWithValue("@pDscDiagnostico",   pDiag.TC_DscDiagnostico);

            await con.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
                return Convert.ToInt32(reader["TN_IdDiagnostico"]);

            return 0;
        }

        public async Task<int> ModificarDiagnostico(eDiagnostico pDiag)
        {
            using var con = new SqlConnection(_cnx);
            using var cmd = new SqlCommand("SP_ModificarDiagnostico", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pIdDiagnostico",  pDiag.TN_IdDiagnostico);
            cmd.Parameters.AddWithValue("@pIdCita",         pDiag.TN_IdCita);
            cmd.Parameters.AddWithValue("@pDscDiagnostico", pDiag.TC_DscDiagnostico);

            await con.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
                return Convert.ToInt32(reader["TN_IdDiagnostico"]);

            return 0;
        }

        public async Task<int> EliminarDiagnostico(int pId)
        {
            using var con = new SqlConnection(_cnx);
            using var cmd = new SqlCommand("SP_EliminarDiagnostico", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pIdDiagnostico", pId);

            await con.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
                return Convert.ToInt32(reader["TN_IdDiagnostico"]);

            return 0;
        }

        private eDiagnostico MapearDiagnostico(SqlDataReader r) => new eDiagnostico
        {
            TN_IdDiagnostico  = Convert.ToInt32(r["TN_IdDiagnostico"]),
            TN_IdCita         = Convert.ToInt32(r["TN_IdCita"]),
            TC_DscDiagnostico = r["TC_DscDiagnostico"].ToString()!,
            TF_FecCita        = Convert.ToDateTime(r["TF_FecCita"]),
            TC_NomMascota     = r["TC_NomMascota"].ToString()
        };
    }
}
