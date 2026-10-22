using System.Windows.Forms;
using ProjectTaskDatabaseWinForms.Models;
using ProjectTaskDatabaseWinForms.Services;

namespace ProjectTaskDatabaseWinForms;

// Класс Form1 (основная часть): главное окно; общие методы загрузки, сохранения и очистки полей; модификатор partial позволяет разнести код по файлам Form1.*.cs
public partial class Form1 : Form
{
    // Поле _dataService: объект работы с файлом; запись new() вызывает конструктор, тип берётся из объявления
    private readonly TextDataService _dataService = new();
    // Поле _data: данные приложения в памяти; формы читают и меняют их, а сохранение вызывается отдельно
    private AppData _data = new();

    // Конструктор Form1: создаёт элементы окна, загружает данные и обновляет все таблицы
    public Form1()
    {
        InitializeComponent();
        LoadData();
        RefreshAll();
    }

    // Метод LoadData: чтение данных из файла; при ошибке показывается предупреждение и используются пустые данные
    private void LoadData()
    {
        try
        {
            _data = _dataService.Load();
            // Оператор if: если файла ещё нет, создаётся пустой файл
            if (!File.Exists(_dataService.FilePath))
            {
                _dataService.Save(_data);
            }
        }
        catch (Exception exception)
        {
            MessageBox.Show(
                $"Не удалось прочитать файл данных. Будет использовано пустое хранилище.\n\n{exception.Message}",
                "Ошибка чтения данных",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            _data = new AppData();
        }
    }

    // Метод SaveAndRefresh: сохранение данных в файл и обновление таблиц; ошибка записи показывается сообщением
    private void SaveAndRefresh()
    {
        try
        {
            _dataService.Save(_data);
            RefreshAll();
        }
        catch (Exception exception)
        {
            MessageBox.Show(
                $"Не удалось сохранить данные в текстовый файл.\n\n{exception.Message}",
                "Ошибка сохранения",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    // Метод ShowValidation: окно с сообщением о неверных данных; запись => задаёт тело метода одним выражением
    private void ShowValidation(string text) =>
        MessageBox.Show(text, "Проверка данных", MessageBoxButtons.OK, MessageBoxIcon.Information);

    // Методы ClearProjectFields, ClearTaskFields и ClearEmployeeFields: очистка полей ввода и снятие выделения в соответствующей таблице
    private void ClearProjectFields()
    {
        txtProjectName.Clear();
        txtProjectDescription.Clear();
        dgvProjects.ClearSelection();
    }

    private void ClearTaskFields()
    {
        txtTaskTitle.Clear();
        txtTaskDescription.Clear();
        nupTaskProgress.Value = 0;
        cmbTaskProject.SelectedIndex = -1;
        // Тернарный оператор ? : выбирается первый пункт списка (Не назначен) или -1, если список пуст
        cmbTaskEmployee.SelectedIndex = cmbTaskEmployee.Items.Count > 0 ? 0 : -1;
        dgvTasks.ClearSelection();
    }

    private void ClearEmployeeFields()
    {
        txtEmployeeName.Clear();
        txtEmployeePosition.Clear();
        dgvEmployees.ClearSelection();
    }
}
