using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba1
{
    public class LessonForDay
    {
        public LessonForDay(Lesson lesson, List<Student> students)
        {
            this.lesson = lesson;
            this.students = students;   
        }
        public int Id { get; set; }
        public Lesson lesson = new Lesson();
        public List<Student> students = new List<Student>();
        public void MarkVisits()
        {
            for(var i = 0; i < students.Count; i++)
            {
                students[i].Print();
                Console.Write("Присудствует 1 - Да 0 - нет: ");
                int chooseVisit = Convert.ToInt32(Console.ReadLine());
                students[i].IsVisit = chooseVisit == 1 ? true : false;
            }
        }
        public void Print()
        {
            lesson.Print();
            foreach(var student in students)
            {
                student.Print();
            }
        }
    }
}
