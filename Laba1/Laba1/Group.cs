using Newtonsoft.Json;
using Laba1;
using System.Collections.Generic;

namespace Laba1
{
    public class Group
    {
        private static string DBFileGroup = "C:\\Users\\Urbanovich\\source\\repos\\Laba1\\Laba1\\group.json";
        public Group() 
        {
            Lessons = new List<Lesson>();
            Students = new List<Student>();
        }
        public Group(string name, List<Lesson> lessons)
        {
            Name = name;
            Lessons = lessons;  
            Id = Count++;
        }
        public int Count = 0;
        public int Id { get; set; } 
        public string? Name { get; set; }
        public List<Lesson> Lessons { get; set; }
        public List<Student> Students { get; set; }
        public Magazine Magazine { get; set; }
        public void CreateGroup()
        {
            Console.Write("Введите номер группы: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Name = Console.ReadLine();
            Console.ResetColor();
            List<Lesson> newLessons = new List<Lesson>();
            List<Student> newStudents = new List<Student>();
            while (true)
            {
                Console.Write("1 - Создать предмет 2 - выбрать предмет из списка x - законьчить: ");
                Console.ForegroundColor = ConsoleColor.Green;
                string createOrChoose = Console.ReadLine();
                Console.ResetColor();
                if (createOrChoose == "x")
                    break;
                switch (createOrChoose)
                {
                    case "1":
                        Lesson newlesson = new Lesson();
                        newlesson.CreateLesson();
                        newLessons.Add(newlesson);
                        Lesson.Save(newlesson);
                        break;
                    case "2":
                        var lessons = Lesson.GetLessons();
                        Console.WriteLine("Выберете номер предмета для добовления:");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Id = Convert.ToInt32(Console.ReadLine());
                        Console.ResetColor();
                        Lesson addLesson = lessons.FirstOrDefault(x => x.Id == Id);
                        addLesson.Print();
                        var updateLessons = Lessons;
                        newLessons.Add(addLesson);
                        break;
                }
            }
            while (true)
            {
                Console.Write("1 - Создать студента 2 - выбрать студента из списка x - законьчить: ");
                Console.ForegroundColor = ConsoleColor.Green;
                string сhoose = Console.ReadLine();
                Console.ResetColor();
                if (сhoose == "x")
                    break;
                switch (сhoose)
                {
                    case "1":
                        Student newStudent = new Student();
                        newStudent.CreateStudent();
                        newStudents.Add(newStudent);
                        Student.Save(newStudent);
                        break;
                    case "2":
                        var students = Student.GetStusents();
                        Student.PrintStusents();
                        Console.WriteLine("Выберете номер студента для добовления:");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Id = Convert.ToInt32(Console.ReadLine());
                        Console.ResetColor();
                        Student addStudent = students.FirstOrDefault(x => x.Id == Id);
                        addStudent.Print();
                        newStudents.Add(addStudent);
                        break;
                }
            }
            Lessons = newLessons;
            Students = newStudents;
            Magazine = new Magazine(Students, Lessons);
            Save(this);
        }
        public void Print()
        {
            Console.WriteLine("Id: " + Id + " Name: " + Name);
        }
        public static List<Group> GetGroups()
        {
            List<Group> groups = new List<Group>();
            try
            {
                string json = File.ReadAllText(DBFileGroup);
                groups = JsonConvert.DeserializeObject<List<Group>>(json);
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
           
            for (var i = 0; i < groups.Count(); i++)
            {
                groups[i].Print();
            }
            return groups;
        }
        public static void Save(Group group)
        {
            string jsonGroup = File.ReadAllText(DBFileGroup);
            List<Group> groups = JsonConvert.DeserializeObject<List<Group>>(jsonGroup);
            groups.Add(group);
            group.Id = 0;
            if (groups.Count > 0)
            {
                int maxId = groups.Max(x => x.Id);
                group.Id = ++maxId;
            }
            string serialise = JsonConvert.SerializeObject(groups);
            File.WriteAllText(DBFileGroup, serialise);
        }
    }
}
