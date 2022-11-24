using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace Laba1
{
    public class Student
    {
        public static string DBFileStudent = "C:\\Users\\Urbanovich\\source\\repos\\Laba1\\Laba1\\student.json";
        public Student() { }
        public Student(string? name, string? lastName, int course, double reating)
        {
            Name = name;
            LastName = lastName;
            Course = course;
            Reating = reating;
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public int Course { get; set; }
        public double Reating { get; set; }
        public bool IsVisit { get; set; }
        public static List<Student> GetStusents()
        {
            List<Student> students = new List<Student>();
            try
            {
                string jsonStudent = File.ReadAllText(DBFileStudent);
                students = JsonConvert.DeserializeObject<List<Student>>(jsonStudent);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return students;
        }
        public void CreateStudent()
        {
            Console.Write("Введите имя студента: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Name = Console.ReadLine();
            Console.ResetColor();

            Console.Write("Введите фамилию студента: ");
            Console.ForegroundColor = ConsoleColor.Green;
            LastName = Console.ReadLine();
            Console.ResetColor();

            Console.Write("Введите номер курса студента: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Course = Convert.ToInt32(Console.ReadLine());
            Console.ResetColor();

            Console.Write("Введите райтинг студента: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Reating = Convert.ToDouble(Console.ReadLine());
            Console.ResetColor();
        }
        public static void PrintStusents()
        {
            var students = GetStusents();
            for (var i = 0; i < students.Count(); i++)
            {
                students[i].Print();
            }
        }
        public static void Save(Student student)
        {
            string jsonStudent = File.ReadAllText(DBFileStudent);
            List<Student> students = JsonConvert.DeserializeObject<List<Student>>(jsonStudent);
            student.Id = 0;
            if (students.Count > 0)
            {
                int maxId = students.Max(x => x.Id);
                student.Id = ++maxId;
            }
            students.Add(student);
            string serialiseStudent = JsonConvert.SerializeObject(students);
            File.WriteAllText(DBFileStudent, serialiseStudent);
        }
        public static void Delete(int Id)
        {
            var students = GetStusents();
            Student studentForDeletion = students.FirstOrDefault(x => x.Id == Id); 
            if(studentForDeletion != null)
            {
                students.Remove(studentForDeletion);
            }
            string serialiseStudent = JsonConvert.SerializeObject(students);
            File.WriteAllText(DBFileStudent, serialiseStudent);
        }
        public void Edit(Student student)
        {
            Name = student.Name;
            LastName = student.LastName;
            Course = student.Course;
            Reating = student.Reating;
        }
        public void GetStatistics()
        {
            Console.WriteLine($"Успеваемость студента: {Reating}");
        }
        public void Print()
        {
            Console.WriteLine($"Имя: {Name} Фамилия: {LastName} Курс:{Course} Райтинг: {Reating}");

        }
    }
}
