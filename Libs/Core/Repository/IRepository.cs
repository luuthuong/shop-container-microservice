using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repository
{
    public interface IRepository<TEntity> where TEntity: class
    {
        Task<TEntity> FindById(Guid id, CancellationToken cancellationToken = default);
        Task<TEntity> FindOneAsync(ISpecification)
    }
}
