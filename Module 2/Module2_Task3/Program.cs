using System;

namespace Task3Composition
{
    // класс Author описывает автора книги
    class Author
    {
        // свойства с private set разрешают изменение значения только внутри класса
        public string Name { get; private set; }
        public int BirthYear { get; private set; }

        // конструктор инициализирует свойства автора
        public Author(string name, int birthYear)
        {
            Name = name;
            BirthYear = birthYear;
        }
    }

    // класс Book описывает книгу, содержит объект автора
    class Book
    {
        public string Title { get; private set; }
        public int Year { get; private set; }
        public Author Author { get; private set; } // свойство типа Author хранит ссылку на объект автора

        // конструктор принимает название, год и объект автора
        public Book(string title, int year, Author author)
        {
            Title = title;
            Year = year;
            Author = author;
        }

        // метод выводит информацию о книге и её авторе
        public void PrintInfo()
        {
            Console.WriteLine($"Книга: {Title}, год выпуска: {Year}, автор: {Author.Name}, год рождения автора: {Author.BirthYear}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Author author1 = new Author("Фёдор Достоевский", 1821);
            Author author2 = new Author("Лев Толстой", 1828);

            // при создании книги объект автора передаётся в конструктор, устанавливая композиционную связь
            Book book1 = new Book("Преступление и наказание", 1866, author1);
            Book book2 = new Book("Война и мир", 1869, author2);

            book1.PrintInfo();
            book2.PrintInfo();

            Console.ReadKey();
        }
    }
}