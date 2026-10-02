using System;

namespace Task1Person
{
    // class — шаблон для создания объекта
    class Person
    {
        // private — доступ только внутри класса
        private string name;
        private int age;
        private string address;

        // Конструктор — инициализация объекта при создании через new
        public Person(string name, int age, string address)
        {
            // this — текущий объект
            this.name = name;
            this.age = age;
            this.address = address;
        }

        // Установка имени
        public void SetName(string name)
        {
            this.name = name;
        }

        // Получение имени
        public string GetName()
        {
            return name;
        }

        // Установка возраста
        // Если возраст отрицательный, смена значения не выполняется
        public void SetAge(int age)
        {
            if (age < 0)
            {
                Console.WriteLine("Ошибка: возраст не может быть отрицательным.");
                return; // завершение метода
            }

            this.age = age;
        }

        // Получение возраста
        public int GetAge()
        {
            return age;
        }

        // Установка адреса
        public void SetAddress(string address)
        {
            this.address = address;
        }

        // Получение адреса
        public string GetAddress()
        {
            return address;
        }

        // Вывод информации о человеке
        public void PrintInfo()
        {
            Console.WriteLine($"Имя: {name}, возраст: {age}, адрес: {address}");
        }
    }

    class Program
    {
        // Main — точка входа в программу
        static void Main(string[] args)
        {
            // Создание объекта класса Person
            Person person1 = new Person("Иван Иванов", 20, "Минск");

            // Вызов метода объекта
            person1.PrintInfo();

            // Смена возраста и адреса
            person1.SetAge(21);
            person1.SetAddress("Санкт-Петербург");

            Console.WriteLine("После изменений:");
            person1.PrintInfo();

            // Создание второго объекта
            Person person2 = new Person("Анна Петрова", 23, "Орша");
            person2.PrintInfo();

            // Если возраст отрицательный, смена значения не выполняется
            person2.SetAge(42);

            Console.ReadKey();
        }
    }
}