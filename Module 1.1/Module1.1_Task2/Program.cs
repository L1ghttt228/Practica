while (true)
{
    Console.Write("Введите радиус круга: ");
    string input = Console.ReadLine();

    if (double.TryParse(input, out double radius) && radius > 0)
    {
        double area = Math.PI * Math.Pow(radius, 2);
        Console.WriteLine($"Площадь круга: {Math.Round(area, 2)}");
    }
    else
    {
        Console.WriteLine("Ошибка: введите положительное число.");
    }

    Console.WriteLine("Для выхода введите 'q' (или нажмите Enter для повтора):");
    if (Console.ReadLine().ToLower() == "q") break;
    Console.Clear();
}