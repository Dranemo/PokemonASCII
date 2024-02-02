using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

using FunctionUsefull;

namespace pokemonConsole
{
    internal class Intro
    {
        public string asciiFileOak { get; private set; } = "C:\\Users\\ycaillot\\Desktop\\C-Pokemon\\pokemonConsole\\Assets\\Sprites\\ascii-art_chen.txt";
        public string asciiFileBlue { get; private set; } = "C:\\Users\\ycaillot\\Desktop\\C-Pokemon\\pokemonConsole\\Assets\\Sprites\\ascii-art_blue.txt";
        public string asciiFileRed { get; private set; } = "C:\\Users\\ycaillot\\Desktop\\C-Pokemon\\pokemonConsole\\Assets\\Sprites\\ascii-art_red.txt";
        public string scriptOak { get; private set; } = "C:\\Users\\ycaillot\\Desktop\\C-Pokemon\\pokemonConsole\\ScriptIntroChen.txt";
        public string asciiOak { get; private set; } = "";
        public string asciiBlue { get; private set; } = "";
        public string asciiRed { get; private set; } = "";


        private int widthOak = 45;

        private string hautTextZone = "_____________________________________________";
        private string middleTextZone = "|                                           |";



        static public void LaunchIntro(Player player, Rival rival)
        {
            Intro intro = new Intro();

            intro.asciiOak = File.ReadAllText(intro.asciiFileOak);
            intro.asciiBlue = File.ReadAllText(intro.asciiFileBlue);
            using (StreamReader sr = new StreamReader(intro.asciiFileRed))
            {
                for (int i = 0; i < 31; i++)
                {
                    intro.asciiRed += sr.ReadLine() + Environment.NewLine;
                }
            }
            string line = "0";

            using (StreamReader sr = new StreamReader(intro.scriptOak))
            {
                int lineVide = 0;
                bool firstLine = true;
                int lineReading = 1;

                while ((line = sr.ReadLine()) != null)
                {
                    if (line != "")
                    {
                        if (!firstLine)
                        {
                            Console.ReadKey();
                        }
                        else
                        {
                            firstLine = false;
                        }

                        for (int i = 0; i < line.Length; i++)
                        {
                            if (line[i] == '*')
                            {
                                intro.ReplaceCharacterWithPlayerName(ref line, '*', player.name);
                            }
                            else if (line[i] == '@')
                            {
                                intro.ReplaceCharacterWithPlayerName(ref line, '@', rival.name);
                            }
                        }

                        intro.Print(line, lineReading);
                        Functions.ClearInputBuffer();
                        lineReading++;
                        
                    }
                    else
                    {
                        lineVide++;
                        firstLine = true;

                        if (lineVide == 1)
                        {
                            do
                            {
                                player.name = Console.ReadLine();
                            } while (string.IsNullOrEmpty(player.name));
                        }
                        else if (lineVide == 2)
                        {
                            do
                            {
                                rival.name = Console.ReadLine();
                            } while (string.IsNullOrEmpty(rival.name));
                        }
                    }
                }
            }
        }

        public void Print(string line, int lineReading)
        {
            Console.Clear();
            if (lineReading <= 9)
            {
                Console.WriteLine(asciiOak);
            }
            else if ( lineReading <= 14) 
            {
                Console.WriteLine(asciiBlue);
            }
            else 
            { 
                Console.WriteLine(asciiRed); 
            }
            

            Console.WriteLine(hautTextZone);
            int lineWriting = 1;
            int charRestant = widthOak - 4;


            if (!string.IsNullOrEmpty(line))
            {
                Console.Write("| ");

                for (int i = 0; i < line.Length; i++)
                {
                    if (i < widthOak - 4)
                    {
                        Console.Write(line[i]);
                        charRestant--;
                    }
                    else if (i >= widthOak - 4 && i < (widthOak - 4) * 2)
                    {
                        if (i == (widthOak - 4))
                        {
                            lineWriting++;
                            Console.WriteLine(" |");
                            Console.Write("| ");
                            charRestant = widthOak - 4;
                        }
                        Console.Write(line[i]);
                        charRestant--;
                    }
                    else if (i >= (widthOak - 4) * 2)
                    {
                        if (i == (widthOak - 4) * 2)
                        {
                            lineWriting++;
                            Console.WriteLine(" |");
                            Console.Write("| ");
                            charRestant = widthOak - 4;
                        }
                        Console.Write(line[i]);
                        charRestant--;
                    }

                    Task.Delay(50).Wait();
                }

            }

            for (int i = charRestant; i > 0; i--)
            {
                Console.Write(" ");
            }

            Console.WriteLine(" |");

            while (lineWriting < 3)
            {
                Console.WriteLine(middleTextZone);
                lineWriting++;
            }

            Console.WriteLine(hautTextZone);
            

            Console.WriteLine();
        }

        public void ReplaceCharacterWithPlayerName(ref string line, char characterToReplace, string playerName)
        {
            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == characterToReplace)
                {
                    line = line.Remove(i, 1);

                    StringBuilder stringBuilder = new StringBuilder(line);
                    stringBuilder.Insert(i, playerName);

                    line = stringBuilder.ToString();
                }
            }
        }




    }
}