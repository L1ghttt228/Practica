using System;

namespace Task2Shapes
{
    // абстрактный класс Shape: базовый шаблон для фигур, объект которого нельзя создать напрямую
    abstract class Shape
    {
        public abstract double Area(); // абстрактный метод площади, производные классы обязаны его реализовать
        public abstract double Perimeter(); // абстрактный метод периметра
    }

    // производный класс Circle наследует класс Shape
    class Circle : Shape
    {
        private double radius;

        // конструктор вызывается оператором new при создании объекта
        public Circle(double radius)
        {
            this.radius = radius;
        }

        // override заменяет абстрактный метод базового класса
        public override double Area()
        {
            return Math.PI * radius * radius; 
        }

        // override заменяет абстрактный метод базового класса
        public override double Perimeter()
        {
            return 2 * Math.PI * radius;
        }
    }

    // производный класс Rectangle наследует класс Shape
    class Rectangle : Shape
    {
        private double width; 
        private double height;

        // конструктор принимает размеры и записывает их в поля объекта
        public Rectangle(double width, double height)
        {
            this.width = width; 
            this.height = height; 
        }

        // override заменяет абстрактный метод базового класса
        public override double Area()
        {
            return width * height; 
        }

        // override заменяет абстрактный метод базового класса
        public override double Perimeter()
        {
            return 2 * (width + height); 
        }
    }

    // класс Program содержит точку входа в программу
    class Program
    {
        static void Main(string[] args)
        {
            Shape circle = new Circle(3.0); // переменная базового типа Shape хранит объект наследника Circle
            Shape rectangle = new Rectangle(5.0, 6.0); // переменная базового типа Shape хранит объект наследника Rectangle

            Shape[] shapes = new Shape[] // объявление массива объектов базового типа
            {
                circle, 
                rectangle 
            };

            // цикл foreach перебирает каждый элемент массива shapes
            foreach (Shape shape in shapes)
            {
                
                Console.WriteLine($"{shape.GetType().Name}: площадь = {shape.Area():F2}, периметр = {shape.Perimeter():F2}");
            }

            Console.ReadKey();
        }
    }
}