using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace pokemonConsole
{
    internal class Menu_principal
    {
        private static int size;

        private static string pokemonMenuIcon = "  POKEMON";
        private static string itemsMenuIcon = "  OBJETS";
        private static string playerMenuIcon = "  ";
        private static string saveMenuIcon = "  SAUVER";
        private static string optionMenuIcon = "  OPTIONS";
        private static string retourMenuIcon = "  RETOUR";

        private static List<string> icons = new List<string>();

        private static string hautMenu = "0-----------0";
        private static string middleMenu = "|           |";

        private static bool alreadySelected;

        private static int position;


        private static int endPositionXText;
        private static int endPositionYText;

        public static void Open(Player player, int mapWidth, Rival rival)
        {
            if (player.pokemonParty.Count != 0)
            {
                icons.Add(pokemonMenuIcon);
            }
            if (playerMenuIcon == "  ") playerMenuIcon += player.name;


            icons.Add(itemsMenuIcon);
            icons.Add(playerMenuIcon);
            icons.Add(saveMenuIcon);
            icons.Add(optionMenuIcon);
            icons.Add(retourMenuIcon);

            size = icons.Count;

            alreadySelected = false;
            foreach(string icon in icons)
            {
                if (icon[0] == '>')
                {
                    alreadySelected = true;
                }
            }

            if (!alreadySelected)
            {
                icons[0] = icons[0].Remove(0, 1);
                icons[0] = icons[0].Insert(0, ">");
                position = 0;
            }

            PrintMenu(mapWidth);

            ConsoleKeyInfo keyInfo;
            bool choiceDone = false;
            while (!choiceDone)
            {
                keyInfo = Console.ReadKey();

                switch (keyInfo.Key)
                {
                    case ConsoleKey.DownArrow:
                        if (position != size - 1)
                        {
                            ChangeSelected(position, position +1);
                            PrintMenu(mapWidth);
                            position++;
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        if (position != 0)
                        {
                            ChangeSelected(position, position - 1);
                            PrintMenu(mapWidth);
                            position--;
                        }
                        break;
                    case ConsoleKey.Enter:
                        if (icons[position] == "> POKEMON")
                        {
                            MenuPokemon.Open(player);
                            Map.DrawMap();
                            PrintMenu(mapWidth);
                        }
                        else if (icons[position] == "> OBJETS")
                        {

                        }
                        else if (icons[position] == "> " + player.name)
                        {

                        }
                        else if (icons[position] == "> SAUVER")
                        {
                            Save.Saving(player, rival);
                            Save.SavingItem();
                            choiceDone = true;
                        }
                        else if (icons[position] == "> RETOUR")
                        {
                            choiceDone = true;
                        }

                        break;
                    case ConsoleKey.Escape:
                        choiceDone = true;
                        break;
                    default:
                        Console.SetCursorPosition(endPositionXText, endPositionYText);
                        Console.Write(" ");
                        Console.SetCursorPosition(endPositionXText, endPositionYText);
                        break;
                }
            }
            icons.Clear();
        }

        private static void ChangeSelected(int position, int nextPosition)
        {
            icons[position] = icons[position].Remove(0, 1);
            icons[position] = icons[position].Insert(0, " ");

            icons[nextPosition] = icons[nextPosition].Remove(0, 1);
            icons[nextPosition] = icons[nextPosition].Insert(0, ">");
        }

        private static void PrintMenu(int mapWidth)
        {
            Console.SetCursorPosition(mapWidth - 3, 1);
            Console.Write(hautMenu);
            for (int i = 2; i < size + 2; i++)
            {
                Console.SetCursorPosition(mapWidth - 3, i);
                Console.Write(middleMenu);

                Console.SetCursorPosition(mapWidth - 3 + 2, i);
                Console.Write(icons[i - 2]);
            }
            Console.SetCursorPosition(mapWidth - 3, size + 2);
            Console.Write(hautMenu);

            endPositionXText = Console.CursorLeft;
            endPositionYText = Console.CursorTop;
        }
    }

    class MenuPokemon
    {
        private static string pokemonLine1 = "  ";
        private static string pokemonLine2 = "   ";

        private static string littleMenuStats = "  STATS";
        private static string littleMenuOrdre = "  ORDRE";
        private static string littleMenuRetour = "  RETOUR";

        private static bool isChangingOnTop;

        

        private static List<string> pokemonLines1 = new List<string>();
        private static List<string> pokemonLines2 = new List<string>();
        private static List<string> littleMenuLines = new List<string>();


        private static bool alreadySelected;
        private static bool alreadySelectedLittle;
        private static int position;
        private static int positionLittle;
        private static int endPositionXText;
        private static int endPositionYText;

        private static int PositionChangement;

        public static void Open(Player player, bool isCombat = false)
        {
            player.addPokemonToParty(new Pokemon(9, 45));

            for (int i = 0; i < player.pokemonParty.Count; i++)
            {
                Pokemon pokemon = player.pokemonParty[i];


                pokemonLines1.Add(pokemonLine1);
                pokemonLines1[i] += pokemon.name;

                for (int j = pokemon.name.Length; j <= 10; j++)
                {
                    pokemonLines1[i] += " ";
                }
                pokemonLines1[i] += "N" + pokemon.level;


                pokemonLines2.Add(pokemonLine2);
                int pvPerSix = pokemon.pvLeft * 6 / pokemon.pv;
                string barPv = "PV";
                for (int j = 0; j < pvPerSix; j++)
                {
                    barPv += "=";
                }
                for (int j = barPv.Length; j < 8; j++)
                {
                    barPv += "*";
                }
                pokemonLines2[i] += barPv;

                pokemonLines2[i] += "  ";
                for (int j = 3; j > pokemon.pvLeft.ToString().Length ; j--)
                {
                    pokemonLines2[i] += " ";
                }
                pokemonLines2[i] += pokemon.pvLeft + "/";
                for (int j = 3; j > pokemon.pv.ToString().Length; j--)
                {
                    pokemonLines2[i] += " ";
                }
                pokemonLines2[i] += pokemon.pv;
            }
            
            

            alreadySelected = false;
            foreach (string pokemon in pokemonLines1)
            {
                if (pokemon[0] == '>')
                {
                    alreadySelected = true;
                }
            }

            if (!alreadySelected)
            {
                pokemonLines1[0] = pokemonLines1[0].Remove(0, 1);
                pokemonLines1[0] = pokemonLines1[0].Insert(0, ">");
                position = 0;
            }

            PrintMenu(false);


            bool isSwitching = false;
            bool retour = false;
            ConsoleKeyInfo keyInfo;

            while (!retour)
            {
                keyInfo = Console.ReadKey(true);

                switch (keyInfo.Key)
                {
                    case ConsoleKey.DownArrow:
                        if (position != pokemonLines1.Count - 1)
                        {
                            ChangeSelected(position, position + 1, pokemonLines1);
                            PrintMenu(isSwitching);
                            position++;
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        if (position != 0)
                        {
                            ChangeSelected(position, position - 1, pokemonLines1);
                            PrintMenu(isSwitching);
                            position--;
                        }
                        break;
                    case ConsoleKey.Enter:
                        if (!isSwitching) OpenLittleMenu(ref isSwitching, player);
                        else
                        {
                            if (PositionChangement != position && !isCombat)
                            {
                                Pokemon temp = player.pokemonParty[position];
                                player.pokemonParty[position] = player.pokemonParty[PositionChangement];
                                player.pokemonParty[PositionChangement] = temp;

                                string tempStr = pokemonLines1[position];
                                string tempStr2 = pokemonLines2[position];
                                pokemonLines1[position] = pokemonLines1[PositionChangement];
                                pokemonLines2[position] = pokemonLines2[PositionChangement];
                                pokemonLines1[PositionChangement] = tempStr;
                                pokemonLines2[PositionChangement] = tempStr2;

                                ChangeSelected(PositionChangement, position, pokemonLines1);
                            }
                            else if(isCombat)
                            {

                            }
                            isChangingOnTop = false;
                            isSwitching = false;
                        }

                        PrintMenu(isSwitching);
                        break;
                    case ConsoleKey.Escape:
                        retour = true;
                        break;
                }
            }
            pokemonLines1.Clear();
            pokemonLines2.Clear();
        }
        private static void OpenLittleMenu(ref bool isSwitching, Player player)
        {

            littleMenuLines.Add(littleMenuStats);
            littleMenuLines.Add(littleMenuOrdre);
            littleMenuLines.Add(littleMenuRetour);

            alreadySelectedLittle = false;
            foreach (string button in littleMenuLines)
            {
                if (button[0] == '>')
                {
                    alreadySelectedLittle = true;
                }
            }

            if (!alreadySelectedLittle)
            {
                littleMenuLines[0] = littleMenuLines[0].Remove(0, 1);
                littleMenuLines[0] = littleMenuLines[0].Insert(0, ">");
                positionLittle = 0;
            }

            PrintLittleMenu();

            bool retour = false;
            ConsoleKeyInfo keyInfo;

            while (!retour)
            {
                keyInfo = Console.ReadKey();

                switch (keyInfo.Key)
                {
                    case ConsoleKey.DownArrow:
                        if (positionLittle != littleMenuLines.Count - 1)
                        {
                            ChangeSelected(positionLittle, positionLittle + 1, littleMenuLines);
                            PrintLittleMenu();
                            positionLittle++;
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        if (positionLittle != 0)
                        {
                            ChangeSelected(positionLittle, positionLittle - 1, littleMenuLines);
                            PrintLittleMenu();
                            positionLittle--;
                        }
                        break;
                    case ConsoleKey.Enter:
                        if (positionLittle == 0)
                        {
                            player.pokemonParty[position].AfficherDetailsMenu();
                            retour = true;
                        }   
                        else if (positionLittle == 1)
                        {
                            PositionChangement = position;
                            isChangingOnTop = true;
                            isSwitching = true;
                            retour = true;
                        }
                        else if (positionLittle == 2) retour = true;
                        break;
                    case ConsoleKey.Escape:
                        retour = true;
                        break;
                    default:
                        Console.SetCursorPosition(endPositionXText, endPositionYText);
                        Console.Write(" ");
                        Console.SetCursorPosition(endPositionXText, endPositionYText);
                        break;
                }
            }
            littleMenuLines.Clear();
        }



        private static void ChangeSelected(int position, int nextPosition, List<string> list)
        {
            if (list[position][0] != '}')
            {
                list[position] = list[position].Remove(0, 1);
                list[position] = list[position].Insert(0, " ");
            }
            if (isChangingOnTop)
            {
                list[position] = list[position].Remove(0, 1);
                list[position] = list[position].Insert(0, "}");
            }

            if (list[nextPosition][0] == '}') isChangingOnTop = true;
            else isChangingOnTop = false;
            list[nextPosition] = list[nextPosition].Remove(0, 1);
            list[nextPosition] = list[nextPosition].Insert(0, ">");
        }
        private static void ChangeSelectedCombat()
        {

        }
        private static void PrintMenu(bool isSwitching)
        {
            Console.Clear();
            for (int i = 0; i < pokemonLines1.Count; i++)
            {
                Console.WriteLine(pokemonLines1[i]);
                foreach (char c in pokemonLines2[i])
                {
                    if (c == '=')
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write(c);
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else if (c == '*')
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write('=');
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.Write(c);
                    }
                }
                Console.WriteLine();
                Console.WriteLine();
            }

            for (int i = pokemonLines1.Count;i < 6; i++)
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
            }

            string topBox =    "0-------------------0";
            string middleBox = "|                   |";

            Console.WriteLine(topBox);
            int tempPositionXText = Console.CursorLeft;
            int tempPositionYText = Console.CursorTop;
            Console.WriteLine(middleBox);
            Console.WriteLine(middleBox);
            Console.WriteLine(topBox);
            int tempPositionXText2 = Console.CursorLeft;
            int tempPositionYText2 = Console.CursorTop;

            Console.SetCursorPosition(tempPositionXText + 2, tempPositionYText);


            string text = "";
            if (!isSwitching) text = "Selectionnez un POKEMON.";
            else if (isSwitching) text = "Nouvelle position du POKEMON...";

            int charRestant = topBox.Length-4;
            int lineWriting = 1;
            string[] words = text.Split(" ");
            int wordIndex = 0;


            for (int i = 0; i < text.Length; i++)
            {

                if (charRestant > 0 && lineWriting == 1)
                {
                    Console.Write(text[i]);
                    charRestant--;

                    // Si le caractère actuel est un espace, passe au mot suivant
                    if (text[i] == ' ' || (charRestant == 0 && text[i +1] == ' '))
                    {
                        wordIndex++;

                        // Vérifie si le mot suivant peut s'insérer dans la ligne actuelle
                        if (wordIndex < words.Length && words[wordIndex].Length > charRestant)
                        {
                            charRestant = topBox.Length - 4;
                            lineWriting++;
                            Console.SetCursorPosition(tempPositionXText + 2, tempPositionYText+1);
                        }
                    }
                }
                else if (charRestant > 0 && lineWriting == 2)
                {
                    if (!(charRestant == topBox.Length - 4 && text[i] == ' '))
                    {
                        Console.Write(text[i]);
                    }

                    charRestant--;
                }
            }

            Console.SetCursorPosition(tempPositionXText2, tempPositionYText2);
            endPositionXText = Console.CursorLeft;
            endPositionYText = Console.CursorTop;
        }

        private static void PrintLittleMenu()
        {
            string hautBox = "0---------0";
            string middleBox = "|         |";

            Console.SetCursorPosition(endPositionXText + 10, endPositionYText - 5);
            Console.Write(hautBox);
            Console.SetCursorPosition(endPositionXText + 10, endPositionYText - 4);
            for (int i = 4; i >= 1; i--)
            {
                Console.Write(middleBox); 
                Console.SetCursorPosition(endPositionXText + 10, endPositionYText - i);
            }
            Console.Write(hautBox);

            Console.SetCursorPosition(endPositionXText + 12, endPositionYText - 4);
            Console.Write(littleMenuLines[0]);
            Console.SetCursorPosition(endPositionXText + 12, endPositionYText - 3);
            Console.Write(littleMenuLines[1]);
            Console.SetCursorPosition(endPositionXText + 12, endPositionYText - 2);
            Console.Write(littleMenuLines[2]);

            Console.SetCursorPosition(endPositionXText, endPositionYText);
        }
    }
}
