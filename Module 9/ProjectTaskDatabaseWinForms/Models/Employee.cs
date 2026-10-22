namespace ProjectTaskDatabaseWinForms.Models;

// Класс Employee (модель): сотрудник, которому может быть назначена задача
public sealed class Employee
{
    // Свойство Id типа Guid: уникальный идентификатор сотрудника
    public Guid Id { get; set; } = Guid.NewGuid();
    // Свойство FullName типа string: ФИО
    public string FullName { get; set; } = string.Empty;
    // Свойство Position типа string: должность
    public string Position { get; set; } = string.Empty;
}
