using ProjectTaskDatabaseWinForms.Models;

namespace ProjectTaskDatabaseWinForms;

// Часть класса Form1 (модификатор partial позволяет разнести код по нескольким файлам): обработчики и методы вкладки проектов
public partial class Form1
{
    // Обработчик кнопки добавления проекта: проверка названия, добавление и сохранение
    private void btnAddProject_Click(object? sender, EventArgs e)
    {
        string name = txtProjectName.Text.Trim();
        // Оператор if: название обязательно; метод IsNullOrWhiteSpace ловит пустую строку и одни пробелы
        if (string.IsNullOrWhiteSpace(name))
        {
            ShowValidation("Введите название проекта.");
            return;
        }

        // Метод Add: добавление нового проекта, созданного оператором new с инициализатором
        _data.Projects.Add(new Project
        {
            Name = name,
            Description = txtProjectDescription.Text.Trim()
        });
        // Вызов метода SaveAndRefresh: запись в файл и обновление таблиц
        SaveAndRefresh();
        ClearProjectFields();
    }

    // Обработчик кнопки изменения проекта: выбранная строка получает новые значения из полей
    private void btnEditProject_Click(object? sender, EventArgs e)
    {
        // Переменная selected типа ProjectGridRow?: выбранная строка таблицы или null, если ничего не выбрано
        ProjectGridRow? selected = GetSelectedProjectRow();
        if (selected is null)
        {
            ShowValidation("Выберите проект в таблице.");
            return;
        }

        string name = txtProjectName.Text.Trim();
        if (string.IsNullOrWhiteSpace(name))
        {
            ShowValidation("Введите название проекта.");
            return;
        }

        // Метод FirstOrDefault с лямбда-выражением: поиск по идентификатору выбранной строки; результат null, если записи нет
        Project? project = _data.Projects.FirstOrDefault(item => item.Id == selected.Id);
        if (project is null) return;

        project.Name = name;
        project.Description = txtProjectDescription.Text.Trim();
        SaveAndRefresh();
        ClearProjectFields();
    }

    // Обработчик кнопки удаления проекта: вместе с проектом удаляются его задачи
    private void btnDeleteProject_Click(object? sender, EventArgs e)
    {
        ProjectGridRow? selected = GetSelectedProjectRow();
        if (selected is null)
        {
            ShowValidation("Выберите проект в таблице.");
            return;
        }

        // Метод RemoveAll с условием: удаление всех задач проекта, чтобы не осталось задач без проекта
        _data.Tasks.RemoveAll(task => task.ProjectId == selected.Id);
        _data.Projects.RemoveAll(project => project.Id == selected.Id);
        SaveAndRefresh();
        ClearProjectFields();
        ClearTaskFields();
    }

    // Обработчик выбора строки в таблице проектов: вывод её данных в поля ввода
    private void dgvProjects_SelectionChanged(object? sender, EventArgs e)
    {
        ProjectGridRow? selected = GetSelectedProjectRow();
        if (selected is null) return;

        txtProjectName.Text = selected.Name;
        txtProjectDescription.Text = selected.Description;
    }

    // Метод GetSelectedProjectRow: выбранная строка таблицы; оператор as приводит привязанный объект к типу ProjectGridRow
    private ProjectGridRow? GetSelectedProjectRow() =>
        dgvProjects.SelectedRows.Count == 0 ? null : dgvProjects.SelectedRows[0].DataBoundItem as ProjectGridRow;
}
