using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CommandMaint.Models
{
	[Table("Clientes")]
	public class Clientes
	 {
		[Key]
		public int IdCliente { get; set; }
		[MaxLength(30)]
		[Required]
		public string NombreCliente { get; set; }
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
