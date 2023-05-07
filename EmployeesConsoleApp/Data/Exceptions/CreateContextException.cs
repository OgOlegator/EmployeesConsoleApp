using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesConsoleApp.Data.Exceptions
{
    /// <summary>
    /// Обработчик ошибок при создании контекста
    /// </summary>
    internal class CreateContextException : Exception
    {
        public CreateContextException(string? message) : base(message)
        {
        }
    }
}
