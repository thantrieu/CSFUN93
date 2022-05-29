using System;
using Newtonsoft.Json;

namespace L93Exercises1
{
    // lớp mô tả thông tin giám đốc
    class Director : Employee
    {

        [JsonProperty("receivedDate")]
        public DateTime ReceivedDate { get; set; } // ngày nhận chức
        [JsonProperty("bonusRate")]
        public float BonusRate { get; set; } // phần trăm tiền thưởng
        [JsonProperty("responsibilityAmount")]
        public long ResponsibilityAmount { get; set; } // tiền trách nhiệm

        public Director() { }

        public Director(string id, string fullName, string email, string phone,
            long salary, float workingDay, long received, float bonus, string role, DateTime startDate) :
            base(id, fullName, email, phone, salary, workingDay, received, role)
        {
            ReceivedDate = startDate;
            Role = role;
            BonusRate = bonus;
        }

        public override long CalculateSalary(long profit = 0)
        {
            var baseSalary = base.CalculateSalary();
            ReceivedSalary = baseSalary + (long)(profit * BonusRate + 0.15 * baseSalary);
            return ReceivedSalary;
        }
    }
}
