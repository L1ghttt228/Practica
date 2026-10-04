using System;

namespace Task1Figure
{
    interface IShape
    {
        double Area(); // объявление метода вычисления площади
        double Perimeter(); // объявление метода вычисления периметра
    }

    // объявление класса Circle реализующий интерфейс IShape
    class Circle : IShape
    {
        // закрытое поле radius хранит радиус, доступ к нему возможен только внутри класса
        private double radius;

        // конструктор класса: выполняется автоматически при создании объекта оператором new
        public Circle(double radius)
        {
            this.radius = radius; // this.radius — поле объекта, radius — параметр
        }

        // реализация метода интерфейса Area
        public double Area()
        {
            return Math.PI * radius * radius;
        }

        // реализация метода интерфейса Perimeter
        public double Perimeter()
        {
            return 2 * Math.PI * radius;
        }
    }

        class Rectangle : IShape
    {
        private double width;
        private double height;

        // конструктор принимает ширину и высоту и записывает их в поля объекта
        public Rectangle(double width, double height)
        {
            this.width = width;
            this.height = height;
        }

        // метод Area возвращает площадь прямоугольника
        public double Area()
        {
            return width * height;
        }

        // метод Perimeter возвращает периметр прямоугольника
        public double Perimeter()
        {
            return 2 * (width + height);
        }
    }

        class Triangle : IShape
    {
        private double sideA; 
        private double sideB; 
        private double sideC; 

        // конструктор принимает три стороны и записывает их в поля объекта
        public Triangle(double sideA, double sideB, double sideC)
        {
            this.sideA = sideA;
            this.sideB = sideB;
            this.sideC = sideC;
        }

        public double Area()
        {
            double semi = Perimeter() / 2; 
            return Math.Sqrt(semi * (semi - sideA) * (semi - sideB) * (semi - sideC)); // Math.Sqrt извлекает квадратный корень
        }

        // метод Perimeter возвращает сумму длин сторон
        public double Perimeter()
        {
            return sideA + sideB + sideC;
        }
    }

        class Program
    {
        static void Main(string[] args)
        {
            // объявление массива shapes типа IShape
            IShape[] shapes = new IShape[]
            {
                new Circle(3.0), // оператор new создаёт объект круга
                new Rectangle(4.0, 5.0), // создаётся объект прямоугольника
                new Triangle(3.0, 4.0, 5.0) // создаётся объект треугольника
            };

            // цикл foreach: переменная shape по очереди принимает значение каждого элемента массива shapes
            foreach (IShape shape in shapes)
            {
            Console.WriteLine($"{shape.GetType().Name}: площадь = {shape.Area():F2}, периметр = {shape.Perimeter():F2}");
            }

            Console.ReadKey(); // ожидание нажатия любой клавиши, чтобы окно консоли не закрылось
        }
    }
}