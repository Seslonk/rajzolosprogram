using System;

namespace rajzoloprogram
{
    struct Pixel
    {
        public char Karakter;
        public ConsoleColor Szin;
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            try { Console.SetWindowSize(80, 25); Console.SetBufferSize(80, 25); } catch { }

            bool fut = true;
            int kurzorX = 10;
            int kurzorY = 5;
            char aktivKarakter = '#';
            ConsoleColor aktivSzin = ConsoleColor.White;

            Pixel[,] rajzlap = new Pixel[56, 22];
            for (int y = 0; y < 22; y++)
            {
                for (int x = 0; x < 56; x++)
                {
                    rajzlap[x, y].Karakter = ' ';
                    rajzlap[x, y].Szin = ConsoleColor.White;
                }
            }

            Console.Clear();

            Console.SetCursorPosition(0, 0);
            Console.WriteLine("RAJZOLO MENÜ ");
            Console.WriteLine("----------------");
            Console.WriteLine("X: Kilepes      ");
            Console.WriteLine("Spc: Rajz/Radir ");
            Console.WriteLine("C: Karakter vált");
            Console.WriteLine("S: Szín váltás  ");
            Console.WriteLine("F1: Mind torol  ");
            Console.WriteLine("----------------");

            while (fut)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.SetCursorPosition(0, 8);
                Console.WriteLine($"X: {kurzorX,-2} Y: {kurzorY,-2}    ");

                Console.Write("Karakter: [");
                Console.ForegroundColor = aktivSzin;
                Console.Write(aktivKarakter);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("]   ");

                Console.Write("Szín: ");
                Console.ForegroundColor = aktivSzin;
                Console.Write($"{aktivSzin,-10}");
                Console.ForegroundColor = ConsoleColor.Gray;

                for (int y = 0; y < 22; y++)
                {
                    Console.SetCursorPosition(21, y);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write("|");
                    for (int x = 0; x < 56; x++)
                    {
                        Console.SetCursorPosition(22 + x, y);
                        Console.ForegroundColor = rajzlap[x, y].Szin;
                        Console.Write(rajzlap[x, y].Karakter);
                    }
                }

                int kepernyoX = 22 + kurzorX;
                int kepernyoY = kurzorY;
                if (kurzorX >= 0 && kurzorX < 56 && kurzorY >= 0 && kurzorY < 22)
                {
                    Console.SetCursorPosition(kepernyoX, kepernyoY);
                    Console.ForegroundColor = ConsoleColor.Yellow;
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
                        if (rajzlap[kurzorX, kurzorY].Karakter != ' ')
                        {
                            rajzlap[kurzorX, kurzorY].Karakter = ' ';
                            rajzlap[kurzorX, kurzorY].Szin = ConsoleColor.White;
                        }
                        else
                        {
                            rajzlap[kurzorX, kurzorY].Karakter = aktivKarakter;
                            rajzlap[kurzorX, kurzorY].Szin = aktivSzin;
                        }
                    }
                }
                else if (gomb.Key == ConsoleKey.C)
                {
                    Console.SetCursorPosition(0, 16);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("Új karakter: ");
                    var ujBetu = Console.ReadKey();
                    aktivKarakter = ujBetu.KeyChar;

                    Console.SetCursorPosition(0, 16);
                    Console.Write("                 ");
                }
                else if (gomb.Key == ConsoleKey.S)
                {
                    if (aktivSzin == ConsoleColor.White) aktivSzin = ConsoleColor.Red;
                    else if (aktivSzin == ConsoleColor.Red) aktivSzin = ConsoleColor.Green;
                    else if (aktivSzin == ConsoleColor.Green) aktivSzin = ConsoleColor.Blue;
                    else if (aktivSzin == ConsoleColor.Blue) aktivSzin = ConsoleColor.Yellow;
                    else if (aktivSzin == ConsoleColor.Yellow) aktivSzin = ConsoleColor.Cyan;
                    else if (aktivSzin == ConsoleColor.Cyan) aktivSzin = ConsoleColor.Magenta;
                    else aktivSzin = ConsoleColor.White;
                }
                else if (gomb.Key == ConsoleKey.F1)
                {
                    for (int y = 0; y < 22; y++)
                    {
                        for (int x = 0; x < 56; x++)
                        {
                            rajzlap[x, y].Karakter = ' ';
                            rajzlap[x, y].Szin = ConsoleColor.White;
                        }
                    }
                }
            }
        }
    }
}