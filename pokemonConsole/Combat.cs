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
            Pokemon pokemon = new Pokemon(78, 19);
            Pokemon pokemonAdverse = new Pokemon(129, 19);

            pokemon.AfficherDetailsPokemon();
            Console.WriteLine();
            pokemonAdverse.AfficherDetailsPokemon();

            Console.WriteLine();
            Console.WriteLine();

            pokemon.LevelUp();

            pokemon.pvLeft = pokemon.pv;
            pokemonAdverse.pvLeft = pokemonAdverse.pv;

            while (pokemon.pvLeft > 0 && pokemonAdverse.pvLeft > 0)
            {
                // Demander à l'utilisateur d'entrer son action
                Console.WriteLine("Entrez 'Attaque' pour attaquer : ");
                string modeAttaque = Console.ReadLine();

                if (modeAttaque.ToLower() == "attaque")
                {
                    // Calculer les dégâts subis par le Pokémon du joueur
                    float PvRestantPokemonJoueur = CalculerDegatSubitPokemonJoueur(pokemon, pokemonAdverse, 1f, 1f, 1f, 1f);

                    // Calculer les dégâts subis par le Pokémon de l'adversaire
                    float PvRestantPokemonAdverse = CalculerDegatSubitPokemonAdverse(pokemon, pokemonAdverse, 1f, 1f, 1f, 1f);

                    // Mise à jour des PV du Pokémon du joueur
                    pokemon.pvLeft = (int)PvRestantPokemonJoueur;

                    // Mise à jour des PV du Pokémon de l'adversaire
                    pokemonAdverse.pvLeft = (int)PvRestantPokemonAdverse;

                    Console.WriteLine($"Les nouveaux PV du Pokemon du joueur sont = {pokemon.pvLeft}");
                    Console.WriteLine($"Les nouveaux PV du Pokemon de l'adversaire sont = {pokemonAdverse.pvLeft}\n");
                }
                else
                {
                    Console.WriteLine("Action non reconnue. Essayez 'Attaque'.\n");
                }

                // À ce stade, la boucle s'arrête car l'un des Pokémon a 0 PV ou moins
                if (pokemon.pvLeft <= 0)
                {
                    Console.WriteLine("Le Pokemon du joueur a perdu !");
                }
                if (pokemonAdverse.pvLeft <= 0)
                {
                    Console.WriteLine("Le Pokemon de l'adversaire a perdu !");
                }
            }
        }

        static float CalculerDegatSubitPokemonJoueur(Pokemon pokemon, Pokemon pokemonAdverse, float ATT, float puiATT, float mod, float STAB)
        {
            // Nouvelles variables pour stocker les nouveaux PV
            float PvRestantPokemonJoueur;

            float multiplicateurType1 = TypeModifier.CalculerMultiplicateur(pokemon.getListType()[0], pokemonAdverse.getListType()[0]);
            float multiplicateurType2 = 1f;

            if (pokemon.getListType().Count > 1 && !string.IsNullOrEmpty(pokemon.getListType()[1]) &&
                pokemonAdverse.getListType().Count > 1 && !string.IsNullOrEmpty(pokemonAdverse.getListType()[1]))
            {
                multiplicateurType2 = TypeModifier.CalculerMultiplicateur(pokemon.getListType()[1], pokemonAdverse.getListType()[1]);
            }

            double randomFactor = (new Random().NextDouble() * (1 - 0.85)) + 0.85;

            // A Changer dès que les comps seront intégrées
            float PUI = ATT * puiATT * mod;

            Random random = new Random();
            double randomDouble = random.NextDouble() * 100.0;
            float randomChanceTauxCrits = (float)Math.Round(randomDouble, 2);

            int vitessePokemon = pokemon.spd;
            int vitesseArrondie = (int)Math.Round((double)vitessePokemon / 2) * 2;
            float chanceTauxCrits = (float)(vitesseArrondie / 256.0 * 100.0);
            float chanceTauxCritsArrondi = (float)Math.Round(chanceTauxCrits, 2);

            double DamageEffectue = (((((pokemon.level * 0.4 + 2) * ATT * PUI) / pokemonAdverse.def) / 50) + 2) * multiplicateurType1 * multiplicateurType2 * randomFactor * STAB;
            double DamageEffectueCrits = DamageEffectue * ((2 * pokemon.spd + 5) / (pokemon.level + 5));

            if (randomChanceTauxCrits <= chanceTauxCritsArrondi)
            {
                PvRestantPokemonJoueur = (float)(pokemon.pvLeft - DamageEffectueCrits);
            }
            else
            {
                PvRestantPokemonJoueur = (float)(pokemon.pvLeft - DamageEffectue);
            }

            float PvRestantPokemonJoueurArrondi = (float)Math.Floor(PvRestantPokemonJoueur);

            if (PvRestantPokemonJoueurArrondi < 0)
            {
                PvRestantPokemonJoueurArrondi = 0;
            }

            return PvRestantPokemonJoueurArrondi;
        }

        static float CalculerDegatSubitPokemonAdverse(Pokemon pokemon, Pokemon pokemonAdverse, float ATT, float puiATT, float mod, float STAB)
        {
            // Nouvelles variables pour stocker les nouveaux PV
            float PvRestantPokemonAdverse;

            float multiplicateurType1 = TypeModifier.CalculerMultiplicateur(pokemon.getListType()[0], pokemonAdverse.getListType()[0]);
            float multiplicateurType2 = 1f;

            if (pokemon.getListType().Count > 1 && !string.IsNullOrEmpty(pokemon.getListType()[1]) &&
                pokemonAdverse.getListType().Count > 1 && !string.IsNullOrEmpty(pokemonAdverse.getListType()[1]))
            {
                multiplicateurType2 = TypeModifier.CalculerMultiplicateur(pokemon.getListType()[1], pokemonAdverse.getListType()[1]);
            }

            double randomFactor = (new Random().NextDouble() * (1 - 0.85)) + 0.85;

            // A Changer dès que les comps seront intégrées
            float PUI = ATT * puiATT * mod;

            Random random = new Random();
            double randomDouble = random.NextDouble() * 100.0;
            float randomChanceTauxCrits = (float)Math.Round(randomDouble, 2);

            int vitessePokemon = pokemon.spd;
            int vitesseArrondie = (int)Math.Round((double)vitessePokemon / 2) * 2;
            float chanceTauxCrits = (float)(vitesseArrondie / 256.0 * 100.0);
            float chanceTauxCritsArrondi = (float)Math.Round(chanceTauxCrits, 2);

            double DamageEffectue = (((((pokemonAdverse.level * 0.4 + 2) * ATT * PUI) / pokemon.def) / 50) + 2) * multiplicateurType1 * multiplicateurType2 * randomFactor * STAB;
            double DamageEffectueCrits = DamageEffectue * ((2 * pokemonAdverse.level + 5) / (pokemonAdverse.level + 5));

            if (randomChanceTauxCrits <= chanceTauxCritsArrondi)
            {
                PvRestantPokemonAdverse = (float)(pokemonAdverse.pvLeft - DamageEffectueCrits);
            }
            else
            {
                PvRestantPokemonAdverse = (float)(pokemonAdverse.pvLeft - DamageEffectue);
            }

            float PvRestantPokemonAdverseArrondi = (float)Math.Floor(PvRestantPokemonAdverse);

            if (PvRestantPokemonAdverseArrondi < 0)
            {
                PvRestantPokemonAdverseArrondi = 0;
            }

            return PvRestantPokemonAdverseArrondi;
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

