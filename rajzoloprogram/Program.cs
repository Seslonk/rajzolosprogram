using System;

namespace rajzoloprogram
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try { Console.SetWindowSize(80, 25); Console.SetBufferSize(80, 25); } catch { }

            bool fut = true;
            Console.Clear();

            // Alap menü kirajzolása
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("RAJZOLO MENÜ ");
            Console.WriteLine("----------------");
            Console.WriteLine("X: Kilepes      ");
            Console.WriteLine("----------------");

            while (fut)
            {
                var gomb = Console.ReadKey(true);
                if (gomb.Key == ConsoleKey.X)
                {
                    fut = false;
                }
            }
        }
    }
}