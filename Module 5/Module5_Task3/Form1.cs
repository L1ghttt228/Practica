using System;
using System.Windows.Forms;

namespace TaskList
{
    // Класс Form1: главное окно программы (наследник класса Form)
    public partial class Form1 : Form
    {
        // Конструктор Form1: выполняется один раз при создании окна
        public Form1()
        {
            // Вызов метода InitializeComponent: создание элементов окна (код в Form1.Designer.cs)
            InitializeComponent();
        }

        // Обработчик кнопки «Добавить»
        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Переменная task типа string: введённый текст без пробелов по краям (метод Trim)
            string task = textBox1.Text.Trim();

            // Оператор if: пустая задача не добавляется
            if (task != "")
            {
                // Метод Items.Add: добавляет задачу в конец списка
                checkedListBox1.Items.Add(task);

                // Метод Clear: очищает поле ввода
                textBox1.Clear();
            }
        }

        // Обработчик кнопки «Выполнено»
        private void btnDone_Click(object sender, EventArgs e)
        {
            // Переменная index типа int: номер выбранной задачи (значение -1 означает, что ничего не выбрано)
            int index = checkedListBox1.SelectedIndex;

            // Оператор if: проверка, что задача выбрана
            if (index != -1)
            {
                // Метод SetItemChecked: ставит флажок, то есть помечает задачу выполненной
                checkedListBox1.SetItemChecked(index, true);
            }
        }

        // Обработчик кнопки «Удалить»
        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Переменная index типа int: номер выбранной задачи
            int index = checkedListBox1.SelectedIndex;

            // Оператор if: проверка, что задача выбрана
            if (index != -1)
            {
                // Метод Items.RemoveAt: удаляет задачу по номеру
                checkedListBox1.Items.RemoveAt(index);
            }
        }
    }
}
