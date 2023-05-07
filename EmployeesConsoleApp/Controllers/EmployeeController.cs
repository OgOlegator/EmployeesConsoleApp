using EmployeesConsoleApp.Data;
using EmployeesConsoleApp.Exceptions;
using EmployeesConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesConsoleApp.Controllers
{
    /// <summary>
    /// Контроллер операций с сотрудниками
    /// </summary>
    public class EmployeeController
    {
        /// <summary>
        /// Объект хранилища данных о сотрудниках
        /// </summary>
        private readonly TextFileContext<Employee> _context;

        public EmployeeController(TextFileContext<Employee> employeeContext)
        {
            _context = employeeContext;
        }

        /// <summary>
        /// Получение информации о всех сотрудниках
        /// </summary>
        /// <returns></returns>
        public List<Employee> Get()
        {
            return _context.DataSet.ToList();
        }

        /// <summary>
        /// Получение информации о сотруднике по ИД
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Employee GetById(int id)
        {
            return _context.DataSet.FirstOrDefault(employee => employee.Id == id) ?? new Employee();
        }

        /// <summary>
        /// Добавление нового сотрудника
        /// </summary>
        /// <param name="employee"></param>
        /// <exception cref="Exception"></exception>
        public void Create(Employee employee)
        {
            try
            {
                _context.DataSet.Add(employee);
                _context.SaveChanges();
            }
            catch(SaveErrorException)
            {
                throw new Exception("Сохранение не выполнено");
            }
        }

        /// <summary>
        /// Обновление информации о сотруднике
        /// </summary>
        /// <param name="employee"></param>
        /// <exception cref="Exception"></exception>
        public void Update(Employee employee)
        {
            var changeEmployee = _context.DataSet.FirstOrDefault(x => x.Id == employee.Id);

            if (changeEmployee == null)
                throw new Exception($"Запись с ключом {employee.Id} не найдена");

            changeEmployee.FirstName = employee.FirstName != null ? employee.FirstName : changeEmployee.FirstName;
            changeEmployee.LastName = employee.LastName != null ? employee.LastName : changeEmployee.LastName;
            changeEmployee.SalaryPerHour = employee.SalaryPerHour != -1 ? employee.SalaryPerHour : changeEmployee.SalaryPerHour;

            try
            {
                _context.DataSet.Update(changeEmployee);
                _context.SaveChanges();
            }
            catch (KeyNotFoundException)
            {
                throw new Exception($"Запись с ключом {employee.Id} не найдена");
            }
            catch (UpdateErrorException)
            {
                throw new Exception("Не удалось обновить");
            }
            catch (SaveErrorException)
            {
                throw new Exception("Сохранение не выполнено");
            }
        }

        /// <summary>
        /// Удаление информации о сотруднике
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="Exception"></exception>
        public void Delete(int id)
        {
            var deleteEmployee = _context.DataSet.FirstOrDefault(employee => employee.Id == id);

            if (deleteEmployee == null)
                return;

            try
            {
                var result = _context.DataSet.Remove(deleteEmployee);

                if (result)
                    _context.SaveChanges();
            }
            catch(KeyNotFoundException)
            {
                throw new Exception($"Запись с ключом {id} не найдена");
            }
            catch(SaveErrorException)
            {
                throw new Exception("Сохранение не выполнено");
            }
        }
    }
}
