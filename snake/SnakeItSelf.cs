using System;
using System.Collections.Generic;

namespace snake
{
    
    class SnakeItSelf
    {
        private readonly ConsoleColor _headColor;
        private readonly ConsoleColor _bodyColor;
        public SnakeItSelf(int initialX, int initialY, ConsoleColor headColor, ConsoleColor bodyColor, int bodyLenght = 0)
        {
            _headColor = headColor;
            _bodyColor = bodyColor;
            Head = new Pixel(initialX, initialY, _headColor);

            for(int i = bodyLenght; i >= 0; i--)
            {
                Body.Enqueue(item: new Pixel(x: Head.X - i - 1, initialY, _bodyColor));

            }

            Draw();
        }

        public Pixel Head { get; private set; }

        public Queue<Pixel> Body { get; } = new Queue<Pixel>();

        public void Move(Direction direction, bool eat = false)
        {
            Clear();
            Body.Enqueue(item: new Pixel(Head.X, Head.Y, _bodyColor));
            if (!eat)
               Body.Dequeue();

            Head = direction switch
            {
                Direction.Up => new Pixel(Head.X, y: Head.Y - 1, _headColor),
                Direction.Down => new Pixel(Head.X, y: Head.Y + 1, _headColor),
                Direction.Right => new Pixel(x: Head.X + 1, Head.Y, _headColor),
                Direction.Left => new Pixel(x: Head.X - 1, Head.Y, _headColor),
                _=> Head
            };

            Draw();
        }
        public void Draw()
        {
            Head.Draw();

            foreach(Pixel pixel in Body)
            {
                pixel.Draw();
            }
        }

        public void Clear()
        {
            Head.Clear();
            foreach (Pixel pixel in Body)
            {
                pixel.Clear();
            }
        }
    }
}
