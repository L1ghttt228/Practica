using System.Windows.Forms;
using ProjectTaskDatabaseWinForms.Models;

namespace ProjectTaskDatabaseWinForms;

// Часть класса Form1 (модификатор partial позволяет разнести код по нескольким файлам): отображение таблиц, списков и отчётов; вложенные классы в конце файла описывают строки таблиц
public partial class Form1
{
    // Метод RefreshAll: заполнение всех таблиц и списков по текущим данным
    private void RefreshAll()
    {
        // Запрос LINQ: Select превращает проекты в строки таблицы с вычисленным статусом, OrderBy сортирует по названию, ToList создаёт список
        BindGrid(dgvProjects, _data.Projects
            .Select(project => new ProjectGridRow
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                Status = GetProjectStatus(project.Id)
            })
            .OrderBy(project => project.Name)
            .ToList());

        // Тот же приём для задач: вместо идентификаторов в строку попадают названия проекта и исполнителя; ThenBy задаёт вторую сортировку
        BindGrid(dgvTasks, _data.Tasks
            .Select(task => new TaskGridRow
            {
                Id = task.Id,
                Title = task.Title,
                ProjectName = GetProjectName(task.ProjectId),
                EmployeeName = GetEmployeeName(task.EmployeeId),
                Description = task.Description,
                Progress = task.Progress,
                Status = task.Status
            })
            .OrderBy(task => task.ProjectName)
            .ThenBy(task => task.Title)
            .ToList());

        BindGrid(dgvEmployees, _data.Employees.OrderBy(employee => employee.FullName).ToList());
        // Вызовы методов RefreshTaskLookups и RefreshReports: обновление выпадающих списков и отчётов
        RefreshTaskLookups();
        RefreshReports();
    }

    // Метод BindGrid: привязка списка строк к таблице; сначала DataSource = null, чтобы таблица заново построила данные
    private static void BindGrid(DataGridView grid, object rows)
    {
        grid.DataSource = null;
        grid.DataSource = rows;
        grid.ClearSelection();
    }

    // Метод RefreshTaskLookups: заполнение выпадающих списков проектов и исполнителей
    private void RefreshTaskLookups()
    {
        cmbTaskProject.DataSource = _data.Projects
            .OrderBy(project => project.Name)
            .Select(project => new LookupItem(project.Id, project.Name))
            .ToList();
        // Свойства DisplayMember и ValueMember: какое поле показывать и какое использовать как значение; nameof подставляет имя свойства строкой без ошибок в написании
        cmbTaskProject.DisplayMember = nameof(LookupItem.Text);
        cmbTaskProject.ValueMember = nameof(LookupItem.Id);
        cmbTaskProject.SelectedIndex = -1;

        // Первым пунктом идёт Не назначен с идентификатором null; запись new(...) создаёт объект типа, известного из контекста
        var employees = new List<LookupItem> { new(null, "Не назначен") };
        employees.AddRange(_data.Employees
            .OrderBy(employee => employee.FullName)
            .Select(employee => new LookupItem(employee.Id, employee.FullName)));
        cmbTaskEmployee.DataSource = employees;
        cmbTaskEmployee.DisplayMember = nameof(LookupItem.Text);
        cmbTaskEmployee.ValueMember = nameof(LookupItem.Id);
        cmbTaskEmployee.SelectedIndex = 0;
    }

    // Метод RefreshReports: построение отчётов по проектам и задачам
    private void RefreshReports()
    {
        BindGrid(dgvProjectReport, _data.Projects
            .Select(project =>
            {
                // Переменная projectTasks: задачи текущего проекта; из них считаются количество, завершённые и средний прогресс
                List<TaskItem> projectTasks = _data.Tasks.Where(task => task.ProjectId == project.Id).ToList();
                return new ProjectReportRow
                {
                    ProjectName = project.Name,
                    Status = GetProjectStatus(projectTasks),
                    TaskCount = projectTasks.Count,
                    CompletedCount = projectTasks.Count(task => task.Progress == 100),
                    // Тернарный оператор ? : для проекта без задач средний прогресс равен 0, иначе берётся округлённое среднее (Math.Round)
                    AverageProgress = projectTasks.Count == 0 ? 0 : (int)Math.Round(projectTasks.Average(task => task.Progress))
                };
            })
            .OrderBy(row => row.ProjectName)
            .ToList());

        BindGrid(dgvTaskReport, _data.Tasks
            .Select(task => new TaskReportRow
            {
                Title = task.Title,
                ProjectName = GetProjectName(task.ProjectId),
                EmployeeName = GetEmployeeName(task.EmployeeId),
                Progress = task.Progress,
                Status = task.Status
            })
            .OrderBy(row => row.ProjectName)
            .ThenBy(row => row.Title)
            .ToList());
    }

    // Обработчик кнопки обновления отчёта
    private void btnRefreshReport_Click(object? sender, EventArgs e) => RefreshReports();

    // Метод GetProjectName: название проекта по идентификатору; оператор ?? подставляет Удалённый проект, если проекта нет
    private string GetProjectName(Guid projectId) =>
        _data.Projects.FirstOrDefault(project => project.Id == projectId)?.Name ?? "Удалённый проект";

    // Метод GetEmployeeName: ФИО исполнителя или Не назначен
    private string GetEmployeeName(Guid? employeeId) =>
        employeeId is null
            ? "Не назначен"
            : _data.Employees.FirstOrDefault(employee => employee.Id == employeeId.Value)?.FullName ?? "Не назначен";

    // Метод GetProjectStatus (перегрузка): статус проекта по его идентификатору
    private string GetProjectStatus(Guid projectId) =>
        GetProjectStatus(_data.Tasks.Where(task => task.ProjectId == projectId).ToList());

    // Метод GetProjectStatus: статус по задачам; метод All проверяет условие для каждой задачи
    private static string GetProjectStatus(IReadOnlyCollection<TaskItem> tasks)
    {
        if (tasks.Count == 0) return "Нет задач";
        if (tasks.All(task => task.Progress == 100)) return "Завершен";
        if (tasks.All(task => task.Progress == 0)) return "Не начат";
        return "В работе";
    }

    // Класс LookupItem с основным конструктором (Guid? id, string text): пункт выпадающего списка; модификатор sealed запрещает наследование
    private sealed class LookupItem(Guid? id, string text)
    {
        public Guid? Id { get; } = id;
        public string Text { get; } = text;
    }

    // Классы ProjectGridRow, TaskGridRow, ProjectReportRow и TaskReportRow: строки таблиц; свойства init можно задать только при создании объекта
    private sealed class ProjectGridRow
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public string Status { get; init; } = string.Empty;
    }

    private sealed class TaskGridRow
    {
        public Guid Id { get; init; }
        public string Title { get; init; } = string.Empty;
        public string ProjectName { get; init; } = string.Empty;
        public string EmployeeName { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public int Progress { get; init; }
        public string Status { get; init; } = string.Empty;
    }

    private sealed class ProjectReportRow
    {
        public string ProjectName { get; init; } = string.Empty;
        public string Status { get; init; } = string.Empty;
        public int TaskCount { get; init; }
        public int CompletedCount { get; init; }
        public int AverageProgress { get; init; }
    }

    private sealed class TaskReportRow
    {
        public string Title { get; init; } = string.Empty;
        public string ProjectName { get; init; } = string.Empty;
        public string EmployeeName { get; init; } = string.Empty;
        public int Progress { get; init; }
        public string Status { get; init; } = string.Empty;
    }
}
