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
            bool ecsetLenn = false;
            bool wasdMod = false;

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

            while (fut)
            {
                if (ecsetLenn && kurzorX >= 0 && kurzorX < 56 && kurzorY >= 0 && kurzorY < 22)
                {
                    rajzlap[kurzorX, kurzorY].Karakter = aktivKarakter;
                    rajzlap[kurzorX, kurzorY].Szin = aktivSzin;
                }

                Console.SetCursorPosition(0, 0);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("==RAJZOLO MENÜ==");
                Console.WriteLine("----------------");
                Console.WriteLine("X: Kilepes      ");
                Console.WriteLine("Spc: Rajz/Radir ");
                Console.WriteLine("E: Ecset Fel/Le ");
                Console.WriteLine("Q: Szin valtas  ");
                Console.WriteLine("C: Karakter valt");
                Console.WriteLine("F2: Iranyitas   ");
                Console.WriteLine("F1: Mind torol  ");
                Console.WriteLine("----------------");

                Console.SetCursorPosition(0, 10);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"X: {kurzorX,-2} Y: {kurzorY,-2}    ");

                Console.Write("Karakter: [");
                Console.ForegroundColor = aktivSzin;
                Console.Write(aktivKarakter);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("]   ");

                Console.Write("Szin: ");
                Console.ForegroundColor = aktivSzin;
                Console.Write($"{aktivSzin,-10}");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("          ");

                Console.Write("Ecset: ");
                if (ecsetLenn)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("LENN      ");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("FENN      ");
                }
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("          ");

                Console.Write("Mod:  ");
                if (wasdMod)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("WASD      ");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("NYILAK    ");
                }
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("          ");

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
                else if ((wasdMod && gomb.Key == ConsoleKey.W) || (!wasdMod && gomb.Key == ConsoleKey.UpArrow))
                {
                    if (kurzorY > 0) kurzorY--;
                }
                else if ((wasdMod && gomb.Key == ConsoleKey.S) || (!wasdMod && gomb.Key == ConsoleKey.DownArrow))
                {
                    if (kurzorY < 21) kurzorY++;
                }
                else if ((wasdMod && gomb.Key == ConsoleKey.A) || (!wasdMod && gomb.Key == ConsoleKey.LeftArrow))
                {
                    if (kurzorX > 0) kurzorX--;
                }
                else if ((wasdMod && gomb.Key == ConsoleKey.D) || (!wasdMod && gomb.Key == ConsoleKey.RightArrow))
                {
                    if (kurzorX < 55) kurzorX++;
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
                else if (gomb.Key == ConsoleKey.E)
                {
                    ecsetLenn = !ecsetLenn;
                }
                else if (gomb.Key == ConsoleKey.Q)
                {
                    if (aktivSzin == ConsoleColor.White) aktivSzin = ConsoleColor.Red;
                    else if (aktivSzin == ConsoleColor.Red) aktivSzin = ConsoleColor.Green;
                    else if (aktivSzin == ConsoleColor.Green) aktivSzin = ConsoleColor.Blue;
                    else if (aktivSzin == ConsoleColor.Blue) aktivSzin = ConsoleColor.Yellow;
                    else if (aktivSzin == ConsoleColor.Yellow) aktivSzin = ConsoleColor.Cyan;
                    else if (aktivSzin == ConsoleColor.Cyan) aktivSzin = ConsoleColor.Magenta;
                    else aktivSzin = ConsoleColor.White;
                }
                else if (gomb.Key == ConsoleKey.C)
                {
                    Console.SetCursorPosition(0, 20);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("Uj karakter: ");
                    var ujBetu = Console.ReadKey();
                    aktivKarakter = ujBetu.KeyChar;

                    Console.SetCursorPosition(0, 20);
                    Console.Write("                 ");
                }
                else if (gomb.Key == ConsoleKey.F2)
                {
                    wasdMod = !wasdMod;
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