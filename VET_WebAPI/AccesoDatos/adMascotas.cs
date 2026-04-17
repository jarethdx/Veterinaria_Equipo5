using Microsoft.Data.SqlClient;
using VET_WebAPI.Entidades;

namespace VET_WebAPI.AccesoDatos
{
    public class adMascotas
    {
        private readonly string _cnx;

        public adMascotas(string pCnx) => _cnx = pCnx;

        public async Task<List<eMascota>> ObtenerMascotas()
        {
            var lLista = new List<eMascota>();
            using var con = new SqlConnection(_cnx);
            using var cmd = new SqlCommand("SP_ObtenerMascotas", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            await con.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
                lLista.Add(MapearMascota(reader));

            return lLista;
        }

        public async Task<eMascota?> ObtenerMascotaPorId(int pId)
        {
            using var con = new SqlConnection(_cnx);
            using var cmd = new SqlCommand("SP_ObtenerMascotaPorId", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pIdMascota", pId);

            await con.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
                return MapearMascota(reader);

            return null;
        }

        public async Task<int> InsertarMascota(eMascota pMascota)
        {
            using var con = new SqlConnection(_cnx);
            using var cmd = new SqlCommand("SP_InsertarMascota", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pNomMascota", pMascota.TC_NomMascota);
            cmd.Parameters.AddWithValue("@pIdCliente",  pMascota.TN_IdCliente);

            await con.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
                return Convert.ToInt32(reader["TN_IdMascota"]);

            return 0;
        }

        public async Task<int> ModificarMascota(eMascota pMascota)
        {
            using var con = new SqlConnection(_cnx);
            using var cmd = new SqlCommand("SP_ModificarMascota", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pIdMascota",  pMascota.TN_IdMascota);
            cmd.Parameters.AddWithValue("@pNomMascota", pMascota.TC_NomMascota);
            cmd.Parameters.AddWithValue("@pIdCliente",  pMascota.TN_IdCliente);

            await con.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
                return Convert.ToInt32(reader["TN_IdMascota"]);

            return 0;
        }

        public async Task<int> EliminarMascota(int pId)
        {
            using var con = new SqlConnection(_cnx);
            using var cmd = new SqlCommand("SP_EliminarMascota", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pIdMascota", pId);

            await con.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
                return Convert.ToInt32(reader["TN_IdMascota"]);

            return 0;
        }

        private eMascota MapearMascota(SqlDataReader r) => new eMascota
        {
            TN_IdMascota     = Convert.ToInt32(r["TN_IdMascota"]),
            TC_NomMascota    = r["TC_NomMascota"].ToString()!,
            TN_IdCliente     = Convert.ToInt32(r["TN_IdCliente"]),
            TC_NombreCliente = r["TC_NombreCliente"].ToString()
        };
    }
}
