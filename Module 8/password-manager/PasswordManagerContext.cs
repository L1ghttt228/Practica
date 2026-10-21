using PasswordManager.Services;

namespace PasswordManager;

// Класс PasswordManagerContext (наследник ApplicationContext): управляет жизнью приложения и переключает окна, не завершая процесс
public sealed class PasswordManagerContext : ApplicationContext
{
    // Поле _switchingForms типа bool (флаг): true, пока меню закрывается при переходе к записям; нужно, чтобы приложение не завершилось
    private bool _switchingForms;

    // Конструктор: при запуске сразу показывает меню; запись => задаёт тело метода одним выражением
    public PasswordManagerContext() => ShowMenu();

    // Метод ShowMenu: создание и показ стартового окна
    private void ShowMenu()
    {
        var menu = new MainForm(this);
        // Подписка += с лямбда-выражением: если меню закрыл пользователь, а не код перехода, приложение завершается методом ExitThread
        menu.FormClosed += (_, _) =>
        {
            if (!_switchingForms) ExitThread();
        };
        menu.Show();
    }

    // Метод ShowEntries: переход от меню к списку записей; параметры - меню, открытая база и путь к файлу
    public void ShowEntries(MainForm menu, PasswordDatabaseService database, string path)
    {
        var entries = new EntriesForm(database, path);
        // Лямбда-выражение: при закрытии списка база освобождается методом Dispose и снова показывается меню
        entries.FormClosed += (_, _) =>
        {
            database.Dispose();
            ShowMenu();
        };

        // Флаг поднимается на время закрытия меню, чтобы подписка выше не завершила приложение
        _switchingForms = true;
        menu.Close();
        entries.Show();
        _switchingForms = false;
    }
}
