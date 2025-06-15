using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CommandMaint.Models
{
	[Table("EntradasInventario")]
	public class EntradasInventario
	 {
		[Key]
		public int IdEntrada { get; set; }
		[Required]
		public int IdRepuesto { get; set; }
		[Required]
		public int Cantidad { get; set; }
		[Required]
		public DateTime FechaIngreso { get; set; }
		[MaxLength(50)]
		public string? RecibidoPor { get; set; }
		[MaxLength(200)]
		public string? Observacion { get; set; }
		[Required]
		public int UsuarioRegistra { get; set; }
		[Required]
		public DateTime FechaRegistra { get; set; }
	 }
}
