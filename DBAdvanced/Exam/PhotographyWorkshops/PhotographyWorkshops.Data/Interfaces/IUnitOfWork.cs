namespace PhotographyWorkshops.Data.Interfaces
{
    using System;

    using PhotographyWorkshops.Models;

    public interface IUnitOfWork : IDisposable
    {
        IRepository<Accessory> Accessories { get; }

        IRepository<Camera> Cameras { get; }

        IRepository<Lens> Lenses { get; }

        IRepository<Photographer> Photographers { get; }

        IRepository<Workshop> Workshops { get; }

        void Commit();
    }
}
