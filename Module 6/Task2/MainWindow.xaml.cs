using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace TaskManager
{
    // Класс MainWindow: главное окно программы (наследник класса Window); слово partial означает, что часть кода окна лежит в MainWindow.xaml; в классе описаны обработчики кнопок добавления, изменения и удаления задач
    public partial class MainWindow : Window
    {
        // Поле _tasks типа ObservableCollection<TaskItem>: список задач; эта коллекция сама сообщает таблице об добавлении и удалении, поэтому таблица обновляется без лишнего кода
        private ObservableCollection<TaskItem> _tasks;

        // Конструктор MainWindow: выполняется один раз при создании окна
        public MainWindow()
        {
            // Вызов метода InitializeComponent: создание элементов окна по разметке из MainWindow.xaml
            InitializeComponent();

            // Свойство ItemsSource выпадающего списка: массив строк с вариантами статуса; оператор new string[] создаёт массив с тремя значениями
            statusBox.ItemsSource = new string[] { "Новая", "В работе", "Выполнена" };

            // Свойство SelectedIndex: по умолчанию выбран элемент с номером 0, то есть статус «Новая»
            statusBox.SelectedIndex = 0;

            // Вызов метода Load класса Database: чтение задач из файла; оператор new создаёт ObservableCollection на основе прочитанного списка
            _tasks = new ObservableCollection<TaskItem>(Database.Load());

            // Свойство ItemsSource: источник данных таблицы DataGrid, таблица показывает все элементы коллекции
            grid.ItemsSource = _tasks;
        }

        // Метод Selected: возвращает задачу, выбранную в таблице, или null, если строка не выбрана
        private TaskItem Selected()
        {
            // Оператор as: выбранная строка (свойство SelectedItem) приводится к типу TaskItem; если ничего не выбрано, получится null
            return grid.SelectedItem as TaskItem;
        }

        // Метод ReadForm: чтение и проверка данных формы; возвращает true, если всё верно, и записывает значения в переданную задачу
        private bool ReadForm(TaskItem t)
        {
            // Оператор if: ошибка, если название пустое; метод Trim убирает пробелы по краям
            if (titleBox.Text.Trim() == "")
            {
                // Метод MessageBox.Show: окно с сообщением об ошибке; оператор return false сообщает, что проверка не пройдена
                MessageBox.Show("Введите название задачи.");
                return false;
            }

            // Присваивание свойствам задачи: значения из полей формы; метод Replace меняет символ | на пробел, потому что он разделяет поля в файле
            t.Title = titleBox.Text.Trim().Replace('|', ' ');
            t.Description = descriptionBox.Text.Trim().Replace('|', ' ');

            // Свойство SelectedItem выпадающего списка: выбранный статус; оператор as приводит его к типу string
            t.Status = statusBox.SelectedItem as string;

            // Оператор return true: все данные верны
            return true;
        }

        // Обработчик выбора строки в таблице: копирование данных выбранной задачи в поля формы для редактирования
        private void grid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Переменная t: задача, выбранная в таблице (метод Selected даёт null, если строка не выбрана)
            var t = Selected();

            // Оператор if: если строка не выбрана, оператор return завершает метод
            if (t == null) return;

            // Присваивание свойству Text: значения полей задачи выводятся в текстовые поля формы
            titleBox.Text = t.Title;
            descriptionBox.Text = t.Description;

            // Присваивание свойству SelectedItem: в выпадающем списке выбирается статус задачи
            statusBox.SelectedItem = t.Status;
        }

        // Обработчик кнопки «Добавить»: создание новой задачи из данных формы
        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            // Переменная t: новая пустая задача, оператор new создаёт объект класса TaskItem
            var t = new TaskItem();

            // Вызов метода ReadForm: заполнение задачи данными формы; при ошибке оператор return завершает метод
            if (!ReadForm(t)) return;

            // Метод Add: задача добавляется в коллекцию, таблица обновляется сама
            _tasks.Add(t);

            // Вызов метода Save класса Database: запись всего списка в файл
            Database.Save(_tasks);
        }

        // Обработчик кнопки «Изменить»: обновление задачи, выбранной в таблице
        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            // Переменная t: задача, выбранная в таблице
            var t = Selected();

            // Оператор if: если строка не выбрана, оператор return завершает метод
            if (t == null) return;

            // Вызов метода ReadForm: новые значения записываются прямо в выбранную задачу; при ошибке метод завершается
            if (!ReadForm(t)) return;

            // Метод Items.Refresh: принудительная перерисовка таблицы, потому что класс TaskItem сам не сообщает таблице об изменении своих свойств
            grid.Items.Refresh();

            // Запись изменённого списка в файл
            Database.Save(_tasks);
        }

        // Обработчик кнопки «Удалить»: удаление выбранной задачи
        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            // Переменная t: задача, выбранная в таблице
            var t = Selected();

            // Оператор if: если строка не выбрана, оператор return завершает метод
            if (t == null) return;

            // Метод Remove: задача удаляется из коллекции, таблица обновляется сама
            _tasks.Remove(t);

            // Запись изменённого списка в файл
            Database.Save(_tasks);
        }
    }
}
