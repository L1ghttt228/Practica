using System;

namespace Variant2Task2Student
{
    // struct — значимый тип данных
    struct Student
    {
        // Поля структуры
        public string FullName;
        public string GroupNumber;

        // Массив оценок из пяти элементов
        public int[] Grades;

        // Конструктор структуры
        public Student(string fullName, string groupNumber, int[] grades)
        {
            FullName = fullName;
            GroupNumber = groupNumber;
            Grades = grades;
        }

        // Расчёт среднего балла
        public double GetAverageGrade()
        {
            int sum = 0;

            // for — перебор массива по индексам
            for (int i = 0; i < Grades.Length; i++)
            {
                // += — добавление оценки к сумме
                sum += Grades[i];
            }

            // Приведение к double — дробное деление
            return (double)sum / Grades.Length;
        }

        // Проверка оценок: только 4 или 5
        public bool HasOnlyFourAndFive()
        {
            // foreach — перебор всех оценок
            foreach (int grade in Grades)
            {
                // Если оценка не 4 и не 5, возврат false
                if (grade != 4 && grade != 5)
                {
                    return false;
                }
            }

            // Если плохих оценок нет, возврат true
            return true;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Создание массива из десяти студентов
            Student[] students = new Student[]
            {
                new Student("Иванов И.И.",    "П-101", new int[] { 3, 5, 5, 5, 4 }),
                new Student("Петров П.П.",    "П-102", new int[] { 3, 4, 3, 5, 4 }),
                new Student("Сидоров С.С.",   "П-101", new int[] { 4, 4, 4, 4, 4 }),
                new Student("Кузнецова А.В.", "П-103", new int[] { 5, 5, 5, 5, 5 }),
                new Student("Смирнов Д.А.",   "П-102", new int[] { 3, 3, 4, 3, 3 }),
                new Student("Васильева Е.Н.", "П-104", new int[] { 4, 5, 4, 5, 5 }),
                new Student("Морозов К.Л.",   "П-103", new int[] { 2, 3, 3, 4, 3 }),
                new Student("Волков О.С.",   "П-105", new int[] { 5, 4, 5, 4, 5 }),
                new Student("Соколов А.А.",   "П-104", new int[] { 4, 4, 3, 4, 4 }),
                new Student("Лебедев Н.Н.",   "П-105", new int[] { 5, 5, 4, 4, 5 })
            };

            // Сортировка массива по возрастанию среднего балла
            // Лямбда-выражение задаёт правило сравнения двух студентов
            Array.Sort(
                students,
                (a, b) => a.GetAverageGrade().CompareTo(b.GetAverageGrade())
            );

            Console.WriteLine("Студенты после сортировки по возрастанию среднего балла:");

            // Вывод всех студентов
            foreach (Student student in students)
            {
                Console.WriteLine(
                    $"{student.FullName,-20} {student.GroupNumber,-8} " +
                    $"средний балл: {student.GetAverageGrade():F2}");
            }

            Console.WriteLine("\nСтуденты, имеющие оценки только 4 или 5:");

            // Флаг наличия подходящих студентов
            bool found = false;

            // Проверка каждого студента
            foreach (Student student in students)
            {
                if (student.HasOnlyFourAndFive())
                {
                    found = true;
                    Console.WriteLine($"{student.FullName}, группа {student.GroupNumber}");
                }
            }

            // Если подходящих студентов нет
            if (!found)
            {
                Console.WriteLine("Таких студентов нет.");
            }
        }
    }
}