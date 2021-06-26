using EmployeeManagement.Api.Model;
using EmployeeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Api.Repository
{
    public class EmployeeRepository: Repository<Employee> 
    {
        private IRepository<Employee> repository = null;
        public EmployeeRepository()
        {
            repository = new Repository<Employee>();
        }





    }
}
