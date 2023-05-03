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
        public static readonly string FileFolderPath = Path.GetTempPath();
        public const string FileName = "EmployeeData.txt";
        public static readonly string FilePath = FileFolderPath + FileName;

        //Константы с командами
        public const string ActionGetAll = "getall";
        public const string ActionGetById = "get";
        public const string ActionCreate = "add";
        public const string ActionUpdate = "update";
        public const string ActionDelete = "delete";

        /// <summary>
        /// Команды обрабатываемые приложением
        /// </summary>
        public enum ActionName
        {
            GetAll,
            GetById,
            Create,
            Update,
            Delete,
        }
    }
}
