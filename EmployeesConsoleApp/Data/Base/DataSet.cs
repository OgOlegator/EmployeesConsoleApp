using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesConsoleApp.Data.Base
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
        private bool _changedStatus = false;

        /// <summary>
        /// Коллекция данных
        /// </summary>
        private List<TEntity> _entityList;

        /// <summary>
        /// Добавить новый элемент коллекции
        /// </summary>
        /// <param name="item"></param>
        public void Add(TEntity item)
        {
            //Устанавливаем ИД по максимальному значению ИД всего набора данных + 1.
            //Предполагается, что ИД последней записи имеет максимальный ИД. 
            item.Id = (_entityList.LastOrDefault()?.Id ?? 0) + 1;

            _entityList.Add(item);

            _changedStatus = true;
        }

        /// <summary>
        /// Обновление элемента набора
        /// </summary>
        /// <param name="item"></param>
        /// <exception cref="KeyNotFoundException"></exception>
        /// <exception cref="Exception"></exception>
        public void Update(TEntity item)
        {
            var updateItem = _entityList.FirstOrDefault(entity => entity.Id == item.Id);

            if (updateItem == null)
                throw new KeyNotFoundException($"Запись с ключом {item.Id} не найдена");

            _changedStatus = true;

            if (_entityList.Remove(updateItem))
                _entityList.Add(item);
            else
                throw new Exception("Обновление не удалось");
        }

        /// <summary>
        /// Удаление элемента набора
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public bool Remove(TEntity item)
        {
            var removedItem = _entityList.FirstOrDefault(entity => entity.Id == item.Id);

            if (removedItem == null)
                throw new KeyNotFoundException($"Запись с ключом {item.Id} не найдена");

            _changedStatus = true;

            return _entityList.Remove(removedItem);
        }

        /// <summary>
        /// Сброс статуса изменений
        /// </summary>
        public void ResetChangedStatus()
        {
            _changedStatus = false;
        }

        /// <summary>
        /// Заполнение набора данных
        /// </summary>
        /// <param name="jsonData"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void FillFromJson(string jsonData)
        {
            if (jsonData == null)
                throw new ArgumentNullException();

            _entityList = JsonConvert.DeserializeObject<List<TEntity>>(jsonData);
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
