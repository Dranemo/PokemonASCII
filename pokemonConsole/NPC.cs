using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usefull;

namespace pokemonConsole
{
    class NPC : Entity
    {
        public string dialogue { get; set; }

        public NPC(string name_, string dialogue_, char sprite_, string map_, int positionX_, int positionY_, char actualTile) : base (name_, positionX_, positionY_, sprite_, map_, actualTile)
        {
            dialogue = dialogue_;
        }

        
    }

    class Maman : NPC
    {
        public Maman() : base("Maman", "Bonjour mon fils ! Bien dormi ?", '8', "mom.txt", 7, 4, ' ')
        {

        }

        public override void Function(Player player)
        {
            Pokemon.Heal(player);
        }
    }

    
}
