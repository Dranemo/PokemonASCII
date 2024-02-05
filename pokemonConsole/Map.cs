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
    private static List<Entity> entityToRemove = new List<Entity>();
    







    public static void MapPlayer(Player player_, Rival rival_)
    {
        player = player_;
        rival = rival_;

        LoadMap(player.map);
        ConsoleKeyInfo keyInfo;


        player.actuallPositionChar = map[player.PositionX, player.PositionY];

        DrawMap();
        DateTime time = DateTime.Now;

        do
        {
            foreach(Entity entity in entityList)
            {
                if(entity is NPC npc)
                {
                    npc.Update(time, player);
                    if (npc.updated)
                    {
                        npc.updated = false;
                        DrawMap();
                        time = DateTime.Now;
                    }
                }
            }
            keyInfo = Console.ReadKey();

            // Deplacer le joueur en fonction de la touche pressee
            int deltaX = 0, deltaY = 0;
            bool moved = false;

            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    deltaY = -1;
                    moved = true;
                    break;
                case ConsoleKey.DownArrow:
                    deltaY = 1;
                    moved = true;
                    break;
                case ConsoleKey.LeftArrow:
                    deltaX = -1;
                    moved = true;
                    break;
                case ConsoleKey.RightArrow:
                    deltaX = 1;
                    moved = true;
                    break;
                case ConsoleKey.X:
                    Console.SetCursorPosition(player.PositionX+1, player.PositionY);
                    Console.Write(" ");
                    Console.SetCursorPosition(player.PositionX + 1, player.PositionY);

                    Menu_principal.Open(player, mapWidth, rival);

                    DrawMap();

                    break;
                case ConsoleKey.Enter:
                    break;
                default:
                    Console.SetCursorPosition(player.PositionX + 1, player.PositionY);
                    Console.Write(" ");
                    Console.SetCursorPosition(player.PositionX + 1, player.PositionY);
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
                    if (moved)
                    {
                        ChangeMap(0, "route_1.txt", player.PositionX - 3, 35, "\nVers la route 1 !");
                        ChangeMap(13, 10, "chen.txt", 5, 8, "\nVers le labo du Pr.Chen...");
                        ChangeMap(6, 5, "mom.txt", 3, 8, "\nMaman...");
                    }
                    
                }

                else if (IsCurrentMap("chen.txt"))
                {
                    if(moved) ChangeMap(8, "bourg_palette.txt", 13, 11, "\nVers Bourg-Palette...");

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
                    if (moved)
                    {
                        ChangeMap(8, "bourg_palette.txt", 6, 6, "\nVers Bourg-Palette...");
                        ChangeMap(8, 1, "bedroom.txt", 15, 1, "\nChambre...");
                    }
                    

                    foreach (NPC npc in entityList)
                    {
                        CanTalk(npc, keyInfo);
                    }
                }

                else if (IsCurrentMap("route_1.txt"))
                {
                    if(moved) ChangeMap(35, "bourg_palette.txt", player.PositionX + 3, 0, "Vous arrivez à Bourg Palette !");

                    foreach (NPC npc in entityList)
                    {
                        CanTalk(npc, keyInfo);
                    }
                }

                
                // Hautes herbes
                if (map[player.PositionX, player.PositionY] == '#' && moved)
                {
                    if (random.Next(1, 101) <= 10) // chance de rencontrer un Pokemon dans les hautes herbes
                    {
                        Console.WriteLine($"\nCombat lancé !");
                        Thread.Sleep(500);
                        Functions.ClearInputBuffer();
                        Combat.LoopCombat(player);

                        DrawMap();
                    }
                }

                if (entityToRemove.Count!= 0)
                {
                    RemoveEntity();
                }
            }

        } while (keyInfo.Key != ConsoleKey.Escape);

        player.pokemonParty.Clear();
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
            player.map = nextMapFileName;

            player.actuallPositionChar = map[nextX, nextY];

            DrawMap();
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
            player.map = nextMapFileName;

            player.actuallPositionChar = map[nextX, nextY];

            DrawMap();
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
                    npc.Function(player);
                    Functions.ClearInputBuffer();
                    Console.ReadKey();


                    DrawMap();
                }
                
            }
        }


    }
    private static void Open(Pokeball pokeball, ConsoleKeyInfo keyInfo)
    {

        if (pokeball.PositionX != -1 && pokeball.PositionY != -1 && !pokeball.taken)
        {
            if ((player.PositionX + 1 == pokeball.PositionX && player.PositionY == pokeball.PositionY) || (player.PositionX - 1 == pokeball.PositionX && player.PositionY == pokeball.PositionY) || (player.PositionX == pokeball.PositionX && player.PositionY - 1 == pokeball.PositionY) || (player.PositionX == pokeball.PositionX && player.PositionY + 1 == pokeball.PositionY))
            {
                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine(pokeball.name);
                    pokeball.Function(player);
                    entityToRemove.Add(pokeball);

                    if (pokeball.position == 1)
                    {
                        foreach (Entity entity in entityList)
                        {
                            if (entity is Pokeball pokeball2 && pokeball2.position == 2)
                            {
                                entityToRemove.Add(pokeball2);
                            }
                        }
                    }
                    else if (pokeball.position == 2)
                    {
                        foreach (Entity entity in entityList)
                        {
                            if (entity is Pokeball pokeball2 && pokeball2.position == 3)
                            {
                                entityToRemove.Add(pokeball2);
                            }
                        }
                    }
                    else if (pokeball.position == 3)
                    {
                        foreach (Entity entity in entityList)
                        {
                            if (entity is Pokeball pokeball2 && pokeball2.position == 1)
                            {
                                entityToRemove.Add(pokeball2);
                            }
                        }
                    }

                    Thread.Sleep(1000);
                    Functions.ClearInputBuffer();
                }

                DrawMap();
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

            Pokeball pokeball1;
            Pokeball pokeball2;
            Pokeball pokeball3;

            if (player.starterId == null)
            {
                pokeball1 = new Pokeball(1, filename, 8, 3, map[8, 3], 1);
                pokeball2 = new Pokeball(4, filename, 9, 3, map[9, 3], 2);
                pokeball3 = new Pokeball(7, filename, 10, 3, map[10, 3], 3);

                entityList.Add(pokeball1);
                entityList.Add(pokeball2);
                entityList.Add(pokeball3);
            }
            else if (player.starterId == 1)
            {
                pokeball3 = new Pokeball(7, filename, 10, 3, map[10, 3], 3);

                entityList.Add(pokeball3);
            }
            else if (player.starterId == 4)
            {
                pokeball1 = new Pokeball(1, filename, 8, 3, map[8, 3], 1);

                entityList.Add(pokeball1);
            }
            else if (player.starterId == 7)
            {
                pokeball2 = new Pokeball(4, filename, 9, 3, map[9, 3], 2);

                entityList.Add(pokeball2);
            }

            

        }
        else if (filename == "mom.txt")
        {
            Maman maman = new Maman();
            entityList.Add(maman);
        }
        else if (filename == "route_1.txt")
        {
            PotionMan potionMan = new PotionMan();
            entityList.Add(potionMan);
        }
    }


    public static void DrawMap()
    {
        Console.Clear();

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

        DrawEntity();
        DrawPlayer();
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

        if (map[x, y] == '#' && player.pokemonParty.Count == 0)
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


    private static void RemoveEntity()
    {
        foreach(Entity entity in entityToRemove) 
        {
            entityList.Remove(entity);
        }
        entityToRemove.Clear();

        DrawMap();
    }
}
