using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using Usefull;

namespace pokemonConsole
{
    internal class MainMenu
    {
        private static string newGame = "> NOUVELLE PARTIE <";
        private static string loadGame = "CHARGER ";
        private static bool canLoadGame = false;
        private static string quitGame = "QUITTER ";
        private static string logoMainMenuPokemon = "";
        private static string logoMainMenuAscii = "";
        private static string pathLogoFile = "";

        private static string baseDirectory = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);


        static public void Start()
        {
            string soundFilePath = "main_menu.wav";
            GlobalSettings.projectRoot = Path.GetFullPath(Path.Combine(baseDirectory, @"..\..\..\")+@"\GameFiles\"); // Disable this if you want to do a release
            //GlobalSettings.projectRoot = Path.GetFullPath("GameFiles/"); // Enable this if you want to do a release
            Functions.playSound(soundFilePath);
            Player player = new Player();
            Rival rival = new Rival();

            pathLogoFile = Path.Combine(GlobalSettings.projectRoot, "Assets", "mainMenuLogo.txt");
            using (StreamReader reader = new StreamReader(pathLogoFile))
            {
                for (int i = 0; i < 7; i++)
                {
                    logoMainMenuPokemon += reader.ReadLine() + Environment.NewLine;
                }

                // Lire les lignes restantes dans un autre string
                logoMainMenuAscii = reader.ReadToEnd();




                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(logoMainMenuPokemon);
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write(logoMainMenuAscii);
                Console.ForegroundColor = ConsoleColor.White;

                Console.ReadLine();
                PrintMainMenu();


                ConsoleKeyInfo keyInfo;


                bool choiceDone = false;
                while (!choiceDone)
                {
                    keyInfo = Console.ReadKey(true);

                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.DownArrow:
                            if (newGame[0] == '>')
                            {
                                newGame = newGame.Substring(2);
                                newGame = newGame.Substring(0, newGame.Length - 2);

                                if (canLoadGame)
                                {
                                    loadGame = "> " + loadGame + " <";
                                }
                                else
                                {
                                    quitGame = "> " + quitGame + " <";
                                }
                            }
                            else if (canLoadGame && loadGame[0] == '>')
                            {
                                loadGame = loadGame.Substring(2);
                                loadGame = loadGame.Substring(0, loadGame.Length - 2);
                                quitGame = "> " + quitGame + " <";
                            }
                            break;

                        case ConsoleKey.UpArrow:
                            if (quitGame[0] == '>')
                            {
                                quitGame = quitGame.Substring(2);
                                quitGame = quitGame.Substring(0, quitGame.Length - 2);

                                if (canLoadGame)
                                {
                                    loadGame = "> " + loadGame + " <";
                                }
                                else
                                {
                                    newGame = "> " + newGame + " <";
                                }
                            }
                            else if (canLoadGame && loadGame[0] == '>')
                            {
                                loadGame = loadGame.Substring(2);
                                loadGame = loadGame.Substring(0, loadGame.Length - 2);
                                newGame = "> " + newGame + " <";
                            }
                            break;

                        case ConsoleKey.Enter:
                            if (newGame[0] == '>')
                            {
                                player = new Player();

                                Intro.LaunchIntro(player, rival);
                                player.addItemToInventory(1, 15);
                                player.addItemToInventory(5, 5);
                                player.addItemToInventory(31, 50);
                                Map.MapPlayer(player, rival);

                            }
                            else if (canLoadGame && loadGame[0] == '>')
                            {
                                Save.Loading(player, rival);
                                Map.MapPlayer(player, rival);
                            }
                            else
                            {
                                Environment.Exit(0);
                            }

                            break;
                    }

                    PrintMainMenu();
                }

            }
        }
        

        private static void PrintMainMenu()
        {
            Console.Clear();

            Console.WriteLine(newGame);

            if (File.Exists(GlobalSettings.projectRoot + "save.txt"))
            {
                FileInfo fileInfo = new FileInfo(GlobalSettings.projectRoot + "save.txt");
              
                if (fileInfo.Length != 0)
                {
                    Console.WriteLine(loadGame);
                    canLoadGame = true;
                }
            }
            
            Console.WriteLine(quitGame);
        }
    }
}
