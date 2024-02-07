using System;
using inventory;

namespace pokemonConsole
{
    class Player : Entity
    {
        public int id { get; set; }

        public List<Pokemon> pokemonParty = new List<Pokemon>();

        public int? starterId;


        public Player() : base("Player", 8, 8, 'P', "bedroom.txt", ' ')
        {
            Random random = new Random();
            id = random.Next(1, 65536);
            starterId = null;
        }

        public bool IsKO()
        {
            foreach (Pokemon p in pokemonParty)
            {
                if (p.pvLeft > 0)
                {
                    return false; // If any Pokemon is not knocked out, return false
                }
            }
            return true; // If all Pokemon are knocked out, return true
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

