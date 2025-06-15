using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CommandMaint.Models
{
	[Table("Equipos")]
	public class Equipos
	 {
		[Key]
		public int IdEquipo { get; set; }
		[MaxLength(20)]
		[Required]
		public string NombreEquipo { get; set; }
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
