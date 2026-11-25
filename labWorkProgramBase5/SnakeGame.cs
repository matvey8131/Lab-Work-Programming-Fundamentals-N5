using System;
using System.Threading;

namespace LabWork5
{
    /// <summary>
    /// Класс реализации игры "Змiй"
    /// </summary>
    class SnakeGame
    {
        const int START_GAME = 1, GAME_INFORMATION = 2, GAME_EXIT = 3;
        const int DEFAULT_GAME_FRAME_CHANGE_WAITING = 300;
        const int FRAME_CHANGE_WAITING_LIMIT = 20;
        const int FRAME_CHANGE_WAITING_STEP = 1;
        const int DEFAULT_GAME_SNAKE_SIZE = 1;
        const byte GAME_F_FREE_SPACE = 0;
        const byte GAME_F_SNAKE_BODY = 1;
        const byte GAME_F_SNAKE_HEAD = 2;
        const byte GAME_F_FOOD = 3;

        // Размер поля
        const int WIDTH = 20;
        const int HEIGHT = 20;

        static int[,] field = new int[HEIGHT, WIDTH];
        static int[,] screen = new int[HEIGHT, WIDTH];
        static int[] snakeX = new int[WIDTH * HEIGHT];
        static int[] snakeY = new int[WIDTH * HEIGHT];

        static int score, length, frameChangeWaitng;

        static int dirX = 1;
        static int dirY = 0;

        static int foodX;
        static int foodY;

        static int playersBestScore = 0;
        static Random rnd = new Random();

        static bool gameOver = false;

        /// <summary>
        /// Сбрасывает значения игры
        /// </summary>
        public static void ResetValues()
        {
            score = 0;
            length = DEFAULT_GAME_SNAKE_SIZE;
            frameChangeWaitng = DEFAULT_GAME_FRAME_CHANGE_WAITING;
        }

        /// <summary>
        /// Выход из игры
        /// </summary>
        public static void GameExit()
        {
            gameOver = true;
        }

        /// <summary>
        /// Лобби игры
        /// </summary>
        /// <returns>Номер выбранного пункта</returns>
        public static int DrawMenu()
        {
            string[] options = { "Начать", "Информация", "На выходъ в лобби" };
            ConsoleKey key;
            int selected = 0;

            do
            {
                Console.Clear();
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("=== ПОТЕХА В ЗМiЯ ===\n");

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
        /// Информация об игре
        /// </summary>
        public static void GameInformation()
        {
            Console.Clear();
            Console.WriteLine($"Управлять Змiем на стрелочки\nВашъ лучший результатъ: {playersBestScore}\nПоследний результатъ: {score}\n\nНажмите любую кнопку...");
            Console.ReadKey();
        }

        /// <summary>
        /// Запускает игру
        /// </summary>
        public static void StartGame()
        {
            Console.Clear();
            ResetValues();
            InitGame();

            bool isGameOver = false;

            while (!isGameOver)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);

                    if (key.Key == ConsoleKey.UpArrow && dirY != 1) { dirX = 0; dirY = -1; }
                    else if (key.Key == ConsoleKey.DownArrow && dirY != -1) { dirX = 0; dirY = 1; }
                    else if (key.Key == ConsoleKey.LeftArrow && dirX != 1) { dirX = -1; dirY = 0; }
                    else if (key.Key == ConsoleKey.RightArrow && dirX != -1) { dirX = 1; dirY = 0; }
                }

                isGameOver = !Step();
                Draw();
                Thread.Sleep(frameChangeWaitng);
            }

            if (score > playersBestScore)
            {
                playersBestScore = score;
            }

            Console.Clear();
            Console.WriteLine($"Потеха завершена! Результатъ: {score}\nВеличайший результатъ: {playersBestScore}");
            Console.ReadKey();
        }

        static void InitGame()
        {
            for (int y = 0; y < HEIGHT; y++)
            {
                for (int x = 0; x < WIDTH; x++)
                {
                    field[y, x] = GAME_F_FREE_SPACE;
                    screen[y, x] = -1;
                }
            }

            int startX = WIDTH / 2;
            int startY = HEIGHT / 2;

            for (int i = 0; i < length; i++)
            {
                snakeX[i] = startX - i;
                snakeY[i] = startY;
                field[snakeY[i], snakeX[i]] = (i == 0) ? GAME_F_SNAKE_HEAD : GAME_F_SNAKE_BODY;
            }

            dirX = 1;
            dirY = 0;
            SpawnFood();
        }

        static void SpawnFood()
        {
            bool placed = false;

            while (!placed)
            {
                int x = rnd.Next(1, WIDTH - 1);
                int y = rnd.Next(1, HEIGHT - 1);

                if (field[y, x] == GAME_F_FREE_SPACE)
                {
                    foodX = x;
                    foodY = y;
                    field[foodY, foodX] = GAME_F_FOOD;
                    placed = true;
                }
            }
        }

        static bool Step()
        {
            int newX = snakeX[0] + dirX;
            int newY = snakeY[0] + dirY;

            if (newX <= 0 || newX >= WIDTH - 1 || newY <= 0 || newY >= HEIGHT - 1)
            {
                return false;
            }

            if (field[newY, newX] == GAME_F_SNAKE_BODY)
            {
                return false;
            }

            bool ate = (newX == foodX && newY == foodY);

            if (ate)
            {
                for (int i = length; i > 0; i--)
                {
                    snakeX[i] = snakeX[i - 1];
                    snakeY[i] = snakeY[i - 1];
                }
                length++;
            }
            else
            {
                field[snakeY[length - 1], snakeX[length - 1]] = GAME_F_FREE_SPACE;

                for (int i = length - 1; i > 0; i--)
                {
                    snakeX[i] = snakeX[i - 1];
                    snakeY[i] = snakeY[i - 1];
                }
            }

            snakeX[0] = newX;
            snakeY[0] = newY;

            if (length > 1)
            {
                field[snakeY[1], snakeX[1]] = GAME_F_SNAKE_BODY;
            }

            field[snakeY[0], snakeX[0]] = GAME_F_SNAKE_HEAD;

            if (ate)
            {
                score++;

                if (frameChangeWaitng - FRAME_CHANGE_WAITING_STEP > FRAME_CHANGE_WAITING_LIMIT)
                {
                    frameChangeWaitng -= FRAME_CHANGE_WAITING_STEP;
                }
                else
                {
                    frameChangeWaitng = FRAME_CHANGE_WAITING_LIMIT;
                }

                SpawnFood();
            }

            return true;
        }

        static void Draw()
        {
            Console.SetCursorPosition(0, 0);
            Console.Write($"Результатъ: {score}    ");

            Console.SetCursorPosition(0, 1);
            for (int x = 0; x < WIDTH; x++) Console.Write("*");

            for (int y = 0; y < HEIGHT; y++)
            {
                Console.SetCursorPosition(0, y + 2);
                Console.Write("*");

                for (int x = 0; x < WIDTH; x++)
                {
                    if (field[y, x] != screen[y, x])
                    {
                        char symbol = ' ';

                        if (field[y, x] == GAME_F_SNAKE_BODY)
                        {
                            symbol = 'o';
                        }
                        else if (field[y, x] == GAME_F_SNAKE_HEAD)
                        {
                            symbol = '@';
                        }
                        else if (field[y, x] == GAME_F_FOOD)
                        {
                            symbol = '*';
                        }

                        Console.SetCursorPosition(x + 1, y + 2);
                        Console.Write(symbol);
                        screen[y, x] = field[y, x];
                    }
                }

                Console.SetCursorPosition(WIDTH + 1, y + 2);
                Console.Write("*");
            }

            Console.SetCursorPosition(0, HEIGHT + 2);
            for (int x = 0; x < WIDTH; x++)
            {
                Console.Write("*");
            }
        }

        /// <summary>
        /// Вход в игру
        /// </summary>
        public static void Run()
        {
            Console.SetWindowSize(80, 40);
            Console.SetBufferSize(80, 40);

            gameOver = false;
            Console.CursorVisible = false;

            while (!gameOver)
            {
                Console.Clear();

                switch (DrawMenu() + 1)
                {
                    case START_GAME:
                        StartGame();
                        break;
                    case GAME_INFORMATION:
                        GameInformation();
                        break;
                    case GAME_EXIT:
                        GameExit();
                        break;
                }
            }

            Console.CursorVisible = true;
        }
    }
}