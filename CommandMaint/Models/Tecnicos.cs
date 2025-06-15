using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CommandMaint.Models
{
	[Table("Tecnicos")]
	public class Tecnicos
	 {
		[Key]
		public int IdTecnico { get; set; }
		[MaxLength(60)]
		[Required]
		public string NombreTecnico { get; set; }
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
