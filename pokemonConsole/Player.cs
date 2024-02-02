﻿using System;
using inventory;

namespace pokemonConsole
{
    class Player : Entity
    {
        public string name { get; set; }
        public int id { get; private set; }

        public List<Pokemon> pokemonParty = new List<Pokemon>();


        public Player()
        {
            name = "Player";
            Random random = new Random();
            id = random.Next(1, 65536);
        }

        public void addPokemonToParty(Pokemon pokemon)
        {
            if (pokemonParty.Count <= 6)
            {
                pokemonParty.Add(pokemon);
            }
        }
    }
}

