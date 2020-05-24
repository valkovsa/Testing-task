using System.Runtime.Serialization;

namespace TaskForCSh.Models
{
    [DataContract]
    public abstract class Employee
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string Name { get; set; }

        public Employee(int id, string name)
        {
            ID = id;
            Name = name;
        }

        public abstract decimal CalculateMonthlyAverageSalary();
    }
}