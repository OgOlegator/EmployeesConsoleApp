using EmployeesConsoleApp.Data;
using EmployeesConsoleApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesConsoleApp.Tests
{
    public class DataSetTests
    {
        private DataSet<Employee> InitializeDataSet()
        {
            return new DataSet<Employee>();
        }

        [Fact]
        public void AddRemove_ResultOk()
        {
            var dataSet = InitializeDataSet();

            var testData = new Employee { FirstName = "1", LastName = "1", SalaryPerHour = 101 };

            dataSet.Add(testData);

            Assert.Equal(dataSet.Count(), 1);
            Assert.True(dataSet.First().Equals(testData));
            Assert.True(dataSet.IsChanged);

            var result = dataSet.Remove(testData);

            Assert.True(result);
            Assert.True(dataSet.FirstOrDefault(x => x.Id == 1) == null);
            Assert.Equal(dataSet.Count(), 0);

            result = dataSet.Remove(testData);

            Assert.False(result);
            Assert.Equal(dataSet.Count(), 0);
        }

        [Fact]
        public void FillFromJson_ResultOk()
        {
            var testData = new List<Employee>
            {
                new Employee { Id = 1, FirstName = "1", LastName = "1", SalaryPerHour = 101 },
                new Employee { Id = 2, FirstName = "2", LastName = "2", SalaryPerHour = 102 },
                new Employee { Id = 3, FirstName = "3", LastName = "3", SalaryPerHour = 103 },
                new Employee { Id = 4, FirstName = "4", LastName = "4", SalaryPerHour = 104 },
                new Employee { Id = 5, FirstName = "5", LastName = "5", SalaryPerHour = 105 },
            };

            var jsonTestData = JsonConvert.SerializeObject(testData);

            var dataSet = InitializeDataSet();
            dataSet.FillFromJson(jsonTestData);

            Assert.Equal(dataSet.Count(), testData.Count());
            Assert.True(dataSet.FirstOrDefault(x => x.Id == 1).Equals(testData[0]));
            Assert.True(dataSet.FirstOrDefault(x => x.Id == 2).Equals(testData[1]));
            Assert.True(dataSet.FirstOrDefault(x => x.Id == 3).Equals(testData[2]));
            Assert.True(dataSet.FirstOrDefault(x => x.Id == 4).Equals(testData[3]));
            Assert.True(dataSet.FirstOrDefault(x => x.Id == 5).Equals(testData[4]));
        }
    }
}
