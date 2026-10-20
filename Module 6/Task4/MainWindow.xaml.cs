using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace FinanceTracker
{
    // Класс MainWindow: главное окно программы (наследник класса Window); слово partial означает, что часть кода окна лежит в MainWindow.xaml; в классе описаны обработчики кнопок добавления и изменения финансовых записей
    public partial class MainWindow : Window
    {
        // Поле _records типа ObservableCollection<FinanceRecord>: список записей; эта коллекция сама сообщает таблице об добавлении, поэтому таблица обновляется без лишнего кода
        private ObservableCollection<FinanceRecord> _records;

        // Конструктор MainWindow: выполняется один раз при создании окна
        public MainWindow()
        {
            // Вызов метода InitializeComponent: создание элементов окна по разметке из MainWindow.xaml
            InitializeComponent();

            // Свойство ItemsSource выпадающего списка: массив строк с вариантами типа операции; оператор new string[] создаёт массив с двумя значениями
            typeBox.ItemsSource = new string[] { "Доход", "Расход" };

            // Свойство SelectedIndex: по умолчанию выбран элемент с номером 0, то есть «Доход»
            typeBox.SelectedIndex = 0;

            // Свойство SelectedDate: в поле даты по умолчанию ставится сегодняшнее число (DateTime.Today)
            datePicker.SelectedDate = DateTime.Today;

            // Вызов метода Load класса Database: чтение записей из файла; оператор new создаёт ObservableCollection на основе прочитанного списка
            _records = new ObservableCollection<FinanceRecord>(Database.Load());

            // Свойство ItemsSource: источник данных таблицы DataGrid, таблица показывает все элементы коллекции
            grid.ItemsSource = _records;
        }

        // Метод Selected: возвращает запись, выбранную в таблице, или null, если строка не выбрана
        private FinanceRecord Selected()
        {
            // Оператор as: выбранная строка (свойство SelectedItem) приводится к типу FinanceRecord; если ничего не выбрано, получится null
            return grid.SelectedItem as FinanceRecord;
        }

        // Метод ReadForm: чтение и проверка данных формы; возвращает true, если всё верно, и записывает значения в переданную запись
        private bool ReadForm(FinanceRecord r)
        {
            // Переменная amount типа decimal: сюда метод TryParse запишет сумму, переведённую из текста
            decimal amount;

            // Оператор if: ошибка, если категория пустая, сумма не переводится в число или сумма не больше нуля; оператор || означает ИЛИ, а оператор ! означает «не»
            // Метод TryParse возвращает true при успехе и не вызывает исключение при ошибке; метод Replace меняет запятую на точку, чтобы можно было вводить оба варианта
            if (categoryBox.Text.Trim() == "" ||
                !decimal.TryParse(amountBox.Text.Replace(',', '.'), NumberStyles.Number, CultureInfo.InvariantCulture, out amount) ||
                amount <= 0)
            {
                // Метод MessageBox.Show: окно с сообщением об ошибке; оператор return false сообщает, что проверка не пройдена
                MessageBox.Show("Введите категорию и сумму (число больше нуля).");
                return false;
            }

            // Свойство SelectedDate: выбранная дата; оператор ?? берёт сегодняшнее число, если дата не выбрана; свойство Date отбрасывает время
            r.Date = (datePicker.SelectedDate ?? DateTime.Today).Date;

            // Свойство SelectedItem выпадающего списка: выбранный тип операции; оператор as приводит его к типу string
            r.Type = typeBox.SelectedItem as string;

            // Присваивание категории: метод Replace меняет символ | на пробел, потому что он разделяет поля в файле
            r.Category = categoryBox.Text.Trim().Replace('|', ' ');
            r.Amount = amount;

            // Оператор return true: все данные верны
            return true;
        }

        // Обработчик выбора строки в таблице: копирование данных выбранной записи в поля формы для редактирования
        private void grid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Переменная r: запись, выбранная в таблице (метод Selected даёт null, если строка не выбрана)
            var r = Selected();

            // Оператор if: если строка не выбрана, оператор return завершает метод
            if (r == null) return;

            // Присваивание полям формы: дата, тип и категория выбранной записи выводятся в элементы управления
            datePicker.SelectedDate = r.Date;
            typeBox.SelectedItem = r.Type;
            categoryBox.Text = r.Category;

            // Метод ToString с InvariantCulture: сумма выводится в текстовое поле с точкой в качестве разделителя
            amountBox.Text = r.Amount.ToString(CultureInfo.InvariantCulture);
        }

        // Обработчик кнопки «Добавить»: создание новой записи из данных формы
        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            // Переменная r: новая пустая запись, оператор new создаёт объект класса FinanceRecord
            var r = new FinanceRecord();

            // Вызов метода ReadForm: заполнение записи данными формы; при ошибке оператор return завершает метод
            if (!ReadForm(r)) return;

            // Метод Add: запись добавляется в коллекцию, таблица обновляется сама
            _records.Add(r);

            // Вызов метода Save класса Database: запись всего списка в файл
            Database.Save(_records);
        }

        // Обработчик кнопки «Изменить»: обновление записи, выбранной в таблице
        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            // Переменная r: запись, выбранная в таблице
            var r = Selected();

            // Оператор if: если строка не выбрана, оператор return завершает метод
            if (r == null) return;

            // Вызов метода ReadForm: новые значения записываются прямо в выбранную запись; при ошибке метод завершается
            if (!ReadForm(r)) return;

            // Метод Items.Refresh: принудительная перерисовка таблицы, потому что класс FinanceRecord сам не сообщает таблице об изменении своих свойств
            grid.Items.Refresh();

            // Запись изменённого списка в файл
            Database.Save(_records);
        }
    }
}
