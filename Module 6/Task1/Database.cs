using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace EmployeeManager
{
    // Статический класс Database: «база данных» в виде текстового файла employees.txt (чтение и запись списка сотрудников); статический, чтобы вызывать его методы без создания объекта; один сотрудник = одна строка, поля разделены символом |, например: Иванов Иван|Программист|IT|120000
    public static class Database
    {
        // Поле FilePath типа string (модификаторы static readonly): полный путь к файлу employees.txt рядом с exe-файлом программы; метод Path.Combine склеивает папку и имя файла, а readonly запрещает менять значение после создания
        private static readonly string FilePath =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "employees.txt");

        // Метод Load: чтение всех сотрудников из файла; возвращает список List<Employee>
        public static List<Employee> Load()
        {
            // Переменная list типа List<Employee> (ключевое слово var подставляет тип само): сюда складываются сотрудники, прочитанные из файла
            var list = new List<Employee>();

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

                // Метод Add: в список добавляется новый объект Employee, созданный оператором new с инициализатором свойств; метод decimal.Parse переводит текст в число, а InvariantCulture задаёт точку как разделитель
                list.Add(new Employee
                {
                    FullName = p[0],
                    Position = p[1],
                    Department = p[2],
                    Salary = decimal.Parse(p[3], CultureInfo.InvariantCulture)
                });
            }

            // Оператор return: готовый список возвращается тому, кто вызвал метод
            return list;
        }

        // Метод Save: запись всего списка в файл (старое содержимое файла заменяется); параметр типа IEnumerable<Employee> принимает любую коллекцию сотрудников
        public static void Save(IEnumerable<Employee> employees)
        {
            // Переменная lines типа List<string>: сюда складываются строки, которые будут записаны в файл
            var lines = new List<string>();

            // Оператор foreach: цикл по всем сотрудникам из коллекции
            foreach (var e in employees)
            {
                // Склейка полей оператором +: поля сотрудника соединяются символом | в одну строку; метод ToString с InvariantCulture записывает оклад с точкой
                lines.Add(e.FullName + "|" + e.Position + "|" + e.Department + "|" +
                          e.Salary.ToString(CultureInfo.InvariantCulture));
            }

            // Метод File.WriteAllLines: запись всех строк в файл; если файла нет, он создаётся
            File.WriteAllLines(FilePath, lines);
        }
    }
}
