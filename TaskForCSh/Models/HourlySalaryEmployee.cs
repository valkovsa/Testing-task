using System.Runtime.Serialization;

namespace TaskForCSh.Models
{
    [DataContract]
    internal class HourlySalaryEmployee : Employee
    {
        [DataMember]
        private decimal HourlySalary { get; set; }

        public HourlySalaryEmployee(int id, string name, decimal hourlySalary) : base(id, name)
        {
            HourlySalary = hourlySalary;
        }

        public override decimal CalculateMonthlyAverageSalary()
        {
            return (HourlySalary * 20.8m * 8m);
        }
    }
}