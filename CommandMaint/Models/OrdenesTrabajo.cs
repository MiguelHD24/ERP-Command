using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CommandMaint.Models
{
	[Table("OrdenesTrabajo")]
	public class OrdenesTrabajo
	 {
		[Key]
		public int IdOrdenes { get; set; }
		[Required]
		public int IdCliente { get; set; }
		[Required]
		public int IdCodigoProducto { get; set; }
		[Required]
		public int IdEquipo { get; set; }
		[MaxLength(20)]
		[Required]
		public string LotNumber { get; set; }
		[Required]
		public DateTime FechaInicio { get; set; }
		[Required]
		public DateTime FechaFin { get; set; }
		[MaxLength(500)]
		public string? Descripcion { get; set; }
		[Required]
		public int IdTecnico { get; set; }
		public DateTime? MuestreoProduccion { get; set; }
		public DateTime? Quality { get; set; }
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
