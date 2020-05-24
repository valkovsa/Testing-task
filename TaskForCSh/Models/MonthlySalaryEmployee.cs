using System.Runtime.Serialization;

namespace TaskForCSh.Models
{
    [DataContract]
    internal class MonthlySalaryEmployee : Employee
    {
        [DataMember]
        private decimal FixedAverageSalary { get; set; }

        public MonthlySalaryEmployee(int id, string name, decimal fixedAverageSalary) : base(id, name)
        {
            FixedAverageSalary = fixedAverageSalary;
        }

        public override decimal CalculateMonthlyAverageSalary()
        {
            return FixedAverageSalary;
        }
    }
}