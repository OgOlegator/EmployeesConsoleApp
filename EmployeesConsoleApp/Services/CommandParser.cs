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
        private readonly string[] _commandArgs;

        private string _action;

        private int _id;

        private string _firstName;

        private string _lastName;

        private decimal _salary = -1;   //-1, т.к. зп не может быть отрицательной

        /// <summary>
        /// Действие из консоли. Берется первый элемент массива args, т.к. ожидается, что команда пишется в начале, как вариант,
        /// мог искать команду по символу "-"
        /// </summary>
        public string Action {
            get 
            {
                if (_action == null)
                    _action = _commandArgs.First();

                return _action;
            } 
        }

        /// <summary>
        /// ID
        /// </summary>
        /// <exception cref="EmployeeAppException"></exception>
        public int Id {
            get
            {
                if (_id == 0)
                {
                    try
                    {
                        _id = int.Parse(GetValueParamOrDefault(StaticDetails.IdParameter));
                    }
                    catch
                    {
                        throw new EmployeeAppException($"Не передан или некорректен параметр {nameof(Id)}");
                    }
                }

                return _id;
            }
        }

        public string FirstName {
            get
            {
                if (_firstName == null)
                {
                    _firstName = GetValueParamOrDefault(StaticDetails.FirstNameParameter);
                }

                return _firstName;
            }
        }

        public string LastName {
            get
            {
                if (_lastName == null)
                {
                    _lastName = GetValueParamOrDefault(StaticDetails.LastNameParameter);
                }

                return _lastName;
            }
        }

        /// <summary>
        /// ЗП
        /// </summary>
        /// <exception cref="EmployeeAppException"></exception>
        public decimal Salary {
            get
            {
                if (_salary == -1)
                {
                    var salary = GetValueParamOrDefault(StaticDetails.SalaryParameter);

                    if (string.IsNullOrEmpty(salary))
                        return _salary;

                    try
                    {
                        _salary = decimal.Parse(salary);
                    }
                    catch
                    {
                        throw new EmployeeAppException($"Некорректный параметр - {nameof(Salary)}");
                    }
                }

                return _salary;
            }
        }

        public CommandParser(string[] args)
        {
            _commandArgs = args;
        }

        /// <summary>
        /// Получение значений параметров команды из консоли или null если параметр не найден
        /// </summary>
        /// <param name="nameParameter"></param>
        /// <returns></returns>
        private string GetValueParamOrDefault(string nameParameter)
        {
            var argument = _commandArgs.FirstOrDefault(x => x.StartsWith(nameParameter));

            if (argument == null)
                return null;

            return argument.Substring(nameParameter.Length);
        }
    }
}
