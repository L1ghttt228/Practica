namespace TaskManagerWinForms;

// Класс TaskRepository: чтение и запись задач в файл tasks.txt рядом с приложением; модификатор sealed запрещает наследование
public sealed class TaskRepository
{
    // Поле filePath: путь к файлу (метод Path.Combine склеивает папку и имя); readonly запрещает менять путь
    private readonly string filePath = Path.Combine(AppContext.BaseDirectory, "tasks.txt");

    // Метод Load: читает задачи из файла; если файла нет или чтение не удалось, возвращает пустой список
    public List<TaskItem> Load()
    {
        if (!File.Exists(filePath))
        {
            return new List<TaskItem>();
        }

        // Переменная tasks: список для прочитанных задач; запись new() создаёт объект, тип берётся из объявления
        List<TaskItem> tasks = new();
        try
        {
            foreach (string line in File.ReadLines(filePath))
            {
                // Вызов метода TryParse: строка превращается в задачу, а некорректные строки пропускаются
                if (TaskTextFormat.TryParse(line, out TaskItem task))
                {
                    tasks.Add(task);
                }
            }
        }
        // Блок catch: при ошибке чтения возвращается пустой список
        catch (IOException)
        {
            return new List<TaskItem>();
        }

        return tasks;
    }

    // Метод Save: записывает все задачи в файл; метод Select с методом Format превращает каждую задачу в строку
    public void Save(IEnumerable<TaskItem> tasks)
    {
        File.WriteAllLines(filePath, tasks.Select(TaskTextFormat.Format));
    }
}
