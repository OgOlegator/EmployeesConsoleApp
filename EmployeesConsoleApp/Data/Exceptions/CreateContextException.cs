namespace EmployeesConsoleApp.Data.Exceptions
{
    /// <summary>
    /// Обработчик ошибок при создании контекста
    /// </summary>
    internal class CreateContextException : Exception
    {
        public CreateContextException(string? message) : base(message)
        {
        }
    }
}
