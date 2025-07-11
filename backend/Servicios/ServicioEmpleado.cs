using backend.Models;
using Microsoft.Data.SqlClient;

namespace backend.Servicios
{
	public class ServicioEmpleado
	{
		private readonly string _connectionString;

		public ServicioEmpleado(IConfiguration configuration)
		{
			_connectionString = configuration.GetConnectionString("EmpleadosConnection");
		}

		/// <summary>
		/// servivicios para obtener todos los datos 
		/// </summary>
		/// <returns></returns>
		public async Task<List<Empleado>> GetAllAsync()
		{
			var lista = new List<Empleado>();

			using (SqlConnection conn = new SqlConnection(_connectionString))
			using (SqlCommand cmd = new SqlCommand("dbo.sp_empleados_get_all", conn))
			{
				cmd.CommandType = System.Data.CommandType.StoredProcedure;
				await conn.OpenAsync();
				using (var reader = await cmd.ExecuteReaderAsync())
				{
					while (await reader.ReadAsync())
					{
						lista.Add(MapEmpleado(reader));
					}
				}
			}

			return lista;
		}
		/// <summary>
		/// servicio para insertar datos
		/// </summary>
		/// <param name="empleado">recibe el modelo empleados a insertar</param>
		/// <returns></returns>
		public async Task InsertAsync(Empleado empleado)
		{
			using (SqlConnection conn = new SqlConnection(_connectionString))
			using (SqlCommand cmd = new SqlCommand("dbo.sp_empleados_insert", conn))
			{
				cmd.CommandType = System.Data.CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@Codigo", empleado.Codigo);
				cmd.Parameters.AddWithValue("@Puesto", empleado.Puesto);
				cmd.Parameters.AddWithValue("@Nombre", empleado.Nombre);
				cmd.Parameters.AddWithValue("@CodigoJefe", (object?)empleado.CodigoJefe ?? DBNull.Value);

				await conn.OpenAsync();
				await cmd.ExecuteNonQueryAsync();
			}
		}
		/// <summary>
		/// servicio para actualizar empleados 
		/// </summary>
		/// <param name="empleado">recibe el modelo para actualizar datos</param>
		/// <returns></returns>
		public async Task<bool> UpdateAsync(Empleado empleado)
		{
			using (SqlConnection conn = new SqlConnection(_connectionString))
			using (SqlCommand cmd = new SqlCommand("dbo.sp_empleados_update", conn))
			{
				cmd.CommandType = System.Data.CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@Codigo", empleado.Codigo);
				cmd.Parameters.AddWithValue("@Puesto", empleado.Puesto);
				cmd.Parameters.AddWithValue("@Nombre", empleado.Nombre);
				cmd.Parameters.AddWithValue("@CodigoJefe", (object?)empleado.CodigoJefe ?? DBNull.Value);

				await conn.OpenAsync();
				int rows = await cmd.ExecuteNonQueryAsync();
				return rows > 0;
			}
		}
		/// <summary>
		/// servicio para eliminar empleado
		/// </summary>
		/// <param name="codigo">recibe id de dato a eliminar</param>
		/// <returns></returns>
		public async Task<bool> DeleteAsync(int codigo)
		{
			using (SqlConnection conn = new SqlConnection(_connectionString))
			using (SqlCommand cmd = new SqlCommand("dbo.sp_empleados_delete", conn))
			{
				cmd.CommandType = System.Data.CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@Codigo", codigo);

				await conn.OpenAsync();
				int rows = await cmd.ExecuteNonQueryAsync();
				return rows > 0;
			}
		}
		/// <summary>
		/// metodo para mapear datos en modelo de empleado
		/// </summary>
		/// <param name="reader">recibe los datos de la consulta</param>
		/// <returns></returns>
		private Empleado MapEmpleado(SqlDataReader reader)
		{
			return new Empleado
			{
				Codigo = reader.GetInt32(0),
				Puesto = reader.GetString(1),
				Nombre = reader.GetString(2),
				CodigoJefe = reader.IsDBNull(3) ? null : reader.GetInt32(3),
				EstaBorrado = reader.GetBoolean(4)
			};
		}
	}
}
