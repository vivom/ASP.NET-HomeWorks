using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PromoCodeFactory.Core.Domain;
namespace PromoCodeFactory.Core.Abstractions.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<List<T>> GetAllAsync();

        Task DeleteByIdAsync(Guid id);

        Task<T> UpdateAsync(Guid id, T updateElement);

        Task<T> CreateAsync(T newElement);

        Task<T> GetByIdAsync(Guid id);
    }
}