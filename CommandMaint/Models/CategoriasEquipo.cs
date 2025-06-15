using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CommandMaint.Models
{
	[Table("CategoriasEquipo")]
	public class CategoriasEquipo
	 {
		[Key]
		public int IdCategoriaEquipo { get; set; }
		[MaxLength(50)]
		[Required]
		public string NombreCategoria { get; set; }
		[MaxLength(100)]
		public string? Descripcion { get; set; }
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
