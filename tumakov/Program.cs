using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace tumakov
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Task61(args);
            Task63();
            Console.WriteLine("Введите что-нибудь для выхода");
            Console.ReadKey();
        }

        private static void Task61(string[] args)
        {
            Console.WriteLine("Упражнение 6.1");

            if (args.Length == 0)
            {
                Console.WriteLine("Не указано имя файла. Пример использования: VowelConsonantCounter.exe input.txt");
                return;
            }

            string fileName = args[0];

            if (!File.Exists(fileName))
            {
                Console.WriteLine($"Файл '{fileName}' не существует.");
                return;
            }

            List<char> characters = new List<char>();

            try
            {
                characters = GetCharactersFromFile(fileName);
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Ошибка при чтении файла: {ex.Message}");
                return;
            }

            int vowelCount, consonantCount;
            CalculateVowelConsonantCounts(characters, out vowelCount, out consonantCount);

            Console.WriteLine($"Гласных: {vowelCount}, Согласных: {consonantCount}");
        }

        static List<char> GetCharactersFromFile(string fileName)
        {
            List<char> characters = new List<char>();

            using (StreamReader reader = new StreamReader(fileName))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    foreach (char c in line)
                    {
                        characters.Add(c);
                    }
                }
            }

            return characters;
        }

        static void CalculateVowelConsonantCounts(List<char> characters, out int vowels, out int consonants)
        {
            vowels = 0;
            consonants = 0;

            Regex vowelRegex = new Regex("[аеиоуыэюяАЕИОУЫЭЮЯ]", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
            Regex consonantRegex = new Regex("[бвгджзйклмнпрстфхцчшщБВГДЖЗЙКЛМНПРСТФХЦЧШЩ]", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);

            foreach (char c in characters)
            {
                if (char.IsLetter(c))
                {
                    if (vowelRegex.IsMatch(c.ToString()))
                    {
                        vowels++;
                    }
                    else if (consonantRegex.IsMatch(c.ToString()))
                    {
                        consonants++;
                    }
                }
            }
        }

        

        private static void Task63()
        {
            Console.WriteLine("Упражнение 6.3");

           var temperatures = GenerateTemperatures();

           var averageTemperatures = ComputeAverageTemperatures(temperatures);

           var sortedMonths = SortMonthsByAverageTemperature(averageTemperatures);

           PrintResults(sortedMonths);
        }

        static Dictionary<string, double[]> GenerateTemperatures()
        {
            Random random = new Random();
            var months = new[]
            {
                "Январь", "Февраль", "Март",
                "Апрель", "Май", "Июнь",
                "Июль", "Август", "Сентябрь",
                "Октябрь", "Ноябрь", "Декабрь"
            };

            var temperatures = new Dictionary<string, double[]>();

            foreach (string month in months)
            {
                var dailyTemps = new double[30];
                for (int day = 0; day < 30; day++)
                {
                    dailyTemps[day] = random.Next(-20, 40); 
                }
                temperatures[month] = dailyTemps;
            }

            return temperatures;
        }

        static Dictionary<string, double> ComputeAverageTemperatures(Dictionary<string, double[]> temperatures)
        {
            var averages = new Dictionary<string, double>();

            foreach (var kvp in temperatures)
            {
                string month = kvp.Key;
                double[] temps = kvp.Value;
                double averageTemp = temps.Average();
                averages[month] = Math.Round(averageTemp, 2);
            }

            return averages;
        }

        static SortedDictionary<double, string> SortMonthsByAverageTemperature(Dictionary<string, double> averageTemperatures)
        {
            var sortedMonths = new SortedDictionary<double, string>();

            foreach (var kvp in averageTemperatures)
            {
                sortedMonths[kvp.Value] = kvp.Key;
            }

            return sortedMonths;
        }

        static void PrintResults(SortedDictionary<double, string> sortedMonths)
        {
            Console.WriteLine("Средняя температура по месяцам (от самой низкой к высокой):");

            foreach (var kvp in sortedMonths)
            {
                Console.WriteLine($"{kvp.Value}: {kvp.Key}°C");
            }
        }
    }
}






