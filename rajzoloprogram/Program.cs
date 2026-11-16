using System;

namespace rajzoloprogram
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try { Console.SetWindowSize(80, 25); Console.SetBufferSize(80, 25); } catch { }

            bool fut = true;

            char[,] rajzlap = new char[56, 22];
            for (int y = 0; y < 22; y++)
            {
                for (int x = 0; x < 56; x++)
                {
                    rajzlap[x, y] = ' ';
                }
            }

            Console.Clear();

            Console.SetCursorPosition(0, 0);
            Console.WriteLine("RAJZOLO MENÜ ");
            Console.WriteLine("----------------");
            Console.WriteLine("X: Kilepes      ");
            Console.WriteLine("----------------");

            while (fut)
            {
                for (int y = 0; y < 22; y++)
                {
                    Console.SetCursorPosition(21, y);
                    Console.Write("|");
                    for (int x = 0; x < 56; x++)
                    {
                        Console.SetCursorPosition(22 + x, y);
                        Console.Write(rajzlap[x, y]);
                    }
                }

                var gomb = Console.ReadKey(true);
                if (gomb.Key == ConsoleKey.X)
                {
                    fut = false;
                }
            }
        }
    }
}