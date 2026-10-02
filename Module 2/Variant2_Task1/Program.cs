using System;

namespace Variant2Task1Car
{
    // Класс автомобиля
    class Car
    {
        // Свойства автомобиля
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }

        // decimal — тип для денежных значений
        public decimal BasePrice { get; set; }

        // Конструктор автомобиля
        public Car(string brand, string model, int year, decimal basePrice)
        {
            Brand = brand;
            Model = model;
            Year = year;
            BasePrice = basePrice;
        }

        // Проверка процента
        // Если процент некорректный, создание исключения
        private void CheckPercent(decimal percent, string parameterName)
        {
            if (percent < 0m || percent > 100m)
            {
                throw new ArgumentOutOfRangeException(
                    parameterName,
                    "Процент должен быть от 0 до 100.");
            }
        }

        // Расчёт цены со скидкой
        public decimal CalculatePriceWithDiscount(decimal discountPercent)
        {
            CheckPercent(discountPercent, nameof(discountPercent));

            // Формула: базовая цена * (100 - скидка) / 100
            return BasePrice * (100m - discountPercent) / 100m;
        }

        // Расчёт цены с НДС
        public decimal CalculatePriceWithVat(decimal vatPercent)
        {
            CheckPercent(vatPercent, nameof(vatPercent));

            // Формула: базовая цена * (100 + НДС) / 100
            return BasePrice * (100m + vatPercent) / 100m;
        }

        // Итоговый расчёт: сначала скидка, затем НДС
        public decimal CalculateFinalPrice(decimal discountPercent, decimal vatPercent)
        {
            // Получение цены после скидки
            decimal priceAfterDiscount = CalculatePriceWithDiscount(discountPercent);

            // Проверка НДС
            CheckPercent(vatPercent, nameof(vatPercent));

            // Добавление НДС к цене после скидки
            return priceAfterDiscount * (100m + vatPercent) / 100m;
        }

        // Вывод информации об автомобиле
        public void PrintInfo()
        {
            Console.WriteLine($"Автомобиль: {Brand} {Model}, год выпуска: {Year}, базовая цена: {BasePrice} руб.");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Создание объекта автомобиля
            Car car = new Car("Volkswagen", "Passat B5", 2003, 13500m);

            // Вывод исходной информации
            car.PrintInfo();

            // Скидка и НДС
            decimal discountPercent = 13m;
            decimal vatPercent = 19m;

            // Расчёт итоговой цены
            decimal finalPrice = car.CalculateFinalPrice(discountPercent, vatPercent);

            // Вывод результата
            Console.WriteLine($"Цена со скидкой {discountPercent}% и НДС {vatPercent}%: {finalPrice:F2} руб.");

            Console.ReadKey();
        }
    }
}