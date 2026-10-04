using System;

namespace Task1Person
{
    // объявление класса Person: шаблон для создания объектов, описывающих человека
    class Person
    {
        private string name; // закрытое поле имени, доступное только внутри класса
        private int age; // закрытое поле возраста
        private string address; // закрытое поле адреса

        // конструктор вызывается оператором new при создании объекта
        public Person(string name, int age, string address)
        {
            this.name = name; // this.name — поле объекта, оператор = записывает в него значение параметра name
            this.age = age;
            this.address = address;
        }

        // метод устанавливает имя, принимает строку и не возвращает значение
        public void SetName(string name)
        {
            this.name = name;
        }

        // метод возвращает имя, не принимает параметров
        public string GetName()
        {
            return name;
        }

        // метод устанавливает возраст с предварительной проверкой значения
        public void SetAge(int age)
        {
            // оператор if проверяет условие: если возраст меньше нуля, выполняется блок кода
            if (age < 0)
            {
                Console.WriteLine("Ошибка: возраст не может быть отрицательным.");
                return; // оператор return завершает выполнение метода, смена значения не выполняется
            }

            this.age = age;
        }

        // метод возвращает возраст
        public int GetAge()
        {
            return age; 
        }

        // метод устанавливает адрес
        public void SetAddress(string address)
        {
            this.address = address;
        }

        // метод возвращает адрес
        public string GetAddress()
        {
            return address; 
        }

        // метод выводит информацию о человеке в консоль
        public void PrintInfo()
        {
            Console.WriteLine($"Имя: {name}, возраст: {age}, адрес: {address}"); 
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Person person1 = new Person("Иван Иванов", 20, "Минск"); // оператор new создаёт объект класса Person и вызывает его конструктор
            person1.PrintInfo(); 

            person1.SetAge(21); 
            person1.SetAddress("Санкт-Петербург"); 

            Console.WriteLine("После изменений:");
            person1.PrintInfo(); 

            Person person2 = new Person("Анна Петрова", 23, "Орша"); 
            person2.PrintInfo(); 

            person2.SetAge(42); 

            Console.ReadKey(); // ожидание нажатия любой клавиши, чтобы окно консоли не закрылось
        }
    }
}