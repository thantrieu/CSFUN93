using System.Collections.Generic;
using Newtonsoft.Json;

namespace L93Exercises3
{
    // lớp mô tả thông tin lớp học phần
    class Course
    {
        [JsonIgnore]
        private static int autoId = 10000;
        [JsonProperty("courseId")]
        public int CourseId { get; set; }
        [JsonProperty("subject")]
        public Subject Subject { get; set; }
        [JsonProperty("teacher")]
        public string Teacher { get; set; } // người dạy
        [JsonProperty("numOfStudent")]
        public int NumberOfStudent { get; set; }
        [JsonProperty("transcripts")]
        public List<Transcript> Transcripts { get; set; }

        public Course() { }

        public Course(int id)
        {
            CourseId = id == 0 ? autoId++ : id;
        }

        public Course(int id, Subject subject, string teacher, int numberOfStudent) : this(id)
        {
            Teacher = teacher;
            Subject = subject;
            NumberOfStudent = numberOfStudent;
            Transcripts = new List<Transcript>();
        }

        internal static void SetAutoId(int v)
        {
            autoId = v;
        }
    }
}
