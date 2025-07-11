using System.ComponentModel.DataAnnotations;

namespace frontend.Models
{
	public class Empleado
	{
		[Required]
		[Range(0, int.MaxValue, ErrorMessage = "El Código debe ser un número positivo.")]
		public int Codigo { get; set; }

		[Required]
		public string Puesto { get; set; }

		[Required]
		public string Nombre { get; set; }

		[Range(0, int.MaxValue, ErrorMessage = "El Código Jefe debe ser un número positivo o 0 si no aplica.")]
		public int? CodigoJefe { get; set; }

		public bool EstaBorrado { get; set; }
	}
}