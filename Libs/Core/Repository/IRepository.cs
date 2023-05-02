using Core.Specifications.Interfaces;

namespace Core.Repository
{
    public interface IRepository<TEntity> where TEntity: class
    {
        Task<TEntity> FindById(Guid id, CancellationToken cancellationToken = default); 
        Task<TEntity> FindOneAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);
    }
}
