using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CommandMaint.Models
{
	[Table("RepuestosOrdenTrabajo")]
	public class RepuestosOrdenTrabajo
	 {
		[Key]
		public int IdRepuestoOrden { get; set; }
		[Required]
		public int IdOrdenes { get; set; }
		[Required]
		public int IdRepuesto { get; set; }
		[Required]
		public int Cantidad { get; set; }
		[Required]
		public bool Activo { get; set; }
		[Required]
		public int UsuarioRegistra { get; set; }
		[Required]
		public DateTime FechaRegistra { get; set; }
		public int? UsuarioActualiza { get; set; }
		public DateTime? FechaActualizacion { get; set; }
	 }
}
