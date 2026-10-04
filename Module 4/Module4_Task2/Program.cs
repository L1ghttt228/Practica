using System;

namespace Task2Product
{
    // интерфейс IProduct задаёт: название, стоимость и остаток товара
    interface IProduct
    {
        string GetName(); // объявление метода получения названия товара
        decimal GetCost(); // объявление метода получения стоимости партии товара
        int GetStock(); // объявление метода получения остатка товара на складе
    }

    // класс FoodProduct реализует интерфейс IProduct
    class FoodProduct : IProduct
    {
        private string name; 
        private decimal unitPrice; // decimal — тип данных для денежных сумм
        private int quantity; 
        private decimal discountPercent;

        // конструктор принимает параметры товара и записывает их в поля объекта
        public FoodProduct(string name, decimal unitPrice, int quantity, decimal discountPercent)
        {
            this.name = name; 
            this.unitPrice = unitPrice; 
            this.quantity = quantity; 
            this.discountPercent = discountPercent; 
        }

        // метод GetName возвращает название товара
        public string GetName()
        {
            return name; 
        }

        // метод GetCost возвращает стоимость партии с учётом скидки
        public decimal GetCost()
        {
            return unitPrice * quantity * (100m - discountPercent) / 100m; 
        }

        // метод GetStock возвращает остаток товара на складе
        public int GetStock()
        {
            return quantity; 
        }
    }

    // класс ElectronicsProduct реализует интерфейс IProduct
    class ElectronicsProduct : IProduct
    {
        private string name; 
        private decimal unitPrice; 
        private int quantity; 
        private decimal warrantyPercent; 

        // конструктор принимает параметры товара и записывает их в поля объекта
        public ElectronicsProduct(string name, decimal unitPrice, int quantity, decimal warrantyPercent)
        {
            this.name = name; 
            this.unitPrice = unitPrice; 
            this.quantity = quantity; 
            this.warrantyPercent = warrantyPercent; 
        }

        // метод GetName возвращает название товара
        public string GetName()
        {
            return name; 
        }

        // метод GetCost возвращает стоимость партии с учётом надбавки
        public decimal GetCost()
        {
            return unitPrice * quantity * (100m + warrantyPercent) / 100m; 
        }

        // метод GetStock возвращает остаток товара на складе
        public int GetStock()
        {
            return quantity; 
        }
    }

    // класс ClothingProduct реализует интерфейс IProduct
    class ClothingProduct : IProduct
    {
        private string name; 
        private decimal unitPrice; 
        private int quantity; 

        // конструктор принимает параметры товара и записывает их в поля объекта
        public ClothingProduct(string name, decimal unitPrice, int quantity)
        {
            this.name = name; 
            this.unitPrice = unitPrice; 
            this.quantity = quantity; 
        }

        // метод GetName возвращает название товара
        public string GetName()
        {
            return name; 
        }

        // метод GetCost возвращает стоимость партии без надбавок и скидок
        public decimal GetCost()
        {
            return unitPrice * quantity; 
        }

        // метод GetStock возвращает остаток товара на складе
        public int GetStock()
        {
            return quantity; 
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // объявление массива products типа IProduct
            IProduct[] products = new IProduct[]
            {
                new FoodProduct("Молоко", 80m, 50, 10m), // создаётся объект пищевого товара и кладётся в первый элемент массива
                new ElectronicsProduct("Ноутбук", 50000m, 5, 5m), // создаётся объект электроники и кладётся во второй элемент
                new ClothingProduct("Футболка", 1000m, 100) // создаётся объект одежды и кладётся в третий элемент
            };

            // цикл foreach: переменная product по очереди принимает каждый элемент массива products
            foreach (IProduct product in products)
            {
                // вывод названия, остатка и стоимости; методы вызываются у фактического типа объекта
                Console.WriteLine($"{product.GetName()}: остаток = {product.GetStock()} шт., стоимость партии = {product.GetCost():F2} руб.");
            }

            Console.ReadKey(); // ожидание нажатия любой клавиши, чтобы окно консоли не закрылось
        }
    }
}