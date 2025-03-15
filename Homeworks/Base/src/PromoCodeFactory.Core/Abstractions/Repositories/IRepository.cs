using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PromoCodeFactory.Core.Domain;
using PromoCodeFactory.Core.Domain.Administration;

namespace PromoCodeFactory.Core.Abstractions.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task DeleteByIdAsync(Guid id);

        Task<T> UpdateAsync(Guid id, Employee employee);

        Task<T> CreateAsync(Employee employee);

        Task<T> GetByIdAsync(Guid id);
    }
}