namespace StudentManagerConsole;

// Класс StudentStorage: чтение и запись списка студентов в текстовый файл; одна строка файла - один студент в формате ФИО|возраст|группа
public sealed class StudentStorage
{
    // Константа FileName типа string: имя файла по умолчанию; const задаёт неизменяемое значение
    private const string FileName = "students.txt";

    // Поле _filePath типа string: полный путь к файлу; readonly запрещает менять его после создания
    private readonly string _filePath;

    // Конструктор: если путь не передан (null), файл берётся в текущей папке; оператор ?? подставляет значение по умолчанию, а знак ? у типа string? разрешает null
    public StudentStorage(string? filePath = null)
    {
        _filePath = filePath ?? Path.Combine(Environment.CurrentDirectory, FileName);
    }

    // Метод Load: читает студентов из файла; если файла нет или он недоступен, возвращает пустой список
    public List<Student> Load()
    {
        // Оператор if: если файла ещё нет, возвращается пустой список (запись [] создаёт пустую коллекцию)
        if (!File.Exists(_filePath))
        {
            return [];
        }

        // Блок try-catch: ошибки чтения не должны останавливать программу, поэтому выводится сообщение и возвращается пустой список
        try
        {
            // Цепочка LINQ: строка делится по символу |, остаются строки с тремя полями и числовым возрастом, затем создаются объекты Student
            return File.ReadLines(_filePath)
                .Select(line => line.Split('|'))
                .Where(fields => fields.Length == 3 && int.TryParse(fields[1], out _))
                .Select(fields => new Student
                {
                    FullName = fields[0],
                    Age = int.Parse(fields[1]),
                    Group = fields[2]
                })
                .ToList();
        }
        // Блок catch: ошибка ввода-вывода (файл занят или повреждён)
        catch (IOException)
        {
            Console.WriteLine("Не удалось прочитать файл списка. Список начат пустым.");
            return [];
        }
        // Блок catch: нет прав доступа к файлу
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("Нет доступа к файлу списка. Список начат пустым.");
            return [];
        }
    }

    // Метод Save: записывает список в файл; возвращает true при успехе и false при ошибке
    public bool Save(IEnumerable<Student> students)
    {
        try
        {
            // Метод Path.GetDirectoryName возвращает папку файла; при необходимости она создаётся методом Directory.CreateDirectory
            var directory = Path.GetDirectoryName(_filePath);
            if (!string.IsNullOrEmpty(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // Метод File.WriteAllLines: запись строк в файл; символ | в ФИО и группе заменяется на /, чтобы не ломать формат; интерполированная строка $ собирает строку студента
            File.WriteAllLines(_filePath, students.Select(student =>
                $"{student.FullName.Replace('|', '/')}|{student.Age}|{student.Group.Replace('|', '/')}"));
            return true;
        }
        catch (IOException)
        {
            Console.WriteLine("Не удалось сохранить список.");
            return false;
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("Нет доступа для сохранения списка.");
            return false;
        }
    }
}
