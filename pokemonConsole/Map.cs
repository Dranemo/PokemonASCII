using pokemonConsole;
using System;
using System.ComponentModel.Design;
using System.Reflection;
using Usefull;

internal class Map
{
    private static char[,] map;
    private static Random random = new Random();
    private static string currentMapFileName="";

    private static Player player;

    







    public static void MapPlayer(Player player_)
    {
        player = player_;

        LoadMap(player.map);
        ConsoleKeyInfo keyInfo;


        player.actuallPositionChar = map[player.PositionX, player.PositionY];

        Console.Clear();
        DrawMap();
        DrawPlayer();

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


            bool playerMoved = MovePlayer(deltaX, deltaY);



            if (playerMoved)
            { 
                //Console.Clear();
                //DrawMap();
                DrawPlayer();




            // NPC
                if (IsCurrentMap("bedroom.txt"))
                {
                    ChangeMap(15, 1, "mom.txt", 8, 1, "\nMaman...");
                }

                else if (IsCurrentMap("bourg_palette"))
                {
                    ChangeMap(0, "route_1.txt", player.PositionX - 3, 35, "\nVers la route 1 !");
                    ChangeMap(13, 10, "chen.txt", 5, 8, "\nVers le labo du Pr.Chen...");
                    ChangeMap(6, 5, "mom.txt", 3, 8, "\nMaman...");
                }

                else if (IsCurrentMap("chen.txt"))
                {
                    ChangeMap(8, "bourg_palette.txt", 13, 11, "\nVers Bourg-Palette...");


                    CanTalk(2, 7, "Voici 3 Pokémon! Mais... Ils sont dans des Poké Balls. Plus jeune, j'étais un sacré Dresseur de Pokémon! Et oui! Mais avec l'âge, il ne m'en reste plus que 3! Choisis-en un!", keyInfo);
                    CanTalk(4, 4, "Yo minable !", keyInfo);
                    Open(3, 8, "Salamèche", keyInfo);
                    Open(3, 9, "Carapuce", keyInfo);
                    Open(3, 10, "Bulbizarre", keyInfo);
                }

                else if (IsCurrentMap("mom.txt"))
                {
                    ChangeMap(8, "bourg_palette.txt", 6, 6, "\nVers Bourg-Palette...");
                    ChangeMap(8, 1, "bedroom.txt", 15, 1, "\nChambre...");


                    CanTalk(7, 4, "Bonjour mon fils ! Bien dormi ?", keyInfo);
                }

                else if (IsCurrentMap("route_1.txt"))
                {
                    ChangeMap(35, "bourg_palette.txt", player.PositionX + 3, 0, "Vous arrivez à Bourg Palette !");
                }


                // Transitions entre les maps

                


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



    private static void ChangeMap(int x, int y, string nextMapFileName, int nextX, int nextY, string loadingText)
    {

        if (player.PositionX == x && player.PositionY == y)
        {
            Console.WriteLine($"\n{loadingText}");
            Thread.Sleep(500);
            Functions.ClearInputBuffer();
            LoadMap(nextMapFileName);
            player.PositionX = nextX;
            player.PositionY = nextY;

            Console.Clear();
            DrawMap();
            DrawPlayer();
        }
    }
    private static void ChangeMap(int y, string nextMapFileName, int nextX, int nextY, string loadingText)
    {

        if (player.PositionY == y)
        {
            Console.WriteLine($"\n{loadingText}");
            Thread.Sleep(500);
            Functions.ClearInputBuffer();
            LoadMap(nextMapFileName);
            player.PositionX = nextX;
            player.PositionY = nextY;

            Console.Clear();
            DrawMap();
            DrawPlayer();
        }
    }
    private static void CanTalk(int npcX, int npcY, string dialogue, ConsoleKeyInfo keyInfo)
    {

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
                    DrawPlayer();
                }
                
            }
        }


    }
    private static void Open(int chestX, int chestY, string dialogue, ConsoleKeyInfo keyInfo)
    {

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
                DrawPlayer();
            }
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
    private static bool MovePlayer(int deltaX, int deltaY)
    {
        // limites du deplacement pour eviter de sortir de la carte

        int newX = player.PositionX + deltaX;
        int newY = player.PositionY + deltaY;

        if (IsInsideMap(newX, newY) && IsWalkable(newX, newY))
        {
            int x = player.PositionX;
            int y = player.PositionY;
            // Effacer l'ancienne position du joueur
            Console.SetCursorPosition(x, y);
            
            if (map[x, y] == '~')
            {
                Console.ForegroundColor = ConsoleColor.Blue;
            }
            else if (map[x, y] == '#')
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
            }

            Console.Write(player.actuallPositionChar);
            Console.ForegroundColor = ConsoleColor.White;



            // Mettre à jour la position du joueur
            player.PositionX = newX;
            player.PositionY = newY;

            player.actuallPositionChar = map[newX, newY];

            return true;
        }
        else
        {
            return false;
        }
    }
    private static void DrawPlayer()
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
