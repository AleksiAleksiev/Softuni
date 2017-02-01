namespace PhotographyWorkshops.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Linq;

    using PhotographyWorkshops.Models;

    public class PhotographyWorkshopsContext : DbContext
    {
        public PhotographyWorkshopsContext()
            : base("name=PhotographyWorkshopsContext")
        {
        }

        public DbSet<Accessory> Accessories { get; set; }

        public DbSet<Camera> Cameras { get; set; }

        public DbSet<Lens> Lenses { get; set; }

        public DbSet<Photographer> Photographers { get; set; }

        public DbSet<Workshop> Workshops { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Entity<Lens>().Property(lens => lens.MaxAperture).HasPrecision(18, 1);
            base.OnModelCreating(modelBuilder);
        }
    }
}