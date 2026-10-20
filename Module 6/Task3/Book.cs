namespace LibraryApp
{
    // Класс Book (модель): один объект соответствует одной строке файла books.txt и хранит данные одной книги
    public class Book
    {
        // Автоматическое свойство Title типа string: название книги
        public string Title { get; set; }

        // Свойство Author типа string: автор книги
        public string Author { get; set; }

        // Свойство Reader типа string: имя читателя, который арендовал книгу; пустая строка означает, что книга свободна
        public string Reader { get; set; }

        // Вычисляемое свойство Status типа string (только get, без set): статус для таблицы; значение считается из поля Reader и в файл не записывается
        public string Status
        {
            // Метод string.IsNullOrEmpty: проверка, что строка равна null или пуста; оператор ?: (условие ? если да : если нет) выбирает нужный текст; оператор + склеивает текст и имя читателя
            get { return string.IsNullOrEmpty(Reader) ? "Свободна" : "Арендована: " + Reader; }
        }
    }
}
