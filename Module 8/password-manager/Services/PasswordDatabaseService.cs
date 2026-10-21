using System.Security.Cryptography;
using DgNet.Keepass;
using PasswordManager.Models;

namespace PasswordManager.Services;

// Класс PasswordDatabaseService (реализует IDisposable): вся работа с базой KDBX через библиотеку; создание, открытие, добавление, изменение и удаление записей
public sealed class PasswordDatabaseService : IDisposable
{
    // Поле _database типа Database?: активная база; знак ? разрешает значение null, оно означает, что база не открыта
    private Database? _database;

    // Свойство Records только для чтения: список записей; значение вычисляется методом GetRecords при каждом обращении
    public IReadOnlyList<PasswordRecord> Records => GetRecords();

    // Свойство IsOpen типа bool: true, если база открыта
    public bool IsOpen => _database is not null;

    // Метод Create: создание новой базы с мастер-паролем и сохранение её в файл
    public void Create(string path, string masterPassword)
    {
        Close();
        // Вызов статического метода Database.Create библиотеки: создание базы в памяти с ключом из мастер-пароля
        Database database = Database.Create(masterPassword);
        // Блок try-catch: если файл не записался, незавершённая база освобождается, а ошибка передаётся дальше оператором throw
        try
        {
            database.Metadata.Name = Path.GetFileNameWithoutExtension(path);
            database.SaveAs(path);
            _database = database;
        }
        catch
        {
            database.Dispose();
            throw;
        }
    }

    // Метод Open: открытие существующей базы по пути и мастер-паролю; при неверном пароле библиотека выбрасывает исключение
    public void Open(string path, string masterPassword)
    {
        Close();
        Database database = Database.Open(path, masterPassword);
        _database = database;
    }

    // Метод Add: добавление записи в корневую группу и сохранение файла; при ошибке сохранения запись убирается из памяти
    public void Add(PasswordRecord record)
    {
        Database database = RequireOpen();
        // Вызов метода ToEntry: преобразование модели приложения в запись библиотеки
        Entry entry = ToEntry(record);
        database.RootGroup.AddEntry(entry);
        try
        {
            database.Save();
        }
        catch
        {
            database.RootGroup.RemoveEntry(entry);
            throw;
        }
    }

    // Метод Update: изменение записи по Id; при ошибке сохранения значения возвращаются к прежним
    public void Update(PasswordRecord record)
    {
        Database database = RequireOpen();
        // Метод FindEntry с лямбда-выражением: поиск записи по идентификатору; результат null, если записи нет
        Entry? entry = database.FindEntry(item => item.Uuid == record.Id);
        if (entry is null) throw new InvalidOperationException("Запись не найдена.");

        // Копия значений до редактирования нужна для отката при ошибке сохранения
        PasswordRecord previous = FromEntry(entry);
        CopyToEntry(record, entry);
        try
        {
            database.Save();
        }
        catch
        {
            CopyToEntry(previous, entry);
            throw;
        }
    }

    // Метод Delete: удаление записи по Id; при ошибке сохранения запись возвращается в группу
    public void Delete(Guid id)
    {
        Database database = RequireOpen();
        Entry? entry = database.FindEntry(item => item.Uuid == id);
        if (entry is null) return;

        database.RootGroup.RemoveEntry(entry);
        try
        {
            database.Save();
        }
        catch
        {
            database.RootGroup.AddEntry(entry);
            throw;
        }
    }

    // Метод Close: освобождение базы и её ключей; значение null в поле _database означает, что база закрыта
    public void Close()
    {
        _database?.Dispose();
        _database = null;
    }

    // Метод Dispose (интерфейс IDisposable): позволяет освободить базу автоматически, например через блок using
    public void Dispose() => Close();

    // Метод GetRecords: чтение всех записей и преобразование в модели приложения, чтобы формы не зависели от библиотеки KDBX
    private List<PasswordRecord> GetRecords()
    {
        Database database = RequireOpen();
        return database.RootGroup.Entries.Select(FromEntry).ToList();
    }

    // Метод RequireOpen: возвращает открытую базу или выбрасывает исключение; оператор ?? подставляет исключение, если база равна null
    private Database RequireOpen() =>
        _database ?? throw new InvalidOperationException("Сначала откройте базу паролей.");

    // Метод ToEntry: создание записи библиотеки из модели
    private static Entry ToEntry(PasswordRecord record)
    {
        var entry = new Entry { Uuid = record.Id };
        CopyToEntry(record, entry);
        return entry;
    }

    // Метод CopyToEntry: копирование полей модели в запись библиотеки; используется при создании, изменении и откате
    private static void CopyToEntry(PasswordRecord record, Entry entry)
    {
        entry.Title = record.Service;
        entry.UserName = record.Login;
        entry.Password = record.Password;
        entry.Url = record.Website;
        entry.Notes = record.Notes;
    }

    // Метод FromEntry: преобразование записи библиотеки в модель приложения
    private static PasswordRecord FromEntry(Entry entry) => new()
    {
        Id = entry.Uuid,
        Service = entry.Title,
        Login = entry.UserName,
        Password = entry.Password,
        Website = entry.Url,
        Notes = entry.Notes,
        UpdatedAt = DateTime.Now
    };
}

// Статический класс PasswordGenerator: генерация случайного пароля; статический, потому что создавать объект не нужно
public static class PasswordGenerator
{
    // Константы Lower, Upper, Digits и Symbols типа string: алфавиты, из которых собирается пароль; похожие символы (l, I, O, 0, 1) исключены
    private const string Lower = "abcdefghijkmnopqrstuvwxyz";
    private const string Upper = "ABCDEFGHJKLMNPQRSTUVWXYZ";
    private const string Digits = "23456789";
    private const string Symbols = "!@#$%&*+-=?";

    // Метод Generate: возвращает пароль заданной длины (по умолчанию 20); допустимая длина от 12 до 64
    public static string Generate(int length = 20)
    {
        // Оператор if: при недопустимой длине выбрасывается исключение ArgumentOutOfRangeException
        if (length < 12 || length > 64) throw new ArgumentOutOfRangeException(nameof(length));
        string all = Lower + Upper + Digits + Symbols;
        char[] result = new char[length];
        // Первые четыре символа берутся по одному из каждого алфавита, чтобы в пароле были все классы символов
        result[0] = Pick(Lower);
        result[1] = Pick(Upper);
        result[2] = Pick(Digits);
        result[3] = Pick(Symbols);
        // Цикл for: остальные символы выбираются из объединённого алфавита
        for (int i = 4; i < result.Length; i++) result[i] = Pick(all);
        // Цикл for с перестановкой по алгоритму Фишера-Йетса: перемешивание, чтобы обязательные символы не стояли на известных местах
        for (int i = result.Length - 1; i > 0; i--)
        {
            int j = RandomNumberGenerator.GetInt32(i + 1);
            // Кортежное присваивание: обмен значений двух элементов массива
            (result[i], result[j]) = (result[j], result[i]);
        }
        return new string(result);
    }

    // Метод Pick: случайный символ алфавита; RandomNumberGenerator вместо Random даёт непредсказуемый выбор
    private static char Pick(string alphabet) => alphabet[RandomNumberGenerator.GetInt32(alphabet.Length)];
}
