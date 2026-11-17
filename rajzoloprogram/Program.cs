using System;

namespace rajzoloprogram
{
    // (Itt az AI segítségét használtam: csináltam egy Pixel nevű struktúrát, hogy egy helyen tudjam tárolni a karaktert meg a színét is.)
        struct Pixel
    {
        public char Karakter;
        public ConsoleColor Szin;
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            // (Itt az AI segítségét használtam: ez a try-catch arra jó, hogy ne fagyjon ki a program, ha más méretű ablakban fut.)
            try { Console.SetWindowSize(80, 25); Console.SetBufferSize(80, 25); } catch { }
            
            // Változók deklarálása és inicializálása
            bool fut = true;
            int kurzorX = 10;
            int kurzorY = 5;
            char aktivKarakter = '#';
            ConsoleColor aktivSzin = ConsoleColor.White;
            bool ecsetLenn = false;
            bool wasdMod = false;

            // Itt hozom létre a rajzlapot, és feltöltöm üres helyekkel.
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

            // Ez a fő rajzoló ciklus, ez fut addig, amíg ki nem lépek.
            while (fut)
            {
                // Ha az ecset lent van, akkor a kurzor helyére beírja az aktuális karaktert és színt
                if (ecsetLenn && kurzorX >= 0 && kurzorX < 56 && kurzorY >= 0 && kurzorY < 22)
                {
                    rajzlap[kurzorX, kurzorY].Karakter = aktivKarakter;
                    rajzlap[kurzorX, kurzorY].Szin = aktivSzin;
                }

                // Menü kirajzolása a képernyő bal oldalára
                Console.SetCursorPosition(0, 0);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("==RAJZOLO MENÜ==");
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("----------------");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("X: Kilepes      ");
                Console.WriteLine("Spc: Rajz/Radir ");
                Console.WriteLine("E: Ecset Fel/Le ");
                Console.WriteLine("Q: Szin valtas  ");
                Console.WriteLine("C: Karakter valt");
                Console.WriteLine("F2: Iranyitas   ");
                Console.WriteLine("F1: Mind torol  ");
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("----------------");
                Console.ForegroundColor = ConsoleColor.Gray;

                // Itt írom ki a bal oldalra az aktuális X és Y koordinátákat.
                Console.SetCursorPosition(0, 10);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"X: {kurzorX,-2} Y: {kurzorY,-2}    ");

                Console.Write("Karakter: [");
                Console.ForegroundColor = aktivSzin;
                Console.Write(aktivKarakter);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("]");

                Console.Write("Szin: ");
                Console.ForegroundColor = aktivSzin;
                Console.Write($"{aktivSzin,-10}");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("");

                Console.Write("Ecset: ");
                if (ecsetLenn)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("LENN");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("FENN");
                }
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("");

                Console.Write("Mod: ");
                if (wasdMod)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("WASD");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("NYILAK");
                }
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("");

                // Rajzlap keretének és tartalmának kirajzolása for ciklussal
                for (int y = 0; y < 22; y++)
                {
                    Console.SetCursorPosition(21, y);
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.Write("|");
                    for (int x = 0; x < 56; x++)
                    {
                        Console.SetCursorPosition(22 + x, y);
                        Console.ForegroundColor = rajzlap[x, y].Szin;
                        Console.Write(rajzlap[x, y].Karakter);
                    }
                }

                // Itt jelenítem meg magát a sárga kurzort ('O') a rajzlapon.
                int kepernyoX = 22 + kurzorX;
                int kepernyoY = kurzorY;
                if (kurzorX >= 0 && kurzorX < 56 && kurzorY >= 0 && kurzorY < 22)
                {
                    Console.SetCursorPosition(kepernyoX, kepernyoY);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("O");
                }

                // Beolvasom, hogy milyen gombot nyomott le a felhasználó.
                var gomb = Console.ReadKey(true);

                // Ha X-et nyom, akkor kilépünk a ciklusból.
                if (gomb.Key == ConsoleKey.X)
                {
                    fut = false;
                }
                // Itt figyelem a mozgást: ha F2-vel átváltotta WASD-re, akkor azzal mozog, különben a nyilakkal.
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
                // Space: rajzolás vagy radírozás
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
                // Az E betűvel kapcsolom be vagy ki az ecsetet (ha lenn van, folyamatosan rajzol lépés közben).
                else if (gomb.Key == ConsoleKey.E)
                {
                    ecsetLenn = !ecsetLenn;
                }
                // Q-val váltogatom a színeket sorban egymás után 
                else if (gomb.Key == ConsoleKey.Q)
                {
                    if (aktivSzin == ConsoleColor.White) aktivSzin = ConsoleColor.Red;
                    else if (aktivSzin == ConsoleColor.Red) aktivSzin = ConsoleColor.Green;
                    else if (aktivSzin == ConsoleColor.Green) aktivSzin = ConsoleColor.Blue;
                    else if (aktivSzin == ConsoleColor.Blue) aktivSzin = ConsoleColor.DarkYellow;
                    else if (aktivSzin == ConsoleColor.DarkYellow) aktivSzin = ConsoleColor.Cyan;
                    else if (aktivSzin == ConsoleColor.Cyan) aktivSzin = ConsoleColor.Magenta;
                    else aktivSzin = ConsoleColor.White;
                }
                // C betűvel lekérem a konzolról, hogy milyen új karaktert szeretne használni a felhasználó.
                else if (gomb.Key == ConsoleKey.C)
                {
                    Console.SetCursorPosition(0, 20);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("Uj karakter: ");
                    var ujBetu = Console.ReadKey();
                    aktivKarakter = ujBetu.KeyChar;

                    Console.SetCursorPosition(0, 20);
                    Console.Write("");
                }
                // F2-vel váltom át az irányítást a nyilak és a WASD között.
                else if (gomb.Key == ConsoleKey.F2)
                {
                    wasdMod = !wasdMod;
                }
                // F1 gomb: a teljes rajzlap letörlése (üresre állítása
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