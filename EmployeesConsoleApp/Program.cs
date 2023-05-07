using EmployeesConsoleApp;
using EmployeesConsoleApp.Controllers;
using EmployeesConsoleApp.Data;
using EmployeesConsoleApp.Data.Exceptions;
using EmployeesConsoleApp.Exceptions;
using EmployeesConsoleApp.Models;
using EmployeesConsoleApp.Services;

namespace EmployeeConsoleApp
{
    internal class Program
    {
        /// <summary>
        /// Точка входа
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            TextFileContext<Employee> employeeContext;

            try
            {
                employeeContext = new TextFileContext<Employee>().SetContext(StaticDetails.FileFolderPath, StaticDetails.FileName);
            }
            catch (CreateContextException ex) 
            {
                Console.WriteLine(ex.Message);
                return;
            }

            var controller = new EmployeeController(employeeContext);

            var commandParser = new CommandParser(args);

            try
            {
                switch (commandParser.Action)
                {
                    case StaticDetails.ActionGetAll:
                        {
                            var result = controller.Get();

                            foreach (var employee in result)
                                Console.WriteLine(employee.ToString());

                            break;
                        }
                    case StaticDetails.ActionGetById:
                        {
                            var result = controller.GetById(commandParser.Id);
                            Console.WriteLine(result.ToString());

                            break;
                        }
                    case StaticDetails.ActionCreate:
                        {
                            controller.Create(new Employee
                            {
                                FirstName = commandParser.FirstName ?? "",
                                LastName = commandParser.LastName ?? "",
                                SalaryPerHour = commandParser.Salary != -1 ? commandParser.Salary : 0,
                            });

                            Console.WriteLine("Ok");

                            break;
                        }
                    case StaticDetails.ActionUpdate:
                        {
                            var changeModel = new Employee
                            {
                                Id = commandParser.Id,
                                FirstName = commandParser.FirstName,
                                LastName = commandParser.LastName,
                                SalaryPerHour = commandParser.Salary,
                            };

                            controller.Update(changeModel);
                            Console.WriteLine("Ok");

                            break;
                        }
                    case StaticDetails.ActionDelete:
                        {
                            controller.Delete(commandParser.Id);
                            Console.WriteLine("Ok");

                            break;
                        }
                    default:
                        Console.WriteLine("Введено некорректное действие");
                        break;
                }
            }
            catch (EmployeeAppException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}