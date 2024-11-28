using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ssprokofeva_dz5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Task1();
            Task2();
            Task3();
            Task4();
            Console.WriteLine("нужно нажать для закрытия окна");
            Console.ReadKey();
        }
        /// <summary>
        /// Соствить студента из группы, создать словарь студентов из нашей группы
        /// </summary>
        /// <returns></returns>
        static void Task2()
        {
            Console.WriteLine("задание 2");
            var students = ReadStudentsFromFile("students.txt");

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\nМеню:");
                Console.WriteLine("a. Добавить нового студента");
                Console.WriteLine("b. Удалить студента");
                Console.WriteLine("c. Сортировать студентов по баллам");
                Console.WriteLine("d. Показать всех студентов");
                Console.WriteLine("e. Выход");

                string choice = Console.ReadLine();

                switch (choice.ToLower())
                {
                    case "a":
                        AddNewStudent(students);
                        break;
                    case "b":
                        DeleteStudent(students);
                        break;
                    case "c":
                        SortByScore(students);
                        break;
                    case "d":
                        ShowAllStudents(students);
                        break;
                    case "e":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Неправильный выбор. Попробуйте еще раз.");
                        break;
                }
            }
        }

        private static void AddNewStudent(List<Student> students)
        {
            Console.Write("Введите фамилию студента: ");
            string surname = Console.ReadLine();

            Console.Write("Введите имя студента: ");
            string name = Console.ReadLine();

            Console.Write("Введите год рождения студента: ");
            int year = int.Parse(Console.ReadLine());

            Console.Write("Введите название экзамена: ");
            string exam = Console.ReadLine();

            Console.Write("Введите балл студента: ");
            double score = double.Parse(Console.ReadLine());

            var newStudent = new Student(surname, name, year, exam, score);
            students.Add(newStudent);

            Console.WriteLine($"Добавлен новый студент: {newStudent}");
        }

        private static void DeleteStudent(List<Student> students)
        {
            Console.Write("Введите фамилию студента: ");
            string surname = Console.ReadLine();

            Console.Write("Введите имя студента: ");
            string name = Console.ReadLine();

            var studentToDelete = students.FirstOrDefault(s => s.Surname == surname && s.Name == name);

            if (studentToDelete != null)
            {
                students.Remove(studentToDelete);
                Console.WriteLine($"Удалён студент: {studentToDelete}");
            }
            else
            {
                Console.WriteLine("Студент не найден.");
            }
        }

        private static void SortByScore(List<Student> students)
        {
            var sortedStudents = students.OrderBy(s => s.Score).ToList();

            Console.WriteLine("\nСортированные студенты по баллам:\n");
            foreach (var student in sortedStudents)
            {
                Console.WriteLine(student);
            }
        }

        private static void ShowAllStudents(List<Student> students)
        {
            Console.WriteLine("\nВсе студенты:\n");
            foreach (var student in students)
            {
                Console.WriteLine(student);
            }
        }

        private static List<Student> ReadStudentsFromFile(string fileName)
        {
            var students = new List<Student>();

            try
            {
                using (StreamReader reader = new StreamReader(fileName))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        var parts = line.Split(',');
                        var student = new Student(parts[0].Trim(), parts[1].Trim(), int.Parse(parts[2].Trim()), parts[3].Trim(), double.Parse(parts[4].Trim()));
                        students.Add(student);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при чтении файла: {ex.Message}");
            }

            return students;
        }
    }

    public class Student
    {
        public string Surname { get; set; }
        public string Name { get; set; }
        public int BirthYear { get; set; }
        public string Exam { get; set; }
        public double Score { get; set; }

        public Student(string surname, string name, int birthYear, string exam, double score)
        {
            Surname = surname;
            Name = name;
            BirthYear = birthYear;
            Exam = exam;
            Score = score;
        }

        public override string ToString()
        {
            return $"Студент: {Surname} {Name}, год рождения: {BirthYear}, экзамен: {Exam}, балл: {Score}";
        }
    }
      
}