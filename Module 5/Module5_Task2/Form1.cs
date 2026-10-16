using System;
using System.IO;
using System.Windows.Forms;

namespace TextEditor
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

        // Обработчик кнопки «Открыть»
        private void btnOpen_Click(object sender, EventArgs e)
        {
            // Оператор if: метод ShowDialog показывает окно выбора файла, а результат DialogResult.OK означает, что файл выбран
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Метод File.ReadAllLines: читает файл в массив строк; свойство Lines выводит эти строки в текстовое поле
                textBox1.Lines = File.ReadAllLines(openFileDialog1.FileName);
            }
        }

        // Обработчик кнопки «Сохранить»
        private void btnSave_Click(object sender, EventArgs e)
        {
            // Оператор if: метод ShowDialog показывает окно «Сохранить как», а результат DialogResult.OK означает, что имя файла выбрано
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Метод File.WriteAllLines: записывает строки из текстового поля в файл (существующий файл перезаписывается)
                File.WriteAllLines(saveFileDialog1.FileName, textBox1.Lines);
            }
        }
    }
}
