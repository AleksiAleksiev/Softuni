namespace PhotographyWorkshops.Data
{
    using PhotographyWorkshops.Data.Interfaces;
    using PhotographyWorkshops.Models;

    public class UnitOfWork : IUnitOfWork
    {
        private readonly PhotographyWorkshopsContext context;

        public UnitOfWork(PhotographyWorkshopsContext context)
        {
            this.context = context;
            this.Accessories = new Repository<Accessory>(this.context.Accessories);
            this.Cameras = new Repository<Camera>(this.context.Cameras);
            this.Lenses = new Repository<Lens>(this.context.Lenses);
            this.Photographers = new Repository<Photographer>(this.context.Photographers);
            this.Workshops = new Repository<Workshop>(this.context.Workshops);
        }

        public IRepository<Accessory> Accessories { get; }

        public IRepository<Camera> Cameras { get; }

        public IRepository<Lens> Lenses { get; }

        public IRepository<Photographer> Photographers { get; }

        public IRepository<Workshop> Workshops { get; }

        public void Commit()
        {
            this.context.SaveChanges();
        }

        public void Dispose()
        {
            this.context.Dispose();
        }
    }
}
