namespace EmployeesConsoleApp.Exceptions
{
    /// <summary>
    /// Общий класс исключений консольного приложения. Чтобы разделить исключения, которые предпологались при работе приложения и информация
    /// о которых будет сообщаться пользователю, и исключения, которые не ожидались и нарушают логику приложения.
    /// </summary>
    internal class EmployeeAppException : Exception
    {
        public EmployeeAppException(string? message) : base(message)
        {
        }
    }
}
