using inventory;
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
    class NPC : Entity
    {
        public string dialogue { get; set; }
        public bool updated;

        public NPC(string name_, string dialogue_, char sprite_, string map_, int positionX_, int positionY_, char actualTile) : base (name_, positionX_, positionY_, sprite_, map_, actualTile)
        {
            dialogue = dialogue_;
            updated = false;
        }

        public virtual void Update(DateTime deltatime, Player player)
        {

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

    class RivalNPC :NPC
    {
        public Rival rival { get; private set; } = new Rival();
        public RivalNPC (Rival rival) : base(rival.name, "Yo minable !", 'R', "chen.txt", 4, 4, ' ')
        {
            rival = this.rival;
        }
        public override void Function(Player player)
        {
            Combat.LoopCombat(player, rival.pokemonParty);
        }
    }
    class PotionMan : NPC
    {
        public PotionMan() : base("PotionMan", "Tiens ! Une Potion", 'E', "route_1.txt", 3, 24, ' ') { }

        public override void Update(DateTime deltatime, Player player)
        {
            Random random = new Random();

            DateTime endTime = DateTime.Now;

            if ((endTime - deltatime).TotalMilliseconds > 2000)
            {
                bool movedChose = false;
                while (!movedChose)
                {
                    int direction = random.Next(1, 5);
                    switch(direction)
                    {
                        case 1: // haut
                            if (PositionY > 24 && !(PositionX == player.PositionX && PositionY-1 == player.PositionY))
                            {
                                PositionY--;
                                movedChose = true;
                            }
                            break;
                        case 2: // bas
                            if (PositionY < 21 && !(PositionX == player.PositionX && PositionY + 1 == player.PositionY))
                            {
                                PositionY++;
                                movedChose = true;
                            }
                            break;
                        case 3: // gauche
                            if (PositionX > 2 && !(PositionX - 1 == player.PositionX && PositionY == player.PositionY))
                            {
                                PositionX--;
                                movedChose = true;
                            }
                            break;
                        case 4: // droite
                            if (PositionX < 5 && !(PositionX + 1 == player.PositionX && PositionY == player.PositionY))
                            {
                                PositionX++;
                                movedChose = true;
                            }
                            break;
                    }
                }
                updated = true;
            }
        }

        public override void Function(Player player)
        {
            Item.LoadItemsFromSaveFile($"{AdresseFile.FileDirection}\\SaveItemInGame.txt");

            // Ensuite, vous pouvez utiliser la fonction Function
            //player.Function();

            Item itemToGive = Item.AllItems.FirstOrDefault(i => i.ID == 5);

            // Vérifiez si l'objet a été trouvé
            if (itemToGive != null)
            {
                // Augmentez la quantité de l'objet dans l'inventaire du joueur
                itemToGive.Quantity++;

                // Affichez la nouvelle quantité
                Console.WriteLine($"Vous avez maintenant {itemToGive.Quantity} {itemToGive.Name}(s) dans votre inventaire.");

                // Vous pouvez également sauvegarder l'inventaire mis à jour dans un fichier si nécessaire
                Item.SaveQuantitiesToFile($"{AdresseFile.FileDirection}\\SaveItemInGame.txt", Item.AllItems);
            }
            else
            {
                Console.WriteLine("Erreur : Objet introuvable.");
            }
        }
    }

    
}
