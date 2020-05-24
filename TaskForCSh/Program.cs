using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TaskForCSh.Init;
using TaskForCSh.Models;

namespace TaskForCSh
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                List<Employee> collection = new Initializer().GetEmployees();

                #region Task A

                Console.WriteLine("a) Упорядоченная последовательность:");
                IOrderedEnumerable<Employee> orderedColl = collection.OrderByDescending(emp => emp.CalculateMonthlyAverageSalary()).ThenBy(emp => emp.Name);
                foreach (var emp in orderedColl)
                {
                    Console.WriteLine($"ID: {emp.ID}, имя: {emp.Name}, зарплата: {emp.CalculateMonthlyAverageSalary()}");
                }
                Console.WriteLine(new string('-', 60));

                #endregion Task A

                #region Task B

                Console.WriteLine("b) Первые 5 имен первой последовательности:");
                foreach (var emp in orderedColl.Take(5))
                {
                    Console.WriteLine($"Имя: {emp.Name}");
                }
                Console.WriteLine(new string('-', 60));

                #endregion Task B

                #region Task C

                Console.WriteLine("c) Последние 3 ID из первой последовательности:");
                foreach (var emp in orderedColl.Skip(Math.Max(0, collection.Count - 3)))
                {
                    Console.WriteLine($"ID: {emp.ID}");
                }
                Console.WriteLine(new string('-', 60));

                #endregion Task C

                #region Task D

                Console.WriteLine("d) Запись коллекции");
                JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
                string filePath = ".\\Employees.txt";

                string json = JsonConvert.SerializeObject(orderedColl.ToList(), Formatting.Indented, settings);
                File.WriteAllText(filePath, json);
                Console.WriteLine($"Файл \"{filePath}\" записан. Для чтения данных из файла нажмите клавишу.");

                Console.ReadKey();

                if (IsFileValid(filePath))
                {
                    string readFile = File.ReadAllText(filePath);
                    var readColl = JsonConvert.DeserializeObject<List<Employee>>(readFile, settings);
                    Console.WriteLine("Коллекция сотрудников из файла:");
                    foreach (var emp in readColl)
                    {
                        Console.WriteLine($"ID: {emp.ID}, имя: {emp.Name}, зарплата: {emp.CalculateMonthlyAverageSalary()}");
                    }
                    Console.WriteLine(new string('-', 60));
                }
                else
                {
                    Console.WriteLine("Ошибка открытия файла.");
                }

                #endregion Task D
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ReadKey();
            }
        }

        #region Task E

        private static bool IsFileValid(string filePath)
        {
            bool isFileExist = File.Exists(filePath);
            bool isFileExtRight = Path.GetExtension(filePath) == ".txt";

            return isFileExist && isFileExtRight;
        }

        #endregion Task E
    }
}