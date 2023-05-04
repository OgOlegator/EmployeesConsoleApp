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
                Console.WriteLine("--------command--------");
                
                var inputCommand = Console.ReadLine();

                Console.WriteLine("--------result---------");

                StaticDetails.ActionName actionName;

                try
                {
                    actionName = CommandParser.GetActionName(inputCommand);
                }
                catch (Exception)
                {
                    Console.WriteLine("Введено некорректное действие");
                    Console.WriteLine("----------end----------");
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
                            var id = int.Parse(CommandParser.GetValueParameter(inputCommand, StaticDetails.IdParameter));
                            var result = _controller.GetById(id);

                            Console.WriteLine(result.ToString());

                            break;
                        }
                    case StaticDetails.ActionName.Create:
                        {
                            _controller.Create(new Employee
                            {
                                FirstName = CommandParser.GetValueParameter(inputCommand, StaticDetails.FirstNameParameter),
                                LastName = CommandParser.GetValueParameter(inputCommand, StaticDetails.LastNameParameter),
                                SalaryPerHour = decimal.Parse(CommandParser.GetValueParameter(inputCommand, StaticDetails.SalaryParameter)),
                            });

                            break;
                        }
                    case StaticDetails.ActionName.Update:
                        {


                            break;
                        }
                    case StaticDetails.ActionName.Delete:
                        {
                            var id = int.Parse(CommandParser.GetValueParameter(inputCommand, StaticDetails.IdParameter));
                            _controller.Delete(id);

                            break;
                        }
                    default:
                        Console.WriteLine("Введено некорректное действие");
                        break;
                }

                Console.WriteLine("-----------------------");
                Console.WriteLine();
            }
        }
    }
}