using EmployeesConsoleApp;
using EmployeesConsoleApp.Controllers;
using EmployeesConsoleApp.Data;
using EmployeesConsoleApp.Services;

namespace EmployeeConsoleApp
{
    internal class Program
    {
        /// <summary>
        /// Статус работы приложения
        /// </summary>
        private static bool _appIsWork;
        private static ApplicationContext _context;
        public static EmployeeController _controller;

        /// <summary>
        /// Точка входа
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            _context = new ApplicationContext();

            _controller = new EmployeeController(_context);

            _appIsWork = true;

            while (_appIsWork)
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


                            break;
                        }
                    case StaticDetails.ActionName.GetById:
                        {


                            break;
                        }
                    case StaticDetails.ActionName.Create:
                        {


                            break;
                        }
                    case StaticDetails.ActionName.Update:
                        {


                            break;
                        }
                    case StaticDetails.ActionName.Delete:
                        {


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