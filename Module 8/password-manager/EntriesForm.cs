using PasswordManager.Models;
using PasswordManager.Services;

namespace PasswordManager;

// Класс EntriesForm: окно со списком записей, поиском, добавлением, изменением и удалением; модификатор partial означает, что часть кода окна лежит в EntriesForm.Designer.cs
public partial class EntriesForm : Form
{
    // Поле _database: уже открытая база, которую форма получает от контекста; освобождает её контекст при закрытии окна
    private readonly PasswordDatabaseService _database;

    // Конструктор EntriesForm: принимает базу и путь, выводит имя файла и заполняет список
    public EntriesForm(PasswordDatabaseService database, string path)
    {
        _database = database;
        InitializeComponent();
        pathLabel.Text = Path.GetFileName(path);
        RefreshEntries();
    }

    // Метод RefreshEntries: заполнение списка записями с учётом поиска по сервису и логину без учёта регистра; записи сортируются по сервису
    private void RefreshEntries()
    {
        string query = searchBox.Text.Trim();
        // Методы BeginUpdate и EndUpdate: на время заполнения список не перерисовывается, поэтому нет мерцания
        entriesList.BeginUpdate();
        entriesList.Items.Clear();
        // Цикл foreach с запросами LINQ: Where отбирает подходящие записи, OrderBy сортирует их
        foreach (PasswordRecord record in _database.Records
                     .Where(item => item.Service.Contains(query, StringComparison.CurrentCultureIgnoreCase)
                                 || item.Login.Contains(query, StringComparison.CurrentCultureIgnoreCase))
                     .OrderBy(item => item.Service, StringComparer.CurrentCultureIgnoreCase))
        {
            // Свойство Tag хранит идентификатор, а не пароль; по нему форма находит запись для изменения и удаления
            var row = new ListViewItem(record.Service) { Tag = record.Id };
            row.SubItems.Add(record.Login);
            entriesList.Items.Add(row);
        }
        entriesList.EndUpdate();
        // Интерполированная строка $: в заголовок подставляется количество записей
        titleLabel.Text = $"Ваши доступы · {_database.Records.Count}";
    }

    // Обработчик изменения текста в поле поиска: обновление списка при каждом вводе
    private void SearchBox_TextChanged(object? sender, EventArgs e) => RefreshEntries();

    // Обработчик кнопки добавления: открывает редактор, добавляет запись в базу и выделяет её в списке
    private void AddButton_Click(object? sender, EventArgs e)
    {
        // Окно редактора; значение null означает создание новой записи
        using var editor = new EntryEditorForm(null);
        // Оператор if: при отмене или отсутствии данных оператор return завершает метод; оператор || означает ИЛИ
        if (editor.ShowDialog(this) != DialogResult.OK || editor.Record is null) return;
        // Блок try-catch: ошибка сохранения показывается пользователю методом ShowError
        try
        {
            _database.Add(editor.Record);
            RefreshEntries();
            SelectRecord(editor.Record.Id);
        }
        catch (Exception exception) { ShowError("Не удалось добавить запись", exception); }
    }

    // Обработчик кнопки изменения (и двойного щелчка по списку): открывает редактор с данными выбранной записи и сохраняет результат
    private void EditButton_Click(object? sender, EventArgs e)
    {
        // Переменная selected типа PasswordRecord?: выбранная запись или null, если ничего не выбрано
        PasswordRecord? selected = GetSelectedRecord();
        if (selected is null)
        {
            // Оператор is: сообщение показывается, только если метод вызван кнопкой, а не двойным щелчком по списку
            if (sender is Button)
                MessageBox.Show(this, "Сначала выберите запись.", "Нет выбранной записи", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }
        // Окно редактора с данными выбранной записи
        using var editor = new EntryEditorForm(selected);
        if (editor.ShowDialog(this) != DialogResult.OK || editor.Record is null) return;
        try
        {
            _database.Update(editor.Record);
            RefreshEntries();
            SelectRecord(editor.Record.Id);
        }
        catch (Exception exception) { ShowError("Не удалось изменить запись", exception); }
    }

    // Обработчик кнопки удаления: удаление выбранной записи после подтверждения
    private void DeleteButton_Click(object? sender, EventArgs e)
    {
        PasswordRecord? selected = GetSelectedRecord();
        if (selected is null)
        {
            MessageBox.Show(this, "Сначала выберите запись.", "Нет выбранной записи", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }
        // Оператор if: удаление выполняется, только если пользователь нажал Да; кнопка Нет выбрана по умолчанию
        if (MessageBox.Show(this, $"Удалить запись «{selected.Service}»?", "Подтвердите удаление",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) != DialogResult.Yes) return;
        try
        {
            _database.Delete(selected.Id);
            RefreshEntries();
        }
        catch (Exception exception) { ShowError("Не удалось удалить запись", exception); }
    }

    // Метод GetSelectedRecord: возвращает запись, выбранную в списке, или null
    private PasswordRecord? GetSelectedRecord()
    {
        if (entriesList.SelectedItems.Count == 0) return null;
        // Приведение (Guid): идентификатор достаётся из свойства Tag; знак ! сообщает компилятору, что значение не null
        Guid id = (Guid)entriesList.SelectedItems[0].Tag!;
        // Метод FirstOrDefault: поиск записи по идентификатору; результат null, если записи нет
        return _database.Records.FirstOrDefault(item => item.Id == id);
    }

    // Метод SelectRecord: выделение строки с нужным идентификатором и прокрутка к ней
    private void SelectRecord(Guid id)
    {
        foreach (ListViewItem row in entriesList.Items)
        {
            // Оператор continue: строки с другим идентификатором пропускаются
            if ((Guid)row.Tag! != id) continue;
            row.Selected = true;
            // Метод EnsureVisible прокручивает список к строке; оператор break прекращает поиск
            row.EnsureVisible();
            break;
        }
    }

    // Обработчик кнопки закрытия: закрытие окна методом Close
    private void CloseButton_Click(object? sender, EventArgs e) => Close();

    // Метод ShowError: окно с сообщением об ошибке; static, потому что метод не использует поля класса
    private static void ShowError(string title, Exception exception) =>
        MessageBox.Show(title + ":\n" + exception.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
}
