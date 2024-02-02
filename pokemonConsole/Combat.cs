using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pokemonConsole
{
    internal class Combat
    {
        public static void UneLoopDeCombatDeAxel()
        {
            Pokemon pokemon = new Pokemon(8, 19);
            Pokemon pokemonAdverse = new Pokemon(78, 19);
            Random random = new Random();

            pokemon.AfficherDetailsPokemon();
            Console.WriteLine();
            pokemonAdverse.AfficherDetailsPokemon();

            Console.WriteLine();
            Console.WriteLine();
            int nbFuite = 0;
            bool fuiteReussit = false;
            Capacity capacityUsed = null;
            while (pokemon.pvLeft > 0 && pokemonAdverse.pvLeft > 0 && !fuiteReussit)
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
                                break;
                            case 2:
                                capacityUsed = listAttackActual[1];
                                break;
                            case 3:
                                capacityUsed = listAttackActual[2];
                                break;
                            case 4:
                                capacityUsed = listAttackActual[3];
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
                        break;
                    case 4:
                        nbFuite++;
                        int spdQuart = (int)Math.Floor(pokemonAdverse.spd / 4.0);
                        int fuiteEuclidienne = (spdQuart % 255 == 0) ? 0 : 1;
                        int randomFuiteValue = randomFuite.Next(0, 256);
                        int fuite = (pokemon.spd * 32 / (spdQuart % 255)) + (30 * nbFuite);

                        if (fuite > 255 || randomFuiteValue < fuite || fuiteEuclidienne == 0)
                        {
                            fuiteReussit = false;
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

                            Console.WriteLine("Vous n'avez pas réussi à fuir le Pokémon adverse !\n");
                            Console.WriteLine($"Les nouveaux PV du Pokemon du joueur sont = {pokemon.pvLeft}");
                            Console.WriteLine($"Les nouveaux PV du Pokemon de l'adversaire sont = {pokemonAdverse.pvLeft}\n");
                        }
                        break;
                }

                if (pokemon.pvLeft <= 0)
                {
                    Console.WriteLine("Le Pokemon du joueur a perdu !");
                }
                if (pokemonAdverse.pvLeft <= 0)
                {
                    Console.WriteLine("Le Pokemon de l'adversaire a perdu !");
                }
                if (fuiteReussit)
                {
                    Console.WriteLine("Vous avez réussi à fuir le combat");
                }

                static double CalculerDegatSubitPokemon(Pokemon pokemon, Pokemon pokemonAdverse, Capacity capacity)
                {
                    // Dégâts infligés = (((((((Niveau × 2 ÷ 5) +2) × Puissance × Att[Spé] ÷ 50) ÷ Def[Spé]) × Mod1) +2) × CC × Mod2 × R ÷ 100) × STAB × Type1 × Type2 × Mod3

                    Random random = new Random();

                    int atkSpeOrNot = 0;
                    int defSpeOrNot = 0;
                    float isBurn = 1;
                    float critChance = 1;
                    float critDamage = 1;
                    float randomMod = (random.Next(217, 256) * 100) / 255;
                    int stab = 1;

                    float efficaciteType1 = TypeModifier.CalculerMultiplicateur(capacity.type, pokemonAdverse.getListType()[0]);
                    float efficaciteType2 = 1;

                    if (pokemonAdverse.getListType().Count > 1)
                    {
                        efficaciteType2 = TypeModifier.CalculerMultiplicateur(capacity.type, pokemonAdverse.getListType()[1]);
                    }


                    // Détermine si la capacité est physique ou spécial selon le type
                    if (capacity.type == "DRAGON" || capacity.type == "EAU" || capacity.type == "ELECTRIK" || capacity.type == "FEU" || capacity.type == "GLACE" || capacity.type == "PLANTE" || capacity.type == "PSY")
                    {
                        atkSpeOrNot = pokemon.spe;
                        defSpeOrNot = pokemonAdverse.spe;
                    }
                    else
                    {
                        atkSpeOrNot = pokemon.atk;
                        defSpeOrNot = pokemonAdverse.def;
                    }

                    // Si le Pokemon est burn, l'attaque est divisée par deux
                    if (pokemon.statusProblem == "BRN")
                    {
                        isBurn = 0.5f;
                    }

                    // Critique
                    critChance = ((int)Math.Round(pokemon.spd / 2.0) * 2) / 256;
                    if (critChance == 0)
                    {
                        critDamage = 1;
                    }
                    else
                    {
                        critDamage = 2;
                    }
                    foreach (string typePokemon in pokemon.getListType())
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

