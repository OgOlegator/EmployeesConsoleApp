using EmployeesConsoleApp;
using EmployeesConsoleApp.Controllers;
using EmployeesConsoleApp.Data;
using EmployeesConsoleApp.Models;
using EmployeesConsoleApp.Services;

namespace EmployeeConsoleApp
{
    internal class Program
    {
        /// <summary>
        /// Объект для работы с хранилищем данных
        /// </summary>
        private static TextFileContext<Employee> _employeeContext = new TextFileContext<Employee>(StaticDetails.FullFilePath);

        /// <summary>
        /// Контроллер для выполнения основной логики приложения
        /// </summary>
        public static EmployeeController _controller = new EmployeeController(_employeeContext);

        /// <summary>
        /// Точка входа
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        { 
            while (true)
            {
                Console.WriteLine("Введите команду:");
                Console.WriteLine("command");
                
                var inputCommand = Console.ReadLine();

                if(string.IsNullOrEmpty(inputCommand))
                {
                    Console.WriteLine("Команда не заполнена");
                    continue;
                }

                Console.WriteLine("result");

                StaticDetails.ActionName actionName;

                try
                {
                    actionName = CommandParser.GetActionName(inputCommand);
                }
                catch (Exception)
                {
                    Console.WriteLine("Введена некорректная команда");
                    Console.WriteLine("end");
                    Console.WriteLine();
                    continue;
                }

                switch (actionName)
                {
                    case StaticDetails.ActionName.GetAll:
                        {
                            var result = _controller.Get();

                            foreach(var employee in result)
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
                            try
                            {
                                _controller.Create(new Employee
                                {
                                    FirstName = CommandParser.GetValueParamOrDefault(inputCommand, StaticDetails.FirstNameParameter) ?? "",
                                    LastName = CommandParser.GetValueParamOrDefault(inputCommand, StaticDetails.LastNameParameter) ?? "",
                                    SalaryPerHour = decimal.Parse(CommandParser.GetValueParamOrDefault(inputCommand, StaticDetails.SalaryParameter) ?? "0"),
                                });
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }

                            break;
                        }
                    case StaticDetails.ActionName.Update:
                        {
                            var id = CommandParser.GetValueParamOrDefault(inputCommand, StaticDetails.IdParameter);
                            var firstName = CommandParser.GetValueParamOrDefault(inputCommand, StaticDetails.FirstNameParameter);
                            var lastName = CommandParser.GetValueParamOrDefault(inputCommand, StaticDetails.LastNameParameter);
                            var salary = CommandParser.GetValueParamOrDefault(inputCommand, StaticDetails.SalaryParameter);

                            if(id == null)
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

                            try
                            {
                                _controller.Update(changeModel);
                                Console.WriteLine("Ok");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }

                            break;
                        }
                    case StaticDetails.ActionName.Delete:
                        {
                            var id = int.Parse(CommandParser.GetValueParamOrDefault(inputCommand, StaticDetails.IdParameter));

                            try
                            {
                                _controller.Delete(id);
                                Console.WriteLine("Ok");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }

                            break;
                        }
                    default:
                        Console.WriteLine("Введено некорректное действие");
                        break;
                }

                Console.WriteLine("end");
                Console.WriteLine();
            }
        }
    }
}