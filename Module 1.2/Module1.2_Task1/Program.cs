using System;

class Program
{
    static void Main()
    {
        Console.Write("Введите размер массива N: ");
        int n = int.Parse(Console.ReadLine());
        double[] array = new double[n]; // Выделение памяти под массив из n элементов типа double

        // Цикл с шагом 1 для заполнения массива
        for (int i = 0; i < n; i++)
        {
            Console.Write($"array[{i}] = ");
            // Преобразование строки в вещественное число
            array[i] = double.Parse(Console.ReadLine());
        }

        // Math.Abs() возвращает модуль числа
        double maxAbs = Math.Abs(array[0]);

        // Последовательный перебор элементов без использования индексов
        foreach (double item in array)
        {
            if (Math.Abs(item) > maxAbs)
                maxAbs = Math.Abs(item); // Обновляем максимальный модуль
        }

        // Изменение элементов массива по их индексам
        for (int i = 0; i < n; i++)
            array[i] /= maxAbs; // (array[i] = array[i] / maxAbs)

        Console.WriteLine("\nИзмененный массив:");
        // Вывод результатов
        foreach (double item in array)
            Console.Write($"{item:F2} "); // Форматирование числа до 2 знаков после запятой
    }
}