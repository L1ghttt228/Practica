using System;

namespace Task5Drawing
{
    // интерфейс IDrawing задаёт: методы рисования линии, круга и прямоугольника
    interface IDrawing
    {
        void DrawLine(int x1, int y1, int x2, int y2); // объявление метода рисования линии по двум точкам
        void DrawCircle(int x, int y, int radius); // объявление метода рисования круга по центру и радиусу
        void DrawRectangle(int x, int y, int width, int height); // объявление метода рисования прямоугольника по углу и размерам
    }

    // класс Canvas — холст, реализующий все методы интерфейса IDrawing
    class Canvas : IDrawing
    {
        private int width;
        private int height;

        // конструктор принимает размеры холста и записывает их в поля объекта
        public Canvas(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        // вспомогательный закрытый метод: проверяет, лежит ли точка внутри холста
        private bool IsInside(int x, int y)
        {
            return x >= 0 && x < width && y >= 0 && y < height; // оператор && требует истинности всех четырёх условий
        }

        // метод DrawLine рисует линию между двумя точками
        public void DrawLine(int x1, int y1, int x2, int y2)
        {
            // оператор ! отрицает результат IsInside; оператор || означает ИЛИ: достаточно одной точки вне холста
            if (!IsInside(x1, y1) || !IsInside(x2, y2))
            {
                Console.WriteLine("Линия не нарисована: точки за пределами холста."); 
                return; 
            }

            Console.WriteLine($"Линия нарисована от ({x1}; {y1}) до ({x2}; {y2})."); 
        }

        // метод DrawCircle рисует круг по координатам центра и радиусу
        public void DrawCircle(int x, int y, int radius)
        {
            // проверка границ
            if (x - radius < 0 || x + radius >= width || y - radius < 0 || y + radius >= height)
            {
                Console.WriteLine("Круг не нарисован: фигура за пределами холста."); 
                return; 
            }

            Console.WriteLine($"Круг нарисован с центром ({x}; {y}) и радиусом {radius}.");
        }

        // метод DrawRectangle рисует прямоугольник по левому верхнему углу и размерам
        public void DrawRectangle(int x, int y, int rectWidth, int rectHeight)
        {
            // проверка границ
            if (x < 0 || y < 0 || x + rectWidth >= width || y + rectHeight >= height)
            {
                Console.WriteLine("Прямоугольник не нарисован: фигура за пределами холста."); 
                return; 
            }

            Console.WriteLine($"Прямоугольник нарисован от ({x}; {y}) размером {rectWidth} x {rectHeight}."); 
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            IDrawing canvas = new Canvas(100, 100); // оператор new создаёт объект холста размером 100 на 100

            canvas.DrawLine(10, 10, 50, 50); // вызов метода рисования линии
            canvas.DrawCircle(50, 50, 20); // вызов метода рисования круга
            canvas.DrawRectangle(10, 10, 30, 40); // вызов метода рисования прямоугольника

            canvas.DrawCircle(90, 90, 30); // вызов с параметрами вне границ

            Console.ReadKey();
        }
    }
}