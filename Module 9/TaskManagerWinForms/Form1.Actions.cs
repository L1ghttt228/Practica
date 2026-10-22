namespace TaskManagerWinForms;

// Часть класса Form1 (модификатор partial позволяет разнести код по нескольким файлам): обработчики кнопок и методы чтения полей формы
public partial class Form1
{
    // Обработчик кнопки добавления: проверка названия, создание задачи из полей и сохранение
    private void buttonAdd_Click(object? sender, EventArgs e)
    {
        // Оператор if: при пустом названии оператор return завершает метод
        if (!HasValidTitle())
        {
            return;
        }

        tasks.Add(CreateTaskFromForm());
        SaveAndRefresh();
    }

    // Обработчик кнопки изменения: выбранная задача получает значения из полей
    private void buttonUpdate_Click(object? sender, EventArgs e)
    {
        // Оператор is not: шаблон проверяет, что задача выбрана, и при успехе даёт переменную task; иначе показывается сообщение
        if (SelectedTask is not TaskItem task)
        {
            MessageBox.Show("Выберите задачу в таблице.", "Редактирование", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        if (!HasValidTitle())
        {
            return;
        }

        UpdateTaskFromForm(task);
        SaveAndRefresh();
    }

    // Обработчик кнопки удаления: задача удаляется из списка методом Remove
    private void buttonDelete_Click(object? sender, EventArgs e)
    {
        if (SelectedTask is not TaskItem task)
        {
            MessageBox.Show("Выберите задачу в таблице.", "Удаление", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        tasks.Remove(task);
        SaveAndRefresh();
    }

    // Метод HasValidTitle: проверка, что название не пустое; при ошибке показывает сообщение и возвращает false
    private bool HasValidTitle()
    {
        if (!string.IsNullOrWhiteSpace(textBoxTitle.Text))
        {
            return true;
        }

        MessageBox.Show("Введите название задачи.", "Проверка данных", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return false;
    }

    // Метод CreateTaskFromForm: создание новой задачи и заполнение её из полей формы
    private TaskItem CreateTaskFromForm()
    {
        TaskItem task = new();
        UpdateTaskFromForm(task);
        return task;
    }

    // Метод UpdateTaskFromForm: копирует значения полей формы в задачу; используется и при добавлении, и при изменении
    private void UpdateTaskFromForm(TaskItem task)
    {
        task.Title = textBoxTitle.Text.Trim();
        task.Description = textBoxDescription.Text.Trim();
        // Операторы ?. и ??: если приоритет не выбран, берётся Средний
        task.Priority = comboBoxPriority.SelectedItem?.ToString() ?? "Средний";
        task.DueDate = dateTimePickerDueDate.Value.Date;
        // Метод Decimal.ToInt32: значение числового поля превращается в целое число
        task.Progress = Decimal.ToInt32(numericUpDownProgress.Value);
    }

    // Метод SaveAndRefresh: сохранение задач в файл и обновление таблицы
    private void SaveAndRefresh()
    {
        repository.Save(tasks);
        RefreshTaskGrid();
    }
}
