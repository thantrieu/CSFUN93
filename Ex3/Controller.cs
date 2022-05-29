using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace L93Exercises3
{
    class Controller : Icontroller
    {
        public bool IsBirthdateValid(string birthdate)
        {
            var pattern = @"^\d{2}/\d{2}/\d{4}$";
            var regex = new Regex(pattern);
            return regex.IsMatch(birthdate);
        }

        public bool IsCourseIdValid(string courseId)
        {
            var pattern = @"^\d{5}$";
            var regex = new Regex(pattern, RegexOptions.IgnoreCase);
            if (regex.IsMatch(courseId))
            {
                var value = int.Parse(courseId);
                return value >= 10000;
            }
            return false;
        }

        public bool IsEmailValid(string email)
        {
            var pattern = @"^[a-z0-9_]+[a-z0-9-_.]*@[a-z0-9]+\.[a-z]{2,4}$";
            var regex = new Regex(pattern, RegexOptions.IgnoreCase);
            return regex.IsMatch(email);
        }

        public bool IsFullNameValid(string fullName)
        {
            var pattern = @"^[a-z]+[a-z ]*$";
            var regex = new Regex(pattern, RegexOptions.IgnoreCase);
            return regex.IsMatch(fullName);
        }

        public bool IsPhoneNumberValid(string phoneNumber)
        {
            var pattern = @"^(03|08|09)\d{8}$";
            var regex = new Regex(pattern);
            return regex.IsMatch(phoneNumber);
        }

        public bool IsStudentIdValid(string studentId)
        {
            var pattern = @"^ST\d{4}$";
            var regex = new Regex(pattern, RegexOptions.IgnoreCase);
            return regex.IsMatch(studentId);
        }

        public bool IsSubjectIdValid(string subjectId)
        {
            var pattern = @"^\d{4}$";
            var regex = new Regex(pattern);
            if (regex.IsMatch(subjectId))
            {
                var value = int.Parse(subjectId);
                return value >= 1000;
            }
            return false;
        }

        public bool IsTranscriptIdValid(string transcriptId)
        {
            var pattern = @"^\d{4}$";
            var regex = new Regex(pattern);
            if (regex.IsMatch(transcriptId))
            {
                var value = int.Parse(transcriptId);
                return value >= 1000;
            }
            return false;
        }

        public List<Course> ReadCoursesFromFile(string fileName)
        {
            List<Course> courseList = new List<Course>();
            FileInfo fileReader = new FileInfo(fileName);
            if (!fileReader.Exists)
            {
                Console.WriteLine("==> Không tồn tại file dữ liệu lớp học. <==");
                return courseList;
            }
            using (var streamReader = fileReader.OpenText())
            {
                var data = streamReader.ReadToEnd();
                try
                {
                    var jObject = JObject.Parse(data);
                    var jArray = jObject["courses"];
                    foreach (var item in jArray)
                    {
                        courseList.Add(item.ToObject<Course>());
                    }
                }
                catch (JsonReaderException e)
                {
                    Console.WriteLine(e);
                }
            }
            return courseList;
        }

        public List<Student> ReadStudentsFromFile(string fileName)
        {
            var students = new List<Student>();
            var fileReader = new FileInfo(fileName);
            if(!fileReader.Exists)
            {
                Console.WriteLine("==> Không tồn tại file dữ liệu sinh viên. <==");
                return students;
            }
            using (var streamReader = fileReader.OpenText())
            {
                var data = streamReader.ReadToEnd();
                try
                {
                    var jObject = JObject.Parse(data);
                    var jArray = jObject["students"];
                    foreach (var item in jArray)
                    {
                        students.Add(item.ToObject<Student>());
                    }
                }
                catch (JsonReaderException e)
                {
                    Console.WriteLine(e);
                }
            }
            return students;
        }

        public List<Subject> ReadSubjectsFromFile(string fileName)
        {
            var subjects = new List<Subject>();
            var fileReader = new FileInfo(fileName);
            if (!fileReader.Exists)
            {
                Console.WriteLine("==> Không tồn tại file dữ liệu môn học. <==");
                return subjects;
            }
            using (var streamReader = fileReader.OpenText())
            {
                var data = streamReader.ReadToEnd();
                try
                {
                    var jObject = JObject.Parse(data);
                    var jArray = jObject["subjects"];
                    foreach (var item in jArray)
                    {
                        subjects.Add(item.ToObject<Subject>());
                    }
                }
                catch (JsonReaderException e)
                {
                    Console.WriteLine(e);
                }
            }
            return subjects;
        }     

        public void WriteCourseDataToFile(List<Course> courses, string fileName)
        {
            var fileWriter = new FileInfo(fileName);
            using (var fileStream = fileWriter.OpenWrite())
            {
                using (var streamWriter = new StreamWriter(fileStream))
                {
                    var root = new { courses };
                    var strJson = JsonConvert.SerializeObject(root, Formatting.Indented);
                    streamWriter.Write(strJson);
                }
            }
        }

        public void WriteStudentDataToFile(List<Student> students, string fileName)
        {
            var fileWriter = new FileInfo(fileName);
            using (var fileStream = fileWriter.OpenWrite())
            {
                using (var streamWriter = new StreamWriter(fileStream))
                {
                    var root = new { students };
                    var strJson = JsonConvert.SerializeObject(root, Formatting.Indented);
                    streamWriter.Write(strJson);
                }
            }
        }

        public void WriteSubjectDataToFile(List<Subject> subjects, string fileName)
        {
            var fileWriter = new FileInfo(fileName);
            using (var fileStream = fileWriter.OpenWrite())
            {
                using (var streamWriter = new StreamWriter(fileStream))
                {
                    var root = new { subjects };
                    var strJson = JsonConvert.SerializeObject(root, Formatting.Indented);
                    streamWriter.Write(strJson);
                }
            }
        }

        public void SetStudentAutoId(List<Student> students)
        {
            int maxId = 1000;
            foreach (var student in students)
            {
                var idNumberPart = student.StudentId.Substring(2);
                var idNumberValue = int.Parse(idNumberPart);
                if (idNumberValue > maxId)
                {
                    maxId = idNumberValue;
                }
            }
            Student.SetAutoId(maxId == 1000 ? maxId : maxId + 1);
        }

        public void SetSubjectAutoId(List<Subject> subjects)
        {
            int maxId = 1000;
            foreach (var subject in subjects)
            {
                if (subject.SubjectId > maxId)
                {
                    maxId = subject.SubjectId;
                }
            }
            Subject.SetAutoId(maxId == 1000 ? maxId : maxId + 1);
        }

        public void SetCourseAutoId(List<Course> courses)
        {
            int maxId = 10000;
            foreach (var course in courses)
            {
                if (course.CourseId > maxId)
                {
                    maxId = course.CourseId;
                }
            }
            Course.SetAutoId(maxId == 10000 ? maxId : maxId + 1);
        }

        public void SetTranscriptAutoId(List<Transcript> transcripts)
        {
            int maxId = 1000;
            foreach (var tran in transcripts)
            {
                if (tran.TranscriptId > maxId)
                {
                    maxId = tran.TranscriptId;
                }
            }
            Transcript.SetAutoId(maxId == 1000 ? maxId : maxId + 1);
        }
    }
}
