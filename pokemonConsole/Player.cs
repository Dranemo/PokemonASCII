using System;
using inventory;

namespace pokemonConsole
{
    class Player : Entity
    {
        public int id { get; set; }

        public List<Pokemon> pokemonParty = new List<Pokemon>();


        public Player() : base("Player", 8, 8, 'P', "bedroom.txt", ' ')
        {
            Random random = new Random();
            id = random.Next(1, 65536);
        }

        public bool IsKO()
        {
            foreach (var p in pokemonParty)
            {
                if (p.pvLeft <= 0)
                {
                    Thread.Sleep(1000);
                    return true;
                }
            }
            return false;
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

