using Laba1;
using System.Text.Json;
using System;
using Newtonsoft.Json;

internal class Program
{
    private static void Main(string[] args)
    {
        string DBFileStudent = "C:\\Users\\Urbanovich\\source\\repos\\Laba1\\Laba1\\student.json";
        string DBFileGroup = "C:\\Users\\Urbanovich\\source\\repos\\Laba1\\Laba1\\group.json";

        while (true)
        {
            Console.WriteLine("1 - Создать группу");
            Console.WriteLine("2 - Работать с группой");
            Console.Write("Введите номер: ");
            Console.ForegroundColor = ConsoleColor.Green;
            string number = Console.ReadLine();
            Console.ResetColor();
            switch (number)
            {
                case "1":
                    Group newGroup = new Group();
                    newGroup.CreateGroup();
                    break;
                case "2":
                    var groups = Group.GetGroups();
                    Console.Write("Выберите группу для дольнейшей работы:");
                    Console.ForegroundColor = ConsoleColor.Green;
                    int numberOfGroup = Convert.ToInt32(Console.ReadLine());
                    Console.ResetColor();
                    var currentGroup = groups.FirstOrDefault(x => x.Id == Convert.ToInt32(numberOfGroup));
                    Console.WriteLine("1 - добавить день");
                    Console.WriteLine("2 - отметить отсутствующих");
                    Console.WriteLine("3 - Просмотреть журнал");
                    Console.Write("Выберите действие с группой : ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    var numberWorkWithGroup = Console.ReadLine();
                    Console.ResetColor();
                    switch (numberWorkWithGroup)
                    {
                        case "1":
                            currentGroup.Magazine.CreateNewDay();
                            break;
                        case "2":
                            currentGroup.Magazine.CurrentDay.PrintLesson();
                            Console.Write("Выберите предмет: ");
                            Console.ForegroundColor = ConsoleColor.Green;
                            var numberLesson = Convert.ToInt32(Console.ReadLine());
                            Console.ResetColor();
                            var allLesson = currentGroup.Magazine.CurrentDay.lessonsForDay;
                            allLesson.FirstOrDefault(x => x.lesson.Id == numberLesson).MarkVisits();
                            currentGroup.Magazine.Days[currentGroup.Magazine.Days.Count - 1].lessonsForDay = allLesson;
                            currentGroup.Magazine.CurrentDay.lessonsForDay = allLesson;
                            break;
                        case"3":
                            currentGroup.Magazine.PrintMagazine();
                            break;
                    }
                    groups.FirstOrDefault(x => x.Id == currentGroup.Id);
                    string serialiseGroups = JsonConvert.SerializeObject(groups);
                    File.WriteAllText(DBFileGroup, serialiseGroups);
                    break;
            }
        }

        while (false)
        {
            Console.WriteLine("1 - Добавить студента");
            Console.WriteLine("2 - Просмотреть студенов");
            Console.WriteLine("3 - Добавить группу");
            Console.WriteLine("4 - Добавить предмет");
            Console.WriteLine("5 - Удалить студента");
            Console.WriteLine("6 - Изменить данные студента");
            Console.WriteLine("7 - Просмотеть статистику струдента");
            Console.WriteLine("8 - Просмотеть пары студента");
            Console.Write("Введите номер: ");
            string number = Console.ReadLine();
            switch (number)
            {
                case "1":
                    Student student = CreateNewStudent();
                    Student.Save(student);
                    break;
                case "2":
                    Student.PrintStusents();
                    break;
                case "3":
                    Console.Write("Введите номер группы: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    string nameGroup = Console.ReadLine();
                    Console.ResetColor();
                    List<Lesson> lessonsOfGroup = new List<Lesson>();
                    while (true)
                    {
                        Console.Write("Введите прдметы группы: ");
                        var lessons = Lesson.GetLessons();
                        Console.ForegroundColor = ConsoleColor.Green;
                        int numberOfLesson = Convert.ToInt32(Console.ReadLine());
                        lessonsOfGroup.Add(lessons[numberOfLesson - 1]);
                        Console.ResetColor();
                        Console.Write("Введите x - чтобы законьчить: ");
                        Console.ForegroundColor = ConsoleColor.Green;
                        var isEnd = Console.ReadLine();
                        Console.ResetColor();
                        if (isEnd == "x")
                            break;
                    }
                    Group group = new Group(nameGroup, lessonsOfGroup);
                    Group.Save(group);

                    break;
                case "4":
                    Console.Write("Введите название предмета: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    string nameLesson = Console.ReadLine();
                    Console.ResetColor();

                    Console.Write("Введите колличество часов: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    int аmountHours = Convert.ToInt32(Console.ReadLine());
                    Console.ResetColor();

                    Lesson lesson = new Lesson(nameLesson, аmountHours);
                    Lesson.Save(lesson);
                    break;
                case "5":
                    var students = Student.GetStusents();
                    Student.PrintStusents();
                    Console.WriteLine("Выберете номер студента для удаления:");
                    Console.ForegroundColor = ConsoleColor.Green;
                    int numberOfStudentForDelete = Convert.ToInt32(Console.ReadLine());
                    Console.ResetColor();
                    Student.Delete(numberOfStudentForDelete);
                    break;
                case "6":
                    var studentsEdit = Student.GetStusents();
                    Student.PrintStusents();
                    Console.WriteLine("Выберете номер студента для редактирования:");
                    Console.ForegroundColor = ConsoleColor.Green;
                    int Id = Convert.ToInt32(Console.ReadLine());
                    Console.ResetColor();
                    var newStudent = CreateNewStudent();
                    studentsEdit.FirstOrDefault(x => x.Id == Id).Edit(newStudent);
                    string serialiseStudent = JsonConvert.SerializeObject(studentsEdit);
                    File.WriteAllText(DBFileStudent, serialiseStudent);
                    break;
                case "7":
                    var studentsStatistics = Student.GetStusents();
                    Student.PrintStusents();
                    Console.WriteLine("Выберете номер студента для просмотра статистики:");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Id = Convert.ToInt32(Console.ReadLine());
                    Console.ResetColor();
                    var student1 = studentsStatistics.FirstOrDefault(x => x.Id == Id);
                    break;
            }

        }
        static Student CreateNewStudent()
        {
            Console.Write("Введите имя студента: ");
            Console.ForegroundColor = ConsoleColor.Green;
            string name = Console.ReadLine();
            Console.ResetColor();

            Console.Write("Введите фамилию студента: ");
            Console.ForegroundColor = ConsoleColor.Green;
            string lastName = Console.ReadLine();
            Console.ResetColor();

            Console.Write("Введите номер курса студента: ");
            Console.ForegroundColor = ConsoleColor.Green;
            int course = Convert.ToInt32(Console.ReadLine());
            Console.ResetColor();

            Console.Write("Введите райтинг студента: ");
            Console.ForegroundColor = ConsoleColor.Green;
            double reating = Convert.ToDouble(Console.ReadLine());
            Console.ResetColor();

            Student student = new Student(name, lastName, course, reating);
            return student;
        }
        static Student FindStudent(string str)
        {
            var studentsStatistics = Student.GetStusents();
            Student.PrintStusents();
            Console.WriteLine("Выберете номер студента для просмотра статистики:");
            Console.ForegroundColor = ConsoleColor.Green;
            var Id = Convert.ToInt32(Console.ReadLine());
            Console.ResetColor();
            return studentsStatistics.FirstOrDefault(x => x.Id == Id);
        }
    }
}