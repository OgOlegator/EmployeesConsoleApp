using EmployeesConsoleApp.Controllers;
using EmployeesConsoleApp.Data;
using EmployeesConsoleApp.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesConsoleApp.Tests
{
    public class EmployeesControllerTests
    {
        [Fact]
        public void AddEmployeeAndSaveInContext()
        {
            var mockSet = new Mock<DataSet<Employee>>();

            var mockContext = new Mock<TextFileContext<Employee>>();
            mockContext.Object.DataSet = mockSet.Object;

            var controller = new EmployeeController(mockContext.Object);
            controller.Create(new Employee { FirstName = "1", LastName = "1", SalaryPerHour = 100 });

            mockSet.Verify(m => m.Add(It.IsAny<Employee>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Fact]
        public void GetMethodsReturnTrueResults()
        {
            var employeeData = GetTestEmployees();

            var mockSet = new Mock<DataSet<Employee>>();
            mockSet.As<IQueryable<Employee>>().Setup(dataSet => dataSet.GetEnumerator()).Returns(() => employeeData.GetEnumerator());

            var mockContext = new Mock<TextFileContext<Employee>>();
            mockContext.Object.DataSet = mockSet.Object;

            var controller = new EmployeeController(mockContext.Object);

            var resultGet = controller.Get().All(x => GetTestEmployees().Any(y => y.Equals(x)));
            var resultGetById = controller.GetById(2).Equals(GetTestEmployees().FirstOrDefault(x => x.Id == 2));

            Assert.True(resultGet);
            Assert.True(resultGetById);
        }


        private List<Employee> GetTestEmployees()
            => new List<Employee>
            {
                new Employee { Id = 1, FirstName = "1", LastName = "1", SalaryPerHour = 101 },
                new Employee { Id = 2, FirstName = "2", LastName = "2", SalaryPerHour = 102 },
                new Employee { Id = 3, FirstName = "3", LastName = "3", SalaryPerHour = 103 },
                new Employee { Id = 4, FirstName = "4", LastName = "4", SalaryPerHour = 104 },
                new Employee { Id = 5, FirstName = "5", LastName = "5", SalaryPerHour = 105 },
            };

    }
}
