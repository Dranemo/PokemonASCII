﻿using System;
using System.Collections.Generic;
using pokemonConsole;

class AfficherPokemon
{
    static void AfficherDetailsPokemon(Pokemon pokemon)
    {
        Console.WriteLine("Name = " + pokemon.getName());
        Console.WriteLine("Level = " + pokemon.getLevel());
        Console.WriteLine("PV = " + pokemon.getPv());
        Console.WriteLine("ATK = " + pokemon.getAtk());
        Console.WriteLine("DEF = " + pokemon.getDef());
        Console.WriteLine("SPE = " + pokemon.getSpe());
        Console.WriteLine("SPD = " + pokemon.getSpd());

        foreach (string type in pokemon.getListType())
        {
            Console.WriteLine($"Type = {type}");
        }
        Console.WriteLine("");
    }
    static void Main()
    {
        Pokemon pokemon = GeneratePokemon.generatePokemon(3, 25);
        Pokemon pokemonAdverse = GeneratePokemon.generatePokemon(15, 25);

        AfficherDetailsPokemon(pokemon);
        AfficherDetailsPokemon(pokemonAdverse);

        float multiplicateurType1 = CalculerMultiplicateur1(pokemon.getListType()[0], pokemonAdverse.getListType()[0]);
        Console.WriteLine($"Multiplicateur type 1 = {multiplicateurType1}");
        float multiplicateurType2 = CalculerMultiplicateur2(pokemon.getListType()[1], pokemonAdverse.getListType()[1]);
        Console.WriteLine($"Multiplicateur type 2 = {multiplicateurType2}");
        

    }

    static float CalculerMultiplicateur(string typePokemon, string typeAdverse)
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

    static float CalculerMultiplicateur1(string type1Pokemon, string type1Adverse)
    {
        return CalculerMultiplicateur(type1Pokemon, type1Adverse);
    }

    static float CalculerMultiplicateur2(string type2Pokemon, string type2Adverse)
    {
        return CalculerMultiplicateur(type2Pokemon, type2Adverse);
    }
}
