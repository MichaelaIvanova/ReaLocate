namespace ReaLocate.Data.Repository
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    public interface IApplicationDbContext : IDisposable
    {
        int SaveChanges();

        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        
    }
}