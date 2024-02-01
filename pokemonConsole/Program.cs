
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

        MainMenu.Start();

        Player player = new Player("player");


        Pokemon salameche = new Pokemon(52, 55);

        player.addPokemonToParty(salameche);

        foreach (Pokemon pokemon in player.pokemonParty) 
        {
            pokemon.AfficherDetailsPokemon();
            //pokemon.LevelUp();
            //pokemon.AfficherDetailsPokemon();
        }
    }
}