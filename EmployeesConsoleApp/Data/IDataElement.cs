using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesConsoleApp.Data
{
    /// <summary>
    /// Интерфейс для логического объединения сущностей класса DataSet<> для работы с обобщенными типами
    /// </summary>
    public interface IDataElement
    {

        int Id { get; set; }

    }
}
