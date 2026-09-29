Random rnd = new Random();

while (true)
{
    int[] numbers = new int[20];
    int min = int.MaxValue;
    int max = int.MinValue;

    Console.Write("Сгенерированный массив: ");

    for (int i = 0; i < numbers.Length; i++)
    {
        numbers[i] = rnd.Next(1, 101);
        Console.Write(numbers[i] + " ");

        if (numbers[i] < min) min = numbers[i];
        if (numbers[i] > max) max = numbers[i];
    }

    Console.WriteLine($"Минимальное значение: {min}");
    Console.WriteLine($"Максимальное значение: {max}");

    Console.WriteLine("Для выхода введите 'q' (или нажмите Enter для генерации нового массива):");
    if (Console.ReadLine().ToLower() == "q") break;
    Console.Clear();
}