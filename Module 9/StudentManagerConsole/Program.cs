using System.Text;

namespace StudentManagerConsole;

// Статический класс Program: консольное меню для работы со списком студентов; модификатор internal делает класс видимым только внутри проекта
internal static class Program
{
    // Константы MaxNameLength и MaxGroupLength типа int: предельная длина ФИО и названия группы
    private const int MaxNameLength = 100;
    private const int MaxGroupLength = 50;

    // Метод Main: точка входа; загружает список и в бесконечном цикле показывает меню
    private static void Main()
    {
        // Свойство OutputEncoding: кодировка UTF8 нужна, чтобы русский текст и символ — правильно отображались в консоли
        Console.OutputEncoding = Encoding.UTF8;

        // Переменные storage и students: объект работы с файлом и список студентов, загруженный при запуске (ключевое слово var подставляет тип само)
        var storage = new StudentStorage();
        var students = storage.Load();

        Console.WriteLine("Список студентов");

        // Цикл while (true): меню повторяется, пока оператор return не завершит метод
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("1 — Показать список");
            Console.WriteLine("2 — Добавить студента");
            Console.WriteLine("3 — Удалить студента");
            Console.WriteLine("4 — Редактировать студента");
            Console.WriteLine("5 — Найти студентов");
            Console.WriteLine("6 — Сортировать список");
            Console.WriteLine("0 — Выход");
            Console.Write("Выберите действие: ");

            // Оператор switch: выбор действия по введённой строке; оператор ?. возвращает null, если ввод закрыт, а метод Trim убирает пробелы
            switch (Console.ReadLine()?.Trim())
            {
                case "1":
                    ShowStudents(students);
                    break;
                case "2":
                    AddStudent(students, storage);
                    break;
                case "3":
                    RemoveStudent(students, storage);
                    break;
                case "4":
                    EditStudent(students, storage);
                    break;
                case "5":
                    SearchStudents(students);
                    break;
                case "6":
                    SortStudents(students);
                    break;
                // Ветки case 0 и null: выход из программы оператором return (null означает конец ввода)
                case "0":
                case null:
                    return;
                default:
                    Console.WriteLine("Введите номер действия из меню.");
                    break;
            }
        }
    }

    // Метод AddStudent: ввод данных нового студента, добавление в список и сохранение в файл
    private static void AddStudent(List<Student> students, StudentStorage storage)
    {
        // Метод ReadRequiredText возвращает текст или null, если ввод прерван; при null оператор return завершает метод
        var fullName = ReadRequiredText("ФИО: ", MaxNameLength);
        if (fullName is null)
        {
            return;
        }

        // Переменная age типа int?: значение null означает отмену; свойство HasValue показывает, есть ли число
        var age = ReadAge("Возраст: ");
        if (!age.HasValue)
        {
            return;
        }

        var group = ReadRequiredText("Группа: ", MaxGroupLength);
        if (group is null)
        {
            return;
        }

        // Метод Add: добавление нового объекта Student, созданного оператором new с инициализатором свойств
        students.Add(new Student
        {
            FullName = fullName,
            Age = age.Value,
            Group = group
        });

        // Оператор if: сообщение об успехе выводится, только если файл сохранён
        if (storage.Save(students))
        {
            Console.WriteLine("Студент добавлен и список сохранён.");
        }
    }

    // Метод RemoveStudent: выбор студента по номеру, удаление из списка и сохранение
    private static void RemoveStudent(List<Student> students, StudentStorage storage)
    {
        // Метод SelectStudent возвращает выбранного студента или null; при null оператор return завершает метод
        var student = SelectStudent(students, "Удаление студента");
        if (student is null)
        {
            return;
        }

        // Метод Remove: удаление объекта из списка
        students.Remove(student);
        if (storage.Save(students))
        {
            Console.WriteLine("Студент удалён и список сохранён.");
        }
    }

    // Метод EditStudent: изменение данных студента; пустой ввод оставляет старое значение
    private static void EditStudent(List<Student> students, StudentStorage storage)
    {
        var student = SelectStudent(students, "Редактирование студента");
        if (student is null)
        {
            return;
        }

        // Методы ReadEditableText и ReadEditableAge показывают текущее значение в скобках и допускают пустой ввод
        var fullName = ReadEditableText($"ФИО [{student.FullName}]: ", MaxNameLength);
        if (fullName is null)
        {
            return;
        }

        var age = ReadEditableAge($"Возраст [{student.Age}]: ");
        if (!age.HasValue)
        {
            return;
        }

        var group = ReadEditableText($"Группа [{student.Group}]: ", MaxGroupLength);
        if (group is null)
        {
            return;
        }

        // Тернарный оператор ? : пустое значение оставляет прежнее, иначе записывается новое; для возраста значение 0 означает «не менять»
        student.FullName = string.IsNullOrWhiteSpace(fullName) ? student.FullName : fullName;
        student.Age = age.Value == 0 ? student.Age : age.Value;
        student.Group = string.IsNullOrWhiteSpace(group) ? student.Group : group;

        if (storage.Save(students))
        {
            Console.WriteLine("Данные студента изменены и список сохранён.");
        }
    }

    // Метод SearchStudents: поиск студентов по ФИО, группе или возрасту
    private static void SearchStudents(List<Student> students)
    {
        Console.WriteLine("Поиск: 1 — ФИО, 2 — группа, 3 — возраст");
        Console.Write("Критерий: ");
        var criterion = Console.ReadLine()?.Trim();

        // Переменная found типа IEnumerable<Student>: результат поиска; значение задаётся в одной из веток switch
        IEnumerable<Student> found;
        switch (criterion)
        {
            // Поиск по части ФИО: метод Where из LINQ с лямбда-выражением s => ... отбирает студентов без учёта регистра (OrdinalIgnoreCase)
            case "1":
            {
                var query = ReadRequiredText("Введите часть ФИО: ", MaxNameLength);
                if (query is null)
                {
                    return;
                }

                found = students.Where(s => s.FullName.Contains(query, StringComparison.OrdinalIgnoreCase));
                break;
            }
            case "2":
            {
                var query = ReadRequiredText("Введите часть названия группы: ", MaxGroupLength);
                if (query is null)
                {
                    return;
                }

                found = students.Where(s => s.Group.Contains(query, StringComparison.OrdinalIgnoreCase));
                break;
            }
            // Поиск по возрасту: оператор == сравнивает возраст с введённым числом
            case "3":
            {
                var age = ReadAge("Введите возраст: ");
                if (!age.HasValue)
                {
                    return;
                }

                found = students.Where(s => s.Age == age.Value);
                break;
            }
            // Ветка default: неверный критерий, оператор return завершает метод
            default:
                Console.WriteLine("Введите номер критерия поиска.");
                return;
        }

        ShowStudents(found.ToList());
    }

    // Метод SortStudents: показ списка, отсортированного по выбранному критерию
    private static void SortStudents(List<Student> students)
    {
        Console.WriteLine("Сортировка: 1 — ФИО, 2 — возраст, 3 — группа");
        Console.Write("Критерий: ");

        // Выражение switch: каждой ветке соответствует сортировка; OrderBy задаёт основной ключ, ThenBy дополнительный; ветка _ (null) означает неверный критерий
        IEnumerable<Student>? sorted = Console.ReadLine()?.Trim() switch
        {
            "1" => students.OrderBy(s => s.FullName, StringComparer.CurrentCultureIgnoreCase),
            "2" => students.OrderBy(s => s.Age).ThenBy(s => s.FullName, StringComparer.CurrentCultureIgnoreCase),
            "3" => students.OrderBy(s => s.Group, StringComparer.CurrentCultureIgnoreCase)
                           .ThenBy(s => s.FullName, StringComparer.CurrentCultureIgnoreCase),
            _ => null
        };

        if (sorted is null)
        {
            Console.WriteLine("Введите номер критерия сортировки.");
            return;
        }

        ShowStudents(sorted.ToList());
    }

    // Метод SelectStudent: показ списка и выбор студента по номеру; возвращает null, если список пуст или ввод отменён
    private static Student? SelectStudent(List<Student> students, string action)
    {
        if (students.Count == 0)
        {
            Console.WriteLine("Список студентов пуст.");
            return null;
        }

        Console.WriteLine(action);
        ShowStudents(students);
        var index = ReadNumberInRange("Номер студента: ", 1, students.Count);
        // Тернарный оператор ? : номер пользователя (с единицы) превращается в индекс списка (с нуля) вычитанием единицы
        return index.HasValue ? students[index.Value - 1] : null;
    }

    // Метод ShowStudents: печать нумерованного списка студентов; тип IReadOnlyList разрешает только читать список
    private static void ShowStudents(IReadOnlyList<Student> students)
    {
        if (students.Count == 0)
        {
            Console.WriteLine("Список студентов пуст.");
            return;
        }

        // Цикл for: перебор студентов по индексу; номер в списке равен i + 1
        for (var i = 0; i < students.Count; i++)
        {
            var student = students[i];
            Console.WriteLine($"{i + 1}. {student.FullName} | {student.Age} лет | группа {student.Group}");
        }
    }

    // Метод ReadRequiredText: запрашивает непустой текст не длиннее maxLength; повторяет запрос при ошибке, возвращает null при закрытом вводе
    private static string? ReadRequiredText(string prompt, int maxLength)
    {
        while (true)
        {
            Console.Write(prompt);
            var value = Console.ReadLine();
            if (value is null)
            {
                return null;
            }

            value = value.Trim();
            if (value.Length == 0)
            {
                Console.WriteLine("Значение не должно быть пустым.");
            }
            else if (value.Length > maxLength)
            {
                Console.WriteLine($"Максимальная длина: {maxLength} символов.");
            }
            else
            {
                return value;
            }
        }
    }

    // Метод ReadEditableText: то же, но пустая строка допустима (она означает «оставить прежнее»)
    private static string? ReadEditableText(string prompt, int maxLength)
    {
        while (true)
        {
            Console.Write(prompt);
            var value = Console.ReadLine();
            if (value is null)
            {
                return null;
            }

            value = value.Trim();
            if (value.Length <= maxLength)
            {
                return value;
            }

            Console.WriteLine($"Максимальная длина: {maxLength} символов.");
        }
    }

    // Метод ReadAge: запрашивает возраст от 1 до 120; тип int? позволяет вернуть null при отмене
    private static int? ReadAge(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            var value = Console.ReadLine();
            if (value is null)
            {
                return null;
            }

            // Метод int.TryParse записывает число в переменную age через out; шаблон is >= 1 and <= 120 проверяет диапазон
            if (int.TryParse(value, out var age) && age is >= 1 and <= 120)
            {
                return age;
            }

            Console.WriteLine("Введите целый возраст от 1 до 120.");
        }
    }

    // Метод ReadEditableAge: как ReadAge, но пустой ввод возвращает 0, то есть «не менять»
    private static int? ReadEditableAge(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            var value = Console.ReadLine();
            if (value is null)
            {
                return null;
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                return 0;
            }

            if (int.TryParse(value, out var age) && age is >= 1 and <= 120)
            {
                return age;
            }

            Console.WriteLine("Введите целый возраст от 1 до 120 или оставьте поле пустым.");
        }
    }

    // Метод ReadNumberInRange: запрашивает число от min до max включительно
    private static int? ReadNumberInRange(string prompt, int min, int max)
    {
        while (true)
        {
            Console.Write(prompt);
            var value = Console.ReadLine();
            if (value is null)
            {
                return null;
            }

            if (int.TryParse(value, out var number) && number >= min && number <= max)
            {
                return number;
            }

            Console.WriteLine($"Введите число от {min} до {max}.");
        }
    }
}
