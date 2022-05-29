using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace L93Exercises1
{
    class Controller : IController
    {
        public List<Employee> ReadEmployeesFromFile(string fileName)
        {
            FileInfo file = new FileInfo(fileName);
            List<Employee> employees = new List<Employee>();
            try
            {
                using (StreamReader sr = file.OpenText())
                {
                    var data = sr.ReadToEnd();
                    try
                    {
                        var jObject = JObject.Parse(data);
                        var jArray = jObject["employees"];
                        foreach (var item in jArray)
                        {
                            var obj = item as JObject;
                            if(obj.ContainsKey("receivedDate"))
                            {
                                employees.Add(item.ToObject<Director>());
                            } else if(obj.ContainsKey("bonusRate"))
                            {
                                employees.Add(item.ToObject<Manager>());
                            } else
                            {
                                employees.Add(item.ToObject<Employee>());
                            }
                        }
                    }
                    catch (JsonReaderException e)
                    {
                        Console.WriteLine(e);
                    }
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("File cần đọc không tồn tại.");
                Console.WriteLine(e);
            }
            return employees;
        }

        public void UpdateEmployeeAutoId(List<Employee> employees)
        {
            Employee.SetAutoId(FindMaxId() + 1);
            int FindMaxId()
            {
                int maxId = 0;
                foreach (Employee employee in employees)
                {
                    var idNumber = int.Parse(employee.EmpId.Substring(3));
                    if (idNumber > maxId)
                    {
                        maxId = idNumber;
                    }
                }
                return maxId;
            }
        }

        public void WriteEmployeeDataToFile(List<Employee> employees, string fileName)
        {
            FileInfo fileWriter = new FileInfo(fileName);
            fileWriter.Delete();
            using(StreamWriter writer = new StreamWriter(fileWriter.OpenWrite()))
            {
                var root = new
                {
                    employees
                };
                var strJson = JsonConvert.SerializeObject(root, Formatting.Indented);
                writer.Write(strJson);
            }
        }
    }
}
