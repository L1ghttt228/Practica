namespace ProjectTaskDatabaseWinForms.Models;

// Класс AppData: все данные приложения, которые сохраняются в одном текстовом файле
public sealed class AppData
{
    // Свойство Projects типа List<Project>: список проектов; запись [] создаёт пустой список
    public List<Project> Projects { get; set; } = [];
    // Свойство Tasks типа List<TaskItem>: список задач
    public List<TaskItem> Tasks { get; set; } = [];
    // Свойство Employees типа List<Employee>: список сотрудников
    public List<Employee> Employees { get; set; } = [];
}
