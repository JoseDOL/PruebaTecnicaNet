using backend.Models;
using backend.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EmpleadosController : ControllerBase
	{
		private readonly ServicioEmpleado _servicio;
		
		public EmpleadosController(ServicioEmpleado servicio)
		{
			_servicio = servicio;
		}
		/// <summary>
		/// endpoint para obetener toodos los datos de la tabla
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Empleado>>> Get()
		{
			var empleados = await _servicio.GetAllAsync();
			return Ok(empleados);
		}
		/// <summary>
		/// endpoint para insetar datos 
		/// </summary>
		/// <param name="empleado"> recibe el modelo de empleado </param>
		/// <returns></returns>
		[HttpPost]
		public async Task<ActionResult> Post(Empleado empleado)
		{
			await _servicio.InsertAsync(empleado);
			return CreatedAtAction(nameof(Get), new { id = empleado.Codigo }, empleado);
		}
		/// <summary>
		/// endpoint para actualizar empleados
		/// </summary>
		/// <param name="id">id de empleado</param>
		/// <param name="empleado">empleado a modificar </param>
		/// <returns></returns>
		[HttpPut("{id}")]
		public async Task<ActionResult> Put(int id, Empleado empleado)
		{
			if (id != empleado.Codigo)
				return BadRequest();

			bool updated = await _servicio.UpdateAsync(empleado);
			if (!updated)
				return NotFound();

			return NoContent();
		}

		/// <summary>
		/// endpoint para eliminar datos
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpDelete("{id}")]
		public async Task<ActionResult> Delete(int id)
		{
			bool deleted = await _servicio.DeleteAsync(id);
			if (!deleted)
				return NotFound();

			return NoContent();
		}
	}
}
