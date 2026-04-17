using Microsoft.Data.SqlClient;
using VET_WebAPI.Entidades;

namespace VET_WebAPI.AccesoDatos
{
    // Esta clase se conecta directamente a SQL Server y llama los stored procedures
    public class adClientes
    {
        private readonly string _cnx;

        public adClientes(string pCnx)
        {
            _cnx = pCnx;
        }

        // Llama SP_ObtenerClientes → retorna lista completa
        public async Task<List<eCliente>> ObtenerClientes()
        {
            var lLista = new List<eCliente>();

            using var con = new SqlConnection(_cnx);
            using var cmd = new SqlCommand("SP_ObtenerClientes", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            await con.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                lLista.Add(MapearCliente(reader));
            }
            return lLista;
        }

        // Llama SP_ObtenerClientePorId → retorna un cliente
        public async Task<eCliente?> ObtenerClientePorId(int pId)
        {
            using var con = new SqlConnection(_cnx);
            using var cmd = new SqlCommand("SP_ObtenerClientePorId", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pIdCliente", pId);

            await con.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
                return MapearCliente(reader);

            return null;
        }

        // Llama SP_InsertarCliente → retorna el ID generado
        public async Task<int> InsertarCliente(eCliente pCliente)
        {
            using var con = new SqlConnection(_cnx);
            using var cmd = new SqlCommand("SP_InsertarCliente", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pNombre",   pCliente.TC_Nombre);
            cmd.Parameters.AddWithValue("@pAp1",      pCliente.TC_Ap1);
            cmd.Parameters.AddWithValue("@pAp2",      pCliente.TC_Ap2);
            cmd.Parameters.AddWithValue("@pTelefono", pCliente.TC_NumTelefono);
            cmd.Parameters.AddWithValue("@pCorreo",   pCliente.TC_CorreoElectronico);

            await con.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
                return Convert.ToInt32(reader["TN_IdCliente"]);

            return 0;
        }

        // Llama SP_ModificarCliente
        public async Task<int> ModificarCliente(eCliente pCliente)
        {
            using var con = new SqlConnection(_cnx);
            using var cmd = new SqlCommand("SP_ModificarCliente", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pIdCliente", pCliente.TN_IdCliente);
            cmd.Parameters.AddWithValue("@pNombre",    pCliente.TC_Nombre);
            cmd.Parameters.AddWithValue("@pAp1",       pCliente.TC_Ap1);
            cmd.Parameters.AddWithValue("@pAp2",       pCliente.TC_Ap2);
            cmd.Parameters.AddWithValue("@pTelefono",  pCliente.TC_NumTelefono);
            cmd.Parameters.AddWithValue("@pCorreo",    pCliente.TC_CorreoElectronico);

            await con.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
                return Convert.ToInt32(reader["TN_IdCliente"]);

            return 0;
        }

        // Llama SP_EliminarCliente
        public async Task<int> EliminarCliente(int pId)
        {
            using var con = new SqlConnection(_cnx);
            using var cmd = new SqlCommand("SP_EliminarCliente", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pIdCliente", pId);

            await con.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
                return Convert.ToInt32(reader["TN_IdCliente"]);

            return 0;
        }

        // Mapea una fila del reader a un objeto eCliente
        private eCliente MapearCliente(SqlDataReader r) => new eCliente
        {
            TN_IdCliente         = Convert.ToInt32(r["TN_IdCliente"]),
            TC_Nombre            = r["TC_Nombre"].ToString()!,
            TC_Ap1               = r["TC_Ap1"].ToString()!,
            TC_Ap2               = r["TC_Ap2"].ToString()!,
            TC_NumTelefono       = r["TC_NumTelefono"].ToString()!,
            TC_CorreoElectronico = r["TC_CorreoElectronico"].ToString()!
        };
    }
}
