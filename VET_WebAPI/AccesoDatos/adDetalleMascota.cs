using Microsoft.Data.SqlClient;
using VET_WebAPI.Entidades;

namespace VET_WebAPI.AccesoDatos
{
    public class adDetalleMascota
    {
        private readonly string _cnx;

        public adDetalleMascota(string pCnx) => _cnx = pCnx;

        public async Task<eDetalleMascota?> ObtenerDetalleMascota(int pIdMascota)
        {
            using var con = new SqlConnection(_cnx);
            using var cmd = new SqlCommand("SP_ObtenerDetalleMascota", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pIdMascota", pIdMascota);

            await con.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
                return MapearDetalle(reader);

            return null;
        }

        public async Task<int> InsertarDetalle(eDetalleMascota pDetalle)
        {
            using var con = new SqlConnection(_cnx);
            using var cmd = new SqlCommand("SP_InsertarDetalleMascota", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pIdMascota", pDetalle.TN_IdMascota);
            cmd.Parameters.AddWithValue("@pRaza",      pDetalle.TC_Raza);
            cmd.Parameters.AddWithValue("@pPeso",      pDetalle.TN_Peso);
            cmd.Parameters.AddWithValue("@pColor",     pDetalle.TC_Color);

            await con.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
                return Convert.ToInt32(reader["TN_IdMascota"]);

            return 0;
        }

        public async Task<int> ModificarDetalle(eDetalleMascota pDetalle)
        {
            using var con = new SqlConnection(_cnx);
            using var cmd = new SqlCommand("SP_ModificarDetalleMascota", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pIdMascota", pDetalle.TN_IdMascota);
            cmd.Parameters.AddWithValue("@pRaza",      pDetalle.TC_Raza);
            cmd.Parameters.AddWithValue("@pPeso",      pDetalle.TN_Peso);
            cmd.Parameters.AddWithValue("@pColor",     pDetalle.TC_Color);

            await con.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
                return Convert.ToInt32(reader["TN_IdMascota"]);

            return 0;
        }

        private eDetalleMascota MapearDetalle(SqlDataReader r) => new eDetalleMascota
        {
            TN_IdMascota  = Convert.ToInt32(r["TN_IdMascota"]),
            TC_Raza       = r["TC_Raza"].ToString()!,
            TN_Peso       = Convert.ToDecimal(r["TN_Peso"]),
            TC_Color      = r["TC_Color"].ToString()!,
            TC_NomMascota = r["TC_NomMascota"].ToString()
        };
    }
}
