using EmployeeManagement.Api.Model;
using EmployeeManagement.Api.Repository;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Caching.Memory;
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
        private AppDbContext context = null;
        private readonly IRepository<Employee> repository;
        private readonly IMemoryCache memoryCache;
       // private readonly IEmployeeRepository employee_repository;


        public EmployeesController(IRepository<Employee> repository,IMemoryCache memoryCache)
        {
            this.repository = repository;
            this.memoryCache = memoryCache;
         //   this.employee_repository = new EmployeeRepository();
          
            
        }
        [HttpGet]
        public async Task<ActionResult> GetEmployees()
        {
            //var employees = await repository.GetAll();

            if (!memoryCache.TryGetValue("data", out IEnumerable<Employee> employees))
            {
                employees = await repository.GetAll();

                var cacheOptions = new MemoryCacheEntryOptions()
                {
                    AbsoluteExpiration = DateTime.Now.AddDays(1)

                };
                memoryCache.Set("data", employees);


            }


            return Ok(employees);
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
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEmployee(int id)
        {
            var result = await repository.GetById(id);
            if (result==null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "this user doesn't exist");
            }
            await repository.Delete(result);
            return Ok("the employee deleted successfully");  
                  
        }
        [HttpPut]
        public async Task<ActionResult< Employee>> UpdateEmployee(Employee employee)
        {
            if (employee == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,"Error Updating data");
            }


            return await repository.Update(employee);
            
        }
       // [HttpGet("{Search}")]
        //public async Task<ActionResult<IEnumerable<Employee>>> SearchEmployees(string name,Gender? gender)
        //{

        //   var result =await employee_repository.SearchEmployees(name, gender);

        //    if (result.Any())
        //    {
        //        return Ok(result);
        //    }
        //    else
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, "No data for this search");
        //    }


               

        //}

    }
}

