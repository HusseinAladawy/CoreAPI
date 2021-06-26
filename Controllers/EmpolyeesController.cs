using EmployeeManagement.Api.Repository;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
        //public async Task<ActionResult<Employee>> GetEmpoloyeeByEmail(String email)
        //{
        //    var result = await repository.Find(x=>x.Email==email);
            
        //    if (result == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(result.FirstOrDefault());
        //}
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
           
            if (employee==null )
            {
                return BadRequest();

            }
            var emp = await repository.Find(x => x.Email == employee.Email);
            if (emp.Count() > 0)
            {
                ModelState.AddModelError("Email", "Employee email is already exist");
                return BadRequest(ModelState);
            }
            else
            {
                var result = await repository.Insert(employee);

                return result;
            }
              
            
          
           
        }
        [HttpPut]
        public async Task<ActionResult< Employee>> UpdateEmployee(Employee employee)
        {
            if (employee == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,"Error Updating data");
            }

            var result =await repository.Update(employee);
            await repository.Save();
            




            return result;
            
        }
    }
}
