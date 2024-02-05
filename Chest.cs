using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usefull;

namespace pokemonConsole
{
    class Chest : Entity
    {
        public string dialogue { get; set; }
        public bool updated;

        public Chest(string name_, string dialogue_, char sprite_, string map_, int positionX_, int positionY_, char actualTile) : base(name_, positionX_, positionY_, sprite_, map_, actualTile)
        {
            dialogue = dialogue_;
            updated = false;
        }

        public virtual void Update(DateTime deltatime, Player player)
        {

        }
    }

    