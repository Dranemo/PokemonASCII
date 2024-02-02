using System;
using inventory;

namespace pokemonConsole
{
    class Rival
    {
        public string name { get; set; }

        public List<Pokemon> pokemonParty = new List<Pokemon>();

        public Rival()
        {
             name = "Rival";
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

