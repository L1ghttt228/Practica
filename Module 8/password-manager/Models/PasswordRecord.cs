namespace PasswordManager.Models;

// Класс PasswordRecord (модель): хранит данные одной учётной записи; модификатор sealed запрещает наследование
public sealed class PasswordRecord
{
    // Свойство Id типа Guid: уникальный идентификатор записи; по нему запись находят для изменения и удаления
    public Guid Id { get; set; } = Guid.NewGuid();

    // Свойство Service типа string: название сервиса
    public string Service { get; set; } = string.Empty;

    // Свойство Login типа string: логин
    public string Login { get; set; } = string.Empty;

    // Свойство Password типа string: пароль
    public string Password { get; set; } = string.Empty;

    // Свойство Website типа string: адрес сайта
    public string Website { get; set; } = string.Empty;

    // Свойство Notes типа string: заметки
    public string Notes { get; set; } = string.Empty;

    // Свойство UpdatedAt типа DateTime: дата последнего изменения
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}
