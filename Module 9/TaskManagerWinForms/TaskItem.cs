namespace TaskManagerWinForms;

// Класс TaskItem (модель): хранит данные одной задачи
public class TaskItem
{
    // Свойство Title типа string: название задачи
    public string Title { get; set; } = string.Empty;

    // Свойство Description типа string: описание задачи
    public string Description { get; set; } = string.Empty;

    // Свойство Priority типа string: приоритет; по умолчанию Средний
    public string Priority { get; set; } = "Средний";

    // Свойство DueDate типа DateTime: срок выполнения; по умолчанию сегодняшняя дата
    public DateTime DueDate { get; set; } = DateTime.Today;

    // Свойство Progress типа int: прогресс выполнения в процентах
    public int Progress { get; set; }
}
