using System;

namespace Task5Events
{
    // Класс с данными события
    class TemperatureChangedEventArgs : EventArgs
    {
        public double Temperature { get; private set; }

        // Конструктор данных события
        public TemperatureChangedEventArgs(double temperature)
        {
            Temperature = temperature;
        }
    }

    // Класс датчика температуры
    class TemperatureSensor
    {
        // event — событие для уведомления подписчиков
        // Пустой делегат — отсутствие ошибки при отсутствии подписчиков
        public event EventHandler<TemperatureChangedEventArgs> TemperatureChanged = delegate { };

        private double currentTemperature;

        // Смена температуры
        public void SetTemperature(double temperature)
        {
            // Если температура не меняется, создание события не выполняется
            if (Math.Abs(currentTemperature - temperature) < 1e-9)
            {
                Console.WriteLine($"Температура {temperature} не изменилась. Событие не создаётся.");
                return;
            }

            currentTemperature = temperature;
            Console.WriteLine($"Датчик: температура стала {temperature}");

            // Вызов события
            // Все подписанные методы будут выполнены
            TemperatureChanged(this, new TemperatureChangedEventArgs(temperature));
        }
    }

    // Класс термостата
    class Thermostat
    {
        // Порог температуры для отопления
        private double threshold = 20.0;

        // Флаг состояния отопления
        private bool heatingOn = false;

        // Подписка на событие датчика
        public void Subscribe(TemperatureSensor sensor)
        {
            // += — добавление метода в обработчики события
            sensor.TemperatureChanged += HandleTemperatureChanged;
        }

        // Обработчик события
        // Реакция термостата на смену температуры
        private void HandleTemperatureChanged(object sender, TemperatureChangedEventArgs e)
        {
            Console.WriteLine($"Термостат: получена температура {e.Temperature:F1}");

            // Если температура ниже порога и отопление выключено, включение отопления
            if (e.Temperature < threshold && !heatingOn)
            {
                heatingOn = true;
                Console.WriteLine("Термостат: отопление включено.");
            }
            // Если температура выше или равна порогу и отопление включено, выключение отопления
            else if (e.Temperature >= threshold && heatingOn)
            {
                heatingOn = false;
                Console.WriteLine("Термостат: отопление выключено.");
            }
            // Если смена состояния не требуется
            else
            {
                Console.WriteLine("Термостат: состояние отопления не меняется.");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Создание датчика
            TemperatureSensor sensor = new TemperatureSensor();

            // Создание термостата
            Thermostat thermostat = new Thermostat();

            // Подписка термостата на событие датчика
            thermostat.Subscribe(sensor);

            // Смена температуры и проверка события
            sensor.SetTemperature(18.0); // Включение отопления
            sensor.SetTemperature(28.0); // Выключение отопления
            sensor.SetTemperature(20.0); // Включение отопления
            sensor.SetTemperature(20.0); // Если температура не меняется, событие не создаётся

            Console.ReadKey();
        }
    }
}