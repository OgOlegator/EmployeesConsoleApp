using EmployeesConsoleApp.Data;

namespace EmployeesConsoleApp.Models
{
    /// <summary>
    /// Модель сотрудник
    /// </summary>
    public class Employee : IDataElement
    {
        public int Id { get; set;  }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public decimal SalaryPerHour { get; set; }

        /// <summary>
        /// Для корректного вывода результата в консоль
        /// </summary>
        /// <returns></returns>
        public override string ToString()
            => $"Id = {Id}, FirstName = {FirstName}, LastName = {LastName}, SalaryPerHour = {SalaryPerHour}";

        /// <summary>
        /// Перекрытие метода Equals. Реализовано сравнение по значению. Используется в юнит-тестах
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool Equals(Employee obj)
            => Id == obj.Id
            && FirstName == obj.FirstName
            && LastName == obj.LastName
            && SalaryPerHour == obj.SalaryPerHour;
    }
}
