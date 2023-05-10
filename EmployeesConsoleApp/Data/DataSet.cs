using EmployeesConsoleApp.Data.Exceptions;
using Newtonsoft.Json;
using System.Collections;

namespace EmployeesConsoleApp.Data
{
    /// <summary>
    /// Реализация набора данных для работы с коллекцией данных из контекста.
    /// Сущность используемая в классе (TEntity) должна реализовывать интерфейс IDataElement
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class DataSet<TEntity> : IEnumerable<TEntity> where TEntity : IDataElement
    {
        /// <summary>
        /// Отслеживание изменений коллекции
        /// </summary>
        public bool IsChanged { get; private set; } = false;

        /// <summary>
        /// Коллекция данных
        /// </summary>
        private List<TEntity> _entityList = new List<TEntity>();

        /// <summary>
        /// Добавить новый элемент коллекции
        /// </summary>
        /// <param name="item"></param>
        public virtual void Add(TEntity item)
        {
            //Устанавливаем ИД по максимальному значению ИД всего набора данных + 1.
            //Предполагается, что ИД последней записи имеет максимальный ИД. 
            item.Id = (_entityList?.LastOrDefault()?.Id ?? 0) + 1;

            _entityList.Add(item);

            IsChanged = true;
        }

        /// <summary>
        /// Удаление элемента набора
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public virtual bool Remove(TEntity item)
        {
            var removedItem = _entityList.FirstOrDefault(entity => entity.Id == item.Id);

            if (removedItem == null)
                return false;

            IsChanged = true;

            return _entityList.Remove(removedItem);
        }

        /// <summary>
        /// Сброс статуса изменений
        /// </summary>
        public void ResetChangedStatus()
        {
            IsChanged = false;
        }

        /// <summary>
        /// Заполнение набора данных
        /// </summary>
        /// <param name="jsonData"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public virtual void FillFromJson(string jsonData)
        {
            if (jsonData == null)
                throw new ArgumentNullException();

            var deserializeData = JsonConvert.DeserializeObject<List<TEntity>>(jsonData);

            if(deserializeData != null)
                _entityList = deserializeData;
        }

        // Реализация методов интерфейса IEnumerable
        public IEnumerator<TEntity> GetEnumerator()
        {
            return _entityList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
