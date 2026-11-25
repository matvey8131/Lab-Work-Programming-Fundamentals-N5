using System;
using System.IO;
using System.Threading;
using System.Diagnostics;
using LabWork5;
using System.Reflection.Metadata.Ecma335;

class Program
{
    const byte START_GAME = 1, AUTHOR_INFO = 2, ARRAY_SORT = 3, GAME = 4, EXIT_PROGRAM = 5;
    static bool programExit = false;

    /// <summary>
    /// Метод показывает информацию об авторе
    /// </summary>
    public static void AuthorInfo()
    {
        Console.Clear();

        try
        {
            string fileAnimation = File.ReadAllText(@"hello.txt");
            string[] animation = fileAnimation.Split('@');

            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.CursorVisible = false;

            for (int i = 0; i < animation.Length; i++)
            {
                Console.Clear();
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("\tМАТЕРОВ МАТВЕЙ АЛЕКСАНДРОВИЧ 6102-090301D");
                Console.WriteLine(animation[i].PadRight(Console.WindowWidth));

                Thread.Sleep(200);
            }

            Console.CursorVisible = true;
        }
        catch (DirectoryNotFoundException)
        {
            Console.WriteLine("\nУПСИ-ДУПИ\nТутъ ничерта нетути!");
        }
        catch (Exception)
        {

        }
        finally
        {
            Console.WriteLine("\nНажмите на клавишу, дабы вернутся в лобби!");
            Console.ReadKey(true);
        }
    }

    /// <summary>
    /// Метод показывает как работает прикольненький классик ArrayWorker: два конструктора, две сортировки и сравнение времени
    /// </summary>
    public static void ArraySortDemo()
    {
        Console.Clear();

        Stopwatch gnomeSw = new Stopwatch();
        Stopwatch shellSw = new Stopwatch();

        ArrayWorker defaultWorker = new ArrayWorker();
        defaultWorker.Fill();

        ArrayWorker gnomeArray1 = defaultWorker.Copy();
        ArrayWorker shellArray1 = defaultWorker.Copy();

        gnomeSw.Start();
        gnomeArray1.GnomeSort();
        gnomeSw.Stop();

        shellSw.Start();
        shellArray1.ShellSort();
        shellSw.Stop();

        Console.WriteLine("Конструктор без параметров (10 элементов)");
        Console.Write("Вашъ массив: ");
        Console.WriteLine(defaultWorker.ToString());

        Console.Write("После гномика: ");
        for (int i = 0; i < gnomeArray1.Size; i++)
        {
            Console.Write($"{gnomeArray1.getElementByidx(i)} ");
        }
        Console.WriteLine($"\nБыл отсортирован за: {gnomeSw.Elapsed.TotalMilliseconds:F2} мс");

        Console.Write("После шелла: ");
        for (int i = 0; i < shellArray1.Size; i++)
        {
            Console.Write($"{shellArray1.getElementByidx(i)} ");
        }
        Console.WriteLine($"\nБыл отсортирован за: {shellSw.Elapsed.TotalMilliseconds:F2} мс");

        if (gnomeSw.Elapsed.TotalMilliseconds < shellSw.Elapsed.TotalMilliseconds)
        {
            Console.WriteLine($"\nГномья сортировка прошла быстрее на {(shellSw.Elapsed.TotalMilliseconds - gnomeSw.Elapsed.TotalMilliseconds):F4} мс");
        }
        else if (gnomeSw.Elapsed.TotalMilliseconds > shellSw.Elapsed.TotalMilliseconds)
        {
            Console.WriteLine($"\nСортировка Шелла прошла быстрее на {(gnomeSw.Elapsed.TotalMilliseconds - shellSw.Elapsed.TotalMilliseconds):F4} мс");
        }
        else
        {
            Console.WriteLine($"\nОбе сортировки прошлись за одно и тоже время, мазафака");
        }

        int n = InputValidator.UserPositiveInt("Подайте программе размеръ массива, Сир: ",
            "Укажите целое число, Джентельмэнъ!: ");

        ArrayWorker customWorker = new ArrayWorker(n);
        customWorker.Fill();

        ArrayWorker gnomeArray2 = customWorker.Copy();
        ArrayWorker shellArray2 = customWorker.Copy();

        gnomeSw.Reset();
        shellSw.Reset();

        gnomeSw.Start();
        gnomeArray2.GnomeSort();
        gnomeSw.Stop();

        shellSw.Start();
        shellArray2.ShellSort();
        shellSw.Stop();


        Console.WriteLine($"\nКонструктор с параметрами ({n} элементов)");

        if (n <= 10)
        {
            Console.Write("Вашъ массив: ");
            Console.WriteLine(customWorker.ToString());

            Console.Write("После гномика: ");
            for (int i = 0; i < gnomeArray2.Size; i++)
            {
                Console.Write($"{gnomeArray2.getElementByidx(i)} ");
            }
            Console.WriteLine($"\nБыл отсортирован за: {gnomeSw.Elapsed.TotalMilliseconds:F2} мс");

            Console.Write("После шелла: ");
            for (int i = 0; i < shellArray2.Size; i++)
            {
                Console.Write($"{shellArray2.getElementByidx(i)} ");
            }
            Console.WriteLine($"\nБыл отсортирован за: {shellSw.Elapsed.TotalMilliseconds:F2} мс");
        }
        else
        {
            Console.WriteLine($"\nМассив был отсортирован гномиком за: {gnomeSw.Elapsed.TotalMilliseconds:F2} мс");
            Console.WriteLine($"Массив был отсортирован шеллом за: {shellSw.Elapsed.TotalMilliseconds:F2} мс");
            Console.WriteLine("\nМассивы не могутъ быть выведены на экранъ, такъ какъ длина массива больше 10");
        }

        if (gnomeSw.Elapsed.TotalMilliseconds < shellSw.Elapsed.TotalMilliseconds)
        {
            Console.WriteLine($"\nГномья сортировка прошла быстрее на {(shellSw.Elapsed.TotalMilliseconds - gnomeSw.Elapsed.TotalMilliseconds):F4} мс");
        }
        else if (gnomeSw.Elapsed.TotalMilliseconds > shellSw.Elapsed.TotalMilliseconds)
        {
            Console.WriteLine($"\nСортировка Шелла прошла быстрее на {(gnomeSw.Elapsed.TotalMilliseconds - shellSw.Elapsed.TotalMilliseconds):F4} мс");
        }
        else
        {
            Console.WriteLine($"\nОбе сортировки прошлись за одно и тоже время, мазафака");
        }

        Console.WriteLine("Досвидания, черти");
        Console.ReadKey();
    }

    /// <summary>
    /// Метод спрашивает про выход
    /// </summary>
    /// <returns>true - выйти, false - остаться</returns>
    public static bool ProgramExit()
    {
        while (true)
        {
            Console.Write("Желаете покинуть нас, Сир? (д/н): ");

            switch (Console.ReadLine())
            {
                case "д":

                    Console.Clear();
                    Console.Write("Благополучия Вам и Вашей семье!");
                    return true;

                case "н":

                    return false;

            }
        }
    }

    /// <summary>
    /// Лобби
    /// </summary>
    /// <returns>Номер выбранного пункта</returns>
    public static int DrawMenu()
    {
        string[] options = { "Функция", "Объ поэте", "Сортировка массивов", "Игра", "На выходъ" };
        ConsoleKey key;
        int selected = 0;

        do
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("=== ЛОББИ ===\n");

            for (int i = 0; i < options.Length; i++)
            {
                if (i == selected)
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine($" > {options[i]} ");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"   {options[i]} ");
                }
            }

            key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.UpArrow) selected = (selected - 1 + options.Length) % options.Length;
            if (key == ConsoleKey.DownArrow) selected = (selected + 1) % options.Length;

        } while (key != ConsoleKey.Enter);

        return selected;
    }

    /// <summary>
    /// Метод входа в программу
    /// </summary>
    public static void Main()
    {
        while (!programExit)
        {
            programExit = false;

            switch (DrawMenu() + 1)
            {
                case START_GAME:

                    FunctionGame.GuessTheAnswerGame();

                    break;

                case AUTHOR_INFO:

                    AuthorInfo();

                    break;

                case ARRAY_SORT:

                    ArraySortDemo();

                    break;

                case GAME:

                    SnakeGame.Run();

                    break;

                case EXIT_PROGRAM:

                    programExit = ProgramExit();

                    break;
            }
        }
    }
}