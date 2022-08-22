using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Talabat.BLL.Interfaces;
using Talabat.BLL.Specifications;
using Talabat.DAL.Entities;

namespace Talabat.API.Controllers
{

    public class EmployeesController : BaseAPIController
    {
        private readonly IGenericRepository<Employee> _employeeRepo;
        private readonly IGenericRepository<Department> departrepo;

        public EmployeesController(IGenericRepository<Employee> employeeRepo, IGenericRepository<Department> departrepo)
        {
            _employeeRepo = employeeRepo;
            this.departrepo = departrepo;
        }


        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Employee>>> GetEmployees(string sort)
        {
            var employees = await _employeeRepo.GetAllWithSpecAsync(new EmployeeWithDepartmentSpecification(sort));

            return Ok(employees);
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var employee = await _employeeRepo.GetEntityWithSpecAsync(new EmployeeWithDepartmentSpecification(id));

            return Ok(employee);
        }

        //[HttpGet("departments")]
        //public async Task<ActionResult<IReadOnlyList<Department>>> GetDepartments()
        //{
        //    var department = await departrepo.GetAllWithSpecAsync(new DepWithEmploSpec());

        //    return Ok(department);
        //}
    }
}
