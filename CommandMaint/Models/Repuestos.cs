using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CommandMaint.Models
{
	[Table("Repuestos")]
	public class Repuestos
	 {
		[Key]
		public int IdRepuesto { get; set; }
		[MaxLength(50)]
		[Required]
		public string NumeroParte { get; set; }
		[MaxLength(100)]
		[Required]
		public string Descripcion { get; set; }
		[MaxLength(50)]
		[Required]
		public string Ubicacion { get; set; }
		[Required]
		public int IdCategoriaEquipo { get; set; }
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
