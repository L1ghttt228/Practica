namespace TaskManager
{
    // Класс TaskItem (модель): один объект соответствует одной строке файла tasks.txt и хранит данные одной задачи
    public class TaskItem
    {
        // Автоматическое свойство Title типа string: название задачи
        public string Title { get; set; }

        // Свойство Description типа string: описание задачи
        public string Description { get; set; }

        // Свойство Status типа string: статус задачи (Новая, В работе или Выполнена)
        public string Status { get; set; }
    }
}
