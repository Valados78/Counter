using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Counter
{
    class Program
    {
        // Тут выбрал интерфейс для всех счётчиков, а не абстрактный класс 
        public interface ICounter
        {
            Task AuthorizeAsync(string address, string password);
            DateTime ReadDateTime();
            void WriteDateTime(DateTime dateTime);
            void SetTariffProgram(int tariffNumber, string startTime);
        }

        public class CE207 : ICounter
        {
            private const string Address = "1234567890123456";
            private const string Password = "1234567812345678";
            private List<(int tariffNumber, string startTime)> tariffProgram = new List<(int, string)>();

            public async Task AuthorizeAsync(string address, string password)
            {
                Console.WriteLine("Авторизация в " + this.GetType().Name + "...");
                await Task.Delay(2000); // Имитация 

                if (address != Address || password != Password)
                {
                    throw new UnauthorizedAccessException("Неверный адрес или пароль!");
                } // Исключение

                Console.WriteLine("Авторизация успешна.");
            }

            public DateTime ReadDateTime()
            {
                DateTime currentTime = DateTime.Now;
                Console.WriteLine($"Считанное время с ПК: {currentTime:dd.MM.yyyy HH:mm:ss}");
                return currentTime;
            }

            public void WriteDateTime(DateTime dateTime)
            {
                Console.WriteLine($"Дата и время для записи: {dateTime:yyyy.MM.dd HH.mm.ss}");
            }

            public void SetTariffProgram(int tariffNumber, string startTime)
            {
                if (tariffProgram.Count >= 4)
                {
                    Console.WriteLine("Достигнуто максимальное количество тарифов (4).");
                    return;
                }

                tariffProgram.Add((tariffNumber, startTime));
                Console.WriteLine($"Записан тариф: {tariffNumber}, время начала: {startTime}");
            }
        }

        public class CE208 : ICounter
        {
            private const string Address = "1234567890123456";
            private const string Password = "1234567812345678";
            private List<(int tariffNumber, string startTime)> tariffProgram = new List<(int, string)>();

            public async Task AuthorizeAsync(string address, string password)
            {
                Console.WriteLine("Авторизация в " + this.GetType().Name + "...");
                await Task.Delay(2000); // Имитация 

                if (address != Address || password != Password)
                {
                    throw new UnauthorizedAccessException("Неверный адрес или пароль!");
                } // Исключение

                Console.WriteLine("Авторизация успешна.");
            }

            public DateTime ReadDateTime()
            {
                DateTime currentTime = DateTime.Now;
                Console.WriteLine($"Считанное время с ПК: {currentTime:dd.MM.yyyy HH:mm:ss}");
                return currentTime;
            }

            public void WriteDateTime(DateTime dateTime)
            {
                Console.WriteLine($"Дата и время для записи: {dateTime:yyyy.MM.dd HH.mm.ss}");
            }

            public void SetTariffProgram(int tariffNumber, string startTime)
            {
                if (tariffProgram.Count >= 8)
                {
                    Console.WriteLine("Достигнуто максимальное количество тарифов (8).");
                    return;
                }

                tariffProgram.Add((tariffNumber, startTime));
                Console.WriteLine($"Записан тариф: {tariffNumber}, время начала: {startTime}");
            }
        }

        static async Task Main(string[] args)
        {
            ICounter counter207 = new CE207();
            ICounter counter208 = new CE208();

            try
            {
                await counter207.AuthorizeAsync("1234567890123456", "1"); // Добавил ещё асинхронность, чтобы при провале проверки 1-го, 2-й вел авторизацию тоже
                counter207.ReadDateTime();
                counter207.WriteDateTime(DateTime.Now);

                counter207.SetTariffProgram(1, "00:00");
                counter207.SetTariffProgram(2, "06:00");
                counter207.SetTariffProgram(3, "12:00");
                counter207.SetTariffProgram(4, "18:00");

                Console.WriteLine();

            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine(ex.Message);
            }

            try
            {
                await counter208.AuthorizeAsync("1234567890123456", "1234567812345678");
                counter208.ReadDateTime();
                counter208.WriteDateTime(DateTime.Now);

                counter208.SetTariffProgram(1, "00:00");
                counter208.SetTariffProgram(2, "06:00");
                counter208.SetTariffProgram(3, "12:00");
                counter208.SetTariffProgram(4, "18:00");
                counter208.SetTariffProgram(5, "22:00");
                counter208.SetTariffProgram(6, "23:00");
                counter208.SetTariffProgram(7, "01:00");
                counter208.SetTariffProgram(8, "02:00");

            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}