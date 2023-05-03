using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesConsoleApp.Data.Base
{
    /// <summary>
    /// Реализация набора данных для работы с коллекцией данных из контекста
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class DataSet<TEntity> : IEnumerable<TEntity> where TEntity : class
    {
        /// <summary>
        /// Отслеживание изменений коллекции
        /// </summary>
        private bool _changedStatus;

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
            _entityList.Add(item);

            _changedStatus = true;
        }

        public void Update(TEntity item)
        {

        }

        public bool Remove(TEntity item)
        {
            return true;
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
