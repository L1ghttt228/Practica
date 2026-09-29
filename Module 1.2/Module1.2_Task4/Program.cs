using System;

class Program
{
    static void Main()
    {
        Console.Write("Введите K: "); int k = int.Parse(Console.ReadLine());
        Console.Write("Введите A: "); int a = int.Parse(Console.ReadLine());
        Console.Write("Введите B: "); int b = int.Parse(Console.ReadLine());

        int[] array = new int[k];
        Random rnd = new Random();
        int minIdx = 0, maxIdx = 0;

        // Генерация массива и одновременный поиск индексов
        for (int i = 0; i < k; i++)
        {
            // rnd.Next(min, max) генерирует случайное число в диапазоне [min, max)
            array[i] = rnd.Next(Math.Min(a, b), Math.Max(a, b));

            if (array[i] < array[minIdx]) minIdx = i;
            if (array[i] > array[maxIdx]) maxIdx = i;
        }

        // string.Join() объединяет элементы массива в одну строку через указанный разделитель " "
        Console.WriteLine("Массив: " + string.Join(" ", array));

        int start = Math.Min(minIdx, maxIdx); // По наименьшему индексу определяем левую границу
        int end = Math.Max(minIdx, maxIdx);   // По наибольшему индексу определяем правую границу

        Console.WriteLine($"\nЭлементы от индекса {start} до {end}:");
        // Проход строго от границы start до границы end включительно
        for (int i = start; i <= end; i++)
            Console.Write(array[i] + " ");
        Console.ReadKey();
    }
}