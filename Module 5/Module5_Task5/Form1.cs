using System;
using System.Globalization;
using System.Windows.Forms;

namespace Calculator
{
    // Класс Form1: главное окно программы (наследник класса Form)
    public partial class Form1 : Form
    {
        // Поле first типа double: первое число
        private double first = 0;

        // Поле op типа string: выбранная операция (+, -, *, /); пустая строка означает, что операция не выбрана
        private string op = "";

        // Поле newNumber типа bool: флаг, равный true, когда следующая цифра начинает новое число
        private bool newNumber = true;

        // Конструктор Form1: выполняется один раз при создании окна
        public Form1()
        {
            // Вызов метода InitializeComponent: создание элементов окна (код в Form1.Designer.cs)
            InitializeComponent();
        }

        // Обработчик всех цифровых кнопок: один метод вместо десяти
        private void Digit_Click(object sender, EventArgs e)
        {
            // Переменная b типа Button: приведение (Button) превращает параметр sender в нажатую кнопку
            Button b = (Button)sender;

            // Оператор if с логическим ИЛИ (||): новое число или «0» на экране заменяются нажатой цифрой (b.Text - надпись на кнопке)
            if (newNumber || textBox1.Text == "0")
            {
                textBox1.Text = b.Text;

                // Следующие цифры уже дописываются к числу
                newNumber = false;
            }
            else
            {
                // Ветка else: дописывание цифры справа (оператор + склеивает строки)
                textBox1.Text = textBox1.Text + b.Text;
            }
        }

        // Обработчик кнопки десятичной точки
        private void Dot_Click(object sender, EventArgs e)
        {
            // Оператор if: новое число начинается с записи «0» и десятичной точки
            if (newNumber)
            {
                textBox1.Text = "0.";
                newNumber = false;
            }
            // Оператор else if: метод Contains проверяет, что точки в числе ещё нет, так как вторая точка недопустима
            else if (!textBox1.Text.Contains("."))
            {
                textBox1.Text = textBox1.Text + ".";
            }
        }

        // Обработчик кнопок операций
        private void Operator_Click(object sender, EventArgs e)
        {
            // Метод double.TryParse: превращает текст экрана в число (ключевое слово out записывает его в поле first)
            // Значение InvariantCulture задаёт точку как разделитель независимо от настроек Windows; если на экране не число, return завершает метод
            if (!double.TryParse(textBox1.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out first))
            {
                return;
            }

            // Сохранение операции: это надпись нажатой кнопки
            op = ((Button)sender).Text;

            // Следующее число вводится заново
            newNumber = true;
        }

        // Обработчик кнопки «=»: вычисляет результат
        private void Equals_Click(object sender, EventArgs e)
        {
            // Оператор if: если операция не выбрана, return завершает метод
            if (op == "")
            {
                return;
            }

            // Переменная second типа double: второе число с экрана
            double second;

            // Метод double.TryParse: чтение второго числа, при ошибке return завершает метод
            if (!double.TryParse(textBox1.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out second))
            {
                return;
            }

            // Переменная result типа double: результат вычисления
            double result = 0;

            // Оператор switch: выбирает действие по значению поля op
            switch (op)
            {
                case "+":
                    // Ветка case: сложение
                    result = first + second;
                    // Оператор break: выход из switch
                    break;
                case "-":
                    // Ветка case: вычитание
                    result = first - second;
                    break;
                case "*":
                    // Ветка case: умножение
                    result = first * second;
                    break;
                case "/":
                    // Оператор if: деление на ноль запрещено, поэтому выводится «Ошибка», а return завершает метод
                    if (second == 0)
                    {
                        textBox1.Text = "Ошибка";
                        op = "";
                        newNumber = true;
                        return;
                    }

                    // Ветка case: деление
                    result = first / second;
                    break;
            }

            // Метод ToString: превращает результат в строку для экрана
            textBox1.Text = result.ToString(CultureInfo.InvariantCulture);

            // Сброс операции и подготовка к вводу нового числа
            op = "";
            newNumber = true;
        }

        // Обработчик кнопки «C»: возвращает калькулятор в начальное состояние
        private void Clear_Click(object sender, EventArgs e)
        {
            textBox1.Text = "0";
            first = 0;
            op = "";
            newNumber = true;
        }
    }
}
