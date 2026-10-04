using System;

namespace Task4Interface
{
    interface IDrawable
    {
        void Draw();
    }

    // класс Circle реализует интерфейс IDrawable
    class Circle : IDrawable
    {
        private double radius;

        public Circle(double radius)
        {
            this.radius = radius;
        }

        // реализация метода интерфейса для круга
        public void Draw()
        {
            Console.WriteLine($"Рисуется круг радиуса {radius}");
        }
    }

    // класс Rectangle реализует интерфейс IDrawable
    class Rectangle : IDrawable
    {
        private double width;
        private double height;

        public Rectangle(double width, double height)
        {
            this.width = width;
            this.height = height;
        }

        // реализация метода интерфейса для прямоугольника
        public void Draw()
        {
            Console.WriteLine($"Рисуется прямоугольник {width} x {height}");
        }
    }

    // класс Triangle реализует интерфейс IDrawable
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

        // реализация метода интерфейса для треугольника
        public void Draw()
        {
            Console.WriteLine($"Рисуется треугольник со сторонами {sideA}, {sideB}, {sideC}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // массив интерфейсного типа хранит объекты разных классов
            IDrawable[] objects = new IDrawable[]
            {
                new Circle(10.0),
                new Rectangle(6.0, 6.0),
                new Triangle(2.0, 3.0, 4.0)
            };

            // цикл foreach перебирает каждый элемент массива
            foreach (IDrawable obj in objects)
            {
                obj.Draw();
            }

            Console.ReadKey();
        }
    }
}