using System;

class Program
{
    static void Main()
    {
        Console.Write("Размер матрицы N: ");
        int n = int.Parse(Console.ReadLine());

        // int[,] — синтаксис объявления двумерного прямоугольного массива (матрицы)
        int[,] matrix = new int[n, n];
        Random rnd = new Random();

        // Вложенные циклы for: внешний по строкам (i), внутренний по столбцам (j)
        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
                matrix[i, j] = rnd.Next(-50, 51); // Диапазон [-50, 50] включительно

        int[] rowSums = new int[n]; // Одномерный массив для сохранения сумм строк

        for (int i = 0; i < n; i++)
        {
            int sum = 0;
            // Складываем все элементы текущей i-й строки
            for (int j = 0; j < n; j++) sum += matrix[i, j];
            rowSums[i] = sum;
        }

        // Сортировка методом пузырька
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - 1 - i; j++)
            {
                // Если сумма текущей строки больше суммы следующей — меняем их местами
                if (rowSums[j] > rowSums[j + 1])
                {
                    // 1. Меняем местами суммы в массиве rowSums
                    int tempSum = rowSums[j];
                    rowSums[j] = rowSums[j + 1];
                    rowSums[j + 1] = tempSum;

                    // 2. Третий вложенный цикл for: поэлементно меняем значения ВСЕЙ строки j и j+1 в самой матрице
                    for (int k = 0; k < n; k++)
                    {
                        int tempVal = matrix[j, k];
                        matrix[j, k] = matrix[j + 1, k];
                        matrix[j + 1, k] = tempVal;
                    }
                }
            }
        }

        Console.WriteLine("\nМатрица после сортировки:");
        for (int i = 0; i < n; i++)
        {
            // Specifier '{matrix[i, j],5}' форматирует вывод элемента с шириной поля в 5 символов
            for (int j = 0; j < n; j++) Console.Write($"{matrix[i, j],5} ");
            Console.WriteLine($" | Сумма = {rowSums[i]}");
        }
    }
}