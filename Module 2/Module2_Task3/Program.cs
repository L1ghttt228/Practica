using System;

namespace Task3Composition
{
    // Класс автора
    class Author
    {
        // get — чтение значения
        // private set — смена значения только внутри класса
        public string Name { get; private set; }
        public int BirthYear { get; private set; }

        // Конструктор автора
        public Author(string name, int birthYear)
        {
            Name = name;
            BirthYear = birthYear;
        }
    }

    // Класс книги
    // Композиция: книга содержит объект автора
    class Book
    {
        public string Title { get; private set; }
        public int Year { get; private set; }

        // Поле типа Author — связь книги и автора
        public Author Author { get; private set; }

        // Конструктор книги
        public Book(string title, int year, Author author)
        {
            Title = title;
            Year = year;
            Author = author;
        }

        // Вывод информации о книге
        public void PrintInfo()
        {
            Console.WriteLine(
                $"Книга: {Title}, год выпуска: {Year}, " +
                $"автор: {Author.Name}, год рождения автора: {Author.BirthYear}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Создание объектов авторов
            Author author1 = new Author("Фёдор Достоевский", 1821);
            Author author2 = new Author("Лев Толстой", 1828);

            // Создание книг и связь с авторами
            Book book1 = new Book("Преступление и наказание", 1866, author1);
            Book book2 = new Book("Война и мир", 1869, author2);

            // Вывод информации о книгах
            book1.PrintInfo();
            book2.PrintInfo();

            Console.ReadKey();
        }
    }
}