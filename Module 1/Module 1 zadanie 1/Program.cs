while (true)
{ 
Console.Write("Введите четное число: ");
string input = Console.ReadLine();
    if (int.TryParse(input, out int result))
    {
        if (result % 2 == 0)
        {
            Console.WriteLine("Число четное.");
        }
        else
        {
            Console.WriteLine("Число нечетное.");
        }
    }
    else
    {
        Console.WriteLine("Ошибка: введено не число.");
    }
        Console.WriteLine("Хотите выйти? (введите 'q' для выхода, или нажмите Enter для продолжения)");
        string exitChoice = Console.ReadLine();
        if (exitChoice.ToLower() == "q")
        {
            break;
        }

        Console.Clear();
    }