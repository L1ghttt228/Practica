using System;

namespace Task3Student
{
    // интерфейс IStudent задаёт: средний балл и информация о курсе
    interface IStudent
    {
        double GetAverageGrade(); // объявление метода вычисления среднего балла
        string GetCourseInfo(); // объявление метода получения информации о курсе
    }

    // класс студента первого курса реализует интерфейс IStudent
    class FirstCourseStudent : IStudent
    {
        private string name;
        private int[] grades;

        // конструктор принимает имя и массив оценок и записывает их в поля объекта
        public FirstCourseStudent(string name, int[] grades)
        {
            this.name = name;
            this.grades = grades;
        }

        // метод GetAverageGrade вычисляет средний балл студента
        public double GetAverageGrade()
        {
            int sum = 0;

            // цикл foreach: переменная grade по очереди принимает каждую оценку из массива grades
            foreach (int grade in grades)
            {
                sum += grade; // оператор += прибавляет текущую оценку к накопленной сумме
            }

            return (double)sum / grades.Length;  // дробное деление суммы на количество оценок
        }

        // метод GetCourseInfo возвращает строку с информацией о курсе студента
        public string GetCourseInfo()
        {
            return $"{name} учится на 1 курсе бакалавриата"; 
        }
    }

    // класс студента третьего курса
    class ThirdCourseStudent : IStudent
    {
        private string name;
        private int[] grades;

        // конструктор принимает имя и массив оценок и записывает их в поля объекта
        public ThirdCourseStudent(string name, int[] grades)
        {
            this.name = name; 
            this.grades = grades;
        }

        // метод GetAverageGrade вычисляет средний балл студента
        public double GetAverageGrade()
        {
            int sum = 0;

            foreach (int grade in grades)
            {
                sum += grade;
            }

            return (double)sum / grades.Length;
        }

        // метод GetCourseInfo возвращает строку с информацией о курсе студента
        public string GetCourseInfo()
        {
            return $"{name} учится на 3 курсе бакалавриата";
        }
    }

    // класс студента магистратуры
    class MasterStudent : IStudent
    {
        private string name;
        private int[] grades;

        // конструктор принимает имя и массив оценок и записывает их в поля объекта
        public MasterStudent(string name, int[] grades)
        {
            this.name = name;
            this.grades = grades;
        }

        // метод GetAverageGrade вычисляет средний балл студента
        public double GetAverageGrade()
        {
            int sum = 0;

            foreach (int grade in grades)
            {
                sum += grade;
            }

            return (double)sum / grades.Length;
        }

        // метод GetCourseInfo возвращает строку с информацией о курсе студента
        public string GetCourseInfo()
        {
            return $"{name} учится на 1 курсе магистратуры";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // объявление массива students типа IStudent
            IStudent[] students = new IStudent[]
            {
                new FirstCourseStudent("Иванов И.И.", new int[] { 5, 4, 5, 5, 4 }), // создаётся объект первокурсника с массивом оценок и кладётся в первый элемент
                new ThirdCourseStudent("Петров П.П.", new int[] { 4, 4, 3, 4, 4 }), // создаётся объект третьекурсника и кладётся во второй элемент
                new MasterStudent("Сидоров С.С.", new int[] { 5, 5, 5, 4, 5 }) // создаётся объект магистра и кладётся в третий элемент
            };

            // цикл foreach: переменная student по очереди принимает каждый элемент массива students
            foreach (IStudent student in students)
            {
                Console.WriteLine($"{student.GetCourseInfo()}, средний балл: {student.GetAverageGrade():F2}");
            }

            Console.ReadKey();
        }
    }
}