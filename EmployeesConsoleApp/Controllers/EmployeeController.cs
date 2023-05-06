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
        private readonly TextFileContext<Employee> _context;

        public EmployeeController(TextFileContext<Employee> employeeContext)
        {
            _context = employeeContext;
        }

        public List<Employee> Get()
        {
            return _context.DataSet.ToList();
        }

        public Employee GetById(int id)
        {
            return _context.DataSet.FirstOrDefault(employee => employee.Id == id) ?? new Employee();
        }

        public void Create(Employee employee)
        {
            _context.DataSet.Add(employee);
            _context.SaveChanges();
        }

        public void Update(Employee employee)
        {

            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var deleteEmployee = _context.DataSet.FirstOrDefault(employee => employee.Id == id);

            if (deleteEmployee == null)
                return;

            var result = _context.DataSet.Remove(deleteEmployee);

            if(result)
                _context.SaveChanges();
        }
    }
}
