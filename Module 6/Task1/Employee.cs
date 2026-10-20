namespace EmployeeManager
{
    // Класс Employee (модель): один объект соответствует одной строке файла employees.txt и хранит данные одного сотрудника
    public class Employee
    {
        // Автоматическое свойство FullName типа string: фамилия, имя и отчество
        public string FullName { get; set; }

        // Свойство Position типа string: должность
        public string Position { get; set; }

        // Свойство Department типа string: отдел
        public string Department { get; set; }

        // Свойство Salary типа decimal: оклад; decimal подходит для денег, потому что не даёт ошибок округления
        public decimal Salary { get; set; }
    }
}
