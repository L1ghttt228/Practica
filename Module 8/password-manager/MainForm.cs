using PasswordManager.Services;

namespace PasswordManager;

// Класс MainForm: стартовое окно с кнопками создания, открытия и удаления базы; модификатор partial означает, что часть кода окна лежит в MainForm.Designer.cs
public partial class MainForm : Form
{
    // Поле _applicationContext: ссылка на контекст приложения, через которую выполняется переход к списку записей; readonly запрещает менять ссылку после создания
    private readonly PasswordManagerContext _applicationContext;

    // Конструктор MainForm: запоминает контекст и создаёт элементы окна методом InitializeComponent
    public MainForm(PasswordManagerContext applicationContext)
    {
        _applicationContext = applicationContext;
        InitializeComponent();
    }

    // Обработчик кнопки создания базы: выбор файла, ввод мастер-пароля, создание базы и переход к записям
    private void CreateButton_Click(object? sender, EventArgs e)
    {
        // Объявление using var: диалог сохранения закрывается автоматически при выходе из метода; инициализатор объекта задаёт заголовок, фильтр и расширение
        using var fileDialog = new SaveFileDialog
        {
            Title = "Создать зашифрованную базу",
            Filter = "База KeePass (*.kdbx)|*.kdbx",
            DefaultExt = "kdbx",
            AddExtension = true,
            FileName = "Мои пароли.kdbx",
            OverwritePrompt = true
        };
        // Оператор if: если пользователь отменил выбор файла, оператор return завершает метод
        if (fileDialog.ShowDialog(this) != DialogResult.OK) return;

        // Окно ввода мастер-пароля в режиме создания (именованный аргумент isCreation: true)
        using var passwordDialog = new MasterPasswordDialog(isCreation: true);
        if (passwordDialog.ShowDialog(this) != DialogResult.OK) return;

        // Блок try-catch: при ошибке создания базы пользователю показывается сообщение методом ShowError
        try
        {
            var database = new PasswordDatabaseService();
            // Вызов метода Create: создание файла базы с мастер-паролем из диалога
            database.Create(fileDialog.FileName, passwordDialog.MasterPassword);
            // Вызов метода ShowEntries: закрытие меню и показ списка записей
            _applicationContext.ShowEntries(this, database, fileDialog.FileName);
        }
        catch (Exception exception)
        {
            ShowError("Не удалось создать базу паролей", exception);
        }
    }

    // Обработчик кнопки открытия базы: выбор файла, ввод мастер-пароля, открытие базы и переход к записям
    private void OpenButton_Click(object? sender, EventArgs e)
    {
        // Диалог открытия файла; свойство CheckFileExists требует выбрать существующий файл
        using var fileDialog = new OpenFileDialog
        {
            Title = "Открыть базу паролей",
            Filter = "База KeePass (*.kdbx)|*.kdbx",
            CheckFileExists = true
        };
        if (fileDialog.ShowDialog(this) != DialogResult.OK) return;

        // Окно ввода мастер-пароля в режиме открытия
        using var passwordDialog = new MasterPasswordDialog(isCreation: false);
        if (passwordDialog.ShowDialog(this) != DialogResult.OK) return;

        try
        {
            var database = new PasswordDatabaseService();
            database.Open(fileDialog.FileName, passwordDialog.MasterPassword);
            _applicationContext.ShowEntries(this, database, fileDialog.FileName);
        }
        // Блок catch: чаще всего ошибка означает неверный мастер-пароль или повреждённый файл, поэтому сообщение подсказывает это
        catch (Exception exception)
        {
            MessageBox.Show(this,
                "Базу не удалось открыть. Проверьте мастер-пароль и целостность выбранного файла.\n\n" + exception.Message,
                "Не удалось открыть базу", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    // Обработчик кнопки удаления файла базы: выбор файла, подтверждение и удаление
    private void DeleteButton_Click(object? sender, EventArgs e)
    {
        using var fileDialog = new OpenFileDialog
        {
            Title = "Выберите базу для удаления",
            Filter = "База KeePass (*.kdbx)|*.kdbx",
            CheckFileExists = true
        };
        if (fileDialog.ShowDialog(this) != DialogResult.OK) return;

        string path = fileDialog.FileName;
        // Окно подтверждения; кнопка Нет (Button2) выбрана по умолчанию, чтобы файл не удалился случайно
        DialogResult confirmation = MessageBox.Show(this,
            $"Файл будет удалён без возможности восстановления:\n\n{Path.GetFileName(path)}\n\nУдалить его?",
            "Подтвердите удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Warning,
            MessageBoxDefaultButton.Button2);
        if (confirmation != DialogResult.Yes) return;

        try
        {
            // Метод File.Delete: необратимое удаление файла
            File.Delete(path);
            MessageBox.Show(this, "Файл базы удалён.", "Удаление завершено", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception exception)
        {
            ShowError("Не удалось удалить файл", exception);
        }
    }

    // Метод ShowError: окно с сообщением об ошибке; static, потому что метод не использует поля класса
    private static void ShowError(string title, Exception exception) =>
        MessageBox.Show(title + ":\n" + exception.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
}
