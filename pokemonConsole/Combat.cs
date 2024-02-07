using inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usefull;

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

        private static List<string> firstLine = new List<string>();
        private static List<string> secondLine = new List<string>();

        private static List<string> 



        private static int pokemonWidth;

        public static void LoopCombat(Player player)
        {

            // Generer le pokemon adverse
            Random random = new Random();
            Pokemon pokemon = player.pokemonParty[0];

            int pokemonAdverseId = random.Next(1, 152);
            int pokemonAdverseLevel = 0;
            while (!(pokemonAdverseLevel > 0 && pokemonAdverseLevel <= 100))
            {
                pokemonAdverseLevel = random.Next(pokemon.level - 2, pokemon.level + 3);
            }

            Pokemon pokemonAdverse = new Pokemon(pokemonAdverseId, pokemonAdverseLevel);


            int nbFuite = 0;
            bool fuiteReussit = false;
            Capacity capacityUsed = null;

            // --------------------- Affichage ---------------------//

            PrintPokemon(pokemon, pokemonAdverse);
            PrintMenuChoice();









            while (!player.IsKO() && pokemonAdverse.pvLeft > 0 && !fuiteReussit)
            {
                // Demander à l'utilisateur d'entrer son action
                Console.WriteLine("Attaque");
                Console.WriteLine("Pokemon");
                Console.WriteLine("Sac");
                Console.WriteLine("Fuite");
                int choix = int.Parse(Console.ReadLine());
                Random randomFuite = new Random();
                int PvRestantPokemon = pokemon.pvLeft;
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

                        int PvRestantPokemonAdverse = pokemonAdverse.pvLeft;
                        if (capacityUsed != null && capacityUsed.categorie == 1)
                        {
                            PvRestantPokemonAdverse -= (int)Math.Round(CalculerDegatSubitPokemon(pokemon, pokemonAdverse, capacityUsed));
                            Console.WriteLine(capacityUsed.name);
                        }

                        capacityUsed = pokemonAdverse.listAttackActual[random.Next(0, pokemonAdverse.listAttackActual.Count)];
                        if (capacityUsed.categorie == 1)
                        {
                            PvRestantPokemon -= (int)Math.Round(CalculerDegatSubitPokemon(pokemonAdverse, pokemon, capacityUsed));
                            Console.WriteLine(capacityUsed.name);
                        }
                        else
                        {
                            Console.WriteLine(capacityUsed.name);
                            capacityUsed.Use(pokemonAdverse, pokemon);
                        }

                        pokemon.pvLeft = (int)PvRestantPokemon;
                        pokemonAdverse.pvLeft = PvRestantPokemonAdverse;
                        Console.WriteLine($"Les nouveaux PV du Pokemon du joueur sont = {pokemon.pvLeft}");
                        Console.WriteLine($"Les nouveaux PV du Pokemon de l'adversaire sont = {pokemonAdverse.pvLeft}\n");
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
                                    Console.WriteLine($"Les nouveaux PV du Pokemon du joueur sont = {pokemon.pvLeft}");
                                    Console.WriteLine($"Les nouveaux PV du Pokemon de l'adversaire sont = {pokemonAdverse.pvLeft}\n");
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
                            capacityUsed = pokemonAdverse.listAttackActual[random.Next(0, pokemonAdverse.listAttackActual.Count)];

                            if (capacityUsed.categorie > 0)
                            {
                                pokemon.pvLeft -= (int)Math.Round(CalculerDegatSubitPokemon(pokemonAdverse, pokemon, capacityUsed));
                                Console.WriteLine(capacityUsed.name);
                            }

                            Console.WriteLine("Vous n'avez pas reussi à fuir le Pokemon adverse !\n");
                            Console.WriteLine($"Les nouveaux PV du Pokemon du joueur sont = {pokemon.pvLeft}");
                            Console.WriteLine($"Les nouveaux PV du Pokemon de l'adversaire sont = {pokemonAdverse.pvLeft}\n");
                        }
                        break;
                }
                if (pokemonAdverse.pvLeft <= 0)
                {
                    Console.WriteLine("Le Pokemon de l'adversaire a perdu !");
                    float appartenant ;
                    float echange = 1; // echange = 1.5x
                    int nombrePokemon = 1; // Le nombre de pokemo qui ont combattu


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
                if (fuiteReussit)
                {
                    Console.WriteLine("Vous avez reussi à fuir le combat");
                }

                
            }
        }



        string topBoxAttackChoicePP = "0---------0";
        string middleBoxAttackChoicePP = "|         |";

        string middleBoxAttackChoice = "|               |";


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

            int cursorLeft = Console.CursorLeft;
            int cursorTop = Console.CursorTop; 

            //Print
            Console.WriteLine(topWholeBox + topBox);
            for (int i = 0;i < 2; i++)
            {
                Console.WriteLine(middleWholeBox + middleBox);
            }
            Console.WriteLine(topWholeBox + topBox);

            // Boutons
            firstLine.Add(FightButton);
            firstLine.Add(PokemonButton);
            secondLine.Add(ItemButton);
            secondLine.Add(RunButton);


            bool Selected = false;
            foreach(string button in firstLine)
            {
                if (button[0] == '>') Selected = true;
            }
            foreach(string button in secondLine)
            {
                if (button[0] == '>') Selected = true;
            }

            if (!Selected) 
            {
                firstLine[0] = firstLine[0].Remove(0, 1);
                firstLine[0] = firstLine[0].Insert(0, ">");
                positionX = 0;
                positionY = 0;
            }

            // Print 2
            Console.SetCursorPosition(cursorLeft + offset + 2, cursorTop + 1);
            Console.Write(firstLine[0]);
            Console.SetCursorPosition(Console.CursorLeft +1, Console.CursorTop);
            Console.WriteLine(firstLine[1]);

            Console.SetCursorPosition(Console.CursorLeft + offset + 2, Console.CursorTop);
            Console.Write(secondLine[0]);
            Console.SetCursorPosition(Console.CursorLeft + 1, Console.CursorTop);
            Console.WriteLine(secondLine[1]);

        }
        static private void PrintMenuAttack(Pokemon pokemon)
        {
            int offset = pokemonWidth - 17;


            // Variables
            string topBoxPP = "0---------0";
            string middlePP = "|         |";
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

            int cursorLeft = Console.CursorLeft;
            int cursorTop = Console.CursorTop;

            //Print
            Console.WriteLine(topWholeBox + topBox);
            for (int i = 0; i < 2; i++)
            {
                Console.WriteLine(middleWholeBox + middleBox);
            }
            Console.WriteLine(topWholeBox + topBox);

            // Boutons
            string button = " ";
            foreach (Capacity atk in pokemon.listAttackActual)
            {

            }

            firstLine.Add(FightButton);
            firstLine.Add(PokemonButton);
            secondLine.Add(ItemButton);
            secondLine.Add(RunButton);


            bool Selected = false;
            foreach (string button in firstLine)
            {
                if (button[0] == '>') Selected = true;
            }
            foreach (string button in secondLine)
            {
                if (button[0] == '>') Selected = true;
            }

            if (!Selected)
            {
                firstLine[0] = firstLine[0].Remove(0, 1);
                firstLine[0] = firstLine[0].Insert(0, ">");
                positionX = 0;
                positionY = 0;
            }
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
    }


}

