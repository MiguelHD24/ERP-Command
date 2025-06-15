using Microsoft.EntityFrameworkCore;
namespace CommandMaint.Models
{
    public class CommandContext : DbContext
    {
        public CommandContext(DbContextOptions<CommandContext> Options) : base (Options)
        {
            
        }
        public DbSet<CategoriasEquipo> CategoriasEquipos { get; set; }
        public DbSet<Clientes> Clientes { get; set; }
        public DbSet<CodigoProductos> CodigoProductos { get; set; }
        public DbSet<EntradasInventario> EntradasInventarios { get; set; }
        public DbSet<Equipos> Equipos { get; set; }
        public DbSet<OrdenesTrabajo> OrdenesTrabajos { get; set; }
        public DbSet<Repuestos> Repuestos { get; set; }
        public DbSet<RepuestosOrdenTrabajo> RepuestosOrdenTrabajos { get; set; }
        public DbSet<Tecnicos> Tecnicos { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }
       
    }
}
