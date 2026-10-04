using System;

namespace Task5Events
{
    // класс TemperatureChangedEventArgs наследует EventArgs и хранит данные события
    class TemperatureChangedEventArgs : EventArgs
    {
        public double Temperature { get; private set; }

        public TemperatureChangedEventArgs(double temperature)
        {
            Temperature = temperature;
        }
    }

    // класс TemperatureSensor: генерирует событие изменения температуры
    class TemperatureSensor
    {
        // событие для уведомления подписчиков; пустой делегат предотвращает ошибку при отсутствии подписчиков
        public event EventHandler<TemperatureChangedEventArgs> TemperatureChanged = delegate { };

        private double currentTemperature;

        // метод изменяет температуру и создаёт событие
        public void SetTemperature(double temperature)
        {
            // если температура не изменилась, событие не генерируется
            if (Math.Abs(currentTemperature - temperature) < 1e-9)
            {
                Console.WriteLine($"Температура {temperature} не изменилась. Событие не создаётся.");
                return;
            }

            currentTemperature = temperature;
            Console.WriteLine($"Датчик: температура стала {temperature}");

            // вызов события: выполняются все методы, подписанные на TemperatureChanged
            TemperatureChanged(this, new TemperatureChangedEventArgs(temperature));
        }
    }

    // класс Thermostat: подписывается на событие датчика и реагирует на температуру
    class Thermostat
    {
        private double threshold = 20.0;
        private bool heatingOn = false;

        // метод подписывает термостат на событие датчика
        public void Subscribe(TemperatureSensor sensor)
        {
            // оператор += добавляет метод в список обработчиков события
            sensor.TemperatureChanged += HandleTemperatureChanged;
        }

        // метод-обработчик события: вызывается автоматически при генерации события датчиком
        private void HandleTemperatureChanged(object sender, TemperatureChangedEventArgs e)
        {
            Console.WriteLine($"Термостат: получена температура {e.Temperature:F1}");

            // логика включения или выключения отопления в зависимости от порога
            if (e.Temperature < threshold && !heatingOn)
            {
                heatingOn = true;
                Console.WriteLine("Термостат: отопление включено.");
            }
            else if (e.Temperature >= threshold && heatingOn)
            {
                heatingOn = false;
                Console.WriteLine("Термостат: отопление выключено.");
            }
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
            TemperatureSensor sensor = new TemperatureSensor();
            Thermostat thermostat = new Thermostat();

            thermostat.Subscribe(sensor);

            sensor.SetTemperature(18.0);
            sensor.SetTemperature(28.0);
            sensor.SetTemperature(20.0);
            sensor.SetTemperature(20.0);

            Console.ReadKey();
        }
    }
}