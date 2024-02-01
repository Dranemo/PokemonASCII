
﻿using pokemonConsole;

using System.Collections;
using System.Data;
﻿using System;
using System.Collections.Generic;

class Program
{

        static void Main()
        {
            Pokemon pokemon = GeneratePokemon.generatePokemon(96, 25);
            Pokemon pokemonAdverse = GeneratePokemon.generatePokemon(52, 25);
  
            pokemon.AfficherDetailsPokemon();

            Console.WriteLine();
            Console.WriteLine();

            pokemon.LevelUp();
            pokemon.AfficherDetailsPokemon();

            AfficherDetailsPokemon(pokemon);
            AfficherDetailsPokemon(pokemonAdverse);

            while (pokemon.Pv > 0 && pokemonAdverse.Pv > 0)
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
                    pokemon.Pv = (int)PvRestantPokemonJoueur;

                    // Mise à jour des PV du Pokémon de l'adversaire
                    pokemonAdverse.Pv = (int)PvRestantPokemonAdverse;

                    Console.WriteLine($"Les nouveaux PV du Pokemon du joueur sont = {pokemon.Pv}");
                    Console.WriteLine($"Les nouveaux PV du Pokemon de l'adversaire sont = {pokemonAdverse.Pv}\n");
                }
                else
                {
                    Console.WriteLine("Action non reconnue. Essayez 'Attaque'.\n");
                }

                // À ce stade, la boucle s'arrête car l'un des Pokémon a 0 PV ou moins
                if (pokemon.Pv <= 0)
                {
                    Console.WriteLine("Le Pokemon du joueur a perdu !");
                }
                if (pokemonAdverse.Pv <= 0)
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

            int vitessePokemon = pokemon.getSpd();
            int vitesseArrondie = (int)Math.Round((double)vitessePokemon / 2) * 2;
            float chanceTauxCrits = (float)(vitesseArrondie / 256.0 * 100.0);
            float chanceTauxCritsArrondi = (float)Math.Round(chanceTauxCrits, 2);

            double DamageEffectue = (((((pokemon.getLevel() * 0.4 + 2) * ATT * PUI) / pokemonAdverse.getDef()) / 50) + 2) * multiplicateurType1 * multiplicateurType2 * randomFactor * STAB;
            double DamageEffectueCrits = DamageEffectue * ((2 * pokemon.getLevel() + 5) / (pokemon.getLevel() + 5));

        if (randomChanceTauxCrits <= chanceTauxCritsArrondi)
        {
            PvRestantPokemonJoueur = (float)(pokemon.getPv() - DamageEffectueCrits);
        }
        else
        {
            PvRestantPokemonJoueur = (float)(pokemon.getPv() - DamageEffectue);
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

            int vitessePokemon = pokemon.getSpd();
            int vitesseArrondie = (int)Math.Round((double)vitessePokemon / 2) * 2;
            float chanceTauxCrits = (float)(vitesseArrondie / 256.0 * 100.0);
            float chanceTauxCritsArrondi = (float)Math.Round(chanceTauxCrits, 2);

            double DamageEffectue = (((((pokemonAdverse.getLevel() * 0.4 + 2) * ATT * PUI) / pokemon.getDef()) / 50) + 2) * multiplicateurType1 * multiplicateurType2 * randomFactor * STAB;
            double DamageEffectueCrits = DamageEffectue * ((2 * pokemonAdverse.getLevel() + 5) / (pokemonAdverse.getLevel() + 5));

            if (randomChanceTauxCrits <= chanceTauxCritsArrondi)
            {
                PvRestantPokemonAdverse = (float)(pokemonAdverse.getPv() - DamageEffectueCrits);
            }
            else
            {
                PvRestantPokemonAdverse = (float)(pokemonAdverse.getPv() - DamageEffectue);
            }

            float PvRestantPokemonAdverseArrondi = (float)Math.Floor(PvRestantPokemonAdverse);

            if (PvRestantPokemonAdverseArrondi < 0)
            {
                PvRestantPokemonAdverseArrondi = 0;
            }

            return PvRestantPokemonAdverseArrondi;
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
