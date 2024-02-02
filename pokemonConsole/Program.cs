
﻿using pokemonConsole;

using System.Collections;
using System.Data;
﻿using System;
using System.Collections.Generic;
using inventory;

class Program
{

    static void Main()
    {
        MainMenu.Start();

        Player player = new Player("player");


        Pokemon salameche = new Pokemon(52, 55);

        player.addPokemonToParty(salameche);

        foreach (Pokemon pokemon in player.pokemonParty)
        {
            pokemon.AfficherDetailsPokemon();
            pokemon.GainExp(15000);
            pokemon.AfficherDetailsPokemon();
        } } }
    namespace FunctionUsefull
{
    class Functions
    {
        public static void ClearInputBuffer()
        {
            while (Console.KeyAvailable)
            {
                Console.ReadKey(intercept: true);
            }
        }
    }
}
