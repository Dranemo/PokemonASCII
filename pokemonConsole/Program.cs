
﻿using pokemonConsole;

using System.Collections;
using System.Data;
﻿using System;
using System.Collections.Generic;

class Program
{

    static void Main()
    {
        //Map.MapPlayer();
        /*Combat.UneLoopDeCombatDeAxel();*/


        Pokemon salameche = new Pokemon(148, 55);
        Player player = new Player("player");

        player.addPokemonToParty(salameche);

        foreach (Pokemon pokemon in player.pokemonParty) 
        {
            pokemon.AfficherDetailsPokemon();
            pokemon.LevelUp();
            pokemon.AfficherDetailsPokemon();
        }
    }
}