using System; 
using System.Collections.Generic; 

namespace Task4DataFilter 
{
    // класс записи хранит заголовок документа и дату создания
    class Record
    {
        public string Title { get; private set; } // свойство заголовка записи
        public DateTime Date { get; private set; } // свойство даты записи, DateTime — тип для хранения даты и времени

        // конструктор принимает заголовок и дату и записывает их в свойства объекта
        public Record(string title, DateTime date)
        {
            Title = title; 
            Date = date; 
        }
    }

    // объявление делегата: описывает методы-фильтры, принимающие запись и возвращающие bool
    delegate bool RecordFilter(Record record);

    class Program
    {
        // фильтр по дате: пропускает записи, созданные не ранее 1 января 2024 года
        static bool FilterByRecentDate(Record record)
        {
            return record.Date >= new DateTime(2024, 1, 1); 
        }

        // фильтр по ключевому слову: пропускает записи, содержащие слово "отчёт"
        static bool FilterByKeyword(Record record)
        {
            return record.Title.Contains("отчёт"); 
        }

        // метод применяет переданный делегат-фильтр к списку записей и возвращает новый список
        static List<Record> ApplyFilter(List<Record> records, RecordFilter filter)
        {
            List<Record> result = new List<Record>(); // создание пустого списка для подходящих записей

            // цикл foreach: переменная record по очереди принимает каждую запись исходного списка
            foreach (Record record in records)
            {
                if (filter(record)) // вызов делегата: возвращает true, если запись прошла фильтр
                {
                    result.Add(record); // метод Add добавляет прошедшую фильтр запись в список результата
                }
            }

            return result; // return возвращает список отобранных записей вызывающему коду
        }

        static void Main(string[] args)
        {
            // создание исходного списка записей с разными датами и заголовками
            List<Record> records = new List<Record>
            {
                new Record("Годовой отчёт", new DateTime(2024, 5, 10)),
                new Record("Финансовый отчёт", new DateTime(2023, 11, 2)),
                new Record("Протокол совещания", new DateTime(2024, 2, 20)),
                new Record("Договор поставки", new DateTime(2022, 7, 15))
            };

            RecordFilter filter = FilterByRecentDate; // привязка делегата к фильтру по дате

            List<Record> filtered = ApplyFilter(records, filter); // делегат передаётся в метод и применяется к каждой записи

            Console.WriteLine("Записи с 2024 года:");
            foreach (Record record in filtered) // цикл перебирает отобранные записи
            {
                Console.WriteLine($"{record.Title} ({record.Date:dd.MM.yyyy})"); 
            }

            filter = FilterByKeyword; // перепривязка делегата к фильтру по ключевому слову — фильтр выбирается динамически

            filtered = ApplyFilter(records, filter); // повторное применение фильтрации с новым делегатом

            Console.WriteLine("\nЗаписи со словом \"отчёт\":"); 
            foreach (Record record in filtered) // цикл перебирает отобранные записи
            {
                Console.WriteLine($"{record.Title} ({record.Date:dd.MM.yyyy})");
                    }

            Console.ReadKey(); 
        }
    }
}