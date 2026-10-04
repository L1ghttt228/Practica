using System;

namespace Task2NotificationEvents
{
    // класс-аргументы события
    class NotificationEventArgs : EventArgs
    {
        public string Message { get; private set; } // свойство текста уведомления

        // конструктор записывает переданный текст в свойство Message
        public NotificationEventArgs(string message)
        {
            Message = message; // оператор = записывает значение параметра message в свойство Message
        }
    }

    // класс Notification генерирует три типа событий уведомлений
    class Notification
    {
        // событие получения сообщения; пустой делегат позволяет вызывать событие без проверки на null
        public event EventHandler<NotificationEventArgs> MessageReceived = delegate { };

        // событие входящего звонка
        public event EventHandler<NotificationEventArgs> CallReceived = delegate { };

        // событие получения электронного письма
        public event EventHandler<NotificationEventArgs> EmailReceived = delegate { };

        // метод имитирует получение сообщения и создаёт событие
        public void SendMessage(string message)
        {
            Console.WriteLine("Пришло сообщение.");
            MessageReceived(this, new NotificationEventArgs(message)); // вызов события сообщения
        }

        // метод имитирует входящий звонок и создаёт событие
        public void MakeCall(string message)
        {
            Console.WriteLine("Входящий звонок."); 
            CallReceived(this, new NotificationEventArgs(message)); // вызов события звонка
        }

        // метод имитирует получение письма и создаёт событие
        public void SendEmail(string message)
        {
            Console.WriteLine("Пришло письмо.");
            EmailReceived(this, new NotificationEventArgs(message)); // вызов события письма
        }
    }

    class Program
    {
        // обработчик события сообщения: выводит текст сообщения
        static void OnMessage(object sender, NotificationEventArgs e)
        {
            Console.WriteLine($"SMS: {e.Message}");
        }

        // обработчик события звонка: выводит номер звонящего
        static void OnCall(object sender, NotificationEventArgs e)
        {
            Console.WriteLine($"Звонок от: {e.Message}");
        }

        // обработчик события письма: выводит адрес отправителя
        static void OnEmail(object sender, NotificationEventArgs e)
        {
            Console.WriteLine($"Письмо от: {e.Message}");
        }

        static void Main(string[] args)
        {
            Notification notification = new Notification(); // создание объекта системы уведомлений

            notification.MessageReceived += OnMessage; // оператор += подписывает обработчик OnMessage на событие сообщения
            notification.CallReceived += OnCall; // подписка обработчика OnCall на событие звонка
            notification.EmailReceived += OnEmail; // подписка обработчика OnEmail на событие письма

            notification.SendMessage("Встреча в 15:00");
            notification.MakeCall("+7 900 000-00-00"); 
            notification.SendEmail("report@company.ru"); 

            notification.CallReceived -= OnCall; // оператор -= отписывает обработчик OnCall от события звонка

            notification.MakeCall("+7 900 000-00-00"); // событие создаётся, но обработчик больше не вызывается

            Console.ReadKey();
        }
    }
}