using ProjectTaskDatabaseWinForms.Models;

namespace ProjectTaskDatabaseWinForms;

// Часть класса Form1 (модификатор partial позволяет разнести код по нескольким файлам): обработчики и методы вкладки сотрудников
public partial class Form1
{
    // Обработчик кнопки добавления сотрудника: проверка ФИО, добавление и сохранение
    private void btnAddEmployee_Click(object? sender, EventArgs e)
    {
        string fullName = txtEmployeeName.Text.Trim();
        // Оператор if: ФИО обязательно
        if (string.IsNullOrWhiteSpace(fullName))
        {
            ShowValidation("Введите ФИО сотрудника.");
            return;
        }

        _data.Employees.Add(new Employee
        {
            FullName = fullName,
            Position = txtEmployeePosition.Text.Trim()
        });
        SaveAndRefresh();
        ClearEmployeeFields();
    }

    // Обработчик кнопки изменения сотрудника: выбранная строка получает новые значения
    private void btnEditEmployee_Click(object? sender, EventArgs e)
    {
        // Переменная selected типа Employee?: выбранный сотрудник или null
        Employee? selected = GetSelectedEmployee();
        if (selected is null)
        {
            ShowValidation("Выберите сотрудника в таблице.");
            return;
        }

        string fullName = txtEmployeeName.Text.Trim();
        if (string.IsNullOrWhiteSpace(fullName))
        {
            ShowValidation("Введите ФИО сотрудника.");
            return;
        }

        // Метод FirstOrDefault с лямбда-выражением: поиск по идентификатору выбранной строки; результат null, если записи нет
        Employee? employee = _data.Employees.FirstOrDefault(item => item.Id == selected.Id);
        if (employee is null) return;

        employee.FullName = fullName;
        employee.Position = txtEmployeePosition.Text.Trim();
        SaveAndRefresh();
        ClearEmployeeFields();
    }

    // Обработчик кнопки удаления сотрудника: у его задач исполнитель сбрасывается в null, а сами задачи остаются
    private void btnDeleteEmployee_Click(object? sender, EventArgs e)
    {
        Employee? selected = GetSelectedEmployee();
        if (selected is null)
        {
            ShowValidation("Выберите сотрудника в таблице.");
            return;
        }

        // Цикл foreach с методом Where: перебор задач удаляемого сотрудника
        foreach (TaskItem task in _data.Tasks.Where(task => task.EmployeeId == selected.Id))
        {
            task.EmployeeId = null;
        }
        _data.Employees.RemoveAll(employee => employee.Id == selected.Id);
        SaveAndRefresh();
        ClearEmployeeFields();
        ClearTaskFields();
    }

    // Обработчик выбора строки в таблице сотрудников: вывод данных в поля ввода
    private void dgvEmployees_SelectionChanged(object? sender, EventArgs e)
    {
        Employee? selected = GetSelectedEmployee();
        if (selected is null) return;

        txtEmployeeName.Text = selected.FullName;
        txtEmployeePosition.Text = selected.Position;
    }

    // Метод GetSelectedEmployee: выбранный сотрудник или null
    private Employee? GetSelectedEmployee() =>
        dgvEmployees.SelectedRows.Count == 0 ? null : dgvEmployees.SelectedRows[0].DataBoundItem as Employee;
}
