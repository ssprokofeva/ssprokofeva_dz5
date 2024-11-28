using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ssprokofeva_dz5
{
    public class Program
    {
        static void Main(string[] args)
        {
            Task2();
            Task3();
            Task4();
            Console.WriteLine("Нажмите что угодно для входа");
            Console.ReadKey();
        }

        private static void Task4()
        {
            throw new NotImplementedException();
        }

        private static void Task2()
        {
            throw new NotImplementedException();

        }
        /// <summary>
        /// про бабушек
        /// </summary>
        /// <returns></returns>
        static void Task3()
        {
            Console.WriteLine("Выполнение задания 3...");
            // Создаем очередь бабушек
            var babushkas = new Queue<Babushka>();

            // Создаем стек больниц
            var hospitals = new Stack<Hospital>();

            // Пример создания нескольких бабушек
            babushkas.Enqueue(new Babushka("Мария", 80, new List<string> { "Грипп", "Артрит" }, new List<string> { "Парацетамол", "Ибупрофен" }));
            babushkas.Enqueue(new Babushka("Анна", 75, new List<string> { "Гипертония", "Диабет" }, new List<string> { "Эналаприл", "Метформин" }));
            babushkas.Enqueue(new Babushka("Ольга", 85, new List<string> { "Остеопороз", "Астма" }, new List<string> { "Кальций", "Сальбутамол" }));
            babushkas.Enqueue(new Babushka("Елена", 70, new List<string> { "Бессонница" }, new List<string> { "Мелатонин" }));
            babushkas.Enqueue(new Babushka("Татьяна", 65, new List<string> { "Простуда" }, new List<string> { "Отхаркивающее средство" }));

            // Пример создания нескольких больниц
            hospitals.Push(new Hospital("Городская больница №1", new List<string> { "Грипп", "Гипертония", "Диабет", "Простуда" }, 100));
            hospitals.Push(new Hospital("Клиника здоровья", new List<string> { "Артрит", "Остеопороз", "Астма", "Бессонница" }, 60));
            hospitals.Push(new Hospital("Медицинский центр", new List<string> { "Гипертония", "Диабет", "Простуда" }, 40));

            // Распределение бабушек по больницам
            while (babushkas.Count > 0)
            {
                var currentBabushka = babushkas.Dequeue();
                bool foundHospital = false;

                foreach (var hospital in hospitals)
                {
                    if (hospital.CanAccept(currentBabushka))
                    {
                        hospital.Accept(currentBabushka);
                        Console.WriteLine($"{currentBabushka.Name} поступила в {hospital.Name}");
                        foundHospital = true;
                        break;
                    }
                }

                if (!foundHospital)
                {
                    Console.WriteLine($"{currentBabushka.Name} осталась на улице плакать.");
                }
            }

            // Выводим информацию обо всех бабушках и больницах
            PrintAllBabushkas(babushkas);
            PrintAllHospitals(hospitals);
        }

        private static void PrintAllBabushkas(Queue<Babushka> babushkas)
        {
            Console.WriteLine("\nСписок всех бабушек:");
            foreach (var babushka in babushkas)
            {
                Console.WriteLine($"Имя: {babushka.Name}, Возраст: {babushka.Age}, Болезни: {string.Join(", ", babushka.Diseases)}, Лекарства: {string.Join(", ", babushka.Medicines)}");
            }
        }

        private static void PrintAllHospitals(Stack<Hospital> hospitals)
        {
            Console.WriteLine("\nСписок всех больниц:");
            foreach (var hospital in hospitals)
            {
                Console.WriteLine($"Название: {hospital.Name}, Болезни: {string.Join(", ", hospital.TreatedDiseases)}, Вместимость: {hospital.Capacity}, Заполненность: {hospital.Occupancy}%");
            }
        }
    }

    public class Babushka
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public List<string> Diseases { get; set; }
        public List<string> Medicines { get; set; }

        public Babushka(string name, int age, List<string> diseases, List<string> medicines)
        {
            Name = name;
            Age = age;
            Diseases = diseases;
            Medicines = medicines;
        }
    }

    public class Hospital
    {
        public string Name { get; set; }
        public List<string> TreatedDiseases { get; set; }
        public int Capacity { get; set; }
        public int Occupancy { get; private set; } = 0;

        public Hospital(string name, List<string> treatedDiseases, int capacity)
        {
            Name = name;
            TreatedDiseases = treatedDiseases;
            Capacity = capacity;
        }

        public bool CanAccept(Babushka babushka)
        {
            if (Occupancy >= Capacity)
            {
                return false;
            }

            double matchingDiseases = babushka.Diseases.Intersect(TreatedDiseases).Count();
            double totalDiseases = babushka.Diseases.Count();

            return matchingDiseases / totalDiseases >= 0.5 || totalDiseases == 0;
        }

        public void Accept(Babushka babushka)
        {
            Occupancy++;
        }

        /// <summary>
        /// Задание 2: Работа со студентами
        /// </summary>
        static void Task2()
        {
            Console.WriteLine("Задание 2");
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

                string choice = Console.ReadLine().ToLower();

                switch (choice)
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
                        Console.WriteLine("Неправильный выбор. Попробуйте снова.");
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
                Console.WriteLine($"Удален студент: {studentToDelete}");
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

        /// <summary>
        /// Задание 4: Работа с графами
        /// </summary>
        static void Task4()
        {
            Console.WriteLine("Задание 4");
            var graph = new Graph(5); // Создаем граф с 5 вершинами
            graph.AddEdge(0, 1);
            graph.AddEdge(0, 2);
            graph.AddEdge(1, 2);
            graph.AddEdge(2, 3);
            graph.AddEdge(3, 4);

            Console.WriteLine("Найти кратчайший путь от вершины 0 до вершины 4:");
            graph.ShortestPath(0, 4);
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

    public class Graph
    {
        private readonly Dictionary<int, List<int>> _adjList; // Список смежности

        public Graph(int verticesCount)
        {
            _adjList = new Dictionary<int, List<int>>();
            for (int i = 0; i < verticesCount; ++i)
            {
                _adjList[i] = new List<int>();
            }
        }

        public void AddEdge(int u, int v)
        {
            _adjList[u].Add(v);
            _adjList[v].Add(u); // Если граф ненаправленный
        }

        public void ShortestPath(int start, int end)
        {
            if (start == end)
            {
                Console.WriteLine("Путь найден: " + start);
                return;
            }

            bool[] visited = new bool[_adjList.Count];
            Queue<int> queue = new Queue<int>();
            Dictionary<int, int> parent = new Dictionary<int, int>();

            queue.Enqueue(start);
            visited[start] = true;
            parent[start] = -1; // Нет родителя у стартовой вершины

            while (queue.Count > 0)
            {
                int currentVertex = queue.Dequeue();

                foreach (var neighbor in _adjList[currentVertex])
                {
                    if (!visited[neighbor])
                    {
                        queue.Enqueue(neighbor);
                        visited[neighbor] = true;
                        parent[neighbor] = currentVertex;

                        if (neighbor == end)
                        { // Кратчайший путь найден
                            PrintShortestPath(parent, start, end);
                            return;
                        }
                    }
                }
            }

            Console.WriteLine("Путь не найден");
        }

        private void PrintShortestPath(Dictionary<int, int> parent, int start, int end)
        {
            Stack<int> path = new Stack<int>();
            int current = end;

            while (current != -1)
            {
                path.Push(current);
                current = parent[current];
            }

            Console.Write("Кратчайший путь: ");
            while (path.Count > 0)
            {
                Console.Write(path.Pop() + " ");
            }
            Console.WriteLine();
        }
    }
}  