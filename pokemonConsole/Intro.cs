using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pokemonConsole
{
    internal class Intro
    {
        public string asciiFileOak { get; private set; } = "C:\\Users\\mguellaff\\Desktop\\C-Pokemon\\pokemonConsole\\Assets\\Sprites\\ascii-art_chen.txt";
        public string scriptOak { get; private set; } = "C:\\Users\\mguellaff\\Desktop\\C-Pokemon\\pokemonConsole\\ScriptIntroChen.txt";
        public string asciiOak { get; private set; } = "";



        static public void LaunchIntro()
        {
            Intro intro = new Intro();

            intro.asciiOak = File.ReadAllText(intro.asciiFileOak);
            string line = "0";

            using (StreamReader sr = new StreamReader(intro.scriptOak))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    if (line != "")
                    {
                        intro.Print(line);
                        Console.Read();
                    }
                }
            }
        }

        public void Print(string firstLine)
        {
            Console.Clear();
            Console.WriteLine(asciiOak);
            if (!string.IsNullOrEmpty(firstLine))
            {
                Console.Write(firstLine[0]);
                foreach (char c in firstLine.Skip(1))
                {
                    Console.Write(c);
                    Task.Delay(50).Wait();
                }
            }

            Console.WriteLine();
        }
    }
}