using System;
using System.Collections.Generic; // Необходим для использования динамических списков

class Program
{
    static void Main()
    {
        Console.Write("Введите максимальную сумму: ");
        int maxSum = int.Parse(Console.ReadLine());

        // Динамическая коллекция, размер которой может изменяться во время выполнения
        List<int> resultList = new List<int>();
        Random rnd = new Random();
        int currentSum = 0;

        // Проверка условия перед каждой итерацией (пока разность >= 1)
        while (maxSum - currentSum >= 1)
        {
            int val = rnd.Next(1, 10); // Генерируем числа в диапазоне от 1 до 9

            if (currentSum + val <= maxSum)
            {
                resultList.Add(val); // Метод .Add() добавляет элемент в конец списка
                currentSum += val;   // currentSum = currentSum + val
            }
        }

        // resultList.Count — свойство, возвращающее текущее количество элементов в списке
        Console.WriteLine($"Элементов: {resultList.Count}");
        foreach (int item in resultList) Console.Write(item + " ");
        Console.WriteLine($"\nИтоговая сумма: {currentSum}");
    }
}