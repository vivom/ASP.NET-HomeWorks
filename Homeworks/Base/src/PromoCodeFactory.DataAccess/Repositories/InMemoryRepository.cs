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
        protected IEnumerable<T> Data { get; set; }

        public InMemoryRepository(IEnumerable<T> data)
        {
            Data = data;
        }

        public Task<IEnumerable<T>> GetAllAsync()
        {
            return Task.FromResult(Data);
        }

        public Task<T> GetByIdAsync(Guid id)
        {
            return Task.FromResult(Data.FirstOrDefault(x => x.Id == id));
        }

        public Task DeleteByIdAsync(Guid id)
        {
            var employeeList = Data as List<Employee>;
            var finderEmployee = employeeList.FirstOrDefault(x => x.Id == id);

            if (finderEmployee is not null)
            {
                employeeList.Remove(finderEmployee);
                Data = employeeList as IEnumerable<T>;
            }

            return Task.CompletedTask;
        }

        public Task<T> UpdateAsync(Guid id, Employee employee)
        {
            employee.Id = id;
            var employeeList = Data as List<Employee>;
            var finderEmployee = employeeList.FindIndex(x => x.Id == id);

            employeeList[finderEmployee] = employee;
            Data = employeeList as IEnumerable<T>;

            return Task.FromResult(Data.FirstOrDefault(x => x.Id == id));
        }

        public Task<T> CreateAsync(Employee employee)
        {
            var employeeList = Data as List<Employee>;
            employeeList.Add(employee);


            Data = employeeList as IEnumerable<T>;
            return GetByIdAsync(Data.Where(i => i.Id == employee.Id).FirstOrDefault().Id);
        }
    }
}