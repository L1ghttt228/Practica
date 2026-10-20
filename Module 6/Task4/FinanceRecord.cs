using System;

namespace FinanceTracker
{
    // Класс FinanceRecord (модель): один объект соответствует одной строке файла finance.txt и хранит одну финансовую операцию
    public class FinanceRecord
    {
        // Автоматическое свойство Date типа DateTime: дата операции
        public DateTime Date { get; set; }

        // Свойство Type типа string: тип операции (Доход или Расход)
        public string Type { get; set; }

        // Свойство Category типа string: категория операции (зарплата, еда и т.д.)
        public string Category { get; set; }

        // Свойство Amount типа decimal: сумма операции; decimal подходит для денег, потому что не даёт ошибок округления
        public decimal Amount { get; set; }
    }
}
