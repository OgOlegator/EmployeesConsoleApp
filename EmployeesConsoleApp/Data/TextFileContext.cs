using EmployeesConsoleApp.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesConsoleApp.Data
{
    /// <summary>
    /// Работа с хранилищем данных
    /// </summary>
    public class TextFileContext<TEntity> where TEntity : IDataElement
    {
        /// <summary>
        /// Строка пути до файла
        /// </summary>
        private string _pathToFile;

        /// <summary>
        /// Набор данных
        /// </summary>
        public readonly DataSet<TEntity> DataSet;

        public TextFileContext(string pathToFile)
        {
            SetPathToFile(pathToFile);
            SetDataFromFile();
        }

        /// <summary>
        /// Установка пути до файла
        /// </summary>
        /// <param name="pathToFile"></param>
        private void SetPathToFile(string pathToFile)
        {
            _pathToFile = pathToFile;

            Directory.CreateDirectory(_pathToFile);
        }

        /// <summary>
        /// Чтение данных из файла
        /// </summary>
        private void SetDataFromFile()
        {
            string jsonData = File.ReadAllText(_pathToFile);
            DataSet.FillFromJson(jsonData);
        }

        /// <summary>
        /// Сохранить изменения в файл
        /// </summary>
        /// <exception cref="SaveErrorException"></exception>
        public void SaveChanges()
        {
            string jsonData = JsonConvert.SerializeObject(DataSet);

            try
            {
                File.WriteAllText(_pathToFile, jsonData);
            }
            catch
            {
                throw new SaveErrorException();
            }

            DataSet.ResetChangedStatus();
        }
    }
}
