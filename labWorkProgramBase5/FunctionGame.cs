using System;

namespace LabWork5
{
    /// <summary>
    /// Класс для игры по отгадыванию значения функции
    /// </summary>
    public static class FunctionGame
    {
        const double Pi = Math.PI;

        /// <summary>
        /// Метод вычисляет значение функции
        /// </summary>
        /// <param name="a">Параметр a</param>
        /// <param name="b">Параметр b</param>
        /// <returns>Значение функции</returns>
        public static double CalculateFunction(double a, double b)
        {
            return Math.Sin((Math.Pow(a, 3) + Math.Pow(b, 5)) / 2 * Pi) + Math.Cbrt(Math.Cos(a + b));
        }

        /// <summary>
        /// Метод. Игра: угадать значение функции за 3 попытки
        /// </summary>
        /// <param name="result">Правильный ответ</param>
        public static void CountingAttempts(double result)
        {
            double userResult;
            string finalAnswer = "";
            bool tryFlag = false;

            for (int i = 3; i > 0 && !tryFlag; i--)
            {
                Console.Write($"Осталось {i} попыток! ");
                Console.Write("Каков Ваш ответ? (Число с двумя цифрами после запятой): ");

                userResult = InputValidator.UserValue("", $"Нужно ввести число! У Вас ещё  {i} попыток: ");

                if (Math.Round(result, 2) == Math.Round(userResult, 2))
                {
                    finalAnswer = "настоящий арифметикъ!";
                    tryFlag = true;
                }
                else
                {
                    Console.WriteLine("Вы изъ политеха?");
                    InputValidator.ErrorBeep();
                    finalAnswer = "гуманитарий, идите вон, в САМгту!";
                }
            }

            Console.WriteLine($"Истинный ответ выражения: {result:F2}. Сеньор, да Вы {finalAnswer}");
            Console.WriteLine("Нажмите на клавишу, чтобы вернуться в лобби!");
        }

        /// <summary>
        /// Метод запускает игру
        /// </summary>
        public static void GuessTheAnswerGame()
        {
            double a, b, result;

            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine(@"
                                                            

                                                ⎛ α³ + β⁵ ⎞
                                        f = sin ⎜ ─────── ⎟  +  ∛ cos(α + β)
                                                ⎝   2π    ⎠");

            a = InputValidator.UserValue("Введите значения α: ", "Некорректный ввод для α, повторите ввод, м-сье: ");
            b = InputValidator.UserValue("Введите значения β: ", "Некорректный ввод для β, повторите ввод, м-сье: ");

            result = CalculateFunction(a, b);

            CountingAttempts(result);

            Console.ReadKey(true);
            Console.Clear();
        }
    }
}