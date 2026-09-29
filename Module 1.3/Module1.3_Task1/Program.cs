using System;

class Program
{
       public static int GetGCM(int a, int b)
    {
        // Цикл выполняется, пока делитель b не станет равен 0
        while (b != 0)
        {
            int temp = b;
            b = a % b; // Вычисление остатка от деления
            a = temp;
        }
        return a; // Оператор return возвращает итоговый НОД
    }

    static void Main()
    {
        Console.Write("Числитель: "); int num = int.Parse(Console.ReadLine());
        Console.Write("Знаменатель: "); int den = int.Parse(Console.ReadLine());

        // Вызов статического метода GetGCM
        int gcm = GetGCM(num, den);

        // Математический оператор '/' с целыми числами выполняет деление нацело без остатка
        Console.WriteLine($"Сокращенная дробь: {num / gcm}/{den / gcm}");
    }
}