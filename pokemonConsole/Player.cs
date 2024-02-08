using System;

namespace pokemonConsole
{
    class Player : Entity
    {
        public int id { get; set; }

        public List<Pokemon> pokemonParty = new List<Pokemon>();
        public List<Item> inventory = new List<Item>();

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
        
        public void addItemToInventory(int item_id, int quantity = 1)
        {
            bool itemInInv = false;

            foreach (Item item in inventory)
            {
                if(item.id == item_id)
                {
                    item.quantity += quantity;
                    itemInInv = true;
                }
            }
            if (!itemInInv) 
            {
                inventory.Add(new Item(item_id, quantity));
            }
        }
    }
}

