using EmployeesConsoleApp.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesConsoleApp.Models
{
    /// <summary>
    /// Модель сотрудник
    /// </summary>
    public class Employee : IDataElement
    {
        public int Id { get; set;  }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public decimal SalaryPerHour { get; set; }
    }
}
