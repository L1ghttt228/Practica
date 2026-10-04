using System; 
using System.Collections.Generic; 

namespace Task3TaskManager 
{
    // объявление делегата: описывает методы, принимающие название задачи и не возвращающие значение
    delegate void TaskExecutor(string taskName);

    // класс задачи хранит название и выбранный делегат для выполнения
    class TaskItem
    {
        private string name; 
        private TaskExecutor executor; // закрытое поле-делегат: хранит ссылку на выбранный метод выполнения

        // конструктор принимает название задачи и делегат и записывает их в поля объекта
        public TaskItem(string name, TaskExecutor executor)
        {
            this.name = name; 
            this.executor = executor; 
                }

        // метод Run запускает выполнение задачи через делегат
        public void Run()
        {
            executor(name); // вызов делегата
        }
    }

    class Program
    {
        // действие "отправка уведомления": выводит сообщение о задаче
        static void SendNotification(string taskName)
        {
            Console.WriteLine($"Уведомление: задача \"{taskName}\" запущена."); 
        }

        // действие "запись в журнал": выводит запись с текущим временем
        static void WriteToLog(string taskName)
        {
            Console.WriteLine($"Журнал: {DateTime.Now}: задача \"{taskName}\" запущена."); 
        }

        static void Main(string[] args)
        {
            List<TaskItem> tasks = new List<TaskItem>(); // List — динамический список: элементы добавляются методом Add

            tasks.Add(new TaskItem("Подготовить отчёт", SendNotification)); 
            tasks.Add(new TaskItem("Обновить базу данных", WriteToLog)); 

            TaskExecutor both = SendNotification; // создание делегата, привязанного к методу уведомления
            both += WriteToLog; // оператор += добавляет второй метод в список вызовов делегата
            tasks.Add(new TaskItem("Создать резервную копию", both)); 

            // цикл foreach: переменная task по очереди принимает каждый элемент списка tasks
            foreach (TaskItem task in tasks)
            {
                task.Run(); // вызов метода Run: задача выполняется выбранным для неё делегатом
            }

            Console.ReadKey();
        }
    }
}