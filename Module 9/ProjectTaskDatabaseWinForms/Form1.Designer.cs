#nullable disable

namespace ProjectTaskDatabaseWinForms;

partial class Form1
{
    private System.ComponentModel.IContainer components = null;
    private TabControl tabControlMain;
    private TabPage tabProjects;
    private TabPage tabTasks;
    private TabPage tabEmployees;
    private TabPage tabReports;

    private Label lblProjectName;
    private TextBox txtProjectName;
    private Label lblProjectDescription;
    private TextBox txtProjectDescription;
    private Button btnAddProject;
    private Button btnEditProject;
    private Button btnDeleteProject;
    private DataGridView dgvProjects;

    private Label lblTaskTitle;
    private TextBox txtTaskTitle;
    private Label lblTaskProject;
    private ComboBox cmbTaskProject;
    private Label lblTaskEmployee;
    private ComboBox cmbTaskEmployee;
    private Label lblTaskProgress;
    private NumericUpDown nupTaskProgress;
    private Label lblTaskDescription;
    private TextBox txtTaskDescription;
    private Button btnAddTask;
    private Button btnEditTask;
    private Button btnDeleteTask;
    private DataGridView dgvTasks;

    private Label lblEmployeeName;
    private TextBox txtEmployeeName;
    private Label lblEmployeePosition;
    private TextBox txtEmployeePosition;
    private Button btnAddEmployee;
    private Button btnEditEmployee;
    private Button btnDeleteEmployee;
    private DataGridView dgvEmployees;

    private Label lblProjectReport;
    private DataGridView dgvProjectReport;
    private Label lblTaskReport;
    private DataGridView dgvTaskReport;
    private Button btnRefreshReport;

    private DataGridViewTextBoxColumn colProjectName;
    private DataGridViewTextBoxColumn colProjectDescription;
    private DataGridViewTextBoxColumn colProjectStatus;
    private DataGridViewTextBoxColumn colTaskTitle;
    private DataGridViewTextBoxColumn colTaskProject;
    private DataGridViewTextBoxColumn colTaskEmployee;
    private DataGridViewTextBoxColumn colTaskProgress;
    private DataGridViewTextBoxColumn colTaskStatus;
    private DataGridViewTextBoxColumn colTaskDescription;
    private DataGridViewTextBoxColumn colEmployeeName;
    private DataGridViewTextBoxColumn colEmployeePosition;
    private DataGridViewTextBoxColumn colReportProject;
    private DataGridViewTextBoxColumn colReportProjectStatus;
    private DataGridViewTextBoxColumn colReportTaskCount;
    private DataGridViewTextBoxColumn colReportCompletedCount;
    private DataGridViewTextBoxColumn colReportAverage;
    private DataGridViewTextBoxColumn colReportTask;
    private DataGridViewTextBoxColumn colReportTaskProject;
    private DataGridViewTextBoxColumn colReportTaskEmployee;
    private DataGridViewTextBoxColumn colReportTaskProgress;
    private DataGridViewTextBoxColumn colReportTaskStatus;

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            components?.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        tabControlMain = new TabControl();
        tabProjects = new TabPage();
        lblProjectName = new Label();
        txtProjectName = new TextBox();
        lblProjectDescription = new Label();
        txtProjectDescription = new TextBox();
        btnAddProject = new Button();
        btnEditProject = new Button();
        btnDeleteProject = new Button();
        dgvProjects = new DataGridView();

        tabTasks = new TabPage();
        lblTaskTitle = new Label();
        txtTaskTitle = new TextBox();
        lblTaskProject = new Label();
        cmbTaskProject = new ComboBox();
        lblTaskEmployee = new Label();
        cmbTaskEmployee = new ComboBox();
        lblTaskProgress = new Label();
        nupTaskProgress = new NumericUpDown();
        lblTaskDescription = new Label();
        txtTaskDescription = new TextBox();
        btnAddTask = new Button();
        btnEditTask = new Button();
        btnDeleteTask = new Button();
        dgvTasks = new DataGridView();

        tabEmployees = new TabPage();
        lblEmployeeName = new Label();
        txtEmployeeName = new TextBox();
        lblEmployeePosition = new Label();
        txtEmployeePosition = new TextBox();
        btnAddEmployee = new Button();
        btnEditEmployee = new Button();
        btnDeleteEmployee = new Button();
        dgvEmployees = new DataGridView();

        tabReports = new TabPage();
        lblProjectReport = new Label();
        dgvProjectReport = new DataGridView();
        lblTaskReport = new Label();
        dgvTaskReport = new DataGridView();
        btnRefreshReport = new Button();

        colProjectName = new DataGridViewTextBoxColumn();
        colProjectDescription = new DataGridViewTextBoxColumn();
        colProjectStatus = new DataGridViewTextBoxColumn();
        colTaskTitle = new DataGridViewTextBoxColumn();
        colTaskProject = new DataGridViewTextBoxColumn();
        colTaskEmployee = new DataGridViewTextBoxColumn();
        colTaskProgress = new DataGridViewTextBoxColumn();
        colTaskStatus = new DataGridViewTextBoxColumn();
        colTaskDescription = new DataGridViewTextBoxColumn();
        colEmployeeName = new DataGridViewTextBoxColumn();
        colEmployeePosition = new DataGridViewTextBoxColumn();
        colReportProject = new DataGridViewTextBoxColumn();
        colReportProjectStatus = new DataGridViewTextBoxColumn();
        colReportTaskCount = new DataGridViewTextBoxColumn();
        colReportCompletedCount = new DataGridViewTextBoxColumn();
        colReportAverage = new DataGridViewTextBoxColumn();
        colReportTask = new DataGridViewTextBoxColumn();
        colReportTaskProject = new DataGridViewTextBoxColumn();
        colReportTaskEmployee = new DataGridViewTextBoxColumn();
        colReportTaskProgress = new DataGridViewTextBoxColumn();
        colReportTaskStatus = new DataGridViewTextBoxColumn();

        SuspendLayout();
        tabControlMain.SuspendLayout();
        tabProjects.SuspendLayout();
        tabTasks.SuspendLayout();
        tabEmployees.SuspendLayout();
        tabReports.SuspendLayout();

        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1200, 730);
        MinimumSize = new Size(900, 600);
        Name = "Form1";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Управление проектами и задачами";
        Controls.Add(tabControlMain);

        tabControlMain.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        tabControlMain.Controls.Add(tabProjects);
        tabControlMain.Controls.Add(tabTasks);
        tabControlMain.Controls.Add(tabEmployees);
        tabControlMain.Controls.Add(tabReports);
        tabControlMain.Location = new Point(12, 12);
        tabControlMain.Name = "tabControlMain";
        tabControlMain.SelectedIndex = 0;
        tabControlMain.Size = new Size(1176, 706);
        tabControlMain.TabIndex = 0;

        tabProjects.BackColor = SystemColors.Control;
        tabProjects.Controls.Add(lblProjectName);
        tabProjects.Controls.Add(txtProjectName);
        tabProjects.Controls.Add(lblProjectDescription);
        tabProjects.Controls.Add(txtProjectDescription);
        tabProjects.Controls.Add(btnAddProject);
        tabProjects.Controls.Add(btnEditProject);
        tabProjects.Controls.Add(btnDeleteProject);
        tabProjects.Controls.Add(dgvProjects);
        tabProjects.Location = new Point(4, 24);
        tabProjects.Name = "tabProjects";
        tabProjects.Padding = new Padding(3);
        tabProjects.Size = new Size(1168, 678);
        tabProjects.TabIndex = 0;
        tabProjects.Text = "Проекты";

        lblProjectName.AutoSize = true;
        lblProjectName.Location = new Point(18, 18);
        lblProjectName.Name = "lblProjectName";
        lblProjectName.Size = new Size(115, 15);
        lblProjectName.Text = "Название проекта";

        txtProjectName.Location = new Point(18, 39);
        txtProjectName.Name = "txtProjectName";
        txtProjectName.Size = new Size(350, 23);

        lblProjectDescription.AutoSize = true;
        lblProjectDescription.Location = new Point(385, 18);
        lblProjectDescription.Name = "lblProjectDescription";
        lblProjectDescription.Size = new Size(65, 15);
        lblProjectDescription.Text = "Описание";

        txtProjectDescription.Location = new Point(385, 39);
        txtProjectDescription.Multiline = true;
        txtProjectDescription.Name = "txtProjectDescription";
        txtProjectDescription.Size = new Size(461, 54);

        btnAddProject.Location = new Point(862, 39);
        btnAddProject.Name = "btnAddProject";
        btnAddProject.Size = new Size(92, 29);
        btnAddProject.Text = "Добавить";
        btnAddProject.UseVisualStyleBackColor = true;
        btnAddProject.Click += btnAddProject_Click;

        btnEditProject.Location = new Point(960, 39);
        btnEditProject.Name = "btnEditProject";
        btnEditProject.Size = new Size(92, 29);
        btnEditProject.Text = "Изменить";
        btnEditProject.UseVisualStyleBackColor = true;
        btnEditProject.Click += btnEditProject_Click;

        btnDeleteProject.Location = new Point(1058, 39);
        btnDeleteProject.Name = "btnDeleteProject";
        btnDeleteProject.Size = new Size(92, 29);
        btnDeleteProject.Text = "Удалить";
        btnDeleteProject.UseVisualStyleBackColor = true;
        btnDeleteProject.Click += btnDeleteProject_Click;

        ConfigureGrid(dgvProjects, new DataGridViewColumn[] { colProjectName, colProjectDescription, colProjectStatus }, new Point(18, 122), new Size(1132, 536));
        dgvProjects.SelectionChanged += dgvProjects_SelectionChanged;
        ConfigureColumn(colProjectName, "Name", "Название", "colProjectName");
        ConfigureColumn(colProjectDescription, "Description", "Описание", "colProjectDescription");
        ConfigureColumn(colProjectStatus, "Status", "Статус", "colProjectStatus");

        tabTasks.BackColor = SystemColors.Control;
        tabTasks.Controls.Add(lblTaskTitle);
        tabTasks.Controls.Add(txtTaskTitle);
        tabTasks.Controls.Add(lblTaskProject);
        tabTasks.Controls.Add(cmbTaskProject);
        tabTasks.Controls.Add(lblTaskEmployee);
        tabTasks.Controls.Add(cmbTaskEmployee);
        tabTasks.Controls.Add(lblTaskProgress);
        tabTasks.Controls.Add(nupTaskProgress);
        tabTasks.Controls.Add(lblTaskDescription);
        tabTasks.Controls.Add(txtTaskDescription);
        tabTasks.Controls.Add(btnAddTask);
        tabTasks.Controls.Add(btnEditTask);
        tabTasks.Controls.Add(btnDeleteTask);
        tabTasks.Controls.Add(dgvTasks);
        tabTasks.Location = new Point(4, 24);
        tabTasks.Name = "tabTasks";
        tabTasks.Padding = new Padding(3);
        tabTasks.Size = new Size(1168, 678);
        tabTasks.TabIndex = 1;
        tabTasks.Text = "Задачи";

        lblTaskTitle.AutoSize = true;
        lblTaskTitle.Location = new Point(18, 18);
        lblTaskTitle.Name = "lblTaskTitle";
        lblTaskTitle.Size = new Size(104, 15);
        lblTaskTitle.Text = "Название задачи";

        txtTaskTitle.Location = new Point(18, 39);
        txtTaskTitle.Name = "txtTaskTitle";
        txtTaskTitle.Size = new Size(232, 23);

        lblTaskProject.AutoSize = true;
        lblTaskProject.Location = new Point(270, 18);
        lblTaskProject.Name = "lblTaskProject";
        lblTaskProject.Size = new Size(51, 15);
        lblTaskProject.Text = "Проект";

        cmbTaskProject.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbTaskProject.FormattingEnabled = true;
        cmbTaskProject.Location = new Point(270, 39);
        cmbTaskProject.Name = "cmbTaskProject";
        cmbTaskProject.Size = new Size(210, 23);

        lblTaskEmployee.AutoSize = true;
        lblTaskEmployee.Location = new Point(500, 18);
        lblTaskEmployee.Name = "lblTaskEmployee";
        lblTaskEmployee.Size = new Size(79, 15);
        lblTaskEmployee.Text = "Исполнитель";

        cmbTaskEmployee.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbTaskEmployee.FormattingEnabled = true;
        cmbTaskEmployee.Location = new Point(500, 39);
        cmbTaskEmployee.Name = "cmbTaskEmployee";
        cmbTaskEmployee.Size = new Size(210, 23);

        lblTaskProgress.AutoSize = true;
        lblTaskProgress.Location = new Point(730, 18);
        lblTaskProgress.Name = "lblTaskProgress";
        lblTaskProgress.Size = new Size(79, 15);
        lblTaskProgress.Text = "Прогресс, %";

        nupTaskProgress.Location = new Point(730, 39);
        nupTaskProgress.Maximum = 100;
        nupTaskProgress.Name = "nupTaskProgress";
        nupTaskProgress.Size = new Size(120, 23);

        lblTaskDescription.AutoSize = true;
        lblTaskDescription.Location = new Point(18, 105);
        lblTaskDescription.Name = "lblTaskDescription";
        lblTaskDescription.Size = new Size(65, 15);
        lblTaskDescription.Text = "Описание";

        txtTaskDescription.Location = new Point(18, 126);
        txtTaskDescription.Multiline = true;
        txtTaskDescription.Name = "txtTaskDescription";
        txtTaskDescription.Size = new Size(700, 49);

        btnAddTask.Location = new Point(736, 126);
        btnAddTask.Name = "btnAddTask";
        btnAddTask.Size = new Size(134, 29);
        btnAddTask.Text = "Добавить";
        btnAddTask.UseVisualStyleBackColor = true;
        btnAddTask.Click += btnAddTask_Click;

        btnEditTask.Location = new Point(876, 126);
        btnEditTask.Name = "btnEditTask";
        btnEditTask.Size = new Size(134, 29);
        btnEditTask.Text = "Изменить";
        btnEditTask.UseVisualStyleBackColor = true;
        btnEditTask.Click += btnEditTask_Click;

        btnDeleteTask.Location = new Point(1016, 126);
        btnDeleteTask.Name = "btnDeleteTask";
        btnDeleteTask.Size = new Size(134, 29);
        btnDeleteTask.Text = "Удалить";
        btnDeleteTask.UseVisualStyleBackColor = true;
        btnDeleteTask.Click += btnDeleteTask_Click;

        ConfigureGrid(dgvTasks, new DataGridViewColumn[] { colTaskTitle, colTaskProject, colTaskEmployee, colTaskProgress, colTaskStatus, colTaskDescription }, new Point(18, 195), new Size(1132, 463));
        dgvTasks.SelectionChanged += dgvTasks_SelectionChanged;
        ConfigureColumn(colTaskTitle, "Title", "Задача", "colTaskTitle");
        ConfigureColumn(colTaskProject, "ProjectName", "Проект", "colTaskProject");
        ConfigureColumn(colTaskEmployee, "EmployeeName", "Исполнитель", "colTaskEmployee");
        ConfigureColumn(colTaskProgress, "Progress", "Прогресс, %", "colTaskProgress");
        ConfigureColumn(colTaskStatus, "Status", "Статус", "colTaskStatus");
        ConfigureColumn(colTaskDescription, "Description", "Описание", "colTaskDescription");

        tabEmployees.BackColor = SystemColors.Control;
        tabEmployees.Controls.Add(lblEmployeeName);
        tabEmployees.Controls.Add(txtEmployeeName);
        tabEmployees.Controls.Add(lblEmployeePosition);
        tabEmployees.Controls.Add(txtEmployeePosition);
        tabEmployees.Controls.Add(btnAddEmployee);
        tabEmployees.Controls.Add(btnEditEmployee);
        tabEmployees.Controls.Add(btnDeleteEmployee);
        tabEmployees.Controls.Add(dgvEmployees);
        tabEmployees.Location = new Point(4, 24);
        tabEmployees.Name = "tabEmployees";
        tabEmployees.Padding = new Padding(3);
        tabEmployees.Size = new Size(1168, 678);
        tabEmployees.TabIndex = 2;
        tabEmployees.Text = "Сотрудники";

        lblEmployeeName.AutoSize = true;
        lblEmployeeName.Location = new Point(18, 18);
        lblEmployeeName.Name = "lblEmployeeName";
        lblEmployeeName.Size = new Size(98, 15);
        lblEmployeeName.Text = "ФИО сотрудника";

        txtEmployeeName.Location = new Point(18, 39);
        txtEmployeeName.Name = "txtEmployeeName";
        txtEmployeeName.Size = new Size(350, 23);

        lblEmployeePosition.AutoSize = true;
        lblEmployeePosition.Location = new Point(385, 18);
        lblEmployeePosition.Name = "lblEmployeePosition";
        lblEmployeePosition.Size = new Size(69, 15);
        lblEmployeePosition.Text = "Должность";

        txtEmployeePosition.Location = new Point(385, 39);
        txtEmployeePosition.Name = "txtEmployeePosition";
        txtEmployeePosition.Size = new Size(461, 23);

        btnAddEmployee.Location = new Point(862, 39);
        btnAddEmployee.Name = "btnAddEmployee";
        btnAddEmployee.Size = new Size(92, 29);
        btnAddEmployee.Text = "Добавить";
        btnAddEmployee.UseVisualStyleBackColor = true;
        btnAddEmployee.Click += btnAddEmployee_Click;

        btnEditEmployee.Location = new Point(960, 39);
        btnEditEmployee.Name = "btnEditEmployee";
        btnEditEmployee.Size = new Size(92, 29);
        btnEditEmployee.Text = "Изменить";
        btnEditEmployee.UseVisualStyleBackColor = true;
        btnEditEmployee.Click += btnEditEmployee_Click;

        btnDeleteEmployee.Location = new Point(1058, 39);
        btnDeleteEmployee.Name = "btnDeleteEmployee";
        btnDeleteEmployee.Size = new Size(92, 29);
        btnDeleteEmployee.Text = "Удалить";
        btnDeleteEmployee.UseVisualStyleBackColor = true;
        btnDeleteEmployee.Click += btnDeleteEmployee_Click;

        ConfigureGrid(dgvEmployees, new DataGridViewColumn[] { colEmployeeName, colEmployeePosition }, new Point(18, 104), new Size(1132, 554));
        dgvEmployees.SelectionChanged += dgvEmployees_SelectionChanged;
        ConfigureColumn(colEmployeeName, "FullName", "ФИО", "colEmployeeName");
        ConfigureColumn(colEmployeePosition, "Position", "Должность", "colEmployeePosition");

        tabReports.BackColor = SystemColors.Control;
        tabReports.Controls.Add(lblProjectReport);
        tabReports.Controls.Add(dgvProjectReport);
        tabReports.Controls.Add(lblTaskReport);
        tabReports.Controls.Add(dgvTaskReport);
        tabReports.Controls.Add(btnRefreshReport);
        tabReports.Location = new Point(4, 24);
        tabReports.Name = "tabReports";
        tabReports.Padding = new Padding(3);
        tabReports.Size = new Size(1168, 678);
        tabReports.TabIndex = 3;
        tabReports.Text = "Отчёты";

        lblProjectReport.AutoSize = true;
        lblProjectReport.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        lblProjectReport.Location = new Point(18, 21);
        lblProjectReport.Name = "lblProjectReport";
        lblProjectReport.Size = new Size(142, 19);
        lblProjectReport.Text = "Статус проектов";

        btnRefreshReport.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnRefreshReport.Location = new Point(1009, 17);
        btnRefreshReport.Name = "btnRefreshReport";
        btnRefreshReport.Size = new Size(141, 29);
        btnRefreshReport.Text = "Обновить отчёт";
        btnRefreshReport.UseVisualStyleBackColor = true;
        btnRefreshReport.Click += btnRefreshReport_Click;

        ConfigureGrid(dgvProjectReport, new DataGridViewColumn[] { colReportProject, colReportProjectStatus, colReportTaskCount, colReportCompletedCount, colReportAverage }, new Point(18, 54), new Size(1132, 274));
        ConfigureColumn(colReportProject, "ProjectName", "Проект", "colReportProject");
        ConfigureColumn(colReportProjectStatus, "Status", "Статус", "colReportProjectStatus");
        ConfigureColumn(colReportTaskCount, "TaskCount", "Всего задач", "colReportTaskCount");
        ConfigureColumn(colReportCompletedCount, "CompletedCount", "Завершено", "colReportCompletedCount");
        ConfigureColumn(colReportAverage, "AverageProgress", "Средний прогресс, %", "colReportAverage");

        lblTaskReport.AutoSize = true;
        lblTaskReport.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        lblTaskReport.Location = new Point(18, 345);
        lblTaskReport.Name = "lblTaskReport";
        lblTaskReport.Size = new Size(116, 19);
        lblTaskReport.Text = "Статус задач";

        ConfigureGrid(dgvTaskReport, new DataGridViewColumn[] { colReportTask, colReportTaskProject, colReportTaskEmployee, colReportTaskProgress, colReportTaskStatus }, new Point(18, 375), new Size(1132, 283));
        ConfigureColumn(colReportTask, "Title", "Задача", "colReportTask");
        ConfigureColumn(colReportTaskProject, "ProjectName", "Проект", "colReportTaskProject");
        ConfigureColumn(colReportTaskEmployee, "EmployeeName", "Исполнитель", "colReportTaskEmployee");
        ConfigureColumn(colReportTaskProgress, "Progress", "Прогресс, %", "colReportTaskProgress");
        ConfigureColumn(colReportTaskStatus, "Status", "Статус", "colReportTaskStatus");

        tabReports.ResumeLayout(false);
        tabReports.PerformLayout();
        tabEmployees.ResumeLayout(false);
        tabEmployees.PerformLayout();
        tabTasks.ResumeLayout(false);
        tabTasks.PerformLayout();
        tabProjects.ResumeLayout(false);
        tabProjects.PerformLayout();
        tabControlMain.ResumeLayout(false);
        ResumeLayout(false);
    }

    private static void ConfigureGrid(DataGridView grid, DataGridViewColumn[] columns, Point location, Size size)
    {
        grid.AllowUserToAddRows = false;
        grid.AllowUserToDeleteRows = false;
        grid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        grid.AutoGenerateColumns = false;
        grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        grid.BackgroundColor = SystemColors.Window;
        grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        grid.Columns.AddRange(columns);
        grid.Location = location;
        grid.MultiSelect = false;
        grid.ReadOnly = true;
        grid.RowHeadersVisible = false;
        grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        grid.Size = size;
    }

    private static void ConfigureColumn(DataGridViewTextBoxColumn column, string dataPropertyName, string headerText, string name)
    {
        column.DataPropertyName = dataPropertyName;
        column.HeaderText = headerText;
        column.Name = name;
        column.ReadOnly = true;
    }
}
