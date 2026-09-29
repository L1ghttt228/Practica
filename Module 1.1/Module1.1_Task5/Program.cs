while (true)
{
    Console.Write("Введите ваш возраст: ");
    string input = Console.ReadLine();

    if (int.TryParse(input, out int age))
    {
        if (age >= 18)
        {
            Console.WriteLine("Вы можете получить водительские права.");
        }
        else if (age >= 0)
        {
            Console.WriteLine($"Вам еще рано получать водительские права. Приходите через {18 - age} лет.");
        }
        else
        {
            Console.WriteLine("Ошибка: возраст не может быть отрицательным.");
        }
    }
    else
    {
        Console.WriteLine("Ошибка: некорректный ввод возраста.");
    }

    Console.WriteLine("Для выхода введите 'q' (или нажмите Enter для повтора):");
    if (Console.ReadLine().ToLower() == "q") break;
    Console.Clear();
}