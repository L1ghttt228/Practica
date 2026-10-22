using System.Windows.Forms;
using ProjectTaskDatabaseWinForms.Models;

namespace ProjectTaskDatabaseWinForms;

// Часть класса Form1 (модификатор partial позволяет разнести код по нескольким файлам): обработчики и методы вкладки задач
public partial class Form1
{
    // Обработчик кнопки добавления задачи: данные берутся из полей методом TryGetTaskInput
    private void btnAddTask_Click(object? sender, EventArgs e)
    {
        // Метод TryGetTaskInput возвращает true, если данные верны, и передаёт значения полей через параметры out; при ошибке оператор return завершает метод
        if (!TryGetTaskInput(out string title, out string description, out Guid projectId, out Guid? employeeId, out int progress)) return;

        // Метод Add: добавление задачи, созданной оператором new с инициализатором
        _data.Tasks.Add(new TaskItem
        {
            Title = title,
            Description = description,
            ProjectId = projectId,
            EmployeeId = employeeId,
            Progress = progress
        });
        SaveAndRefresh();
        ClearTaskFields();
    }

    // Обработчик кнопки изменения задачи: выбранная задача получает новые значения
    private void btnEditTask_Click(object? sender, EventArgs e)
    {
        // Переменная selected типа TaskGridRow?: выбранная строка таблицы задач или null
        TaskGridRow? selected = GetSelectedTaskRow();
        if (selected is null)
        {
            ShowValidation("Выберите задачу в таблице.");
            return;
        }

        if (!TryGetTaskInput(out string title, out string description, out Guid projectId, out Guid? employeeId, out int progress)) return;

        // Метод FirstOrDefault с лямбда-выражением: поиск по идентификатору выбранной строки; результат null, если записи нет
        TaskItem? task = _data.Tasks.FirstOrDefault(item => item.Id == selected.Id);
        if (task is null) return;

        task.Title = title;
        task.Description = description;
        task.ProjectId = projectId;
        task.EmployeeId = employeeId;
        task.Progress = progress;
        SaveAndRefresh();
        ClearTaskFields();
    }

    // Обработчик кнопки удаления задачи
    private void btnDeleteTask_Click(object? sender, EventArgs e)
    {
        TaskGridRow? selected = GetSelectedTaskRow();
        if (selected is null)
        {
            ShowValidation("Выберите задачу в таблице.");
            return;
        }

        _data.Tasks.RemoveAll(task => task.Id == selected.Id);
        SaveAndRefresh();
        ClearTaskFields();
    }

    // Обработчик выбора строки в таблице задач: вывод данных в поля ввода и выбор проекта и исполнителя в списках
    private void dgvTasks_SelectionChanged(object? sender, EventArgs e)
    {
        TaskGridRow? selected = GetSelectedTaskRow();
        if (selected is null) return;

        TaskItem? task = _data.Tasks.FirstOrDefault(item => item.Id == selected.Id);
        if (task is null) return;

        txtTaskTitle.Text = task.Title;
        txtTaskDescription.Text = task.Description;
        nupTaskProgress.Value = task.Progress;
        SelectLookup(cmbTaskProject, task.ProjectId);
        SelectLookup(cmbTaskEmployee, task.EmployeeId);
    }

    // Метод TryGetTaskInput: чтение и проверка полей формы; значения возвращаются через параметры out, результат true означает успех
    private bool TryGetTaskInput(out string title, out string description, out Guid projectId, out Guid? employeeId, out int progress)
    {
        title = txtTaskTitle.Text.Trim();
        description = txtTaskDescription.Text.Trim();
        projectId = Guid.Empty;
        employeeId = null;
        // Метод decimal.ToInt32: значение числового поля превращается в целое число
        progress = decimal.ToInt32(nupTaskProgress.Value);

        if (string.IsNullOrWhiteSpace(title))
        {
            ShowValidation("Введите название задачи.");
            return false;
        }

        // Переменная selectedProjectId типа Guid?: выбранный проект или null; метод Any проверяет, что такой проект существует
        Guid? selectedProjectId = GetSelectedLookupId(cmbTaskProject);
        if (selectedProjectId is null || !_data.Projects.Any(project => project.Id == selectedProjectId.Value))
        {
            ShowValidation("Выберите проект для задачи.");
            return false;
        }

        projectId = selectedProjectId.Value;
        employeeId = GetSelectedLookupId(cmbTaskEmployee);
        return true;
    }

    // Метод GetSelectedLookupId: идентификатор выбранного пункта списка; оператор ?. возвращает null, если ничего не выбрано
    private static Guid? GetSelectedLookupId(ComboBox comboBox) =>
        (comboBox.SelectedItem as LookupItem)?.Id;

    // Метод SelectLookup: выбор в списке пункта с нужным идентификатором; оператор is проверяет тип пункта
    private static void SelectLookup(ComboBox comboBox, Guid? id)
    {
        for (int index = 0; index < comboBox.Items.Count; index++)
        {
            if (comboBox.Items[index] is LookupItem item && item.Id == id)
            {
                comboBox.SelectedIndex = index;
                return;
            }
        }
    }

    // Метод GetSelectedTaskRow: выбранная строка таблицы задач или null
    private TaskGridRow? GetSelectedTaskRow() =>
        dgvTasks.SelectedRows.Count == 0 ? null : dgvTasks.SelectedRows[0].DataBoundItem as TaskGridRow;
}
