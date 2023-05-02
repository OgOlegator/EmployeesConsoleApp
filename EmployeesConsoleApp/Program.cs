using EmployeesConsoleApp;
using EmployeesConsoleApp.Services;

namespace EmployeeConsoleApp
{
    internal class Program
    {
        /// <summary>
        /// Статус работы приложения
        /// </summary>
        private static bool _appIsWork;

        /// <summary>
        /// Точка входа
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            _appIsWork = true;

            while (_appIsWork)
            {
                Console.WriteLine("Введите команду:");
                var inputCommand = Console.ReadLine();

                StaticDetails.ActionName actionName;

                try
                {
                    actionName = CommandParser.GetActionName(inputCommand);
                }
                catch (Exception)
                {
                    Console.WriteLine("Введено некорректное действие");
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
            }
        }
    }
}