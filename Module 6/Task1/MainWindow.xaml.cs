using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace EmployeeManager
{
    // Класс MainWindow: главное окно программы (наследник класса Window);
    public partial class MainWindow : Window
    {
        // Поле _employees типа ObservableCollection<Employee>: список сотрудников; эта коллекция сама сообщает таблице об добавлении и удалении, поэтому таблица обновляется без лишнего кода
        private ObservableCollection<Employee> _employees;

        // Конструктор MainWindow: выполняется один раз при создании окна
        public MainWindow()
        {
            // Вызов метода InitializeComponent: создание элементов окна по разметке из MainWindow.xaml
            InitializeComponent();

            // Вызов метода Load класса Database: чтение сотрудников из файла; оператор new создаёт ObservableCollection на основе прочитанного списка
            _employees = new ObservableCollection<Employee>(Database.Load());

            // Свойство ItemsSource: источник данных таблицы DataGrid, таблица показывает все элементы коллекции
            grid.ItemsSource = _employees;
        }

        // Метод Selected: возвращает сотрудника, выбранного в таблице, или null, если строка не выбрана
        private Employee Selected()
        {
            // Оператор as: выбранная строка (свойство SelectedItem) приводится к типу Employee; если ничего не выбрано, получится null
            return grid.SelectedItem as Employee;
        }

        // Метод ReadForm: чтение и проверка данных формы; возвращает true, если всё верно, и записывает значения в переданного сотрудника
        private bool ReadForm(Employee e)
        {
            // Переменная salary типа decimal: сюда метод TryParse запишет оклад, переведённый из текста
            decimal salary;

            // Оператор if: ошибка, если ФИО пустое (метод Trim убирает пробелы по краям) или оклад не удалось перевести в число; оператор || означает ИЛИ, а оператор ! означает «не»
            // Метод TryParse возвращает true при успехе и не вызывает исключение при ошибке; метод Replace меняет запятую на точку, чтобы можно было вводить оба варианта
            if (nameBox.Text.Trim() == "" ||
                !decimal.TryParse(salaryBox.Text.Replace(',', '.'), NumberStyles.Number, CultureInfo.InvariantCulture, out salary))
            {
                // Метод MessageBox.Show: окно с сообщением об ошибке; оператор return false сообщает, что проверка не пройдена
                MessageBox.Show("Введите ФИО и оклад (числом).");
                return false;
            }

            // Присваивание свойствам сотрудника: значения из полей формы; метод Replace меняет символ | на пробел, потому что он разделяет поля в файле
            e.FullName = nameBox.Text.Trim().Replace('|', ' ');
            e.Position = positionBox.Text.Trim().Replace('|', ' ');
            e.Department = departmentBox.Text.Trim().Replace('|', ' ');
            e.Salary = salary;

            // Оператор return true: все данные верны
            return true;
        }

        // Обработчик выбора строки в таблице: копирование данных выбранного сотрудника в поля формы для редактирования
        private void grid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Переменная emp: сотрудник, выбранный в таблице (метод Selected даёт null, если строка не выбрана)
            var emp = Selected();

            // Оператор if: если строка не выбрана, оператор return завершает метод
            if (emp == null) return;

            // Присваивание свойству Text: значения полей сотрудника выводятся в текстовые поля формы
            nameBox.Text = emp.FullName;
            positionBox.Text = emp.Position;
            departmentBox.Text = emp.Department;

            // Метод ToString с InvariantCulture: оклад выводится в текстовое поле с точкой в качестве разделителя
            salaryBox.Text = emp.Salary.ToString(CultureInfo.InvariantCulture);
        }

        // Обработчик кнопки «Добавить»: создание нового сотрудника из данных формы
        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            // Переменная emp: новый пустой сотрудник, оператор new создаёт объект класса Employee
            var emp = new Employee();

            // Вызов метода ReadForm: заполнение сотрудника данными формы; при ошибке оператор return завершает метод
            if (!ReadForm(emp)) return;

            // Метод Add: сотрудник добавляется в коллекцию, таблица обновляется сама
            _employees.Add(emp);

            // Вызов метода Save класса Database: запись всего списка в файл
            Database.Save(_employees);
        }

        // Обработчик кнопки «Изменить»: обновление сотрудника, выбранного в таблице
        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            // Переменная emp: сотрудник, выбранный в таблице
            var emp = Selected();

            // Оператор if: если строка не выбрана, оператор return завершает метод
            if (emp == null) return;

            // Вызов метода ReadForm: новые значения записываются прямо в выбранного сотрудника; при ошибке метод завершается
            if (!ReadForm(emp)) return;

            // Метод Items.Refresh: принудительная перерисовка таблицы, потому что класс Employee сам не сообщает таблице об изменении своих свойств
            grid.Items.Refresh();

            // Запись изменённого списка в файл
            Database.Save(_employees);
        }

        // Обработчик кнопки «Удалить»: удаление выбранного сотрудника
        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            // Переменная emp: сотрудник, выбранный в таблице
            var emp = Selected();

            // Оператор if: если строка не выбрана, оператор return завершает метод
            if (emp == null) return;

            // Метод Remove: сотрудник удаляется из коллекции, таблица обновляется сама
            _employees.Remove(emp);

            // Запись изменённого списка в файл
            Database.Save(_employees);
        }
    }
}
