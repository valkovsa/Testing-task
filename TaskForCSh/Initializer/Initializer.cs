using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TaskForCSh.Models;

namespace TaskForCSh.Init
{
    internal class Initializer
    {
        public List<Employee> GetEmployees()
        {
            var result = Enumerable.Empty<Employee>() as List<Employee>;

            string txt = Properties.Resources.Names;

            string template = @"[A-ЯЁ][а-яё]+\s[A-ЯЁ][а-яё]+\s[A-ЯЁ][а-яё]+";
            Regex nameReg = new Regex(template, RegexOptions.Multiline);
            MatchCollection nameMatch = nameReg.Matches(txt);
            if (nameMatch.Count > 0)
            {
                result = new List<Employee>(nameMatch.Count);
                int id = 1;
                Random rand = new Random();

                foreach (var empName in nameMatch)
                {
                    if (id < nameMatch.Count / 2)
                    {
                        result.Add(new MonthlySalaryEmployee(id, empName.ToString(), rand.Next(1500, 3000)));
                    }
                    else
                    {
                        result.Add(new HourlySalaryEmployee(id, empName.ToString(), rand.Next(10, 20)));
                    }

                    ++id;
                }
            }
            else
            {
                throw new Exception("Not possible to initialize employees' base. The file doesn't contains employees' names");
            }
            return result;
        }
    }
}