using System;

namespace snake
{
    public readonly struct Pixel
    {

        private const char PixelChar = '█'; 
        public Pixel(int x, int y, ConsoleColor color)
        {
            X = x;
            Y = y;
            Color = color;
        }

        public int X { get; }
        public int Y { get; }
        public ConsoleColor Color { get; }
        public void Draw()
        {
            Console.ForegroundColor = Color;
            Console.SetCursorPosition(left: X, top: Y);
            Console.Write(PixelChar);
            
        }

        public void Clear()
        {
            Console.SetCursorPosition(left: X, top: Y);
            Console.Write(' ');
        }
    }
   
}
    

