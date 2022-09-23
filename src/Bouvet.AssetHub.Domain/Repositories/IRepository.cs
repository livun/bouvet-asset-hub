using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bouvet.AssetHub.Domain.Repositories
{
    public interface IRepository<T, K>
    {
        Task<List<T>> Read();

        Task<T> ReadById(K id); 
        Task<T> Create(T entity); 
        Task<T> Update(T entity); 
        Task<T> Delete(T entity);
    }
}
