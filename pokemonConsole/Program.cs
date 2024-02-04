
﻿using pokemonConsole;

using System.Collections;
using System.Data;
﻿using System;
using System.Collections.Generic;
using inventory;

class Program
{

    static void Main()
    {
        MainMenu.Start();

    }
}







namespace Usefull
{
    class Functions
    {
        public static void ClearInputBuffer()
        {
            while (Console.KeyAvailable)
            {
                Console.ReadKey(intercept: true);
            }
        }
    }

    class AdresseFile
    {
        public static string FileDirection = "C:\\Users\\yanae\\Desktop\\C-Pokemon\\pokemonConsole\\GameFiles\\";
    }
}