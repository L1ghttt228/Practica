using System;
using System.Linq; // Подключение пространств имён для работы с Contains

class Program
{
    static void Main()
    {
        Console.Write("Введите K: ");
        int k = int.Parse(Console.ReadLine());

        char[] source = new char[k]; // Символьный массив char
        Random rnd = new Random();
        string vowels = "аеёиоуыэюя"; // Строка-эталон для проверки гласных

        // Генерация букв
        for (int i = 0; i < k; i++)
            // (char) — явное приведение случайного целого кода символа Unicode к типу char
            source[i] = (char)rnd.Next('а', 'я' + 1);

        int consonantCount = 0;
        // Перебор всех символов c из массива source
        foreach (char c in source)
            // Логическое НЕ ('!'): если vowels НЕ содержит символ c, то это согласная
            if (!vowels.Contains(c)) consonantCount++;

        char[] consonants = new char[consonantCount]; // Создание массива точного размера
        int index = 0;

        foreach (char c in source)
            // index++ сначала записывает значение по индексу, затем увеличивает index на 1
            if (!vowels.Contains(c)) consonants[index++] = c;

        Console.WriteLine("Исходный: " + string.Join(" ", source));
        Console.WriteLine("Согласные: " + string.Join(" ", consonants));
        Console.ReadKey(); // Ожидание нажатия клавиши перед завершением программы
    }
}