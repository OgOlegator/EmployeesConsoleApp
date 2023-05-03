using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesConsoleApp.Data.Base
{
    /// <summary>
    /// Абстрактный класс для работы с хранилищем данных
    /// </summary>
    public abstract class TextFileContext
    {
        protected string pathToFile;

        protected void SetContext(string path)
        {
            SetPathToFile(path);
            SetDataFromFile();
        }

        public void SaveChanges()
        {

        }

        private void SetPathToFile(string path)
        {
            pathToFile = path;
        }

        private void SetDataFromFile()
        {

        }
    }
}
