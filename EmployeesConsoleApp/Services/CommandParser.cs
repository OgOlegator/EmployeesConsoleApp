using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesConsoleApp.Services
{
    /// <summary>
    /// Парсинг команды введенной в консоли
    /// </summary>
    public class CommandParser
    {
        /// <summary>
        /// Получение имени действия
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static StaticDetails.ActionName GetActionName(string command)
        {
            if (string.IsNullOrEmpty(command))
                throw new ArgumentNullException();

            switch (command.ToLower().TrimStart('-').Split(" ").First())
            {
                case StaticDetails.ActionGetAll:
                    return StaticDetails.ActionName.GetAll;
                case StaticDetails.ActionGetById:
                    return StaticDetails.ActionName.GetById;
                case StaticDetails.ActionCreate:
                    return StaticDetails.ActionName.Create;
                case StaticDetails.ActionUpdate:
                    return StaticDetails.ActionName.Update;
                case StaticDetails.ActionDelete:
                    return StaticDetails.ActionName.Delete;
                default:
                    throw new ArgumentException();
            }
        }
    }
}
