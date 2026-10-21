using System.Runtime.InteropServices;
using PasswordManager.Models;
using PasswordManager.Services;

namespace PasswordManager;

// Класс EntryEditorForm: окно создания и изменения записи, генерации пароля и копирования его в буфер обмена; модификатор partial означает, что часть кода окна лежит в EntryEditorForm.Designer.cs
public partial class EntryEditorForm : Form
{
    // Поле _id типа Guid: идентификатор записи; у существующей записи остаётся прежний, у новой создаётся новый
    private readonly Guid _id;

    // Поле _clipboardTimer типа Timer: через 30 секунд (Interval задаётся в миллисекундах) очищает буфер обмена; пространство имён указано полностью, чтобы не путать с другим классом Timer
    private readonly System.Windows.Forms.Timer _clipboardTimer = new() { Interval = 30_000 };

    // Поле _lastCopiedPassword типа string?: последний скопированный пароль; нужно, чтобы очистить буфер, только если пользователь не заменил его содержимое
    private string? _lastCopiedPassword;

    // Свойство Record: результат работы окна (готовая запись или null); private set разрешает менять значение только внутри класса
    public PasswordRecord? Record { get; private set; }

    // Конструктор EntryEditorForm: параметр record равен null для новой записи, иначе поля заполняются его данными
    public EntryEditorForm(PasswordRecord? record)
    {
        InitializeComponent();
        // Операторы ?. и ??: берётся Id записи, а если записи нет, создаётся новый идентификатор
        _id = record?.Id ?? Guid.NewGuid();
        if (record is not null)
        {
            Text = "Изменить запись — менеджер паролей";
            titleLabel.Text = "Изменить запись";
            serviceBox.Text = record.Service;
            loginBox.Text = record.Login;
            passwordBox.Text = record.Password;
            websiteBox.Text = record.Website;
            notesBox.Text = record.Notes;
        }
        // Подписка += метода ClipboardTimer_Tick на срабатывание таймера
        _clipboardTimer.Tick += ClipboardTimer_Tick;
        // Лямбда-выражение: при закрытии окна таймер освобождается методом Dispose
        FormClosed += (_, _) => _clipboardTimer.Dispose();
    }

    // Метод очистки буфера обмена по таймеру: пароль удаляется, только если он остался тем же
    private void ClipboardTimer_Tick(object? sender, EventArgs e)
    {
        try
        {
            // Оператор if: очищается только пароль, скопированный этим окном, чужой текст в буфере остаётся
            if (_lastCopiedPassword is not null && Clipboard.ContainsText() && Clipboard.GetText() == _lastCopiedPassword)
                Clipboard.Clear();
        }
        // Пустой блок catch: буфер может быть временно занят другой программой, тогда очистка пропускается
        catch (ExternalException) { }
        _lastCopiedPassword = null;
        _clipboardTimer.Stop();
    }

    // Обработчик кнопки генерации: вставляет в поле случайный пароль из метода Generate
    private void GenerateButton_Click(object? sender, EventArgs e) => passwordBox.Text = PasswordGenerator.Generate();

    // Обработчик кнопки показа пароля: переключает отображение между символами и точками
    private void RevealButton_Click(object? sender, EventArgs e)
    {
        // Переменная reveal типа bool: true, если пароль сейчас скрыт и его нужно показать; символ '\0' означает обычный текст без маскировки
        bool reveal = passwordBox.PasswordChar != '\0';
        // Тернарный оператор ? : выбирает символ маскировки или его отсутствие; текст кнопки меняется в зависимости от режима
        passwordBox.PasswordChar = reveal ? '\0' : '●';
        revealButton.Text = reveal ? "Скрыть" : "Показать";
    }

    // Обработчик кнопки копирования: помещает пароль в буфер обмена и запускает таймер очистки
    private void CopyButton_Click(object? sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(passwordBox.Text)) return;
        try
        {
            // Метод Clipboard.SetText: копирование пароля в буфер обмена
            Clipboard.SetText(passwordBox.Text);
            _lastCopiedPassword = passwordBox.Text;
            // Остановка и повторный запуск таймера: отсчёт 30 секунд начинается заново
            _clipboardTimer.Stop();
            _clipboardTimer.Start();
            MessageBox.Show(this, "Пароль скопирован. Буфер обмена будет очищен через 30 секунд.", "Пароль скопирован",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        // Блок catch: если буфер недоступен, показывается сообщение об ошибке
        catch (ExternalException)
        {
            MessageBox.Show(this, "Не удалось обратиться к буферу обмена.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    // Обработчик кнопки сохранения: проверка названия сервиса и сборка записи
    private void SaveButton_Click(object? sender, EventArgs e)
    {
        string service = serviceBox.Text.Trim();
        // Оператор if: название сервиса обязательно; при ошибке показывается сообщение, а метод Focus возвращает фокус в поле
        if (service.Length == 0)
        {
            MessageBox.Show(this, "Укажите название сервиса.", "Проверьте данные", MessageBoxButtons.OK, MessageBoxIcon.Information);
            serviceBox.Focus();
            return;
        }
        // Оператор new с инициализатором: создание записи из значений полей; Trim убирает пробелы по краям (в пароле и заметках пробелы сохраняются)
        Record = new PasswordRecord
        {
            Id = _id,
            Service = service,
            Login = loginBox.Text.Trim(),
            Password = passwordBox.Text,
            Website = websiteBox.Text.Trim(),
            Notes = notesBox.Text
        };
        // Свойство DialogResult: значение OK сообщает вызывающему коду, что запись готова; метод Close закрывает окно
        DialogResult = DialogResult.OK;
        Close();
    }

    // Обработчик кнопки отмены: закрытие окна без сохранения
    private void CancelButton_Click(object? sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }
}
