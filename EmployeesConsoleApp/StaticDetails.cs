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
        //Имя и путь к файлу
        public static readonly string FileFolderPath = Path.GetTempPath() + "EmployeeDb\\";
        public const string FileName = "EmployeeData.txt";

        //Константы с командами
        public const string ActionGetAll = "-getall";
        public const string ActionGetById = "-get";
        public const string ActionCreate = "-add";
        public const string ActionUpdate = "-update";
        public const string ActionDelete = "-delete";

        //Константы с именами параметров команд вводимых в консоли
        public const string IdParameter = "Id:";
        public const string FirstNameParameter = "FirstName:";
        public const string LastNameParameter = "LastName:";
        public const string SalaryParameter = "Salary:";
    }
}
