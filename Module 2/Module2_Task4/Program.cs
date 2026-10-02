using System;

namespace Task4Interface
{
    // interface — контракт для классов
    // Классы обязаны реализовать метод Draw
    interface IDrawable
    {
        void Draw();
    }

    // Реализация интерфейса классом Circle
    class Circle : IDrawable
    {
        private double radius;

        public Circle(double radius)
        {
            this.radius = radius;
        }

        // Реализация метода интерфейса
        public void Draw()
        {
            Console.WriteLine($"Рисуется круг радиуса {radius}");
        }
    }

    // Реализация интерфейса классом Rectangle
    class Rectangle : IDrawable
    {
        private double width;
        private double height;

        public Rectangle(double width, double height)
        {
            this.width = width;
            this.height = height;
        }

        // Реализация метода интерфейса
        public void Draw()
        {
            Console.WriteLine($"Рисуется прямоугольник {width} x {height}");
        }
    }

    // Реализация интерфейса классом Triangle
    class Triangle : IDrawable
    {
        private double sideA;
        private double sideB;
        private double sideC;

        public Triangle(double sideA, double sideB, double sideC)
        {
            this.sideA = sideA;
            this.sideB = sideB;
            this.sideC = sideC;
        }

        // Реализация метода интерфейса
        public void Draw()
        {
            Console.WriteLine($"Рисуется треугольник со сторонами {sideA}, {sideB}, {sideC}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Массив объектов, реализующих интерфейс
            IDrawable[] objects = new IDrawable[]
            {
                new Circle(10.0),
                new Rectangle(6.0, 6.0),
                new Triangle(2.0, 3.0, 4.0)
            };

            // foreach — перебор объектов массива
            // Полиморфизм: вызов Draw у разных объектов через один тип
            foreach (IDrawable obj in objects)
            {
                obj.Draw();
            }
            Console.ReadKey();
        }
    }
}