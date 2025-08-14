using DB.Models;
using Microsoft.EntityFrameworkCore;

namespace DB.Data
{
    public class PersicufContext : DbContext
    {
        public PersicufContext(DbContextOptions<PersicufContext> options) : base(options) { }

        public DbSet<Campera> Camperas { get; set; }
        public DbSet<Color> Colores { get; set; }
        public DbSet<CorteCuello> CortesCuello { get; set; }
        public DbSet<Domicilio> Domicilios { get; set; }
        public DbSet<Imagen> Imagenes { get; set; }
        public DbSet<Largo> Largos { get; set; }
        public DbSet<Localidad> Localidades { get; set; }
        public DbSet<Manga> Mangas { get; set; }
        public DbSet<Material> Materiales { get; set; }
        public DbSet<Pantalon> Pantalones { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<PedidoPrenda> PedidosPrenda { get; set; }
        public DbSet<Permiso> Permisos { get; set; }
        public DbSet<Prenda> Prendas { get; set; }
        public DbSet<Provincia> Provincias { get; set; }
        public DbSet<Remera> Remeras { get; set; }
        public DbSet<Rubro> Rubros { get; set; }
        public DbSet<TalleAlfabetico> TallesAlfabeticos { get; set; }
        public DbSet<TalleNumerico> TallesNumericos { get; set; }
        public DbSet<Ubicacion> Ubicaciones { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Zapato> Zapatos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Campera>().ToTable("Campera");
            modelBuilder.Entity<Color>().ToTable("Color");
            modelBuilder.Entity<CorteCuello>().ToTable("CorteCuello");
            modelBuilder.Entity<Domicilio>().ToTable("Domicilio");
            modelBuilder.Entity<Imagen>().ToTable("Imagen");
            modelBuilder.Entity<Largo>().ToTable("Largo");
            modelBuilder.Entity<Localidad>().ToTable("Localidad");
            modelBuilder.Entity<Manga>().ToTable("Manga");
            modelBuilder.Entity<Material>().ToTable("Material");
            modelBuilder.Entity<Pantalon>().ToTable("Pantalon");
            modelBuilder.Entity<Pedido>().ToTable("Pedido");
            modelBuilder.Entity<PedidoPrenda>().ToTable("PedidoPrenda");
            modelBuilder.Entity<Permiso>().ToTable("Permiso");
            modelBuilder.Entity<Prenda>().ToTable("Prenda");
            modelBuilder.Entity<Provincia>().ToTable("Provincia");
            modelBuilder.Entity<Remera>().ToTable("Remera");
            modelBuilder.Entity<Rubro>().ToTable("Rubro");
            modelBuilder.Entity<TalleAlfabetico>().ToTable("TalleAlfabetico");
            modelBuilder.Entity<TalleNumerico>().ToTable("TalleNumerico");
            modelBuilder.Entity<Ubicacion>().ToTable("Ubicacion");
            modelBuilder.Entity<Usuario>().ToTable("Usuario");
            modelBuilder.Entity<Zapato>().ToTable("Zapato");
        }




    }
}
