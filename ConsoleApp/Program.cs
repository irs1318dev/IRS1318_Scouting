using System;
using Scouter.Data;
using Scouter.Models;
using System.Data.Entity;
using System.Collections.Generic;

namespace Scouter.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
		{
			Console.CursorVisible = false;
            Console.WriteLine("Initializing Database...");
            DataContext context = new DataContext();
            context.Database.Initialize(true);

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Done...");
			Console.CursorVisible = true;
            Console.ForegroundColor = ConsoleColor.Gray;
#if DEBUG
            Console.ReadKey();
#endif
        }
    }
}