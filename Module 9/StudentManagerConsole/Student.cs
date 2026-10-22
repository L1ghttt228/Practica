namespace StudentManagerConsole;

// Класс Student (модель): хранит данные одного студента; модификатор sealed запрещает наследование
public sealed class Student
{
    // Свойство FullName типа string: ФИО
    public string FullName { get; set; } = string.Empty;
    // Свойство Age типа int: возраст
    public int Age { get; set; }
    // Свойство Group типа string: название группы
    public string Group { get; set; } = string.Empty;
}
