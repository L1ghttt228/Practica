using System;

namespace Variant2Task3Shape
{
    // abstract class — базовый шаблон для фигур
    abstract class Shape
    {
        // abstract method — метод без реализации
        // Наследники обязаны его реализовать
        public abstract double Area();
    }

    // Наследование круга от фигуры
    class Circle : Shape
    {
        private double radius;

        // Конструктор круга
        public Circle(double radius)
        {
            this.radius = radius;
        }

        // override — реализация метода для круга
        public override double Area()
        {
            // Формула площади круга
            return Math.PI * radius * radius;
        }
    }

    // Наследование прямоугольника от фигуры
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

        // override — реализация метода для прямоугольника
        public override double Area()
        {
            // Формула площади прямоугольника
            return width * height;
        }
    }

    // Наследование треугольника от фигуры
    class Triangle : Shape
    {
        private double baseSide;
        private double height;

        // Конструктор треугольника
        public Triangle(double baseSide, double height)
        {
            this.baseSide = baseSide;
            this.height = height;
        }

        // override — реализация метода для треугольника
        public override double Area()
        {
            // Формула площади треугольника
            return 0.5 * baseSide * height;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Создание массива фигур
            // Полиморфизм: один тип хранит разные объекты
            Shape[] shapes = new Shape[]
            {
                new Circle(3.0),
                new Rectangle(4.0, 5.0),
                new Triangle(6.0, 7.0)
            };

            // foreach — перебор фигур
            // Вызов метода Area зависит от реального объекта
            foreach (Shape shape in shapes)
            {
                Console.WriteLine($"{shape.GetType().Name}: площадь = {shape.Area():F2}");
            }
        }
    }
}