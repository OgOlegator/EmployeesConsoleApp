using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesConsoleApp
{
    /// <summary>
    /// Класс для хранения общих статических элементов
    /// </summary>
    public static class StaticDetails
    {
        /// <summary>
        /// Имя папки с файлом с данными сотрудников
        /// </summary>
        public static readonly string FileFolderPath = Path.GetTempPath();


    }
}
