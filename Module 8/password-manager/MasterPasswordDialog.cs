namespace PasswordManager;

// Класс MasterPasswordDialog: диалог ввода мастер-пароля; при создании базы требует подтверждение и длину не менее 12 символов; модификатор partial означает, что часть кода окна лежит в MasterPasswordDialog.Designer.cs
public partial class MasterPasswordDialog : Form
{
    // Поле _isCreation типа bool: true при создании базы, false при открытии; от него зависят проверки и вид окна
    private readonly bool _isCreation;

    // Свойство MasterPassword: введённый пароль, который читает вызывающий код; private set разрешает менять значение только внутри класса
    public string MasterPassword { get; private set; } = string.Empty;

    // Конструктор MasterPasswordDialog: настраивает вид окна под создание или открытие базы
    public MasterPasswordDialog(bool isCreation)
    {
        _isCreation = isCreation;
        InitializeComponent();
        // Поле подтверждения показывается только при создании базы
        confirmLabel.Visible = isCreation;
        confirmBox.Visible = isCreation;
        // Тернарный оператор ? : тексты заголовка, подсказки и кнопки выбираются по режиму
        titleLabel.Text = isCreation ? "Создать мастер-пароль" : "Открыть базу паролей";
        hintLabel.Text = isCreation
            ? "Используйте не менее 12 символов и сохраните пароль отдельно."
            : "Введите пароль, заданный при создании этой базы.";
        confirmButton.Text = isCreation ? "Создать базу" : "Открыть";
        // Лямбда-выражение: при закрытии окна введённый текст стирается из полей
        FormClosed += (_, _) =>
        {
            passwordBox.Clear();
            confirmBox.Clear();
        };
    }

    // Обработчик кнопки показа пароля: одновременно переключает оба поля между символами и точками
    private void RevealButton_Click(object? sender, EventArgs e)
    {
        bool show = passwordBox.PasswordChar != '\0';
        passwordBox.PasswordChar = show ? '\0' : '●';
        confirmBox.PasswordChar = show ? '\0' : '●';
        revealButton.Text = show ? "Скрыть" : "Показать";
    }

    // Обработчик кнопки подтверждения: проверка введённых данных и передача пароля вызывающему коду
    private void ConfirmButton_Click(object? sender, EventArgs e)
    {
        string password = passwordBox.Text;
        // Оператор if: пустой пароль не принимается; метод Focus возвращает фокус в поле
        if (password.Length == 0)
        {
            MessageBox.Show(this, "Введите мастер-пароль.", "Требуется пароль", MessageBoxButtons.OK, MessageBoxIcon.Information);
            passwordBox.Focus();
            return;
        }
        // Оператор && (логическое И): при создании пароль должен быть не короче 12 символов; при открытии длину проверять не нужно
        if (_isCreation && password.Length < 12)
        {
            MessageBox.Show(this, "Для новой базы задайте мастер-пароль длиной не менее 12 символов.",
                "Пароль слишком короткий", MessageBoxButtons.OK, MessageBoxIcon.Information);
            passwordBox.Focus();
            return;
        }
        // Метод string.Equals с параметром Ordinal: точное посимвольное сравнение пароля и подтверждения
        if (_isCreation && !string.Equals(password, confirmBox.Text, StringComparison.Ordinal))
        {
            MessageBox.Show(this, "Введённые пароли не совпадают.", "Проверьте пароль", MessageBoxButtons.OK, MessageBoxIcon.Information);
            confirmBox.Focus();
            return;
        }

        // Запись пароля в свойство и результат OK: окно закрывается, а вызывающий код забирает пароль
        MasterPassword = password;
        DialogResult = DialogResult.OK;
        Close();
    }

    // Обработчик кнопки отмены: закрытие окна с результатом Cancel
    private void CancelButton_Click(object? sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }
}
