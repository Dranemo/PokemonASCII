using pokemonConsole;
using System;
using System.Reflection;
using Usefull;

internal class Map
{
    private static char[,] map;
    private static Random random = new Random();
    private static string currentMapFileName="";







    public static void MapPlayer(Player player)
    {
        LoadMap("chen.txt");
        ConsoleKeyInfo keyInfo;

        player.PositionX = 8;
        player.PositionY = 8;

        Console.Clear();
        DrawMap();
        DrawPlayer(player);

        do
        {
            keyInfo = Console.ReadKey();

            // Deplacer le joueur en fonction de la touche pressee
            int deltaX = 0, deltaY = 0;

            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    deltaY = -1;
                    break;
                case ConsoleKey.DownArrow:
                    deltaY = 1;
                    break;
                case ConsoleKey.LeftArrow:
                    deltaX = -1;
                    break;
                case ConsoleKey.RightArrow:
                    deltaX = 1;
                    break;
            }


            bool playerMoved = MovePlayer(deltaX, deltaY, player);



            if (playerMoved)
            { 
                //Console.Clear();
                //DrawMap();
                DrawPlayer(player);




            // NPC

                if (IsCurrentMap("mom.txt"))
                {
                    CanTalk("mom.txt", '8', 8, 4, "Bonjour mon fils ! Bien dormi ?", player, keyInfo);
                }

                else if (IsCurrentMap("chen.txt"))
                {
                    CanTalk("chen.txt", 'C', 2, 7, "Voici 3 Pokémon! Mais... Ils sont dans des Poké Balls. Plus jeune, j'étais un sacré Dresseur de Pokémon! Et oui! Mais avec l'âge, il ne m'en reste plus que 3! Choisis-en un!", player, keyInfo);
                    CanTalk("chen.txt", 'R', 4, 4, "Yo minable !", player, keyInfo);
                    Open("chen.txt", 'o', 3, 8, "Salamèche", player, keyInfo);
                    Open("chen.txt", 'o', 3, 9, "Carapuce", player, keyInfo);
                    Open("chen.txt", 'o', 3, 10, "Bulbizarre", player, keyInfo);
                }

                // Transitions entre les maps

                if (IsCurrentMap("route_1.txt") && player.PositionY == 35)
                {
                    Console.WriteLine("Vous arrivez à Bourg Palette !");
                    Thread.Sleep(500);
                    Functions.ClearInputBuffer();
                    LoadMap("bourg_palette.txt");
                    player.PositionX += 3;
                    player.PositionY = 0;

                    Console.Clear();
                    DrawMap();
                    DrawPlayer(player);
                }
                else if (IsCurrentMap("bourg_palette.txt") && player.PositionY == 0)
                {
                    Console.WriteLine("\nVers la route 1 !");
                    Thread.Sleep(500);
                    Functions.ClearInputBuffer();
                    LoadMap("route_1.txt");
                    player.PositionX -= 3;
                    player.PositionY = 35;

                    Console.Clear();
                    DrawMap();
                    DrawPlayer(player);
                }
                else if (IsCurrentMap("bourg_palette.txt") && player.PositionX == 13 && player.PositionY == 10)
                {
                    Console.WriteLine("\nVers le labo du Pr.Chen...");
                    Thread.Sleep(500);
                    Functions.ClearInputBuffer();
                    LoadMap("chen.txt");
                    player.PositionX = 5;
                    player.PositionY = 8;

                    Console.Clear();
                    DrawMap();
                    DrawPlayer(player);
                }
                else if (IsCurrentMap("bourg_palette.txt") && player.PositionX == 6 && player.PositionY == 5)
                {
                    Console.WriteLine("\nMaman");
                    Thread.Sleep(500);
                    Functions.ClearInputBuffer();
                    LoadMap("mom.txt");
                    player.PositionX = 3;
                    player.PositionY = 8;

                    Console.Clear();
                    DrawMap();
                    DrawPlayer(player);
                }
                else if (IsCurrentMap("chen.txt") && player.PositionY == 8)
                {
                    Console.WriteLine("\nVers Bourg-Palette...");
                    Thread.Sleep(500);
                    Functions.ClearInputBuffer();
                    LoadMap("bourg_palette.txt");
                    player.PositionX = 13;
                    player.PositionY = 11;

                    Console.Clear();
                    DrawMap();
                    DrawPlayer(player);
                }
                else if (IsCurrentMap("mom.txt") && player.PositionY == 8)
                {
                    Console.WriteLine("\nVers Bourg-Palette...");
                    Thread.Sleep(500);
                    Functions.ClearInputBuffer();
                    LoadMap("bourg_palette.txt");
                    player.PositionX = 6;
                    player.PositionY = 6;

                    Console.Clear();
                    DrawMap();
                    DrawPlayer(player);
                }
                else if (IsCurrentMap("mom.txt") && player.PositionX == 8 && player.PositionY == 1)
                {
                    Console.WriteLine("\nChambre");
                    Thread.Sleep(500);
                    Functions.ClearInputBuffer();
                    LoadMap("bedroom.txt");
                    player.PositionX = 15;
                    player.PositionY = 1;

                    Console.Clear();
                    DrawMap();
                    DrawPlayer(player);
                }
                else if (IsCurrentMap("bedroom.txt") && player.PositionX == 15 && player.PositionY == 1)
                {
                    Console.WriteLine("\nMaman");
                    Thread.Sleep(500);
                    Functions.ClearInputBuffer();
                    LoadMap("mom.txt");
                    player.PositionX = 8;
                    player.PositionY = 1;

                    Console.Clear();
                    DrawMap();
                    DrawPlayer(player);
                }


                if (map[player.PositionX, player.PositionY] == '#')
                {
                    if (random.Next(1, 101) <= 10) // chance de rencontrer un Pokemon dans les hautes herbes
                    {
                        Console.WriteLine($"\nCombat lancé !");
                        Thread.Sleep(500);
                        Functions.ClearInputBuffer();
                        Combat.LoopCombat(player);
                    }
                }
            }

        } while (keyInfo.Key != ConsoleKey.Escape);
    }




    private static void CanTalk(string currentMapFileName, char caractere, int npcX, int npcY, string dialogue, Player player, ConsoleKeyInfo keyInfo)
    {

        string filePath = $"{AdresseFile.FileDirection}Assets\\Maps\\{currentMapFileName}";

        string[] lines = File.ReadAllLines(filePath);

        for (int i = 0; i < lines.Length; i++)
        {
            int charIndex = lines[i].IndexOf(caractere);
            if (charIndex != -1)
            {
                npcX = charIndex;
                npcY = i;
                break;
            }
        }

        if (npcX != -1 && npcY != -1)
        {
            if ((player.PositionX + 1 == npcX && player.PositionY == npcY) || (player.PositionX - 1 == npcX && player.PositionY == npcY) || (player.PositionX == npcX && player.PositionY - 1 == npcY) || (player.PositionX == npcX && player.PositionY + 1 == npcY))
            {
                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    if (!string.IsNullOrEmpty(dialogue))
                    {
                        foreach (char c in dialogue)
                        {
                            Console.Write(c);
                            Task.Delay(50).Wait();
                        }
                    }
                    Functions.ClearInputBuffer();
                    Console.ReadKey();


                    Console.Clear();
                    DrawMap();
                    DrawPlayer(player);
                }
                
            }
        }
        else
        {
            Console.WriteLine($"Le caractère '{caractere}' n'a pas été trouvé.");
        }


    }
    private static void Open(string currentMapFileName, char caractere, int chestX, int chestY, string dialogue, Player player, ConsoleKeyInfo keyInfo)
    {
        string filePath = $"{AdresseFile.FileDirection}Assets\\Maps\\{currentMapFileName}";
        string[] lines = File.ReadAllLines(filePath);

        for (int i = 0; i < lines.Length; i++)
        {
            int charIndex = lines[i].IndexOf(caractere);
            if (charIndex != -1)
            {
                chestX = charIndex;
                chestY = i;
                break;
            }
        }

        if (chestX != -1 && chestY != -1)
        {
            if ((player.PositionX + 1 == chestX && player.PositionY == chestY) || (player.PositionX - 1 == chestX && player.PositionY == chestY) || (player.PositionX == chestX && player.PositionY - 1 == chestY) || (player.PositionX == chestX && player.PositionY + 1 == chestY))
            {
                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine(dialogue);
                    Thread.Sleep(1000);
                    Functions.ClearInputBuffer();
                }

                Console.Clear();
                DrawMap();
                DrawPlayer(player);
            }
        }
        else
        {
            Console.WriteLine($"Le caractère '{caractere}' n'a pas ete trouve.");
        }

    }
    
    
    
    
    
    private static void LoadMap(string filename)
    {

        currentMapFileName = AdresseFile.FileDirection + "Assets\\Maps\\" + filename;

        string[] lines = File.ReadAllLines(currentMapFileName);

        int width = lines[0].Length;
        int height = lines.Length;

        map = new char[width, height];

        for (int y = 0; y < height; y++)
        {
            string line = lines[y];

            for (int x = 0; x < width; x++)
            {
                map[x, y] = line[x];
            }
        }
    }
    private static void DrawMap()
    {
        for (int y = 0; y < map.GetLength(1); y++)
        {
            for (int x = 0; x < map.GetLength(0); x++)
            {
                
                if (map[x, y] == '~')
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                }
                else if (map[x, y] == '#')
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                }
                else if (map[x, y] == 'o')
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.Write(map[x, y]);
            }
            Console.WriteLine();
        }
    }
    private static bool MovePlayer(int deltaX, int deltaY, Player player)
    {
        // limites du deplacement pour eviter de sortir de la carte
        int newX = player.PositionX + deltaX;
        int newY = player.PositionY + deltaY;

        if (IsInsideMap(newX, newY) && IsWalkable(newX, newY))
        {
            // Effacer l'ancienne position du joueur
            Console.SetCursorPosition(player.PositionX, player.PositionY);
            Console.Write(" ");

            // Mettre à jour la position du joueur
            player.PositionX = newX;
            player.PositionY = newY;

            return true;
        }
        else
        {
            return false;
        }
    }
    private static void DrawPlayer(Player player)
    {
        Console.SetCursorPosition(player.PositionX, player.PositionY);
        Console.Write("P");
    }


    private static bool IsCurrentMap(string mapToCheck)
    {
        string fullPathToCheck = AdresseFile.FileDirection + "Assets\\Maps\\" + mapToCheck;
        return currentMapFileName.Equals(fullPathToCheck, StringComparison.OrdinalIgnoreCase);
    }
    private static bool IsInsideMap(int x, int y)
    {
        return x >= 0 && x < map.GetLength(0) && y >= 0 && y < map.GetLength(1);
    }
    private static bool IsWalkable(int x, int y)
    {
        return map[x, y] == ' ' || map[x, y] == '#' || map[x, y] == 'D';
    }
}
