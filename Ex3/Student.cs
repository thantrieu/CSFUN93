﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace L93Exercises3
{
    // lớp mô tả thông tin sinh viên
    class Student : Person
    {
        [JsonIgnore]
        private static int autoId = 1000;
        [JsonProperty("studentId")]
        public string StudentId { get; set; }
        [JsonProperty("major")]
        public string Major { get; set; }

        public Student() { }

        public Student(string studentId)
        {
            StudentId = studentId;
        }

        public Student(string fullName, string address,
             DateTime birthDate, string email, string phoneNumber,
            string studentId, string major) :
            base(fullName, address, birthDate, email, phoneNumber)
        {
            FullName = new FullName(fullName);
            Major = major;
            Address = address;
            StudentId = studentId == null ? $"ST{autoId++}" : studentId;
            Major = major;
        }

        internal static void SetAutoId(int value)
        {
            autoId = value;
        }

        public override bool Equals(object obj)
        {
            return obj is Student student &&
                   StudentId == student.StudentId;
        }

        public override int GetHashCode()
        {
            return 610864741 + EqualityComparer<string>.Default.GetHashCode(StudentId);
        }
    }
}
