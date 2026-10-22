namespace TaskManagerWinForms;

// Часть класса Form1 (модификатор partial позволяет разнести код по нескольким файлам): отображение таблицы и заполнение полей формы
public partial class Form1
{
    // Свойство SelectedTask только для чтения: задача из выбранной строки таблицы или null; оператор as приводит привязанный объект к типу TaskItem
    private TaskItem? SelectedTask => dataGridViewTasks.CurrentRow?.DataBoundItem as TaskItem;

    // Обработчик выбора строки: вывод данных задачи в поля формы
    private void dataGridViewTasks_SelectionChanged(object? sender, EventArgs e)
    {
        if (SelectedTask is not TaskItem task)
        {
            return;
        }

        textBoxTitle.Text = task.Title;
        textBoxDescription.Text = task.Description;
        comboBoxPriority.SelectedItem = task.Priority;
        dateTimePickerDueDate.Value = task.DueDate;
        // Метод Math.Clamp: прогресс не выходит за минимум и максимум числового поля, иначе возникло бы исключение
        numericUpDownProgress.Value = Math.Clamp(task.Progress, (int)numericUpDownProgress.Minimum, (int)numericUpDownProgress.Maximum);
    }

    // Метод RefreshTaskGrid: повторная привязка списка к таблице; значение null сначала сбрасывает источник, чтобы таблица обновилась
    private void RefreshTaskGrid()
    {
        dataGridViewTasks.DataSource = null;
        dataGridViewTasks.DataSource = tasks;
        dataGridViewTasks.ClearSelection();
    }
}
