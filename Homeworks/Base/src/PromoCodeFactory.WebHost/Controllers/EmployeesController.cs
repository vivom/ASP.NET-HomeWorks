using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain.Administration;
using PromoCodeFactory.WebHost.Models;

namespace PromoCodeFactory.WebHost.Controllers
{
    /// <summary>
    /// Сотрудники
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IRepository<Employee> _employeeRepository;

        public EmployeesController(IRepository<Employee> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        /// <summary>
        /// Получить данные всех сотрудников
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<EmployeeShortResponse>> GetEmployeesAsync()
        {
            var employees = await _employeeRepository.GetAllAsync();

            var employeesModelList = employees.Select(x =>
                new EmployeeShortResponse()
                {
                    Id = x.Id,
                    Email = x.Email,
                    FullName = x.FullName,
                }).ToList();

            return employeesModelList;
        }

        /// <summary>
        /// Получить данные сотрудника по Id
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<EmployeeResponse>> GetEmployeeByIdAsync(Guid id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);

            if (employee == null)
                return NotFound();

            var employeeModel = new EmployeeResponse()
            {
                Id = employee.Id,
                Email = employee.Email,
                Roles = employee.Roles is not null ? employee.Roles.Select(x => new RoleItemResponse()
                {
                    Name = x.Name,
                    Description = x.Description
                }).ToList() : null,
                FullName = employee.FullName,
                AppliedPromocodesCount = employee.AppliedPromocodesCount
            };

            return employeeModel;
        }

        /// <summary>
        /// Удалить сотрудника по Id
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id:guid}")]
        public async Task DeleteEmployeeByIdAsync(Guid id)
        {
            await _employeeRepository.DeleteByIdAsync(id);
        }

        /// <summary>
        /// Создать нового сотрудника
        /// </summary>
        /// <returns></returns>
        [HttpPost()]
        public async Task<ActionResult<EmployeeResponse>> CreateNewEmployeeAsync(EmployeeRequest newEmployee)
        {
            var employee = new Employee()
            {
                Id = Guid.NewGuid(),
                FirstName = newEmployee.FirstName,
                LastName = newEmployee.LastName,
                Email = newEmployee.Email,
                AppliedPromocodesCount = newEmployee.AppliedPromocodesCount
            };

            var resultEmployee = await _employeeRepository.CreateAsync(employee);

            var employeeModel = new EmployeeResponse()
            {
                Id = resultEmployee.Id,
                Email = resultEmployee.Email,
                Roles = resultEmployee.Roles is not null ? resultEmployee.Roles.Select(x => new RoleItemResponse()
                {
                    Name = x.Name,
                    Description = x.Description
                }).ToList() : null,
                FullName = resultEmployee.FullName,
                AppliedPromocodesCount = resultEmployee.AppliedPromocodesCount
            };

            return employeeModel;
        }

        /// <summary>
        /// Обновить данные о сотруднике
        /// </summary>
        /// <returns></returns>
        [HttpPost("{id:guid}")]
        public async Task<ActionResult<EmployeeResponse>> UpdateEmployeeAsync(Guid id, EmployeeRequest updateEmployee)
        {
            var employee = new Employee()
            {
                FirstName = updateEmployee.FirstName,
                LastName = updateEmployee.LastName,
                Email = updateEmployee.Email,
                AppliedPromocodesCount = updateEmployee.AppliedPromocodesCount
            };

            var resultEmployee = await _employeeRepository.UpdateAsync(id, employee);

            var employeeModel = new EmployeeResponse()
            {
                Id = resultEmployee.Id,
                Email = resultEmployee.Email,
                Roles = resultEmployee.Roles is not null ? resultEmployee.Roles.Select(x => new RoleItemResponse()
                {
                    Name = x.Name,
                    Description = x.Description
                }).ToList() : null,
                FullName = resultEmployee.FullName,
                AppliedPromocodesCount = resultEmployee.AppliedPromocodesCount
            };

            return employeeModel;
        }
    }
}