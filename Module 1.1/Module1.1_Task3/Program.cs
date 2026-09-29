while (true)
{
    Console.Write("Введите ваше имя: ");
    string firstName = Console.ReadLine();

    Console.Write("Введите вашу фамилию: ");
    string lastName = Console.ReadLine();

    Console.WriteLine($"Результат: {lastName}, {firstName}");

    Console.WriteLine("Для выхода введите 'q' (или нажмите Enter для повтора):");
    if (Console.ReadLine().ToLower() == "q") break;
    Console.Clear();
}