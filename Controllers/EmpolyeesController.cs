using EmployeeManagement.Api.Repository;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;

namespace EmployeeManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController:ControllerBase
    {
        private readonly IRepository<Employee> repository;

        public EmployeesController(IRepository<Employee> repository)
        {
            this.repository = repository;
        }
        [HttpGet]
        public async Task<ActionResult> GetEmployees()
        {
            return Ok(await repository.GetAll());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
          var result = await repository.GetById(id);
            if (result==null)
            {
                return NotFound();
            }
            return result;
        }
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
           
            if (employee==null )
            {
                var result = await repository.Insert(employee);
                return result;
            }
          
            return NotFound();
        }
    }
}
