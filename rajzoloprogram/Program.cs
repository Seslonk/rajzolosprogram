using System;
using System.IO;

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
            bool radirMod = false;
            string hibaUzenet = "";
            string[] menupontok = { "Kilépés", "Rajz/Radír", "Ecset Fel/Le", "Szin váltás", "Karakter váltás", "Mind töröl", "Fájl betöltése", "Kozepre ugrás", "Nyomda Menü" };
            ConsoleKey[] menugombok = { ConsoleKey.Escape, ConsoleKey.Spacebar, ConsoleKey.E, ConsoleKey.Q, ConsoleKey.C, ConsoleKey.Backspace, ConsoleKey.R, ConsoleKey.F };
            int kijeloltMenupont = 0;
            bool nyomdaMenu = false;
            string[] nyomdaPontok = { "F1 - 3x3 X", "F2 - 5x5 X", "F3 - 7x7 X", "F4 - 3x3 Kereszt", "F5 - 5x5 Kereszt", "F6 - 7x7 Kereszt", "F7 - 3x3 Negyzet", "F8 - 5x5 Negyzet", "F9 - 7x7 Negyzet", "F10 - 3x3 Rombusz", "F11 - 5x5 Rombusz", "F12 - 7x7 Rombusz", "Vissza [N]" };

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
                if (ecsetLenn && !radirMod && kurzorX >= 0 && kurzorX < 56 && kurzorY >= 0 && kurzorY < 22)
                {
                    rajzlap[kurzorX, kurzorY].Karakter = aktivKarakter;
                    rajzlap[kurzorX, kurzorY].Szin = aktivSzin;
                }

                // Menü kirajzolása a képernyő bal oldalára
                Console.SetCursorPosition(0, 0);
                for (int i = 0; i < 16; i++)
                {
                    Console.WriteLine(new string(' ', 20));
                }
                Console.SetCursorPosition(0, 0);
                Console.ForegroundColor = ConsoleColor.Cyan;
                if (nyomdaMenu)
                {
                    Console.WriteLine("== NYOMDA MENU ==");
                }
                else
                {
                    Console.WriteLine("== RAJZOLO MENU ==");
                }
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("----------------");
                Console.ForegroundColor = ConsoleColor.Gray;

                if (nyomdaMenu)
                {
                    for (int i = 0; i < nyomdaPontok.Length; i++)
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.WriteLine(nyomdaPontok[i]);
                    }
                }
                else
                {
                    for (int i = 0; i < menupontok.Length; i++)
                    {
                        string menupont = menupontok[i];
                        if (i == kijeloltMenupont)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("> " + menupont);
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.WriteLine("  " + menupont);
                        }
                    }
                }

                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("----------------");
                Console.ForegroundColor = ConsoleColor.Gray;

                // Hibajelzés / üzenet sora 
                Console.SetCursorPosition(0, 15);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("                    ");
                Console.SetCursorPosition(0, 15);
                if (!string.IsNullOrEmpty(hibaUzenet))
                {
                    Console.WriteLine(hibaUzenet);
                    hibaUzenet = "";
                }

                // Itt írom ki a koordinátákat és az állapotokat
                Console.SetCursorPosition(0, 17);
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
                Console.WriteLine("   ");

                // Ecset sor
                Console.Write("Ecset: ");
                if (ecsetLenn)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("LENN  ");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("FENN  ");
                }
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("");

                Console.Write("Rajz mód: ");
                if (radirMod)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("RADÍR  ");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("RAJZ  ");
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
                bool menuParancs = false;

                if (gomb.Key == ConsoleKey.N)
                {
                    nyomdaMenu = !nyomdaMenu;
                    continue;
                }
                else if (nyomdaMenu && (int)gomb.Key >= (int)ConsoleKey.F1 && (int)gomb.Key <= (int)ConsoleKey.F12)
                {
                    int nyomda = (int)gomb.Key - (int)ConsoleKey.F1;
                    Nyomtat(rajzlap, kurzorX, kurzorY, nyomda, aktivKarakter, aktivSzin, ref hibaUzenet);
                    continue;
                }
                else if (!nyomdaMenu && gomb.Key == ConsoleKey.UpArrow)
                {
                    kijeloltMenupont--;
                    if (kijeloltMenupont < 0) kijeloltMenupont = menupontok.Length - 1;
                    continue;
                }
                else if (!nyomdaMenu && gomb.Key == ConsoleKey.DownArrow)
                {
                    kijeloltMenupont++;
                    if (kijeloltMenupont >= menupontok.Length) kijeloltMenupont = 0;
                    continue;
                }
                else if (!nyomdaMenu && gomb.Key == ConsoleKey.Enter)
                {
                    if (kijeloltMenupont == 8)
                    {
                        nyomdaMenu = true;
                        continue;
                    }
                    if (kijeloltMenupont == 1)
                    {
                        radirMod = !radirMod;
                        continue;
                    }
                    menuParancs = true;
                    gomb = new ConsoleKeyInfo('\0', menugombok[kijeloltMenupont], false, false, false);
                }

                // Ha Escape-t nyom, akkor kilépünk a ciklusból.
                if (menuParancs && gomb.Key == ConsoleKey.Escape)
                {
                    Console.Clear();
                    fut = false;
                }
                // Kurzor mozgás határellenőrzéssel és Beep-pel
                else if (gomb.Key == ConsoleKey.W)
                {
                    if (kurzorY > 0) kurzorY--; else { Console.Beep(); hibaUzenet = "Határ!"; }
                    if (radirMod && ecsetLenn) { rajzlap[kurzorX, kurzorY].Karakter = ' '; rajzlap[kurzorX, kurzorY].Szin = ConsoleColor.White; }
                }
                else if (gomb.Key == ConsoleKey.S)
                {
                    if (kurzorY < 21) kurzorY++; else { Console.Beep(); hibaUzenet = "Határ!"; }
                    if (radirMod && ecsetLenn) { rajzlap[kurzorX, kurzorY].Karakter = ' '; rajzlap[kurzorX, kurzorY].Szin = ConsoleColor.White; }
                }
                else if (gomb.Key == ConsoleKey.A)
                {
                    if (kurzorX > 0) kurzorX--; else { Console.Beep(); hibaUzenet = "Határ!"; }
                    if (radirMod && ecsetLenn) { rajzlap[kurzorX, kurzorY].Karakter = ' '; rajzlap[kurzorX, kurzorY].Szin = ConsoleColor.White; }
                }
                else if (gomb.Key == ConsoleKey.D)
                {
                    if (kurzorX < 55) kurzorX++; else { Console.Beep(); hibaUzenet = "Határ!"; }
                    if (radirMod && ecsetLenn) { rajzlap[kurzorX, kurzorY].Karakter = ' '; rajzlap[kurzorX, kurzorY].Szin = ConsoleColor.White; }
                }
                // Space: rajzolás vagy radírozás
                else if (menuParancs && gomb.Key == ConsoleKey.Spacebar)
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
                // Az E betűvel kapcsolom be vagy ki az ecsetet
                else if (menuParancs && gomb.Key == ConsoleKey.E)
                {
                    ecsetLenn = !ecsetLenn;
                }
                // Q-val váltogatom a színeket
                else if (menuParancs && gomb.Key == ConsoleKey.Q)
                {
                    if (aktivSzin == ConsoleColor.White) aktivSzin = ConsoleColor.Red;
                    else if (aktivSzin == ConsoleColor.Red) aktivSzin = ConsoleColor.Green;
                    else if (aktivSzin == ConsoleColor.Green) aktivSzin = ConsoleColor.Blue;
                    else if (aktivSzin == ConsoleColor.Blue) aktivSzin = ConsoleColor.DarkYellow;
                    else if (aktivSzin == ConsoleColor.DarkYellow) aktivSzin = ConsoleColor.Cyan;
                    else if (aktivSzin == ConsoleColor.Cyan) aktivSzin = ConsoleColor.Magenta;
                    else aktivSzin = ConsoleColor.White;
                }
                // C betűvel új karakter beolvasása
                else if (menuParancs && gomb.Key == ConsoleKey.C)
                {
                    Console.SetCursorPosition(0, 24);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("Uj karakter: ");
                    var ujBetu = Console.ReadKey();
                    aktivKarakter = ujBetu.KeyChar;
                    Console.SetCursorPosition(0, 24);
                    Console.Write("               ");
                }
                // Backspace gomb: a teljes rajzlap letörlése
                else if (menuParancs && gomb.Key == ConsoleKey.Backspace)
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
                // 'f' gomb: Ugrás a rajzlap közepére
                else if (menuParancs && gomb.Key == ConsoleKey.F)
                {
                    kurzorX = 28;
                    kurzorY = 11;
                }


                // 'r' gomb: Szöveges minta betöltése fájlból
                else if (menuParancs && gomb.Key == ConsoleKey.R)
                {
                    Console.SetCursorPosition(0, 24);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("Fajlnev: ");
                    string fajlNev = Console.ReadLine();
                    Console.SetCursorPosition(0, 24);
                    Console.Write("");

                    try
                    {
                        if (File.Exists(fajlNev))
                        {
                            string[] sorok = File.ReadAllLines(fajlNev);
                            if (sorok.Length <= 22)
                            {
                                bool joMeret = true;
                                foreach (var s in sorok) { if (s.Length > 56) joMeret = false; }

                                if (joMeret)
                                {
                                    for (int y = 0; y < sorok.Length; y++)
                                    {
                                        for (int x = 0; x < sorok[y].Length; x++)
                                        {
                                            rajzlap[x, y].Karakter = sorok[y][x];
                                            rajzlap[x, y].Szin = aktivSzin;
                                        }
                                    }
                                }
                                else { Console.Beep(); hibaUzenet = "Túl széles fájl!"; }
                            }
                            else { Console.Beep(); hibaUzenet = "Túl magas fájl!"; }
                        }
                        else { Console.Beep(); hibaUzenet = "Nem létezik!"; }
                    }
                    catch
                    {
                        Console.Beep();
                        hibaUzenet = "Hiba!";
                    }

                }




                

             }
        }

        static void Nyomtat(Pixel[,] rajzlap, int kurzorX, int kurzorY, int nyomda, char karakter, ConsoleColor szin, ref string hibaUzenet)
        {
            int meret = nyomda % 3 + 1;
            int sugar = meret;

            if (kurzorX - sugar < 0 || kurzorX + sugar >= 56 || kurzorY - sugar < 0 || kurzorY + sugar >= 22)
            {
                Console.Beep();
                hibaUzenet = "Kilóg!";
                return;
            }

            void Rajzol(int x, int y)
            {
                rajzlap[x, y].Karakter = karakter;
                rajzlap[x, y].Szin = szin;
            }

            for (int dx = -sugar; dx <= sugar; dx++)
            {
                for (int dy = -sugar; dy <= sugar; dy++)
                {
                    bool nyomtat = nyomda < 3 && (dx == dy || dx == -dy)
                        || nyomda >= 3 && nyomda < 6 && (dx == 0 || dy == 0)
                        || nyomda >= 6 && nyomda < 9 && (Math.Abs(dx) == sugar || Math.Abs(dy) == sugar)
                        || nyomda >= 9 && Math.Abs(dx) + Math.Abs(dy) == sugar;

                    if (nyomtat)
                    {
                        Rajzol(kurzorX + dx, kurzorY + dy);
                    }
                }
            }
        }
    }
}
