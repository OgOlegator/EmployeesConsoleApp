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
        /// Объект для работы с хранилищем данных
        /// </summary>
        private static TextFileContext<Employee> _employeeContext;

        /// <summary>
        /// Контроллер для выполнения основной логики приложения
        /// </summary>
        public static EmployeeController _controller;

        /// <summary>
        /// Точка входа
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            try
            {
                _employeeContext = new TextFileContext<Employee>().SetContext(StaticDetails.FileFolderPath, StaticDetails.FileName);
            }
            catch (CreateContextException ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

            _controller = new EmployeeController(_employeeContext);

            while (true)
            {
                Console.WriteLine("Введите команду:");
                
                var inputCommand = Console.ReadLine();

                if(string.IsNullOrEmpty(inputCommand))
                {
                    Console.WriteLine("Команда не заполнена");
                    continue;
                }

                Console.WriteLine();
                Console.WriteLine("result:");

                try
                {
                   var actionName = CommandParser.GetActionName(inputCommand);

                    switch (actionName)
                    {
                        case StaticDetails.ActionName.GetAll:
                            {
                                var result = _controller.Get();

                                foreach (var employee in result)
                                    Console.WriteLine(employee.ToString());

                                break;
                            }
                        case StaticDetails.ActionName.GetById:
                            {
                                var id = int.Parse(CommandParser.GetValueParamOrDefault(inputCommand, StaticDetails.IdParameter));

                                var result = _controller.GetById(id);
                                Console.WriteLine(result.ToString());

                                break;
                            }
                        case StaticDetails.ActionName.Create:
                            {
                                _controller.Create(new Employee
                                {
                                    FirstName = CommandParser.GetValueParamOrDefault(inputCommand, StaticDetails.FirstNameParameter) ?? "",
                                    LastName = CommandParser.GetValueParamOrDefault(inputCommand, StaticDetails.LastNameParameter) ?? "",
                                    SalaryPerHour = decimal.Parse(CommandParser.GetValueParamOrDefault(inputCommand, StaticDetails.SalaryParameter) ?? "0"),
                                });

                                Console.WriteLine("Ok");

                                break;
                            }
                        case StaticDetails.ActionName.Update:
                            {
                                var id = CommandParser.GetValueParamOrDefault(inputCommand, StaticDetails.IdParameter);
                                var firstName = CommandParser.GetValueParamOrDefault(inputCommand, StaticDetails.FirstNameParameter);
                                var lastName = CommandParser.GetValueParamOrDefault(inputCommand, StaticDetails.LastNameParameter);
                                var salary = CommandParser.GetValueParamOrDefault(inputCommand, StaticDetails.SalaryParameter);

                                if (id == null)
                                {
                                    Console.WriteLine("Параметр id некорректно заполнен");
                                    continue;
                                }

                                var changeModel = new Employee
                                {
                                    Id = int.Parse(id),
                                    FirstName = firstName,
                                    LastName = lastName,
                                    SalaryPerHour = decimal.Parse(salary ?? "-1"),
                                };

                                _controller.Update(changeModel);
                                Console.WriteLine("Ok");

                                break;
                            }
                        case StaticDetails.ActionName.Delete:
                            {
                                var id = int.Parse(CommandParser.GetValueParamOrDefault(inputCommand, StaticDetails.IdParameter));

                                _controller.Delete(id);
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
                finally
                {
                    Console.WriteLine();
                    Console.WriteLine("end.");
                    Console.WriteLine();
                }
            }
        }
    }
}