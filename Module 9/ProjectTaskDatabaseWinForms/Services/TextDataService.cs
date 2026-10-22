using ProjectTaskDatabaseWinForms.Models;

namespace ProjectTaskDatabaseWinForms.Services;

// Класс TextDataService: сохранение и загрузка данных в текстовый файл; строка начинается с буквы типа записи: P - проект, E - сотрудник, T - задача
public sealed class TextDataService
{
    // Свойство FilePath только для чтения: путь к файлу рядом с приложением (AppContext.BaseDirectory)
    public string FilePath { get; } = Path.Combine(AppContext.BaseDirectory, "project-task-data.txt");

    // Метод Load: читает файл построчно и собирает объект AppData; некорректные строки пропускаются
    public AppData Load()
    {
        // Переменная data: результат загрузки; если файла нет, остаётся пустой
        var data = new AppData();
        if (!File.Exists(FilePath)) return data;

        // Цикл foreach: чтение файла по одной строке; метод Split делит строку по символу | на поля
        foreach (string line in File.ReadLines(FilePath))
        {
            string[] fields = line.Split('|');
            // Метод Guid.TryParse проверяет, что второе поле является идентификатором; оператор continue пропускает некорректную строку
            if (fields.Length < 2 || !Guid.TryParse(fields[1], out Guid id)) continue;

            // Оператор switch с условиями when: тип записи определяет первое поле, а условие проверяет число полей
            switch (fields[0])
            {
                case "P" when fields.Length == 4:
                    data.Projects.Add(new Project { Id = id, Name = fields[2], Description = fields[3] });
                    break;
                case "E" when fields.Length == 4:
                    data.Employees.Add(new Employee { Id = id, FullName = fields[2], Position = fields[3] });
                    break;
                // Для задачи дополнительно проверяются идентификатор проекта и прогресс; ключевое слово out объявляет переменные прямо в условии
                case "T" when fields.Length == 7
                    && Guid.TryParse(fields[2], out Guid projectId)
                    && int.TryParse(fields[6], out int progress):
                    // Вложенный тернарный оператор ? : пустое поле или неверный идентификатор дают null (задача без исполнителя)
                    Guid? employeeId = string.IsNullOrEmpty(fields[3]) ? null
                        : Guid.TryParse(fields[3], out Guid value) ? value : null;
                    data.Tasks.Add(new TaskItem
                    {
                        Id = id,
                        ProjectId = projectId,
                        EmployeeId = employeeId,
                        Title = fields[4],
                        Description = fields[5],
                        // Метод Math.Clamp ограничивает прогресс диапазоном от 0 до 100
                        Progress = Math.Clamp(progress, 0, 100)
                    });
                    break;
            }
        }

        return data;
    }

    // Метод Save: превращает данные в строки и записывает их в файл целиком
    public void Save(AppData data)
    {
        var lines = new List<string>();
        // Метод AddRange с запросом Select: каждый объект превращается в строку; интерполированная строка $ собирает поля через символ |
        lines.AddRange(data.Projects.Select(project =>
            $"P|{project.Id}|{Clean(project.Name)}|{Clean(project.Description)}"));
        lines.AddRange(data.Employees.Select(employee =>
            $"E|{employee.Id}|{Clean(employee.FullName)}|{Clean(employee.Position)}"));
        lines.AddRange(data.Tasks.Select(task =>
            $"T|{task.Id}|{task.ProjectId}|{task.EmployeeId}|{Clean(task.Title)}|{Clean(task.Description)}|{task.Progress}"));
        // Метод File.WriteAllLines: запись всех строк в файл
        File.WriteAllLines(FilePath, lines);
    }

    // Метод Clean: заменяет символы |, перевод строки и возврат каретки пробелами, чтобы не нарушить формат файла; оператор ?? подставляет пустую строку вместо null
    private static string Clean(string? value) => (value ?? string.Empty)
        .Replace('|', ' ')
        .Replace('\r', ' ')
        .Replace('\n', ' ');
}
