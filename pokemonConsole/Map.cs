using System;

internal class Map
{
    static int playerX = 6;
    static int playerY = 34;
    static char[,] map;
    static Random random = new Random();
    static string currentMapFileName="";
    public static void MapPlayer()
    {
        LoadMap("C:\\Users\\mguellaff\\Desktop\\C-Pokemon\\pokemonConsole\\Assets\\Maps\\route_1.txt");

        ConsoleKeyInfo keyInfo;

        do
        {
            Console.Clear();
            DrawMap();
            DrawPlayer();

            keyInfo = Console.ReadKey();

            // Déplacer le joueur en fonction de la touche pressée
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

            // Vérifier si la nouvelle position est marchable
            int newX = playerX + deltaX;
            int newY = playerY + deltaY;

            if (IsInsideMap(newX, newY) && IsWalkable(newX, newY))
            {
                Console.SetCursorPosition(playerX, playerY);
                Console.Write(map[playerX, playerY]);

                playerX = newX;
                playerY = newY;

                Console.SetCursorPosition(playerX, playerY);
                Console.Write("P");

                // Vérifier si le joueur atteint le point le plus bas de la carte "route_1.txt"
                if (IsCurrentMap("route_1.txt") && newY == 35)
                {
                    Console.WriteLine("Vous arrivez à Bourg Palette !");
                    Thread.Sleep(1000);
                    LoadMap("C:\\Users\\mguellaff\\Desktop\\C-Pokemon\\pokemonConsole\\Assets\\Maps\\bourg_palette.txt");
                    playerX += 3;
                    playerY = 0;
                }
                else if (IsCurrentMap("bourg_palette.txt") && newY == 0)
                {
                    Console.WriteLine("\nVers la route 1 !");
                    Thread.Sleep(1000);
                    LoadMap("C:\\Users\\mguellaff\\Desktop\\C-Pokemon\\pokemonConsole\\Assets\\Maps\\route_1.txt");
                    playerX -= 3;
                    playerY = 35;
                }

                // Si le joueur marche sur un '#', chance aléatoire de lancer un combat
                if (map[playerX, playerY] == '#')
                {
                    if (random.Next(1, 101) <= 6) // 25% de chance de lancer un combat (ajustez selon vos besoins)
                    {
                        Console.WriteLine($"\nCombat lancé !");
                        Thread.Sleep(1000);
                    }
                }
            }

        } while (keyInfo.Key != ConsoleKey.Escape);
    }

    static void LoadMap(string filename)
    {
        currentMapFileName = filename;
        string[] lines = File.ReadAllLines(filename);

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

    static void DrawMap()
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
                else if (map[x, y] == '0')
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
    static bool IsCurrentMap(string currentMapFileName)
    {
        return currentMapFileName.Equals(currentMapFileName, StringComparison.OrdinalIgnoreCase);
    }
    static void DrawPlayer()
    {
        Console.SetCursorPosition(playerX, playerY);
        Console.Write("P");
    }

    static void MovePlayer(int deltaX, int deltaY)
    {
        // Vérifier les limites du déplacement pour éviter de sortir de la carte
        int newX = playerX + deltaX;
        int newY = playerY + deltaY;

        if (IsInsideMap(newX, newY))
        {
            // Effacer l'ancienne position du joueur
            Console.SetCursorPosition(playerX, playerY);
            Console.Write(" ");

            // Mettre à jour la position du joueur
            playerX = newX;
            playerY = newY;
        }
    }

    static bool IsInsideMap(int x, int y)
    {
        return x >= 0 && x < map.GetLength(0) && y >= 0 && y < map.GetLength(1);
    }

    static bool IsWalkable(int x, int y)
    {
        return map[x, y] == ' ' || map[x, y] == '#';
    }
}
