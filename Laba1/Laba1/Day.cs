using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Laba1
{
    public class Day
    {
        public Day(List<Lesson> lessons, List<Student> students)
        {
            this.lessons = lessons;
            this.students = students;
            lessonsForDay = new List<LessonForDay>();
        }
        public string Time { get; set; }
        private List<Student> students { get; set; }
        private List<Lesson> lessons { get; set; }
        public List<LessonForDay> lessonsForDay { get; set; }
        public void CreateDay()
        {
            Console.Write("Введите дату: ");
            Time = Console.ReadLine();
            CreateLessonsForDay();
        }
        public void CreateLessonsForDay()
        {
            var currentLesson = ChooseLessons();
            foreach(var lesson in currentLesson)
            {
                var newLesson = new LessonForDay(lesson, students);
                lessonsForDay.Add(newLesson);
            }
        }
        public List<Lesson> ChooseLessons()
        {
            var chooseLessons = Lesson.GetLessons();
            List<Lesson> newLessonList = new List<Lesson>();
            while (true)
            {
                Console.Write("Выберите предмет для добовления на данный день или х - чтобы выйти: ");
                Console.ForegroundColor = ConsoleColor.Green;
                var Id = Console.ReadLine();
                Console.ResetColor();
                if (Id == "x")
                    break;
                Lesson newLesson = chooseLessons.FirstOrDefault(x => x.Id == Convert.ToInt32(Id));
                newLessonList.Add(newLesson);
            }
            return newLessonList;
        }
        public void PrintLesson() 
        {
            foreach(var lesson in lessonsForDay)
            {
                lesson.lesson.Print();
            }
        }
        public void Print()
        {
            Console.WriteLine(Time);
            foreach(var lesson in lessonsForDay)
            {
                Console.WriteLine(lesson.lesson.Name);
                foreach (var item in lesson.students)
                {
                    Console.WriteLine($"Name: {item.Name} Last name: {item.LastName} is visited: {item.IsVisit}");
                }
            }
        }
        
    }
}
