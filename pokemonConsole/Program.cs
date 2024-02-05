
﻿using pokemonConsole;
using Usefull;

using System.Collections;
using System.Data;
﻿using System;
using System.Collections.Generic;
using inventory;
using System.Numerics;
using System.Text;

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


        public static string ClavierName(int limit)
        {
            string name = "";

            string nameEnter = "";
            for (int i = 0; i < limit; i++)
            {
                nameEnter += "_";
            }

            List<string> firstLine = new List<string>();
            List<string> secondline = new List<string>();
            List<string> thirdLine = new List<string>();
            List<string> fourthLine = new List<string>();
            List<List<string>> lines = new List<List<string>>();

            string A = "[A]";
            string B = " B ";
            string C = " C ";
            string D = " D ";
            string E = " E ";
            string F = " F ";
            string G = " G ";
            string Del = " DEL ";
            firstLine.Add(A);
            firstLine.Add(B);
            firstLine.Add(C);
            firstLine.Add(D);
            firstLine.Add(E);
            firstLine.Add(F);
            firstLine.Add(G);
            firstLine.Add(Del);

            string H = " H ";
            string I = " I ";
            string J = " J ";
            string K = " K ";
            string L = " L ";
            string M = " M ";
            string N = " N ";
            secondline.Add(H);
            secondline.Add(I);
            secondline.Add(J);
            secondline.Add(K);
            secondline.Add(L);
            secondline.Add(M);
            secondline.Add(N);

            string O = " O ";
            string P = " P ";
            string Q = " Q ";
            string R = " R ";
            string S = " S ";
            string T = " T ";
            string U = " U ";
            thirdLine.Add(O);
            thirdLine.Add(P);
            thirdLine.Add(Q);
            thirdLine.Add(R);
            thirdLine.Add(S);
            thirdLine.Add(T);
            thirdLine.Add(U);

            string V = " V ";
            string W = " W ";
            string X = " X ";
            string Y = " Y ";
            string Z = " Z ";
            string Enter = " END ";
            fourthLine.Add(V);
            fourthLine.Add(W);
            fourthLine.Add(X);
            fourthLine.Add(Y);
            fourthLine.Add(Z);
            fourthLine.Add("   ");
            fourthLine.Add("   ");
            fourthLine.Add(Enter);



            lines.Add(firstLine);
            lines.Add(secondline);
            lines.Add(thirdLine);
            lines.Add(fourthLine);

            int x = 0;
            int y = 0;

            int charNumber = 0;

            ConsoleKeyInfo keyInfo;

            Console.Clear();
            Console.WriteLine(nameEnter);
            foreach (List<string> line in lines)
            {
                Console.WriteLine();
                foreach(string str in line)
                {
                    Console.Write(str);
                }
            }


            bool EndName = false;
            while (!EndName)
            {
                keyInfo = Console.ReadKey();

                switch (keyInfo.Key)
                {
                    case ConsoleKey.DownArrow:
                        if (y != lines.Count - 1)
                        {
                            UpdateCell(lines, x, y, " ", " ");
                            y += 1;
                            UpdateCell(lines, x, y, "[", "]");
                        }
                        break;

                    case ConsoleKey.UpArrow:
                        if (y != 0)
                        {
                            UpdateCell(lines, x, y, " ", " ");
                            y -= 1;
                            UpdateCell(lines, x, y, "[", "]");
                        }
                        break;

                    case ConsoleKey.RightArrow:
                        if (x != lines[y].Count - 1)
                        {
                            UpdateCell(lines, x, y, " ", " ");
                            x += 1;
                            UpdateCell(lines, x, y, "[", "]");
                        }
                        break;

                    case ConsoleKey.LeftArrow:
                        if (x != 0)
                        {
                            UpdateCell(lines, x, y, " ", " ");
                            x -= 1;
                            UpdateCell(lines, x, y, "[", "]");
                        }
                        break;

                    case ConsoleKey.Enter:
                        if (charNumber < limit && (lines[y][x] != "[DEL]" && lines[y][x] != "[END]"))
                        {
                            
                            char newChar = lines[y][x][1];

                            nameEnter = nameEnter.Remove(charNumber, 1);

                            StringBuilder stringBuilder = new StringBuilder(nameEnter);
                            stringBuilder.Insert(charNumber, newChar);

                            nameEnter = stringBuilder.ToString();
                            name += newChar;

                            charNumber++;


                            if (charNumber == limit)
                            {
                                UpdateCell(lines, x, y, " ", " ");
                                x = 7;
                                y = 3;
                                UpdateCell(lines, x, y, "[", "]");
                            }
                        }
                        else if (lines[y][x] == "[DEL]")
                        {
                            if (name.Length > 0)
                            {
                                nameEnter = nameEnter.Remove(charNumber - 1, 1);
                                nameEnter = nameEnter.Insert(charNumber - 1, "_");

                                name = name.Substring(0, name.Length - 1);
                                charNumber--;
                            }
                            
                        }
                        else if (lines[y][x] == "[END]" && !string.IsNullOrWhiteSpace(name))
                        {
                            EndName = true;
                        }

                        break;
                }

                Console.Clear();
                Console.WriteLine(nameEnter);
                foreach (List<string> line in lines)
                {
                    Console.WriteLine();
                    foreach (string str in line)
                    {
                        Console.Write(str);
                    }
                }

            }
            return name;
        }

        private static void UpdateCell(List<List<string>> lines, int x, int y, string prefix, string suffix)
        {
            lines[y][x] = lines[y][x].Remove(0, 1);
            lines[y][x] = lines[y][x].Insert(0, prefix);

            lines[y][x] = lines[y][x].Substring(0, lines[y][x].Length - 1);
            lines[y][x] = lines[y][x] + suffix;
        }
    }

    class AdresseFile
    {
        public static string FileDirection = "C:\\Users\\ycaillot\\Desktop\\C-Pokemon\\pokemonConsole\\GameFiles\\";
    }
}