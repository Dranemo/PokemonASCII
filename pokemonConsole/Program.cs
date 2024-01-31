
﻿using pokemonConsole;

using System.Collections;
using System.Data;
﻿using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        /*Map.MapPlayer();*/
        /*Combat.UneLoopDeCombatDeAxel();*/

        Pokemon pokemon = GeneratePokemon.generatePokemon(2, 25);
        pokemon.AfficherDetailsPokemon();
        
        while(pokemon.getListEvo().Count > 0 )
        {
            Console.WriteLine();
            pokemon.Evolution();
            pokemon.AfficherDetailsPokemon();
        }




    }
}
