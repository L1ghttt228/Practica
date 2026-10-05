using System;

namespace Task4Book
{
    // интерфейс IBook задаёт: проверку доступности и выдачу книги
    interface IBook
    {
        bool IsAvailable(); // объявление метода проверки доступности книги
        void Issue(); // объявление метода выдачи книги
    }

    // класс бумажной книги реализует интерфейс IBook
    class PaperBook : IBook
    {
        private string title; 
        private int copies;

        // конструктор принимает название и число экземпляров и записывает их в поля объекта
        public PaperBook(string title, int copies)
        {
            this.title = title; 
            this.copies = copies; 
        }

        // метод IsAvailable проверяет, есть ли свободные экземпляры
        public bool IsAvailable()
        {
            return copies > 0; 
        }

        // метод Issue выдаёт книгу читателю
        public void Issue()
        {
            // оператор ! означает логическое отрицание; если книга недоступна, выполняется блок if
            if (!IsAvailable())
            {
                Console.WriteLine($"Книга \"{title}\" недоступна для выдачи.");
                return; // оператор return завершает метод, и выдача не выполняется
            }

            copies--; // оператор -- уменьшает количество экземпляров на единицу
            Console.WriteLine($"Книга \"{title}\" выдана, осталось экземпляров: {copies}."); 
        }
    }

    // класс электронной книги реализует интерфейс IBook
    class ElectronicBook : IBook
    {
        private string title;

        // конструктор принимает название и записывает его в поле объекта
        public ElectronicBook(string title)
        {
            this.title = title;
        }

        // метод IsAvailable всегда возвращает true: электронных копий неограниченное количество
        public bool IsAvailable()
        {
            return true;
        }

        // метод Issue открывает книгу для чтения, не изменяя никаких полей
        public void Issue()
        {
            Console.WriteLine($"Электронная книга \"{title}\" открыта для чтения.");
        }
    }

    // класс редкой книги реализует интерфейс IBook
    class RareBook : IBook
    {
        private string title;

        // конструктор принимает название и записывает его в поле объекта
        public RareBook(string title)
        {
            this.title = title;
        }

        // метод IsAvailable возвращает true: книга находится в библиотеке
        public bool IsAvailable()
        {
            return true;
        }

        // метод Issue не выдаёт книгу на дом, а информирует о работе в читальном зале
        public void Issue()
        {
            Console.WriteLine($"Книга \"{title}\" выдаётся только в читальном зале.");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // объявление массива books типа IBook
            IBook[] books = new IBook[]
            {
                new PaperBook("Война и мир", 1),
                new ElectronicBook("C# для начинающих"),
                new RareBook("Первое издание Преступления и наказания") 
            };

            // цикл foreach: переменная book по очереди принимает каждый элемент массива books
            foreach (IBook book in books)
            {
                // оператор if вызывает метод IsAvailable; если он вернул true, выполняется первая ветвь
                if (book.IsAvailable())
                {
                    book.Issue();
                }
                else // ветвь else выполняется, если IsAvailable вернул false
                {
                    Console.WriteLine("Книга недоступна для выдачи.");
                }
            }

            books[0].Issue(); // обращение по индексу 0 к первому элементу массива и повторный вызов выдачи

            Console.ReadKey();
        }
    }
}