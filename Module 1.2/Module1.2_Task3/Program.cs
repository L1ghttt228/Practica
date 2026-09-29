using System;

class Program
{
    static void Main()
    {
        Console.Write("Введите количество простых чисел K: ");
        int k = int.Parse(Console.ReadLine());

        int count = 0, currentNumber = 2;

        // Цикл выполняется, пока счетчик найденных чисел меньше K
        while (count < k)
        {
            bool isPrime = true; // Флаг предполагает, что число простое

            // Вычисление квадратного корня
            for (int i = 2; i <= Math.Sqrt(currentNumber); i++)
            {
                // Оператор '%' вычисляет остаток от деления. Остаток == 0 означает, что число делится нацело
                if (currentNumber % i == 0)
                {
                    isPrime = false;
                    break; // Оператор break досрочно прерывает цикл for
                }
            }

            if (isPrime)
            {
                Console.Write($"{currentNumber,6} "); // Вывод с выравниванием в ширину 6 символов
                count++; // Инкремент '++' увеличивает значение счетчика на 1

                // Если остаток от деления count на 10 равен 0, переходим на новую строку
                if (count % 10 == 0) Console.WriteLine();
            }

            currentNumber++; // Переход к следующему проверяемому числу
        }
        Console.ReadKey();
    }
}