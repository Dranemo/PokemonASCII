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
    private static int mapHeight;
    private static int mapWidth;

    private static Player player;
    private static Rival rival;

    private static List<Entity> entityList = new List<Entity>();
    







    public static void MapPlayer(Player player_, Rival rival_)
    {
        player = player_;
        rival = rival_;

        LoadMap(player.map);
        ConsoleKeyInfo keyInfo;


        player.actuallPositionChar = map[player.PositionX, player.PositionY];

        Console.Clear();
        DrawMap();
        DrawEntity();
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


            if (MovePlayer(deltaX, deltaY))
            { 
                DrawPlayer();


                // Transition et npc
                if (IsCurrentMap("bedroom.txt"))
                {
                    ChangeMap(15, 1, "mom.txt", 8, 1, "\nMaman...");
                }

                else if (IsCurrentMap("bourg_palette.txt"))
                {
                    ChangeMap(0, "route_1.txt", player.PositionX - 3, 35, "\nVers la route 1 !");
                    ChangeMap(13, 10, "chen.txt", 5, 8, "\nVers le labo du Pr.Chen...");
                    ChangeMap(6, 5, "mom.txt", 3, 8, "\nMaman...");
                }

                else if (IsCurrentMap("chen.txt"))
                {
                    ChangeMap(8, "bourg_palette.txt", 13, 11, "\nVers Bourg-Palette...");

                    foreach (Entity entity in entityList)
                    {
                        if (entity is NPC npc)
                        {
                            CanTalk(npc, keyInfo);
                        }
                        else if (entity is Pokeball pokeball)
                        {
                            Open(pokeball, keyInfo);
                        }
                    }
                }

                else if (IsCurrentMap("mom.txt"))
                {
                    ChangeMap(8, "bourg_palette.txt", 6, 6, "\nVers Bourg-Palette...");
                    ChangeMap(8, 1, "bedroom.txt", 15, 1, "\nChambre...");

                    foreach (NPC npc in entityList)
                    {
                        CanTalk(npc, keyInfo);
                    }
                }

                else if (IsCurrentMap("route_1.txt"))
                {
                    ChangeMap(35, "bourg_palette.txt", player.PositionX + 3, 0, "Vous arrivez à Bourg Palette !");
                }

                
                // Hautes herbes
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
            DrawEntity();
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
            DrawEntity();
            DrawPlayer();
        }
    }
    private static void CanTalk(NPC npc, ConsoleKeyInfo keyInfo)
    {

        if (npc.PositionX != -1 && npc.PositionY != -1)
        {
            if ((player.PositionX + 1 == npc.PositionX && player.PositionY == npc.PositionY) || (player.PositionX - 1 == npc.PositionX && player.PositionY == npc.PositionY) || (player.PositionX == npc.PositionX && player.PositionY - 1 == npc.PositionY) || (player.PositionX == npc.PositionX && player.PositionY + 1 == npc.PositionY))
            {
                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    if (!string.IsNullOrEmpty(npc.dialogue))
                    {
                        foreach (char c in npc.dialogue)
                        {
                            Console.Write(c);
                            Task.Delay(50).Wait();
                        }
                    }
                    Functions.ClearInputBuffer();
                    Console.ReadKey();


                    Console.Clear();
                    DrawMap();
                    DrawEntity();
                    DrawPlayer();
                }
                
            }
        }


    }
    private static void Open(Pokeball pokeball, ConsoleKeyInfo keyInfo)
    {

        if (pokeball.PositionX != -1 && pokeball.PositionY != -1)
        {
            if ((player.PositionX + 1 == pokeball.PositionX && player.PositionY == pokeball.PositionY) || (player.PositionX - 1 == pokeball.PositionX && player.PositionY == pokeball.PositionY) || (player.PositionX == pokeball.PositionX && player.PositionY - 1 == pokeball.PositionY) || (player.PositionX == pokeball.PositionX && player.PositionY + 1 == pokeball.PositionY))
            {
                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine(pokeball.name);
                    Thread.Sleep(1000);
                    Functions.ClearInputBuffer();
                }

                Console.Clear();
                DrawMap();
                DrawEntity(); 
                DrawPlayer();
            }
        }

    }
    
    
    
    
    
    private static void LoadMap(string filename)
    {

        currentMapFileName = AdresseFile.FileDirection + "Assets\\Maps\\" + filename;

        string[] lines = File.ReadAllLines(currentMapFileName);

        mapWidth = lines[0].Length;
        mapHeight = lines.Length;

        map = new char[mapWidth, mapHeight];

        for (int y = 0; y < mapHeight; y++)
        {
            string line = lines[y];

            for (int x = 0; x < mapWidth; x++)
            {
                map[x, y] = line[x];
            }
        }


        entityList.Clear();
        if (filename == "chen.txt")
        {
            NPC chen = new NPC("Prof.Chen", "Voici 3 Pokémon! Mais... Ils sont dans des Poké Balls. Plus jeune, j'étais un sacré Dresseur de Pokémon! Et oui! Mais avec l'âge, il ne m'en reste plus que 3! Choisis-en un!", 'C', filename, 6, 2, map[6, 2]);
            NPC blue = new NPC(rival.name, "Yo minable !", 'R', filename, 4, 4, map[4, 4]);

            entityList.Add(chen);
            entityList.Add(blue);

            Pokeball pokeball1 = new Pokeball(1, filename, 8, 3, map[8, 3]);
            Pokeball pokeball2 = new Pokeball(4, filename, 9, 3, map[9, 3]);
            Pokeball pokeball3 = new Pokeball(7, filename, 10, 3, map[10, 3]);

            entityList.Add(pokeball1);
            entityList.Add(pokeball2);
            entityList.Add(pokeball3);
        }
        else if (filename == "mom.txt")
        {
            NPC maman = new NPC("Maman", "Bonjour mon fils ! Bien dormi ?", '8', filename, 7, 4, map[7, 4]);
            entityList.Add(maman);
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
        Console.Write(player.sprite);
    }
    private static void DrawEntity()
    {
        foreach (Entity entity in entityList)
        {
            Console.SetCursorPosition(entity.PositionX, entity.PositionY);
            if (entity.sprite == 'o')
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write(entity.sprite);
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.Write(entity.sprite);
            }
        }
        
        
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
        if (map[x, y] != ' ' && map[x, y] != '#' && map[x, y] != 'D')
        {
            return false;
        }

        foreach(Entity entity in entityList)
        {
            if (x == entity.PositionX && y == entity.PositionY)
            {
                return false;
            }
        }

        return true;
    }
}
