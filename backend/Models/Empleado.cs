namespace backend.Models
{
	public class Empleado
	{
		public int Codigo { get; set; }
		public string Puesto { get; set; }
		public string Nombre { get; set; }
		public int? CodigoJefe { get; set; }
		public bool EstaBorrado { get; set; }
	}
}
