using System;

namespace LabWork5
{
    /// <summary>
    /// Класс для проверки вводимых данных
    /// </summary>
    public static class InputValidator
    {
        /// <summary>
        /// Ультравуковой сигнал неккоректно введеного значения для работы программы, потмоу что дауны
        /// </summary>
        public static void ErrorBeep()
        {
            Console.Beep(300, 250);
        }

        /// <summary>
        /// Метод ввода вещественного числа с проверкой
        /// </summary>
        /// <param name="message">Сообщение для ввода</param>
        /// <param name="errorMessage">Сообщение об ошибке</param>
        /// <returns>Введённое число</returns>
        public static double UserValue(string message, string errorMessage)
        {
            double value;

            Console.WriteLine(message);

            while (!double.TryParse(Console.ReadLine(), out value))
            {
                Console.Write(errorMessage);
                ErrorBeep();
            }

            return value;
        }

        /// <summary>
        /// Метод ввода целого числа с проверкой
        /// </summary>
        /// <param name="message">Сообщение для ввода</param>
        /// <param name="errorMessage">Сообщение об ошибке</param>
        /// <returns>Введённое число</returns>
        public static int UserPositiveInt(string message, string errorMessage)
        {
            int n;

            Console.Write(message);

            while (!int.TryParse(Console.ReadLine(), out n) || n <= 0)
            {
                Console.Write(errorMessage);
                ErrorBeep();
            }

            return n;
        }
    }
}