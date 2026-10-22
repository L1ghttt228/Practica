namespace TaskManagerWinForms;

// Часть класса Form1 (модификатор partial позволяет разнести код по нескольким файлам): напоминания о задачах, срок которых сегодня или завтра
public partial class Form1
{
    // Обработчик срабатывания таймера: повторная проверка сроков
    private void timerDueDates_Tick(object? sender, EventArgs e)
    {
        ShowDueDateNotificationOnce();
    }

    // Метод ShowDueDateNotificationOnce: показывает напоминание не более одного раза
    private void ShowDueDateNotificationOnce()
    {
        // Оператор if: если напоминание уже было, оператор return завершает метод
        if (notificationShown)
        {
            return;
        }

        DateTime today = DateTime.Today;
        DateTime tomorrow = today.AddDays(1);
        // Метод Where с лямбда-выражением: задачи со сроком сегодня или завтра; свойство Date отбрасывает время
        List<TaskItem> dueSoon = tasks.Where(task => task.DueDate.Date == today || task.DueDate.Date == tomorrow).ToList();

        // Оператор if: если таких задач нет, ничего не показывается
        if (dueSoon.Count == 0)
        {
            return;
        }

        notificationShown = true;
        // Метод string.Join: названия и сроки задач собираются в строки, разделённые переводом строки (Environment.NewLine)
        string taskNames = string.Join(Environment.NewLine, dueSoon.Select(task => $"• {task.Title} — {task.DueDate:dd.MM.yyyy}"));
        // Метод MessageBox.Show: окно с напоминанием; интерполированная строка $ подставляет список задач
        MessageBox.Show($"Срок следующих задач — сегодня или завтра:{Environment.NewLine}{Environment.NewLine}{taskNames}", "Напоминание о сроках", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
}
