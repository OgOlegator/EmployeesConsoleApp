﻿using EmployeesConsoleApp.Controllers;
using EmployeesConsoleApp.Data;
using EmployeesConsoleApp.Models;
using EmployeesConsoleApp.Exceptions;
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
        public void AddAndSave_CallCreate_ResultOkAndIsCalled()
        {
            var testData = new Employee { FirstName = "1", LastName = "1", SalaryPerHour = 100 };

            var mockSet = new Mock<DataSet<Employee>>();

            var mockContext = new Mock<TextFileContext<Employee>>();
            mockContext.Object.DataSet = mockSet.Object;
            mockContext.Setup(x => x.SaveChanges()).Verifiable();

            var controller = new EmployeeController(mockContext.Object);
            controller.Create(testData);

            mockSet.Verify(m => m.Add(It.IsAny<Employee>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Fact]
        public void Remove_CallDeleteAndSave_ResultOkAndIsCalled()
        {
            var testData = GetTestEmployees();

            var mockSet = new Mock<DataSet<Employee>>();
            mockSet.Setup(x => x.Remove(It.IsAny<Employee>())).Returns(true).Verifiable();
            mockSet.As<IQueryable<Employee>>().Setup(dataSet => dataSet.GetEnumerator()).Returns(() => testData.GetEnumerator());

            var mockContext = new Mock<TextFileContext<Employee>>();
            mockContext.Object.DataSet = mockSet.Object;
            mockContext.Setup(x => x.SaveChanges()).Verifiable();

            var controller = new EmployeeController(mockContext.Object);
            
            controller.Delete(1);

            mockSet.Verify(m => m.Remove(It.IsAny<Employee>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Fact]
        public void Remove_RemoveEmployeeNotExists_ThrowException()
        {
            var mockSet = new Mock<DataSet<Employee>>();
            mockSet.Setup(x => x.Remove(It.IsAny<Employee>())).Returns(true).Verifiable();

            var mockContext = new Mock<TextFileContext<Employee>>();
            mockContext.Object.DataSet = mockSet.Object;

            var controller = new EmployeeController(mockContext.Object);

            Assert.Throws<EmployeeAppException>(() => controller.Delete(1));
        }

        [Fact]
        public void Update_UpdateAndCallSave_ResultOkAndIsCalled()
        {
            var testUpdateEmployee = new Employee { Id = 1, FirstName = "111", LastName = "111", SalaryPerHour = 111 };
            var testData = GetTestEmployees();

            var mockSet = new Mock<DataSet<Employee>>();
            mockSet.As<IQueryable<Employee>>().Setup(dataSet => dataSet.GetEnumerator()).Returns(() => testData.GetEnumerator());

            var mockContext = new Mock<TextFileContext<Employee>>();
            mockContext.Object.DataSet = mockSet.Object;
            mockContext.Setup(x => x.SaveChanges()).Verifiable();

            var controller = new EmployeeController(mockContext.Object);

            controller.Update(testUpdateEmployee);

            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Fact]
        public void Update_UpdateEmployeeNotExists_ThrowException()
        {
            var testData = new Employee { Id = 1, FirstName = "1", LastName = "1", SalaryPerHour = 100 };

            var mockSet = new Mock<DataSet<Employee>>();
            mockSet.Setup(x => x.Remove(It.IsAny<Employee>())).Returns(true).Verifiable();

            var mockContext = new Mock<TextFileContext<Employee>>();
            mockContext.Object.DataSet = mockSet.Object;

            var controller = new EmployeeController(mockContext.Object);

            Assert.Throws<EmployeeAppException>(() => controller.Update(testData));
        }

        [Fact]
        public void GetById_GetEmployeeNotExists_ThrowException()
        {
            var mockSet = new Mock<DataSet<Employee>>();
            mockSet.Setup(x => x.Remove(It.IsAny<Employee>())).Returns(true).Verifiable();

            var mockContext = new Mock<TextFileContext<Employee>>();
            mockContext.Object.DataSet = mockSet.Object;

            var controller = new EmployeeController(mockContext.Object);

            Assert.Throws<EmployeeAppException>(() => controller.GetById(1));
        }

        [Fact]
        public void GetAll_GetById_TrueResults()
        {
            var testData = GetTestEmployees();

            var mockSet = new Mock<DataSet<Employee>>();
            mockSet.As<IQueryable<Employee>>().Setup(dataSet => dataSet.GetEnumerator()).Returns(() => testData.GetEnumerator());

            var mockContext = new Mock<TextFileContext<Employee>>();
            mockContext.Object.DataSet = mockSet.Object;

            var controller = new EmployeeController(mockContext.Object);

            var resultGetById = controller.GetById(2).Equals(testData.FirstOrDefault(x => x.Id == 2));

            Assert.True(resultGetById);

            var resultGet = controller.Get();

            Assert.True(resultGet[0].Equals(testData[0]));
            Assert.True(resultGet[1].Equals(testData[1]));
            Assert.True(resultGet[2].Equals(testData[2]));
            Assert.True(resultGet[3].Equals(testData[3]));
            Assert.True(resultGet[4].Equals(testData[4]));
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
