using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba1
{
    public class Magazine
    {
        public Magazine (List<Student> students, List<Lesson> lessons)
        {
            Students = students;
            Lessons = lessons;
            Days = new List<Day> ();
        }
        public List<Student> Students { get; set; }
        public List<Lesson> Lessons { get; set; }
        public List<Day> Days { get; set; }
        public Day CurrentDay { get; set; }
        public void CreateNewDay()
        {
            CurrentDay = new Day(Lessons, Students);
            CurrentDay.CreateDay();
            Days.Add(CurrentDay);
        }
        public void PrintMagazine()
        {
            foreach (var day in Days)
            {
                day.Print();
            }
        }
    }
}
