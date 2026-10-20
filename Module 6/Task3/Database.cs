using System;
using System.Collections.Generic;
using System.IO;

namespace LibraryApp
{
    // Статический класс Database: «база данных» в виде текстового файла books.txt (чтение и запись списка книг); статический, чтобы вызывать его методы без создания объекта; одна книга = одна строка, поля разделены символом |, например: Война и мир|Лев Толстой|Иванов (если читателя нет, последнее поле пустое)
    public static class Database
    {
        // Поле FilePath типа string (модификаторы static readonly): полный путь к файлу books.txt рядом с exe-файлом программы; метод Path.Combine склеивает папку и имя файла, а readonly запрещает менять значение после создания
        private static readonly string FilePath =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "books.txt");

        // Метод Load: чтение всех книг из файла; возвращает список List<Book>
        public static List<Book> Load()
        {
            // Оператор if: если файла ещё нет (метод File.Exists вернул false; оператор ! означает «не»), он создаётся с несколькими примерами книг
            if (!File.Exists(FilePath))
            {
                // Метод File.WriteAllLines: запись массива строк в файл; пустое последнее поле означает, что книга свободна
                File.WriteAllLines(FilePath, new string[]
                {
                    "Война и мир|Лев Толстой|",
                    "Преступление и наказание|Фёдор Достоевский|",
                    "Мастер и Маргарита|Михаил Булгаков|",
                    "Евгений Онегин|Александр Пушкин|",
                    "Мёртвые души|Николай Гоголь|",
                    "Отцы и дети|Иван Тургенев|"
                });
            }

            // Переменная list типа List<Book> (ключевое слово var подставляет тип само): сюда складываются книги, прочитанные из файла
            var list = new List<Book>();

            // Оператор foreach: цикл по всем строкам файла; метод File.ReadAllLines читает файл целиком и возвращает массив строк
            foreach (string line in File.ReadAllLines(FilePath))
            {
                // Метод Split: строка режется по символу | на массив полей p
                string[] p = line.Split('|');

                // Оператор if: если полей меньше трёх, строка повреждена; оператор continue пропускает её и переходит к следующей
                if (p.Length < 3)
                {
                    continue;
                }

                // Метод Add: в список добавляется новый объект Book, созданный оператором new с инициализатором свойств
                list.Add(new Book { Title = p[0], Author = p[1], Reader = p[2] });
            }

            // Оператор return: готовый список возвращается тому, кто вызвал метод
            return list;
        }

        // Метод Save: запись всего списка в файл (старое содержимое файла заменяется); параметр типа IEnumerable<Book> принимает любую коллекцию книг
        public static void Save(IEnumerable<Book> books)
        {
            // Переменная lines типа List<string>: сюда складываются строки, которые будут записаны в файл
            var lines = new List<string>();

            // Оператор foreach: цикл по всем книгам из коллекции
            foreach (var b in books)
            {
                // Склейка полей оператором +: поля книги соединяются символом | в одну строку
                lines.Add(b.Title + "|" + b.Author + "|" + b.Reader);
            }

            // Метод File.WriteAllLines: запись всех строк в файл; если файла нет, он создаётся
            File.WriteAllLines(FilePath, lines);
        }
    }
}
