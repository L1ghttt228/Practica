using System;

namespace Task2Shapes
{
    // abstract class — базовый шаблон без создания объекта напрямую
    abstract class Shape
    {
        // abstract method — метод без реализации
        // Наследники обязаны его реализовать
        public abstract double Area();

        // abstract method для периметра
        public abstract double Perimeter();
    }

    // Наследование класса Circle от Shape
    class Circle : Shape
    {
        private double radius;

        // Конструктор круга
        public Circle(double radius)
        {
            this.radius = radius;
        }

        // override — замена метода базового класса
        public override double Area()
        {
            // Формула площади круга
            return Math.PI * radius * radius;
        }

        // override — замена метода базового класса
        public override double Perimeter()
        {
            // Формула длины окружности
            return 2 * Math.PI * radius;
        }
    }

    // Наследование класса Rectangle от Shape
    class Rectangle : Shape
    {
        private double width;
        private double height;

        // Конструктор прямоугольника
        public Rectangle(double width, double height)
        {
            this.width = width;
            this.height = height;
        }

        // override — замена метода базового класса
        public override double Area()
        {
            // Формула площади прямоугольника
            return width * height;
        }

        // override — замена метода базового класса
        public override double Perimeter()
        {
            // Формула периметра прямоугольника
            return 2 * (width + height);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Полиморфизм: переменная типа Shape хранит объект наследника
            Shape circle = new Circle(3.0);
            Shape rectangle = new Rectangle(5.0, 6.0);

            // Массив объектов базового типа
            Shape[] shapes = new Shape[]
            {
                circle,
                rectangle
            };

            // foreach — перебор элементов массива
            foreach (Shape shape in shapes)
            {
                // GetType().Name — получение имени реального класса объекта
                Console.WriteLine(
                    $"{shape.GetType().Name}: площадь = {shape.Area():F2}, " +
                    $"периметр = {shape.Perimeter():F2}");
            }
            Console.ReadKey();
        }
    }
}