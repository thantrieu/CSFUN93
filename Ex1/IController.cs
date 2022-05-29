using System.Collections.Generic;

namespace L93Exercises1
{
    interface IController
    {
        List<Employee> ReadEmployeesFromFile(string fileName);
        void WriteEmployeeDataToFile(List<Employee> employees, string fileName);
        void UpdateEmployeeAutoId(List<Employee> employees);
    }
}
