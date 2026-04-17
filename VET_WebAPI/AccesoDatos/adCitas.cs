using Microsoft.Data.SqlClient;
using VET_WebAPI.Entidades;

namespace VET_WebAPI.AccesoDatos
{
    public class adCitas
    {
        private readonly string _cnx;

        public adCitas(string pCnx) => _cnx = pCnx;

        public async Task<List<eCita>> ObtenerCitas()
        {
            var lLista = new List<eCita>();
            using var con = new SqlConnection(_cnx);
            using var cmd = new SqlCommand("SP_ObtenerCitas", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            await con.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
                lLista.Add(MapearCita(reader));

            return lLista;
        }

        public async Task<eCita?> ObtenerCitaPorId(int pId)
        {
            using var con = new SqlConnection(_cnx);
            using var cmd = new SqlCommand("SP_ObtenerCitaPorId", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pIdCita", pId);

            await con.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
                return MapearCita(reader);

            return null;
        }

        public async Task<int> InsertarCita(eCita pCita)
        {
            using var con = new SqlConnection(_cnx);
            using var cmd = new SqlCommand("SP_InsertarCita", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pIdCliente", pCita.TN_IdCliente);
            cmd.Parameters.AddWithValue("@pIdMascota", pCita.TN_IdMascota);
            cmd.Parameters.AddWithValue("@pFecCita",   pCita.TF_FecCita);

            await con.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
                return Convert.ToInt32(reader["TN_IdCita"]);

            return 0;
        }

        public async Task<int> ModificarCita(eCita pCita)
        {
            using var con = new SqlConnection(_cnx);
            using var cmd = new SqlCommand("SP_ModificarCita", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pIdCita",    pCita.TN_IdCita);
            cmd.Parameters.AddWithValue("@pIdCliente", pCita.TN_IdCliente);
            cmd.Parameters.AddWithValue("@pIdMascota", pCita.TN_IdMascota);
            cmd.Parameters.AddWithValue("@pFecCita",   pCita.TF_FecCita);

            await con.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
                return Convert.ToInt32(reader["TN_IdCita"]);

            return 0;
        }

        public async Task<int> EliminarCita(int pId)
        {
            using var con = new SqlConnection(_cnx);
            using var cmd = new SqlCommand("SP_EliminarCita", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pIdCita", pId);

            await con.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
                return Convert.ToInt32(reader["TN_IdCita"]);

            return 0;
        }

        private eCita MapearCita(SqlDataReader r) => new eCita
        {
            TN_IdCita        = Convert.ToInt32(r["TN_IdCita"]),
            TN_IdCliente     = Convert.ToInt32(r["TN_IdCliente"]),
            TN_IdMascota     = Convert.ToInt32(r["TN_IdMascota"]),
            TF_FecCita       = Convert.ToDateTime(r["TF_FecCita"]),
            TC_NombreCliente = r["TC_NombreCliente"].ToString(),
            TC_NomMascota    = r["TC_NomMascota"].ToString()
        };
    }
}
