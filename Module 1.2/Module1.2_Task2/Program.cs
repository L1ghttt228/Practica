using System;

class Program
{
    static void Main()
    {
        // Инициализация массива явным перечислением значений в фигурных скобках
        int[] array = { 12, -5, 48, 7, 0, 48, 15, 3, 9, -2 };

        Console.Write("Введите целое число для замены: ");
        int newValue = int.Parse(Console.ReadLine());

        int maxIndex = 0; // Начинало поиска, с предположением: максимум — первый элемент

        // Итерация со 2 элемента до конца массива
        for (int i = 1; i < array.Length; i++)
        {
            // Оператор сравнения '>' возвращает true, если правый элемент строго больше
            if (array[i] > array[maxIndex])
                maxIndex = i; // Сохранение индекса текущего найденного максимума
        }

        array[maxIndex] = newValue; // Оператор '=' присваивает новое значение по найденному индексу

        Console.WriteLine("Массив после замены:");
        // Итерация по коллекции для вывода
        foreach (int num in array)
            Console.Write(num + " "); // Оператор '+' выполняет конкатенацию строк
        Console.ReadKey();
    }
}