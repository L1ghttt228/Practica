using System.Globalization;
using System.Text;

namespace TaskManagerWinForms;

// Статический класс TaskTextFormat: превращает задачу в строку файла и обратно; поля разделены символом |, а спецсимволы внутри текста экранируются обратной косой чертой
internal static class TaskTextFormat
{
    // Метод Format: собирает строку из полей задачи методом string.Join; InvariantCulture делает формат даты и числа независимым от настроек Windows
    public static string Format(TaskItem task)
    {
        return string.Join("|", Escape(task.Title), Escape(task.Description), Escape(task.Priority),
            task.DueDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
            task.Progress.ToString(CultureInfo.InvariantCulture));
    }

    // Метод TryParse: разбирает строку в задачу; возвращает false, если полей не пять или дата и прогресс неверны
    public static bool TryParse(string line, out TaskItem task)
    {
        // Параметр out должен получить значение до выхода из метода; знак ! подавляет предупреждение о null
        task = null!;
        // Вызов собственного метода Split: деление строки на поля с учётом экранирования
        List<string> fields = Split(line);
        // Оператор if с оператором ||: строка отвергается, если полей не пять или дата либо прогресс не разбираются (TryParseExact требует формат yyyy-MM-dd)
        if (fields.Count != 5 ||
            !DateTime.TryParseExact(fields[3], "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dueDate) ||
            !int.TryParse(fields[4], NumberStyles.Integer, CultureInfo.InvariantCulture, out int progress))
        {
            return false;
        }

        // Оператор new с инициализатором: создание задачи из разобранных полей
        task = new TaskItem
        {
            Title = fields[0],
            Description = fields[1],
            Priority = fields[2],
            DueDate = dueDate,
            Progress = progress
        };
        return true;
    }

    // Метод Escape: экранирование обратной косой черты, символа |, перевода строки и возврата каретки, чтобы они не нарушали формат
    private static string Escape(string value)
    {
        return value.Replace("\\", "\\\\").Replace("|", "\\|").Replace("\r", "\\r").Replace("\n", "\\n");
    }

    // Метод Split: посимвольное деление строки на поля; символ после обратной косой черты считается частью текста
    private static List<string> Split(string line)
    {
        List<string> fields = new();
        // Класс StringBuilder: накапливает символы текущего поля; удобнее и быстрее, чем склеивать строки
        StringBuilder value = new();
        // Переменная escaped типа bool (флаг): true, если предыдущий символ был обратной косой чертой
        bool escaped = false;

        // Цикл foreach: перебор символов строки
        foreach (char character in line)
        {
            if (escaped)
            {
                // Выражение switch: после косой черты n и r превращаются в перевод строки и возврат каретки, остальные символы остаются как есть
                value.Append(character switch { 'n' => '\n', 'r' => '\r', _ => character });
                escaped = false;
            }
            else if (character == '\\')
            {
                escaped = true;
            }
            // Символ | означает конец поля: поле добавляется в список, а накопитель очищается
            else if (character == '|')
            {
                fields.Add(value.ToString());
                value.Clear();
            }
            else
            {
                value.Append(character);
            }
        }

        // Если строка закончилась на косую черту, она сохраняется как обычный символ
        if (escaped)
        {
            value.Append('\\');
        }

        fields.Add(value.ToString());
        return fields;
    }
}
