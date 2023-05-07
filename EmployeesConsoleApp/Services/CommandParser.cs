using EmployeesConsoleApp.Exceptions;
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
        /// Получение имени действия из команды из консоли
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        /// <exception cref="EmployeeAppException"></exception>
        public static StaticDetails.ActionName GetActionName(string command)
        {
            //Удаление лишних пробелов для корректной проверки
            var commandForCheck = command.Trim();

            if(commandForCheck.StartsWith(StaticDetails.ActionGetAll))
                return StaticDetails.ActionName.GetAll;
            
            else if(commandForCheck.StartsWith(StaticDetails.ActionGetById))
                return StaticDetails.ActionName.GetById;
            
            else if(commandForCheck.StartsWith(StaticDetails.ActionCreate))
                return StaticDetails.ActionName.Create;

            else if(commandForCheck.StartsWith(StaticDetails.ActionDelete))
                return StaticDetails.ActionName.Delete;

            else if(commandForCheck.StartsWith(StaticDetails.ActionUpdate))
                return StaticDetails.ActionName.Update;

            else
                throw new EmployeeAppException("Введена некорректная команда");
        }

        /// <summary>
        /// Получение значений параметров команды из консоли или null если параметр не найден
        /// </summary>
        /// <param name="command"></param>
        /// <param name="nameParameter"></param>
        /// <returns></returns>
        public static string GetValueParamOrDefault(string command, string nameParameter)
        {
            if(command == null)
                return null;

            //Получение индекса с которого начинается имя параметра в строке
            var valueParameterIdx = command.Trim().IndexOf(nameParameter);

            if (valueParameterIdx == -1)
                return null;

            //Начиная с valueParameterIdx разбиваем строку по пробелам и берем первую подстроку
            return command.Substring(valueParameterIdx + nameParameter.Length).Split(" ").First();
        }
    }
}
