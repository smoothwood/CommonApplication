using Core.Data;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IAsyncRepository<T> where T:BaseEntity
    {
        Task<T> GetByIdAsync(int id);
        Task<T> GetSingleBySpecAsync(ISpecification<T> spec);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> DeleteAsync(T entity);
        Task<int> CountAsync(ISpecification<T> spec);
        

    }
}
