using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesConsoleApp.Data
{
    /// <summary>
    /// Абстрактный класс для работы с хранилищем данных
    /// </summary>
    public class TextFileContext<TEntity> where TEntity : IDataElement
    {
        /// <summary>
        /// Строка пути до файла
        /// </summary>
        private string _pathToFile;

        /// <summary>
        /// Объект для работы с набором данных
        /// </summary>
        public readonly DataSet<TEntity> _dataSet;

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
            _dataSet.FillFromJson(jsonData);
        }

        /// <summary>
        /// Сохранить изменения в файл
        /// </summary>
        public void SaveChanges()
        {
            string jsonData = JsonConvert.SerializeObject(_dataSet);

            File.WriteAllText(_pathToFile, jsonData);

            _dataSet.ResetChangedStatus();
        }
    }
}
