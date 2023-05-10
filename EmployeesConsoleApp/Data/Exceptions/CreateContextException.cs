namespace EmployeesConsoleApp.Data.Exceptions
{
    /// <summary>
    /// Обработчик ошибок при создании контекста
    /// </summary>
    public class CreateContextException : Exception
    {
        public CreateContextException(string? message) : base(message)
        {
        }
    }
}
