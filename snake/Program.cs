using System;
using System.Linq;
using System.Diagnostics;

namespace snake
{
     
    class Program
    {
        static readonly int fieldWidth = 40;
        static readonly int fieldHeight = 30;
        private const ConsoleColor BorderColor = ConsoleColor.Cyan;
        private const ConsoleColor HeadColor = ConsoleColor.Yellow;
        private const ConsoleColor Bodycolor = ConsoleColor.Magenta;
        private const ConsoleColor FoodColor = ConsoleColor.Red;
        private const int Frames = 150;

        static void Main()
        {
             
            Console.SetWindowSize(fieldWidth, fieldHeight);
            Console.SetBufferSize(fieldWidth, fieldHeight);
            Console.CursorVisible = false;
            while (true)
            {
                GameStart();
                Console.ReadKey();
            }
        }

        static void GameStart()
        {
            Console.Clear();

            DrawBorder();

            Direction movement = Direction.Up;

            var snake = new SnakeItSelf(initialX: fieldWidth / 2, initialY: fieldHeight / 2, HeadColor, Bodycolor);
            Stopwatch stw = new();
            int score = 0;

            Pixel food = GetFood(snake);
            food.Draw();

            while (true)
            {
                stw.Restart();

                Direction prevMovement = movement;
                while (stw.ElapsedMilliseconds <= Frames)
                {
                    if (movement == prevMovement)
                    {
                        movement = Movement(movement);
                    }

                }
                if(snake.Head.X == food.X && snake.Head.Y == food.Y)
                {
                    snake.Move(movement, eat: true);
                    food = GetFood(snake);
                    food.Draw();
                    score++;
                }
                else
                {
                    snake.Move(movement);
                }
               


                if (snake.Head.X == fieldWidth - 1
                    || snake.Head.X == 0
                    || snake.Head.Y == fieldHeight - 1
                    || snake.Head.Y == 0
                    || snake.Body.Any(i => i.X == snake.Head.X && i.Y == snake.Head.Y))

                    break;
            }

            snake.Clear();
            Console.SetCursorPosition(left: 15, top: 10);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("GAME OVER");
            Console.SetCursorPosition(left: 14, top: 13);
            Console.WriteLine($"U'r score: {score}");

        }

        static Pixel GetFood(SnakeItSelf snake)
        {
            Pixel food;

            do
            {
                food = new Pixel(new Random().Next(1, fieldWidth - 2), new Random().Next(1, fieldHeight - 2), FoodColor);
            }
            while (snake.Head.X == food.X && snake.Head.Y == food.Y || snake.Body.Any(i => i.X == snake.Head.X && i.Y == snake.Head.Y));

            return food;
        }


        static Direction Movement(Direction currrentDirection)
        {
            if (!Console.KeyAvailable) return currrentDirection;

            ConsoleKey key = Console.ReadKey(intercept: true).Key;

            currrentDirection = key switch
            {
                ConsoleKey.W when currrentDirection != Direction.Down => Direction.Up,
                ConsoleKey.S when currrentDirection != Direction.Up => Direction.Down,
                ConsoleKey.D when currrentDirection != Direction.Left => Direction.Right,
                ConsoleKey.A when currrentDirection != Direction.Right => Direction.Left,
                _ => currrentDirection
            };
            return currrentDirection;
        }

        static void DrawBorder()
        {
            for (int i = 0; i < fieldWidth; i++)
            {
                new Pixel(x: i, y: 0, BorderColor).Draw();
                new Pixel(x: i, y: fieldHeight -1, BorderColor).Draw();
            }
            for (int i = 0; i < fieldHeight; i++)
            {
                new Pixel(x: 0, y: i, BorderColor).Draw();
                new Pixel(x: fieldWidth - 1, y: i, BorderColor).Draw();
            }
        }
    }

}
