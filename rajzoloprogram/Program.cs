using System;

namespace rajzoloprogram
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try { Console.SetWindowSize(80, 25); Console.SetBufferSize(80, 25); } catch { }

            bool fut = true;
            int kurzorX = 10;
            int kurzorY = 5;
            char aktivKarakter = '#';

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
            Console.WriteLine("Spc: Rajz/Radir ");
            Console.WriteLine("F1: Mind torol  ");
            Console.WriteLine("----------------");

            while (fut)
            {
                Console.SetCursorPosition(0, 7);
                Console.WriteLine($"X: {kurzorX,-2} Y: {kurzorY,-2}    ");

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

                int kepernyoX = 22 + kurzorX;
                int kepernyoY = kurzorY;
                if (kurzorX >= 0 && kurzorX < 56 && kurzorY >= 0 && kurzorY < 22)
                {
                    Console.SetCursorPosition(kepernyoX, kepernyoY);
                    Console.Write("O");
                }

                var gomb = Console.ReadKey(true);

                if (gomb.Key == ConsoleKey.X)
                {
                    fut = false;
                }
                else if (gomb.Key == ConsoleKey.UpArrow && kurzorY > 0)
                {
                    kurzorY--;
                }
                else if (gomb.Key == ConsoleKey.DownArrow && kurzorY < 21)
                {
                    kurzorY++;
                }
                else if (gomb.Key == ConsoleKey.LeftArrow && kurzorX > 0)
                {
                    kurzorX--;
                }
                else if (gomb.Key == ConsoleKey.RightArrow && kurzorX < 55)
                {
                    kurzorX++;
                }
                else if (gomb.Key == ConsoleKey.Spacebar)
                {
                    if (kurzorX >= 0 && kurzorX < 56 && kurzorY >= 0 && kurzorY < 22)
                    {
                        if (rajzlap[kurzorX, kurzorY] != ' ')
                            rajzlap[kurzorX, kurzorY] = ' ';
                        else
                            rajzlap[kurzorX, kurzorY] = aktivKarakter;
                    }
                }
                else if (gomb.Key == ConsoleKey.F1)
                {
                    for (int y = 0; y < 22; y++)
                    {
                        for (int x = 0; x < 56; x++)
                        {
                            rajzlap[x, y] = ' ';
                        }
                    }
                }
            }
        }
    }
}