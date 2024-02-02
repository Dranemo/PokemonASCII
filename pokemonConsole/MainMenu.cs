using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pokemonConsole
{
    internal class MainMenu
    {
        public string newGame { get; set; } = "> NOUVELLE PARTIE <";
        public string loadGame { get; set; } = "CHARGER ";
        public bool canLoadGame { get; set; } = false;
        public string quitGame { get; set; } = "QUITTER ";
        public string logoMainMenuPokemon { get; set; } = "";
        public string logoMainMenuAscii { get; set; } = "";
        public string pathLogoFile { get; private set; } = "C:\\Users\\agathelier\\Desktop\\Nouveau dossier\\pokemonConsole\\Assets\\mainMenuLogo.txt";



        static public void Start()
        {
            MainMenu mainMenu = new MainMenu();

            using (StreamReader reader = new StreamReader(mainMenu.pathLogoFile))
            {
                for (int i = 0; i < 7; i++)
                {
                    mainMenu.logoMainMenuPokemon += reader.ReadLine() + Environment.NewLine;
                }

                // Lire les lignes restantes dans un autre string
                mainMenu.logoMainMenuAscii = reader.ReadToEnd();


                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(mainMenu.logoMainMenuPokemon);
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write(mainMenu.logoMainMenuAscii);
                Console.ForegroundColor = ConsoleColor.White;

                Console.ReadLine();
                mainMenu.PrintMainMenu();


                ConsoleKeyInfo keyInfo;


                bool choiceDone = false;
                while (!choiceDone)
                {
                    keyInfo = Console.ReadKey();

                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.DownArrow:
                            if (mainMenu.newGame[0] == '>')
                            {
                                mainMenu.newGame = mainMenu.newGame.Substring(2);
                                mainMenu.newGame = mainMenu.newGame.Substring(0, mainMenu.newGame.Length - 2);

                                if (mainMenu.canLoadGame)
                                {
                                    mainMenu.loadGame = "> " + mainMenu.loadGame + " <";
                                }
                                else
                                {
                                    mainMenu.quitGame = "> " + mainMenu.quitGame + " <";
                                }
                            }
                            else if (mainMenu.canLoadGame && mainMenu.loadGame[0] == '>')
                            {
                                mainMenu.loadGame = mainMenu.loadGame.Substring(2);
                                mainMenu.loadGame = mainMenu.loadGame.Substring(0, mainMenu.loadGame.Length - 2);
                                mainMenu.quitGame = "> " + mainMenu.quitGame + " <";
                            }
                            break;

                        case ConsoleKey.UpArrow:
                            if (mainMenu.quitGame[0] == '>')
                            {
                                mainMenu.quitGame = mainMenu.quitGame.Substring(2);
                                mainMenu.quitGame = mainMenu.quitGame.Substring(0, mainMenu.quitGame.Length - 2);

                                if (mainMenu.canLoadGame)
                                {
                                    mainMenu.loadGame = "> " + mainMenu.loadGame + " <";
                                }
                                else
                                {
                                    mainMenu.newGame = "> " + mainMenu.newGame + " <";
                                }
                            }
                            else if (mainMenu.canLoadGame && mainMenu.loadGame[0] == '>')
                            {
                                mainMenu.loadGame = mainMenu.loadGame.Substring(2);
                                mainMenu.loadGame = mainMenu.loadGame.Substring(0, mainMenu.loadGame.Length - 2);
                                mainMenu.newGame = "> " + mainMenu.newGame + " <";
                            }
                            break;

                        case ConsoleKey.Enter:
                            if (mainMenu.newGame[0] == '>')
                            {
                                /*Intro.LaunchIntro();*/
                                Map.MapPlayer();
                            }
                            else if (mainMenu.canLoadGame && mainMenu.loadGame[0] == '>')
                            {
                                Console.WriteLine("haha");
                            }
                            else
                            {
                                Environment.Exit(0);
                            }

                            break;
                    }

                    mainMenu.PrintMainMenu();
                }

            }
        }


        private void PrintMainMenu()
        {
            Console.Clear();

            Console.WriteLine(newGame);
            if (File.Exists("C:\\Users\\mguellaff\\Desktop\\C-Pokemon\\pokemonConsole\\save.txt"))
            {
                FileInfo fileInfo = new FileInfo("C:\\Users\\mguellaff\\Desktop\\C-Pokemon\\pokemonConsole\\save.txt");
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
