using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace LibraryApp
{
    // Класс MainWindow: главное окно программы (наследник класса Window); слово partial означает, что часть кода окна лежит в MainWindow.xaml; в классе описаны поиск книг, аренда и возврат
    public partial class MainWindow : Window
    {
        // Поле _books типа List<Book>: полный список книг библиотеки; из него берутся данные для поиска
        private List<Book> _books;

        // Конструктор MainWindow: выполняется один раз при создании окна
        public MainWindow()
        {
            // Вызов метода InitializeComponent: создание элементов окна по разметке из MainWindow.xaml
            InitializeComponent();

            // Вызов метода Load класса Database: чтение книг из файла
            _books = Database.Load();

            // Вызов метода ShowBooks: показ книг в таблице при запуске
            ShowBooks();
        }

        // Метод ShowBooks: показ в таблице книг, которые подходят под строку поиска
        private void ShowBooks()
        {
            // Переменная query типа string: текст из поля поиска; метод Trim убирает пробелы по краям
            string query = searchBox.Text.Trim();

            // Метод Where из LINQ с лямбда-выражением b => ...: остаются книги, у которых название или автор содержит строку поиска; оператор || означает логическое ИЛИ
            // Метод IndexOf с параметром IgnoreCase возвращает позицию подстроки или -1, поэтому условие >= 0 означает «найдено», а регистр букв не важен
            // Метод ToList превращает результат в готовый список, а свойство ItemsSource делает его источником данных таблицы
            grid.ItemsSource = _books.Where(b =>
                b.Title.IndexOf(query, StringComparison.CurrentCultureIgnoreCase) >= 0 ||
                b.Author.IndexOf(query, StringComparison.CurrentCultureIgnoreCase) >= 0).ToList();
        }

        // Метод Selected: возвращает книгу, выбранную в таблице, или null, если строка не выбрана
        private Book Selected()
        {
            // Оператор as: выбранная строка (свойство SelectedItem) приводится к типу Book; если ничего не выбрано, получится null
            return grid.SelectedItem as Book;
        }

        // Обработчик изменения текста в поле поиска: обновление таблицы при каждом вводе символа
        private void searchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Оператор if: событие может сработать до загрузки книг, тогда список ещё равен null и метод завершается оператором return
            if (_books == null) return;

            // Вызов метода ShowBooks: повторный показ списка с новой строкой поиска
            ShowBooks();
        }

        // Обработчик кнопки «Арендовать»: запись имени читателя в выбранную книгу
        private void rentButton_Click(object sender, RoutedEventArgs e)
        {
            // Переменная book: книга, выбранная в таблице (метод Selected даёт null, если строка не выбрана)
            var book = Selected();

            // Оператор if: если строка не выбрана, оператор return завершает метод
            if (book == null) return;

            // Оператор if: нужно ввести имя читателя; если поле пустое, выводится сообщение методом MessageBox.Show и метод завершается
            if (readerBox.Text.Trim() == "")
            {
                MessageBox.Show("Введите имя читателя.");
                return;
            }

            // Оператор if: книга должна быть свободна; если поле Reader не пустое, значит книга уже арендована
            if (!string.IsNullOrEmpty(book.Reader))
            {
                MessageBox.Show("Эта книга уже арендована.");
                return;
            }

            // Присваивание свойству Reader: имя читателя записывается в книгу; метод Replace меняет символ | на пробел, потому что он разделяет поля в файле
            book.Reader = readerBox.Text.Trim().Replace('|', ' ');

            // Вызов метода Save класса Database: запись всего списка в файл
            Database.Save(_books);

            // Обновление таблицы: в ней появится новый статус книги
            ShowBooks();
        }

        // Обработчик кнопки «Вернуть»: книга снова становится свободной
        private void returnButton_Click(object sender, RoutedEventArgs e)
        {
            // Переменная book: книга, выбранная в таблице
            var book = Selected();

            // Оператор if: если строка не выбрана, оператор return завершает метод
            if (book == null) return;

            // Присваивание пустой строки свойству Reader: читателя нет, поэтому книга снова свободна
            book.Reader = "";

            // Запись изменённого списка в файл
            Database.Save(_books);

            // Обновление таблицы: в ней появится новый статус книги
            ShowBooks();
        }
    }
}
