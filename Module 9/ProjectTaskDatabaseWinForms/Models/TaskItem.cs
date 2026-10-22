namespace ProjectTaskDatabaseWinForms.Models;

// Класс TaskItem (модель): задача, относящаяся к проекту и при необходимости назначенная сотруднику
public sealed class TaskItem
{
    // Свойство Id типа Guid: уникальный идентификатор задачи
    public Guid Id { get; set; } = Guid.NewGuid();
    // Свойство ProjectId типа Guid: идентификатор проекта, к которому относится задача
    public Guid ProjectId { get; set; }
    // Свойство EmployeeId типа Guid?: идентификатор исполнителя; значение null означает, что задача не назначена
    public Guid? EmployeeId { get; set; }
    // Свойство Title типа string: название задачи
    public string Title { get; set; } = string.Empty;
    // Свойство Description типа string: описание задачи
    public string Description { get; set; } = string.Empty;
    // Свойство Progress типа int: прогресс выполнения в процентах
    public int Progress { get; set; }

    // Свойство Status только для чтения: текст статуса вычисляется по прогрессу; выражение switch выбирает его (0 - не начата, 100 - завершена, иначе в работе)
    public string Status => Progress switch
    {
        0 => "Не начата",
        100 => "Завершена",
        _ => "В работе"
    };
}
