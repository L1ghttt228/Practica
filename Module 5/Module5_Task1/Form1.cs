using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Paint
{
    // Класс Form1: главное окно программы (наследник класса Form)
    public partial class Form1 : Form
    {
        // Поле bmp типа Bitmap: картинка в памяти, в которой хранится готовый рисунок
        private Bitmap bmp;

        // Поле start типа Point: точка начала фигуры (где нажали кнопку мыши)
        private Point start;

        // Поле current типа Point: текущее положение мыши при растягивании фигуры
        private Point current;

        // Поле drawing типа bool: флаг, равный true, пока кнопка мыши зажата
        private bool drawing = false;

        // Конструктор Form1: выполняется один раз при создании окна
        public Form1()
        {
            // Вызов метода InitializeComponent: создание элементов окна (код в Form1.Designer.cs)
            InitializeComponent();

            // Оператор new: создание картинки размером со весь экран, чтобы рисовать можно было и в развёрнутом окне
            bmp = new Bitmap(SystemInformation.VirtualScreen.Width, SystemInformation.VirtualScreen.Height);

            // Вызов метода ClearBitmap: заливка холста белым цветом
            ClearBitmap();

            // Присваивание свойству Image: PictureBox начинает показывать картинку
            pictureBox1.Image = bmp;
        }

        // Метод ClearBitmap: заливает холст белым цветом
        private void ClearBitmap()
        {
            // Блок using: создаёт объект Graphics для рисования на картинке и сам освобождает его после блока
            using (Graphics g = Graphics.FromImage(bmp))
            {
                // Метод Clear: закрашивает всю картинку белым цветом
                g.Clear(Color.White);
            }
        }

        // Обработчик кнопки «Очистить»
        private void btnClear_Click(object sender, EventArgs e)
        {
            // Вызов метода ClearBitmap: стирание рисунка
            ClearBitmap();

            // Метод Invalidate: перерисовка PictureBox, чтобы очистка стала видна
            pictureBox1.Invalidate();
        }

        // Обработчик нажатия кнопки мыши на холсте
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            // Оператор if: рисовать можно только левой кнопкой, для остальных return завершает метод
            if (e.Button != MouseButtons.Left)
            {
                return;
            }

            // Включение режима рисования
            drawing = true;

            // Запоминание начальной точки фигуры (e.Location - координаты мыши)
            start = e.Location;
            current = e.Location;
        }

        // Обработчик движения мыши над холстом
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            // Оператор if с логическим НЕ (!): если кнопка не зажата, return завершает метод
            if (!drawing)
            {
                return;
            }

            // Запоминание текущей точки мыши
            current = e.Location;

            // Перерисовка холста, чтобы показать фигуру нового размера
            pictureBox1.Invalidate();
        }

        // Обработчик перерисовки холста: показывает фигуру во время растягивания
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            // Оператор if: фигура-черновик рисуется, только пока кнопка мыши зажата
            if (drawing)
            {
                // Вызов метода DrawShape: рисование прямо на экране (e.Graphics), в картинку bmp фигура пока не попадает
                DrawShape(e.Graphics, start, current);
            }
        }

        // Обработчик отпускания кнопки мыши: фигура записывается на холст
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            // Оператор if: если рисование не начиналось, return завершает метод
            if (!drawing)
            {
                return;
            }

            // Выключение режима рисования
            drawing = false;

            // Блок using: объект Graphics для рисования в картинку bmp
            using (Graphics g = Graphics.FromImage(bmp))
            {
                // Вызов метода DrawShape: фигура рисуется в картинку и остаётся на холсте
                DrawShape(g, start, e.Location);
            }

            // Перерисовка холста, чтобы фигура появилась на экране
            pictureBox1.Invalidate();
        }

        // Метод DrawShape: рисует фигуру от точки a до точки b на объекте g (экран или картинка); общий метод нужен, чтобы не дублировать код
        private void DrawShape(Graphics g, Point a, Point b)
        {
            // Свойство SmoothingMode: включает сглаживание, чтобы линии не были «лесенкой» из пикселей
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Переменная width типа float: толщина линии с учётом масштаба экрана (DeviceDpi)
            float width = 2f * DeviceDpi / 96f;

            // Блок using: создаёт объект Pen (перо) чёрного цвета нужной толщины
            using (Pen pen = new Pen(Color.Black, width))
            {
                // Оператор if: свойство Checked показывает, выбран ли переключатель «Линия»
                if (rbLine.Checked)
                {
                    // Метод DrawLine: рисует линию от точки a до точки b
                    g.DrawLine(pen, a, b);
                }
                else
                {
                    // Ветка else: круг или квадрат; для них нужны левый верхний угол и длина стороны
                    // Переменные x и y типа int: левый верхний угол (метод Math.Min берёт меньшее значение, поэтому мышь можно тянуть в любую сторону)
                    int x = Math.Min(a.X, b.X);
                    int y = Math.Min(a.Y, b.Y);

                    // Переменная side типа int: длина стороны (Math.Abs - модуль числа, Math.Max - большее из двух значений)
                    int side = Math.Max(Math.Abs(b.X - a.X), Math.Abs(b.Y - a.Y));

                    // Вложенный оператор if: выбран ли переключатель «Круг»
                    if (rbCircle.Checked)
                    {
                        // Метод DrawEllipse: рисует эллипс в квадратной рамке, то есть круг
                        g.DrawEllipse(pen, x, y, side, side);
                    }
                    else
                    {
                        // Метод DrawRectangle: рисует прямоугольник с равными сторонами, то есть квадрат
                        g.DrawRectangle(pen, x, y, side, side);
                    }
                }
            }
        }
    }
}
