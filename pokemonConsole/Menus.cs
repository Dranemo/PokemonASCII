using System;
using System.Collections.Generic;
using System.Linq;
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
                size = 5;
                icons.Add(pokemonMenuIcon);
            }
            else
            {
                size = 4;
            }
            playerMenuIcon += player.name;

            icons.Add(itemsMenuIcon);
            icons.Add(playerMenuIcon);
            icons.Add(saveMenuIcon);
            icons.Add(retourMenuIcon);

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
                            choiceDone = true;
                        }
                        else if (icons[position] == "> RETOUR")
                        {
                            choiceDone = true;
                        }

                        break;
                    default:
                        Console.SetCursorPosition(endPositionXText, endPositionYText);
                        Console.Write(" ");
                        Console.SetCursorPosition(endPositionXText, endPositionYText);
                        break;
                }
            }
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
}
