using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace FinanceTracker
{
    // Статический класс Database: «база данных» в виде текстового файла finance.txt (чтение и запись списка финансовых операций); статический, чтобы вызывать его методы без создания объекта; одна запись = одна строка, поля разделены символом |, например: 2026-10-20|Доход|Зарплата|50000
    public static class Database
    {
        // Поле FilePath типа string (модификаторы static readonly): полный путь к файлу finance.txt рядом с exe-файлом программы; метод Path.Combine склеивает папку и имя файла, а readonly запрещает менять значение после создания
        private static readonly string FilePath =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "finance.txt");

        // Метод Load: чтение всех записей из файла; возвращает список List<FinanceRecord>
        public static List<FinanceRecord> Load()
        {
            // Переменная list типа List<FinanceRecord> (ключевое слово var подставляет тип само): сюда складываются записи, прочитанные из файла
            var list = new List<FinanceRecord>();

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

                // Оператор if: если полей меньше четырёх, строка повреждена; оператор continue пропускает её и переходит к следующей
                if (p.Length < 4)
                {
                    continue;
                }

                // Метод Add: в список добавляется новый объект FinanceRecord, созданный оператором new с инициализатором свойств
                // Метод DateTime.ParseExact переводит текст вида 2026-10-20 в дату строго по формату yyyy-MM-dd; метод decimal.Parse переводит текст в число, а InvariantCulture задаёт точку как разделитель
                list.Add(new FinanceRecord
                {
                    Date = DateTime.ParseExact(p[0], "yyyy-MM-dd", CultureInfo.InvariantCulture),
                    Type = p[1],
                    Category = p[2],
                    Amount = decimal.Parse(p[3], CultureInfo.InvariantCulture)
                });
            }

            // Оператор return: готовый список возвращается тому, кто вызвал метод
            return list;
        }

        // Метод Save: запись всего списка в файл (старое содержимое файла заменяется); параметр типа IEnumerable<FinanceRecord> принимает любую коллекцию записей
        public static void Save(IEnumerable<FinanceRecord> records)
        {
            // Переменная lines типа List<string>: сюда складываются строки, которые будут записаны в файл
            var lines = new List<string>();

            // Оператор foreach: цикл по всем записям из коллекции
            foreach (var r in records)
            {
                // Склейка полей оператором +: поля записи соединяются символом | в одну строку; метод ToString с шаблоном yyyy-MM-dd записывает дату в нужном формате, а сумма записывается с точкой
                lines.Add(r.Date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) + "|" +
                          r.Type + "|" + r.Category + "|" +
                          r.Amount.ToString(CultureInfo.InvariantCulture));
            }

            // Метод File.WriteAllLines: запись всех строк в файл; если файла нет, он создаётся
            File.WriteAllLines(FilePath, lines);
        }
    }
}
