using System;
using System.Drawing;
using System.Windows.Forms;

namespace ImageViewer
{
    // Класс Form1: главное окно программы (наследник класса Form)
    public partial class Form1 : Form
    {
        // Поле scale типа double: текущий масштаб картинки (значение 1.0 соответствует 100%)
        private double scale = 1.0;

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
                // Метод Image.FromFile: загружает картинку из файла, свойство Image показывает её в PictureBox
                pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);

                // Сброс масштаба до 100% для новой картинки
                scale = 1.0;

                // Присваивание свойствам Width и Height: PictureBox получает размер картинки
                pictureBox1.Width = pictureBox1.Image.Width;
                pictureBox1.Height = pictureBox1.Image.Height;
            }
        }

        // Обработчик кнопки «+»: вызов метода Zoom с множителем 1.25, то есть увеличение
        private void btnZoomIn_Click(object sender, EventArgs e)
        {
            Zoom(1.25);
        }

        // Обработчик кнопки «-»: вызов метода Zoom с множителем 0.8, то есть уменьшение
        private void btnZoomOut_Click(object sender, EventArgs e)
        {
            Zoom(0.8);
        }

        // Метод Zoom: меняет масштаб, параметр factor - множитель размера; общий метод нужен для обеих кнопок, чтобы не повторять код
        private void Zoom(double factor)
        {
            // Оператор if: значение null означает, что картинка не открыта, поэтому return завершает метод
            if (pictureBox1.Image == null)
            {
                return;
            }

            // Вычисление нового масштаба
            scale = scale * factor;

            // Новый размер PictureBox: размер картинки умножается на масштаб, а приведение (int) отбрасывает дробную часть
            pictureBox1.Width = (int)(pictureBox1.Image.Width * scale);
            pictureBox1.Height = (int)(pictureBox1.Image.Height * scale);
        }
    }
}
