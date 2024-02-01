
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

        Pokemon pokemon = new Pokemon(147, 99);
        pokemon.AfficherDetailsPokemon();

        pokemon.LevelUp();
        pokemon.AfficherDetailsPokemon();


    }
}
