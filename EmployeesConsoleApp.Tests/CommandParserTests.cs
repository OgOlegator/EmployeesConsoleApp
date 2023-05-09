using EmployeesConsoleApp.Services;
using System;

namespace EmployeesConsoleApp.Tests
{
    public class CommandParserTests
    {
        [Theory]
        [InlineData("-add FirstName:John LastName:Doe Salary:100", "-add", "John", "Doe", 100)]
        public void IfCorrectParameters_ReturnOk(string command, string action, string firstName, string lastName, decimal salary)
        {
            var parser = new CommandParser(command.Split(' '));

            Assert.Equal(parser.Action, action);
            Assert.Equal(parser.FirstName, firstName);
            Assert.Equal(parser.LastName, lastName);
            Assert.Equal(parser.Salary, salary);
        }

        [Theory]
        [InlineData("-add FirstName:")]
        public void IfEmptyParam_ReturnEmptyString(string command)
        {
            var parser = new CommandParser(command.Split(' '));
            Assert.Equal(parser.FirstName, string.Empty);
        }

        [Theory]
        [InlineData("-get Id:100rrr")]
        //[InlineData("-update Id:1 Salary:100rrr")]
        public void IfIdNotNumericValue_ReturnException(string command)
        {
            var parser = new CommandParser(command.Split(' '));

            //Предпологается, что при некорректном значении из свойства будет выброшен exception
            try
            {
                var result = parser.Id;
                Assert.True(false);
            }
            catch
            {
                Assert.True(true);
            }
        }

        [Theory]
        [InlineData("-update Id:1 Salary:100rrr")]
        public void IfSalaryNotNumericValue_ReturnException(string command)
        {
            var parser = new CommandParser(command.Split(' '));

            //Предпологается, что при некорректном значении из свойства будет выброшен exception
            try
            {
                var result = parser.Salary;
                Assert.True(false);
            }
            catch
            {
                Assert.True(true);
            }
        }
    }
}