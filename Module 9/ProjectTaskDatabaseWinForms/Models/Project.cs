namespace ProjectTaskDatabaseWinForms.Models;

// Класс Project (модель): проект, объединяющий рабочие задачи; модификатор sealed запрещает наследование
public sealed class Project
{
    // Свойство Id типа Guid: уникальный идентификатор; значение по умолчанию создаётся методом Guid.NewGuid
    public Guid Id { get; set; } = Guid.NewGuid();
    // Свойство Name типа string: название проекта
    public string Name { get; set; } = string.Empty;
    // Свойство Description типа string: описание проекта
    public string Description { get; set; } = string.Empty;
}
