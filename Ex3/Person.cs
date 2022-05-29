using System;
using Newtonsoft.Json;

namespace L93Exercises3
{
    // lớp mô tả thông tin người
    class Person
    {
        [JsonProperty("fullName")]
        public FullName FullName { get; set; }
        [JsonProperty("address")]
        public string Address { get; set; }
        [JsonProperty("birthDate")]
        public DateTime BirthDate { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }

        public Person() { }
        public Person(string fullName, string address,
            DateTime birthDate, string email, string phoneNumber)
        {
            FullName = new FullName(fullName);
            Address = address;
            BirthDate = birthDate;
            Email = email;
            PhoneNumber = phoneNumber;
        }
    }
}
