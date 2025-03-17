using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain;
using PromoCodeFactory.Core.Domain.Administration;
namespace PromoCodeFactory.DataAccess.Repositories
{
    public class InMemoryRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected List<T> Data { get; set; }

        public InMemoryRepository(List<T> data)
        {
            Data = data;
        }

        public Task<List<T>> GetAllAsync()
        {
            return Task.FromResult(Data);
        }

        public Task<T> GetByIdAsync(Guid id)
        {
            return Task.FromResult(Data.FirstOrDefault(x => x.Id == id));
        }

        public Task DeleteByIdAsync(Guid id)
        {
            var finderEmployee = Data.FirstOrDefault(x => x.Id == id);

            if (finderEmployee is not null)
            {
                Data.Remove(finderEmployee);
            }

            return Task.CompletedTask;
        }

        public Task<T> UpdateAsync(Guid id, T updateElement)
        {
            updateElement.Id = id;
            var finderEmployee = Data.FindIndex(x => x.Id == id);

            Data[finderEmployee] = updateElement;

            return Task.FromResult(Data.FirstOrDefault(x => x.Id == id));
        }

        public Task<T> CreateAsync(T newElement)
        {
            Data.Add(newElement);
            return GetByIdAsync(Data.Where(i => i.Id == newElement.Id).FirstOrDefault().Id);
        }
    }
}