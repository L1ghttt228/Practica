using System;

namespace Task1FigureDelegate
{
    // объявление делегата: тип, описывающий методы без параметров, возвращающие double
    delegate double AreaCalculator();

    // базовый класс Figure с абстрактным методом площади
    abstract class Figure
    {
        public abstract double Area(); // производные классы обязаны его реализовать метод
    }

    // производный класс Circle наследует класс Figure
    class Circle : Figure
    {
        private double radius;

        // конструктор вызывается оператором new при создании объекта
        public Circle(double radius)
        {
            this.radius = radius; // this.radius — поле объекта, оператор = записывает в него значение параметра radius
        }

        // override заменяет абстрактный метод базового класса
        public override double Area()
        {
            return Math.PI * radius * radius;
        }
    }

    // производный класс Rectangle наследует класс Figure
    class Rectangle : Figure
    {
        private double width;
        private double height;

        // конструктор принимает размеры и записывает их в поля объекта
        public Rectangle(double width, double height)
        {
            this.width = width; 
            this.height = height; 
        }

        public override double Area()
        {
            return width * height; 
        }
    }

    // производный класс Triangle наследует класс Figure
    class Triangle : Figure
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

        // площадь треугольника 
        public override double Area()
        {
            double semi = Perimeter() / 2; 
            return Math.Sqrt(semi * (semi - sideA) * (semi - sideB) * (semi - sideC)); // Math.Sqrt извлекает квадратный корень из произведения разностей
        }

        // вспомогательный закрытый метод возвращает сумму сторон
        private double Perimeter()
        {
            return sideA + sideB + sideC; 
        }
    }

    class Program
    {
        // метод принимает делегат как параметр и вызывает привязанный к нему метод
        static void PrintArea(AreaCalculator calculator)
        {
            Console.WriteLine($"Площадь = {calculator():F2}");
        }

        static void Main(string[] args)
        {
            Circle circle = new Circle(3.0); // создание объекта круга
            Rectangle rectangle = new Rectangle(4.0, 5.0); // создание объекта прямоугольника
            Triangle triangle = new Triangle(3.0, 4.0, 5.0); // создание объекта треугольника

            AreaCalculator calculator; // объявление переменной делегата без привязки метода

            calculator = circle.Area; // привязка делегата к методу Area круга
            PrintArea(calculator); // делегат передаётся в метод как аргумент и вызывается внутри него

            calculator = rectangle.Area; // перепривязка делегата к методу прямоугольника — метод вызова выбирается динамически
            PrintArea(calculator); // теперь делегат вызывает метод Area прямоугольника

            calculator = triangle.Area; // перепривязка делегата к методу треугольника
            PrintArea(calculator); // теперь делегат вызывает метод Area треугольника

            Console.ReadKey();
        }
    }
}