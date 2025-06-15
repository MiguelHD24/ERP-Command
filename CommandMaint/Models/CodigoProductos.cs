using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CommandMaint.Models
{
	[Table("CodigoProductos")]
	public class CodigoProductos
	 {
		[Key]
		public int IdCodigoProducto { get; set; }
		[MaxLength(50)]
		[Required]
		public string CodigoProducto { get; set; }
		[Required]
		public int IdCliente { get; set; }
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
