using System;
using System.Collections.Generic;
using System.IO;

namespace TaskManager
{
    // Статический класс Database: «база данных» в виде текстового файла tasks.txt (чтение и запись списка задач); статический, чтобы вызывать его методы без создания объекта; одна задача = одна строка, поля разделены символом |, например: Купить молоко|В магазине у дома|Новая
    public static class Database
    {
        // Поле FilePath типа string (модификаторы static readonly): полный путь к файлу tasks.txt рядом с exe-файлом программы; метод Path.Combine склеивает папку и имя файла, а readonly запрещает менять значение после создания
        private static readonly string FilePath =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tasks.txt");

        // Метод Load: чтение всех задач из файла; возвращает список List<TaskItem>
        public static List<TaskItem> Load()
        {
            // Переменная list типа List<TaskItem> (ключевое слово var подставляет тип само): сюда складываются задачи, прочитанные из файла
            var list = new List<TaskItem>();

            // Оператор if: если файла ещё нет (метод File.Exists вернул false; оператор ! означает «не»), возвращается пустой список
            if (!File.Exists(FilePath))
            {
                return list;
            }

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

                // Метод Add: в список добавляется новый объект TaskItem, созданный оператором new с инициализатором свойств
                list.Add(new TaskItem { Title = p[0], Description = p[1], Status = p[2] });
            }

            // Оператор return: готовый список возвращается тому, кто вызвал метод
            return list;
        }

        // Метод Save: запись всего списка в файл (старое содержимое файла заменяется); параметр типа IEnumerable<TaskItem> принимает любую коллекцию задач
        public static void Save(IEnumerable<TaskItem> tasks)
        {
            // Переменная lines типа List<string>: сюда складываются строки, которые будут записаны в файл
            var lines = new List<string>();

            // Оператор foreach: цикл по всем задачам из коллекции
            foreach (var t in tasks)
            {
                // Склейка полей оператором +: поля задачи соединяются символом | в одну строку
                lines.Add(t.Title + "|" + t.Description + "|" + t.Status);
            }

            // Метод File.WriteAllLines: запись всех строк в файл; если файла нет, он создаётся
            File.WriteAllLines(FilePath, lines);
        }
    }
}
