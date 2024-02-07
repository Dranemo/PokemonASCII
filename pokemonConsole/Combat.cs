using inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usefull;
using static System.Net.Mime.MediaTypeNames;

namespace pokemonConsole
{
    internal class Combat
    {
        private static string FightButton = " ATTAQ";
        private static string PokemonButton = " PKMN";
        private static string ItemButton = " OBJET";
        private static string RunButton = " FUITE";

        private static int positionX;
        private static int positionY;

        private static int positionAttack;

        private static List<string> firstLine = new List<string>();
        private static List<string> secondLine = new List<string>();
        private static List<List<string>> bothLines = new List<List<string>>();

        private static List<string> listAttack = new List<string>();

        private static int cursorLeft;
        private static int cursorTop;

        private static int pokemonWidth;

        public static void LoopCombat(Player player, List<Pokemon> pokemonPartyAdverse = null)
        {

            // Generer le pokemon adverse
            Random random = new Random();
            Pokemon pokemon = player.pokemonParty[0];

            // Generer un pokemon sauvage
            int pokemonAdverseId = random.Next(1, 152);
            int pokemonAdverseLevel = 0;
            while (!(pokemonAdverseLevel > 0 && pokemonAdverseLevel <= 100))
            {
                pokemonAdverseLevel = random.Next(pokemon.level - 2, pokemon.level + 3);
            }

            Console.WriteLine(pokemonAdverseId);
            Console.WriteLine(pokemonAdverseLevel);
            Pokemon pokemonAdverse = new Pokemon(pokemonAdverseId, pokemonAdverseLevel);
            Pokemon pokemonAdverse;
            if (pokemonPartyAdverse == null)
            { 
                pokemonPartyAdverse=new List<Pokemon>();
                pokemonAdverse = new Pokemon(pokemonAdverseId, pokemonAdverseLevel);
                pokemonPartyAdverse.Add(pokemonAdverse);
            }
            else
            {
                pokemonAdverse = pokemonPartyAdverse[0];
            }
            pokemonPartyAdverse.Add(pokemonAdverse);
            pokemon.AfficherCombat();
            Console.WriteLine();
            pokemonAdverse.AfficherCombat();
            pokemonAdverse.AfficherCombat();

            int nbFuite = 0;
            bool fuiteReussit = false;


            // Boutons Main
            firstLine.Add(FightButton);
            firstLine.Add(PokemonButton);
            secondLine.Add(ItemButton);
            secondLine.Add(RunButton);


            bool Selected = false;
            foreach (string item in firstLine)
            {
                if (item[0] == '>') Selected = true;
            }
            foreach (string item in secondLine)
            {
                if (item[0] == '>') Selected = true;
            }
            if (!Selected)
            {
                firstLine[0] = firstLine[0].Remove(0, 1);
                firstLine[0] = firstLine[0].Insert(0, ">");
                positionX = 0;
                positionY = 0;
            }

            bothLines.Add(firstLine);
            bothLines.Add(secondLine);

            // Boutons Atk
            string button = " ";
            foreach (Capacity atk in pokemon.listAttackActual)
            {
                listAttack.Add(button + atk.name);
            }

            Selected = false;
            foreach (string attack in listAttack)
            {
                if (button[0] == '>') Selected = true;
            }

            if (!Selected)
            {
                listAttack[0] = listAttack[0].Remove(0, 1);
                listAttack[0] = listAttack[0].Insert(0, ">");
                positionAttack = 0;
            }

            // Affichage 
            PrintPokemon(pokemon, pokemonAdverse);
            cursorLeft = Console.CursorLeft; cursorTop = Console.CursorTop;
            PrintMenuChoice();



            // Combat 
            ConsoleKeyInfo keyInfo;
            bool endChoice = false;
            while (!endChoice)
            {
                keyInfo = Console.ReadKey(true);

                switch (keyInfo.Key)
                {
                    case ConsoleKey.DownArrow:
                        if(positionY != bothLines[positionX].Count - 1)
                        {
                            SwitchSelectMain(ref positionX, ref positionY, 0, 1);
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        if (positionY != 0)
                        {
                            SwitchSelectMain(ref positionX, ref positionY, 0, -1);
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        if (positionX != bothLines.Count - 1)
                        {
                            SwitchSelectMain(ref positionX, ref positionY, +1, 0);
                        }
                        break;
                    case ConsoleKey.LeftArrow:
                        if (positionX != 0)
                        {
                            SwitchSelectMain(ref positionX, ref positionY, -1, 0);
                        }
                        break;
                    case ConsoleKey.Enter:
                        capacityUsed = LoopChoiceCap(pokemon);
                        endChoice = true;
                        break;
                }
            }






            while (!player.IsKO() && pokemonAdverse.pvLeft > 0 && !fuiteReussit)

            {
                tour++;
                // Demander à l'utilisateur d'entrer son action
                Console.WriteLine("Attaque");
                Console.WriteLine("Pokemon");
                Console.WriteLine("Sac");
                Console.WriteLine("Fuite");
                int choix = int.Parse(Console.ReadLine());
                Random randomFuite = new Random();
                List<Capacity> listAttackActual = pokemon.listAttackActual;
                
                Capacity.ApplyStatusEffects(pokemon);
                Capacity.ApplyStatusEffects(pokemonAdverse);
                switch (choix)
                {
                    case 1:
                        foreach (Capacity attaque in listAttackActual)
                        {
                            Console.WriteLine(attaque.name);
                        }
                        int choixAttaque = int.Parse(Console.ReadLine());
                        switch (choixAttaque)
                        {
                            case 1:
                                capacityUsed = listAttackActual[0];
                                capacityUsed.Use(pokemon, pokemonAdverse);
                                break;
                            case 2:
                                capacityUsed = listAttackActual[1];
                                capacityUsed.Use(pokemon, pokemonAdverse);
                                break;
                            case 3:
                                capacityUsed = listAttackActual[2];
                                capacityUsed.Use(pokemon, pokemonAdverse);
                                break;
                            case 4:
                                capacityUsed = listAttackActual[3];
                                capacityUsed.Use(pokemon, pokemonAdverse);
                                break;
                        }

                        if (capacityUsed != null && capacityUsed.categorie == 1)
                        {
                            pokemonAdverse.pvLeft -= (int)Math.Round(CalculerDegatSubitPokemon(pokemon, pokemonAdverse, capacityUsed));
                            Console.WriteLine(capacityUsed.name);
                        }

                        break;
                    case 2:
                        ;
                        break;
                    case 3:
                        inventory.Item.LoadItemsFromSaveFile($"{AdresseFile.FileDirection}\\SaveItemInGame.txt");

                        Console.WriteLine("\n1. Utiliser un objet");
                        Console.WriteLine("2. Retour");

                        int choixInventaire = int.Parse(Console.ReadLine());
                       

                        switch (choixInventaire)
                        {
                            case 1:
                                List<inventory.Item> items = inventory.Item.AllItems;
                                Console.WriteLine("\nListe des objets dans votre inventaire :\n");

                                // Affichez uniquement les objets avec une quantité supérieure à 0
                                foreach (var item in Item.AllItems.Where(i => i.Quantity > 0)
                                    .Select((value, index) => new { Index = index, Value = value }))
                                {
                                    Console.WriteLine($"Name: {item.Value.Name}, Quantity: {item.Value.Quantity}");
                                }

                                Console.WriteLine("Choisissez un objet de l'inventaire (numéro) ou 0 pour retourner : ");
                                string choixNomObjet = Console.ReadLine();

                                inventory.Item itemToUse = items.FirstOrDefault(i => i.Name.Equals(choixNomObjet, StringComparison.OrdinalIgnoreCase));

                                if (itemToUse != null && itemToUse.Quantity > 0)
                                {
                                    // Utiliser l'objet sélectionné
                                    Console.WriteLine($"Vous avez utilisé l'objet : {itemToUse.Name}");

                                    itemToUse.Quantity--;
                                    Console.WriteLine($"Nouvelle quantité de {itemToUse.Name} : {itemToUse.Quantity}\n");

                                    // Sauvegarder les quantités dans le fichier
                                    inventory.Item.SaveQuantitiesToFile($"{AdresseFile.FileDirection}\\SaveItemInGame.txt", inventory.Item.AllItems);

                                    capacityUsed = pokemonAdverse.listAttackActual[random.Next(0, pokemonAdverse.listAttackActual.Count)];

                                    if (capacityUsed.categorie > 0)
                                    {
                                        pokemon.pvLeft -= (int)Math.Round(CalculerDegatSubitPokemon(pokemonAdverse, pokemon, capacityUsed));
                                        Console.WriteLine(capacityUsed.name);
                                    }

                                    Console.WriteLine("Le Pokemon adverse vous inflige des dégâts !\n");
                                }
                                else if (choixNomObjet == "0")
                                {
                                }
                                else
                                {
                                    Console.WriteLine("Choix invalide.");
                                }
                                break;


                            case 2:
                                break;
                        }
                        break;

                    case 4:
                        nbFuite++;
                        int spdQuart = (int)Math.Floor(pokemonAdverse.spdCombat / 4.0);
                        int fuiteEuclidienne = (spdQuart % 255 == 0) ? 0 : 1;
                        int randomFuiteValue = randomFuite.Next(0, 256);
                        int fuite = (pokemon.spd * 32 / (spdQuart % 255)) + (30 * nbFuite);

                        if (fuite > 255 || randomFuiteValue < fuite || fuiteEuclidienne == 0)
                        {
                            fuiteReussit = true;
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Vous n'avez pas reussi à fuir le Pokemon adverse !\n");
                        }
                        break;
                }
                if (pokemonAdverse.appartenant==2)
                {
                    double degatsMax = double.MinValue;
                    Capacity meilleureAttaque = null;
                    int positionMeilleureAttaque = -1;
                    for (int i = 0; i < pokemonAdverse.listAttackActual.Count; i++)
                    {
                        Capacity attaque = pokemonAdverse.listAttackActual[i];
                        // Calcul des dégâts infligés par cette attaque
                        double degats = CalculerDegatSubitPokemon(pokemonAdverse, pokemon, attaque);

                        // Mise à jour de l'attaque choisie si les dégâts sont inférieurs au minimum
                        if (degats > degatsMax)
                        {
                            degatsMax = degats;
                            meilleureAttaque = attaque;
                            positionMeilleureAttaque = i; // Mémorisation de la position de l'attaque choisie
                        }
                    }

                    // Affectation de capacityUsed à l'attaque choisie
                    if (positionMeilleureAttaque != -1)
                    {
                        capacityUsed = pokemonAdverse.listAttackActual[positionMeilleureAttaque];
                        pokemon.pvLeft -= (int)Math.Round(CalculerDegatSubitPokemon(pokemonAdverse, pokemon, capacityUsed));
                        Console.WriteLine($"Meilleure capacite : {capacityUsed.name}");
                    }
                }
                else
                {
                    capacityUsed = pokemonAdverse.listAttackActual[random.Next(0, pokemonAdverse.listAttackActual.Count)];
                    pokemon.pvLeft -= (int)Math.Round(CalculerDegatSubitPokemon(pokemonAdverse, pokemon, capacityUsed));
                    Console.WriteLine(capacityUsed.name);
                }
                Console.WriteLine($"Les nouveaux PV du Pokemon du joueur sont = {pokemon.pvLeft}");
                Console.WriteLine($"Les nouveaux PV du Pokemon de l'adversaire sont = {pokemonAdverse.pvLeft}\n");
                if (pokemonPartyAdverse[pokemonEquipeAdverse].pvLeft<=0)
                {
                    if (pokemonEquipeAdverse + 1 < pokemonPartyAdverse.Count)
                    {
                        pokemonAdverse = pokemonPartyAdverse[pokemonEquipeAdverse + 1];
                        pokemonEquipeAdverse++;
                    }
                    float appartenant;
                    float echange = 1;
                    int nombrePokemon = 1; // Le nombre de pokemon qui ont combattu


                    if (pokemonAdverse.appartenant == 0)
                    {
                        appartenant = 1;
                    }
                    else
                    {
                        appartenant = 1.5f;
                    }

                    float expWon = (appartenant * echange * pokemonAdverse.expDonne * pokemonAdverse.level) / 7 * nombrePokemon;
                    pokemon.GainExp((int)Math.Round(expWon));
                    pokemon.GainEV(pokemonAdverse.listPv[0], pokemonAdverse.listAtk[0], pokemonAdverse.listDef[0], pokemonAdverse.listSpe[0], pokemonAdverse.listSpd[0]);
                }
                if (VerifAdverse(pokemonPartyAdverse))
                {
                    Console.WriteLine("L'adversaire a perdu !");
                }
                if (fuiteReussit)
                {
                    Console.WriteLine("Vous avez reussi à fuir le combat");
                }

                
            }
        }
        private static Capacity LoopChoiceCap(Pokemon pokemon)
        {
            PrintMenuAttack(pokemon);
            PrintPPAttack(pokemon);

            ConsoleKeyInfo keyInfo;
            bool endChoice = false;
            while (!endChoice)
            {
                keyInfo = Console.ReadKey(true);

                switch (keyInfo.Key)
                {
                    case ConsoleKey.DownArrow:
                        if(positionAttack < listAttack.Count - 1)
                        {
                            SwitchSelectAttack(ref positionAttack, 1);
                            PrintPPAttack(pokemon);
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        if (positionAttack > 0)
                        {
                            SwitchSelectAttack(ref positionAttack, -1);
                            PrintPPAttack(pokemon);
                        }
                        break;
                    case ConsoleKey.Enter:
                        return pokemon.listAttackActual[positionAttack];
                }
            }
            return new Capacity(165);
        }


        static double CalculerDegatSubitPokemon(Pokemon pokemon, Pokemon pokemonAdverse, Capacity capacity)
        {
            // Degâts infliges = (((((((Niveau × 2 ÷ 5) +2) × Puissance × Att[Spe] ÷ 50) ÷ Def[Spe]) × Mod1) +2) × CC × Mod2 × R ÷ 100) × STAB × Type1 × Type2 × Mod3

            Random random = new Random();

            int atkSpeOrNot = 0;
            int defSpeOrNot = 0;
            float isBurn = 1;
            float critChance = 1;
            float critDamage = 1;
            float randomMod = (random.Next(217, 256) * 100) / 255;
            int stab = 1;

            float efficaciteType1 = TypeModifier.CalculerMultiplicateur(capacity.type, pokemonAdverse.listType[0]);
            float efficaciteType2 = 1;

            if (pokemonAdverse.listType.Count > 1)
            {
                efficaciteType2 = TypeModifier.CalculerMultiplicateur(capacity.type, pokemonAdverse.listType[1]);
            }


            // Determine si la capacite est physique ou special selon le type
            if (capacity.type == "DRAGON" || capacity.type == "EAU" || capacity.type == "ELECTRIK" || capacity.type == "FEU" || capacity.type == "GLACE" || capacity.type == "PLANTE" || capacity.type == "PSY")
            {
                atkSpeOrNot = pokemon.speCombat;
                defSpeOrNot = pokemonAdverse.speCombat;
            }
            else
            {
                atkSpeOrNot = pokemon.atkCombat;
                defSpeOrNot = pokemonAdverse.defCombat;
            }

            // Si le Pokemon est burn, l'attaque est divisee par deux
            if (pokemon.statusProblem == "BRN")
            {
                isBurn = isBurn * 0.5f;
            }

            // Critique
            critChance = ((int)Math.Round(pokemon.spdCombat / 2.0) * 2) / 256;
            if (critChance == 0)
            {
                critDamage = 1;
            }
            else
            {
                critDamage = 2;
            }
            foreach (string typePokemon in pokemon.listType)
            {
                if (capacity.type == typePokemon)
                {
                    stab = 2;
                }
            }


            if (efficaciteType1 * efficaciteType2 > 1)
            {
                Console.WriteLine("C'est super efficace !");
            }
            else if (efficaciteType1 * efficaciteType2 < 1 && efficaciteType1 * efficaciteType2 != 0)
            {
                Console.WriteLine("C'est pas tres efficace !");
            }
            else if (efficaciteType1 * efficaciteType2 == 0)
            {
                Console.WriteLine("Ca n'a pas d'effet");
            }

            double damageDone = (((((((pokemon.level * 2 / 5) + 2) * capacity.puissance * atkSpeOrNot / 50) / defSpeOrNot) * isBurn) + 2) * critDamage * randomMod / 100) * stab * efficaciteType1 * efficaciteType2;
            return damageDone;
        }







        static private void PrintPokemon(Pokemon pokemon, Pokemon pokemonAdverse)
        {
            // Pokemon Adverse
            pokemonWidth = pokemonAdverse.width;
            int offsetPokemon = pokemonWidth - 11;

            Console.Clear();
            Console.ForegroundColor = pokemonAdverse.color;
            Console.WriteLine(pokemonAdverse.asciiArt);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();

            Console.SetCursorPosition(Console.CursorLeft + 1, Console.CursorTop);
            Console.WriteLine(pokemonAdverse.name);
            Console.SetCursorPosition(Console.CursorLeft + 1 + 3, Console.CursorTop);
            if (pokemonAdverse.level.ToString().Length < 3)
            {
                Console.Write("L");
            }
            Console.WriteLine(pokemonAdverse.level);
            Console.SetCursorPosition(Console.CursorLeft + 1, Console.CursorTop);
            Console.Write("|");

            PrintPvBar(pokemonAdverse, true);
            Console.SetCursorPosition(Console.CursorLeft + 1, Console.CursorTop);
            Console.WriteLine("0----------");



            // Pokemon
            Console.SetCursorPosition(Console.CursorLeft + offsetPokemon, Console.CursorTop + 3);
            for (int i = 1; i < pokemon.name.Length; i++)
            {
                Console.Write(' ');
            }
            Console.WriteLine(pokemon.name);
            Console.SetCursorPosition(Console.CursorLeft + offsetPokemon + 5, Console.CursorTop);
            if (pokemon.level.ToString().Length < 3)
            {
                Console.Write("L");
            }
            Console.WriteLine(pokemon.level);
            Console.SetCursorPosition(Console.CursorLeft + offsetPokemon + 2, Console.CursorTop);

            PrintPvBar(pokemon);
            Console.SetCursorPosition(Console.CursorLeft + offsetPokemon + 1, Console.CursorTop);
            Console.WriteLine("---------0");
        }
        static private void PrintPvBar(Pokemon pokemon, bool pokemonAdverse = false)
        {
            int offsetPokemon = pokemonWidth - 11;

            int pvPerSix = pokemon.pvLeft * 6 / pokemon.pv;
            string barPv = "PV";
            for (int i = 0; i < pvPerSix; i++)
            {
                barPv += "=";
            }
            for (int i = barPv.Length; i < 8; i++)
            {
                barPv += "*";
            }
            foreach (char c in barPv)
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

            if (!pokemonAdverse)
            {
                Console.WriteLine('|');
                Console.SetCursorPosition(Console.CursorLeft + offsetPokemon + 3, Console.CursorTop);
                for (int j = 3; j > pokemon.pvLeft.ToString().Length; j--)
                {
                    Console.Write(" ");
                }
                Console.Write(pokemon.pvLeft + "/");
                for (int j = 3; j > pokemon.pv.ToString().Length; j--)
                {
                    Console.Write(" ");
                }
                Console.Write(pokemon.pv + "|");
            }

            Console.WriteLine();
            
        }
        static private void PrintMenuChoice()
        {
            int offset = pokemonWidth - 16;

            // Clear
            for (int i = 4; i > 0; i--)
            {
                Console.SetCursorPosition(cursorLeft, cursorTop - i);
            }
            Console.SetCursorPosition(cursorLeft, cursorTop);


            // Variables
            string topBox =    "0--------------0";
            string middleBox = "|              |";
            string topWholeBox = "0";
            for (int i = 1; i < offset; i++)
            {
                topWholeBox += "-";
            }
            string middleWholeBox = "|";
            for (int i = 1; i < offset; i++)
            {
                middleWholeBox += " ";
            }

            //Print
            Console.WriteLine(topWholeBox + topBox);
            for (int i = 0;i < 4; i++)
            {
                Console.WriteLine(middleWholeBox + middleBox);
            }
            Console.WriteLine(topWholeBox + topBox);

            

            // Print 2

            Console.SetCursorPosition(cursorLeft + offset + 2, cursorTop + 2);
            Console.Write(firstLine[0]);
            Console.SetCursorPosition(Console.CursorLeft +1, Console.CursorTop);
            Console.WriteLine(firstLine[1]);

            Console.WriteLine();

            Console.SetCursorPosition(Console.CursorLeft + offset + 2, Console.CursorTop);
            Console.Write(secondLine[0]);
            Console.SetCursorPosition(Console.CursorLeft + 1, Console.CursorTop);
            Console.Write(secondLine[1]);

        }
        static private void PrintMenuAttack(Pokemon pokemon)
        {
            int offset = pokemonWidth - 17;


            // Variables
            string topBox = "0---------------0";
            string middleBox = "|               |";


            string topWholeBox = "0";
            for (int i = 1; i < offset; i++)
            {
                topWholeBox += "-";
            }
            string middleWholeBox = "|";
            for (int i = 1; i < offset; i++)
            {
                middleWholeBox += " ";
            }

            //Print
            Console.SetCursorPosition(cursorLeft, cursorTop);
            Console.WriteLine(topWholeBox + topBox);
            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine(middleWholeBox + middleBox);
            }
            Console.WriteLine(topWholeBox + topBox);

            

            // Print 2
            Console.SetCursorPosition(cursorLeft, cursorTop + 1);
            foreach (string str in listAttack)
            {
                Console.SetCursorPosition(Console.CursorLeft + offset + 2, Console.CursorTop);
                Console.WriteLine(str);
            }
        }
        static private void PrintPPAttack(Pokemon pokemon)
        {
            string topBox = "0---------0";
            string MiddleBox = "|         |";

            Console.SetCursorPosition(cursorLeft, cursorTop - 4);
            Console.WriteLine(topBox);
            for (int i = 0; i < 3; i++) 
            {
                Console.WriteLine(MiddleBox);
            }
            Console.WriteLine(topBox);

            Console.SetCursorPosition(cursorLeft+1, cursorTop - 3);
            Console.WriteLine("TYPE/");

            Console.SetCursorPosition(Console.CursorLeft + 2, Console.CursorTop);
            Console.WriteLine(pokemon.listAttackActual[positionAttack].type);

            Console.SetCursorPosition(Console.CursorLeft + 3, Console.CursorTop);
            // Print pp

            Console.SetCursorPosition(cursorLeft, cursorTop + 6);
        }


        static private void SwitchSelectMain(ref int x, ref int y, int nextX, int nextY)
        {
            int offset = pokemonWidth - 16;

            bothLines[y][x] = bothLines[y][x].Remove(0, 1);
            bothLines[y][x] = bothLines[y][x].Insert(0, " ");

            Console.SetCursorPosition(cursorLeft + offset + 2 + x * 7, cursorTop + 2 + y * 2);
            Console.Write(bothLines[y][x][0]);

            x += nextX;
            y += nextY;

            bothLines[y][x] = bothLines[y][x].Remove(0, 1);
            bothLines[y][x] = bothLines[y][x].Insert(0, ">");

            Console.SetCursorPosition(cursorLeft + offset + 2 + x * 7, cursorTop + 2 + y * 2);
            Console.Write(bothLines[y][x][0]);
            Console.SetCursorPosition(cursorLeft, cursorTop + 6);
        }
        static private void SwitchSelectAttack(ref int x, int nextX)
        {
            int offset = pokemonWidth - 17;

            listAttack[x] = listAttack[x].Remove(0, 1);
            listAttack[x] = listAttack[x].Insert(0, " ");

            Console.SetCursorPosition(cursorLeft + offset + 2, cursorTop + 1 + x);
            Console.Write(listAttack[x][0]);

            x += nextX;

            listAttack[x] = listAttack[x].Remove(0, 1);
            listAttack[x] = listAttack[x].Insert(0, ">");

            Console.SetCursorPosition(cursorLeft + offset + 2, cursorTop + 1 + x);
            Console.Write(listAttack[x][0]);
            Console.SetCursorPosition(cursorLeft, cursorTop + 6);
        }


        public class TypeModifier
        {
            public static float CalculerMultiplicateur(string typePokemon, string typeAdverse)
            {
                Dictionary<string, Dictionary<string, float>> multiplicateurs = new Dictionary<string, Dictionary<string, float>>()
    {
        {"NORMAL", new Dictionary<string, float>() {{"ROCHE", 0.5f}, {"SPECTRE", 0f}, {"default", 1f}}},
        {"FEU", new Dictionary<string, float>() {{"FEU", 0.5f}, {"EAU", 0.5f}, {"ROCHE", 0.5f}, {"DRAGON", 0.5f}, {"PLANTE", 2f}, {"GLACE", 2f}, {"INSECTE", 2f}, {"default", 1f}}},
        {"EAU", new Dictionary<string, float>() {{"EAU", 0.5f}, {"PLANTE", 0.5f}, {"DRAGON", 0.5f}, {"FEU", 2f}, {"SOL", 2f}, {"ROCHE", 2f}, {"default", 1f}}},
        {"PLANTE", new Dictionary<string, float>() {{"FEU", 0.5f}, {"PLANTE", 0.5f}, {"POISON", 0.5f}, { "VOL", 0.5f }, { "INSECTE", 0.5f }, { "DRAGON", 0.5f }, { "EAU", 2f}, {"SOL", 2f}, {"ROCHE", 2f}, {"default", 1f}}},
        {"ELECTRIK", new Dictionary<string, float>() {{"PLANTE", 0.5f}, {"ELECTRIK", 0.5f}, {"DRAGON", 0.5f}, {"EAU", 2f}, {"VOL", 2f}, {"SOL", 0f}, {"default", 1f}}},
        {"GLACE", new Dictionary<string, float>() {{"EAU", 0.5f}, {"GLACE", 0.5f}, {"PLANTE", 2f}, {"SOL", 2f}, {"VOL", 2f}, {"DRAGON", 2f}, {"default", 1f}}},
        {"COMBAT", new Dictionary<string, float>() {{"POISON", 0.5f}, {"VOL", 0.5f}, {"PSY", 0.5f}, {"INSECTE", 0.5f}, {"NORMAL", 2f}, {"GLACE", 2f}, { "ROCHE", 2f }, { "SPECTRE", 0f }, { "default", 1f}}},
        {"POISON", new Dictionary<string, float>() {{"POISON", 0.5f}, {"SOL", 0.5f}, {"ROCHE", 0.5f}, {"SPECTRE", 0.5f}, {"PLANTE", 2f}, {"INSECTE", 2f}, {"default", 1f}}},
        {"SOL", new Dictionary<string, float>() {{"PLANTE", 0.5f}, {"INSECTE", 0.5f}, {"FEU", 2f}, {"ELECTRIK", 2f}, {"POISON", 2f}, {"ROCHE", 2f}, { "VOL", 0f }, { "default", 1f}}},
        {"VOL", new Dictionary<string, float>() {{"ELECTRIK", 0.5f}, {"ROCHE", 0.5f}, {"PLANTE", 2f}, {"COMBAT", 2f}, {"INSECTE", 2f}, {"default", 1f}}},
        {"PSY", new Dictionary<string, float>() {{"PSY", 0.5f}, {"COMBAT", 2f}, {"POISON", 2f}, {"default", 1f}}},
        {"INSECTE", new Dictionary<string, float>() {{"FEU", 0.5f}, { "COMBAT", 0.5f }, { "VOL", 0.5f }, { "SPECTRE", 0.5f }, { "PLANTE", 2f}, {"POISON", 2f}, { "PSY", 2f }, { "default", 1f}}},
        {"ROCHE", new Dictionary<string, float>() {{"COMBAT", 0.5f}, {"SOL", 0.5f}, {"FEU", 2f}, { "GLACE", 2f }, { "VOL", 2f }, { "INSECTE", 2f }, { "default", 1f}}},
        {"SPECTRE", new Dictionary<string, float>() {{"SPECTRE", 2f}, {"NORMAL", 0f}, { "PSY", 0f }, { "default", 1f}}},
        {"DRAGON", new Dictionary<string, float>() {{"DRAGON", 2f}, { "default", 1f}}},
    };

                if (multiplicateurs.ContainsKey(typePokemon))
                {
                    if (multiplicateurs[typePokemon].ContainsKey(typeAdverse))
                    {
                        return multiplicateurs[typePokemon][typeAdverse];
                    }
                    else
                    {
                        return multiplicateurs[typePokemon]["default"];
                    }
                }
                else
                {
                    return 1f;
                }

            }
        }
        public static bool VerifAdverse(List<Pokemon>pokemonPartyAdverse) 
        {
            if (pokemonPartyAdverse.Count == 0)
            {
                return true;
            }

            foreach (var pokemon in pokemonPartyAdverse)
            {
                if (pokemon.pvLeft > 0)
                {
                    return false; 
                }
            }

            return true;
        }
    }


}

