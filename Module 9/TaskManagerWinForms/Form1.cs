namespace TaskManagerWinForms;

// Класс Form1 (основная часть): главное окно менеджера задач; модификатор partial позволяет разнести код по файлам Form1.*.cs
public partial class Form1 : Form
{
    // Поле repository: объект работы с файлом задач
    private readonly TaskRepository repository = new();
    // Поле tasks типа List<TaskItem>: задачи в памяти; таблица показывает именно этот список
    private List<TaskItem> tasks = new();
    // Поле notificationShown типа bool (флаг): true после показа напоминания, чтобы не показывать его повторно
    private bool notificationShown;

    // Конструктор Form1: создаёт элементы окна
    public Form1()
    {
        InitializeComponent();
    }

    // Обработчик загрузки окна: чтение задач, заполнение таблицы и показ напоминания о сроках
    private void Form1_Load(object? sender, EventArgs e)
    {
        tasks = repository.Load();
        RefreshTaskGrid();
        ShowDueDateNotificationOnce();
    }
}
