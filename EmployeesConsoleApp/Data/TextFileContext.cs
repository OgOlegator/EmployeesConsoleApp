using EmployeesConsoleApp.Data.Exceptions;
using Newtonsoft.Json;

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
        /// Полный путь + имя файла
        /// </summary>
        private string _fullFilePath;

        /// <summary>
        /// Набор данных
        /// </summary>
        public DataSet<TEntity> DataSet { get; set; } = new DataSet<TEntity>();

        /// <summary>
        /// Установка контекста
        /// </summary>
        /// <param name="pathToFile"></param>
        /// <param name="fileName"></param>
        /// <exception cref="CreateContextException"></exception>
        /// <returns></returns>
        public TextFileContext<TEntity> SetContext(string pathToFile, string fileName)
        {
            try
            {
                SetPathToFile(pathToFile, fileName);
                SetDataFromFile();
            }
            catch (CreateContextException ex)
            {
                throw ex;
            }

            return this;
        }

        /// <summary>
        /// Установка пути до файла и файла 
        /// </summary>
        /// <param name="pathToFile"></param>
        /// <param name="fileName"></param>
        /// <exception cref="CreateContextException"></exception>
        private void SetPathToFile(string pathToFile, string fileName)
        {
            _pathToFile = pathToFile;
            _fullFilePath = pathToFile + fileName;

            try
            {
                //Создаем путь к файлу, если его нет
                Directory.CreateDirectory(_pathToFile);

                //Создаем файл если его нет
                if (!File.Exists(_fullFilePath))
                    File.Create(_fullFilePath).Close();
            }
            catch (UnauthorizedAccessException)
            {
                throw new CreateContextException("Нет прав на создание/получение файла с данными. " +
                    "Попробуйте добавить в исключение запускающую программу в антивирусе на вашем компьютере " +
                    "или запустите программу от имени администратора.");
            }
            catch (ArgumentNullException)
            {
                throw new CreateContextException("Путь к файлу с данными = null");
            }
            catch (ArgumentException)
            {
                throw new CreateContextException("Некорректный путь к хранилищу данных");
            }
            catch
            {
                throw new CreateContextException("Ошибка при создании/получении контекста");
            }
        }

        /// <summary>
        /// Чтение данных из файла
        /// </summary>
        /// <exception cref="CreateContextException"></exception>
        private void SetDataFromFile()
        {
            try
            {
                var jsonData = File.ReadAllText(_fullFilePath);
                DataSet.FillFromJson(jsonData);
            }
            catch (ArgumentNullException)
            {
                throw new CreateContextException("Не удалось получить данные из файла");
            }
        }

        /// <summary>
        /// Сохранить изменения в файл
        /// </summary>
        /// <exception cref="SaveErrorException"></exception>
        public virtual void SaveChanges()
        {
            var jsonData = JsonConvert.SerializeObject(DataSet);

            try
            {
                File.WriteAllText(_fullFilePath, jsonData);
            }
            catch
            {
                throw new SaveErrorException();
            }

            DataSet.ResetChangedStatus();
        }
    }
}
