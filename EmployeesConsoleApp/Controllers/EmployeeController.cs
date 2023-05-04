using EmployeesConsoleApp.Data;
using EmployeesConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesConsoleApp.Controllers
{
    public class EmployeeController
    {
        private readonly TextFileContext<Employee> _employeeContext;

        public EmployeeController(TextFileContext<Employee> employeeContext)
        {
            _employeeContext = employeeContext;
        }

        public List<Employee> Get()
        {


            return null;
        }

        public Employee GetById(int id)
        {


            return new Employee();
        }

        public void Create(Employee employee)
        {

        }

        public void Update(Employee employee)
        {

        }

        public void Delete(int id)
        {

        }
    }
}
