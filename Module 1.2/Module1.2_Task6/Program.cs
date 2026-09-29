using System;

class Program
{
    static void Main()
    {
        double[] array = new double[10];
        int[] indices = new int[10];
        Random rnd = new Random();

        // Pаполнение случайными числами и первичными индексами
        for (int i = 0; i < 10; i++)
        {
            // rnd.NextDouble() генерирует число от 0.0 до 1.0 -> умножим его на выражение * 20 - 10;
            array[i] = rnd.NextDouble() * 20 - 10;
            indices[i] = i;
        }

        // Пузырьковая сортировка: внешняя петля определяет количество проходов
        for (int i = 0; i < indices.Length - 1; i++)
        {
            // Внутренняя петля сравнивает соседние элементы
            for (int j = 0; j < indices.Length - 1 - i; j++)
            {
                // Сравнивание реальных значений массива, используя индексы из массива indices
                if (array[indices[j]] > array[indices[j + 1]])
                {
                    // Алгоритм обмена значений с использованием третьей переменной temp
                    int temp = indices[j];
                    indices[j] = indices[j + 1];
                    indices[j + 1] = temp;
                }
            }
        }

        Console.WriteLine("Исходный массив:");
        foreach (var val in array) Console.Write($"{val:F2} ");

        Console.WriteLine("\n\nМассив индексов (по возрастанию элементов):");
        foreach (var idx in indices) Console.Write(idx + " ");
    }
}