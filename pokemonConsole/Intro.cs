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
        private string asciiFileOak = "C:\\Users\\ycaillot\\Desktop\\C-Pokemon\\pokemonConsole\\Assets\\Sprites\\ascii-art_chen.txt";
        private string asciiFileBlue = "C:\\Users\\ycaillot\\Desktop\\C-Pokemon\\pokemonConsole\\Assets\\Sprites\\ascii-art_blue.txt";
        private string asciiFileRed = "C:\\Users\\ycaillot\\Desktop\\C-Pokemon\\pokemonConsole\\Assets\\Sprites\\ascii-art_red.txt";
        private string scriptOak = "C:\\Users\\ycaillot\\Desktop\\C-Pokemon\\pokemonConsole\\ScriptIntroChen.txt";

        private string asciiOak = "";
        private string asciiBlue = "";
        private string asciiRedHalf = "";
        private string asciiRed = "";


        private int widthOak = 45;

        private string hautTextZone = "_____________________________________________";
        private string middleTextZone = "|                                           |";



        static public void LaunchIntro(Player player, Rival rival)
        {
            Intro intro = new Intro();
            intro.LoadAllASCII();


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
                Console.WriteLine(asciiRedHalf); 
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
        public void LoadAllASCII()
        {
            asciiOak = File.ReadAllText(asciiFileOak);
            asciiBlue = File.ReadAllText(asciiFileBlue);
            asciiRed = File.ReadAllText(asciiFileRed);
            using (StreamReader sr = new StreamReader(asciiFileRed))
            {
                for (int i = 0; i < 31; i++)
                {
                    asciiRedHalf += sr.ReadLine() + Environment.NewLine;
                }
            }
        }


        private void AnimationRedShrink()
        {

        }

    }
}