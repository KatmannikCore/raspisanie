using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Laba1
{
    public class Lesson
    {
        private static string DBFileLesson = "C:\\Users\\Urbanovich\\source\\repos\\Laba1\\Laba1\\lesson.json";
        public Lesson() { }
        public Lesson(string? name, int amountHours)
        {
            Name = name;
            AmountHours = amountHours;
        }
        public int Id { get; set; }
        public string? Name { get; set; }
        public int AmountHours { get; set; }

        public void Print()
        {
            Console.WriteLine("Id: " + Id + " Name: " + Name + " AmountHours: " + AmountHours);
        }
        public void CreateLesson()
        {
            Console.Write("Введите название предмета: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Name = Console.ReadLine();
            Console.ResetColor();

            Console.Write("Введите колличество часов: ");
            Console.ForegroundColor = ConsoleColor.Green;
            AmountHours = Convert.ToInt32(Console.ReadLine());
            Console.ResetColor();
        }
        public static List<Lesson> GetLessons()
        {
            string json = File.ReadAllText(DBFileLesson);
            List<Lesson> lessons = JsonConvert.DeserializeObject<List<Lesson>>(json);
            for (var i = 0; i < lessons.Count(); i++)
            {
                lessons[i].Print();
            }
            return lessons;
        }
        public static void Save(Lesson lesson)
        {
            string jsonLesson = File.ReadAllText(DBFileLesson);
            List<Lesson> lessons = JsonConvert.DeserializeObject<List<Lesson>>(jsonLesson);
            lesson.Id = 0;
            if (lessons.Count > 0) 
            {
                int maxId = lessons.Max(x => x.Id);
                lesson.Id = ++maxId;
            }
            lessons.Add(lesson);
            string serialiseLessons = JsonConvert.SerializeObject(lessons);
            File.WriteAllText(DBFileLesson, serialiseLessons);
            
        }
    }
}
