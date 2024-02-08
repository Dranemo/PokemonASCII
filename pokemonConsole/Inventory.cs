using Microsoft.VisualBasic.FileIO;
using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Usefull;

namespace pokemonConsole
{
    class Item
    {
        public int id;
        public string name;

        private string effect1;
        private string effect2;

        private string usage;

        public int quantity;

        private string fileCSV = AdresseFile.FileDirection + "CSV\\item.csv";

        public Item(int item_id, int quantity = 1)
        {
            using (StreamReader sr = new StreamReader(fileCSV))
            {
                string line;
                bool itemFound = false;

                line = sr.ReadLine();
                line = sr.ReadLine();

                while ((line = sr.ReadLine()) != null && !itemFound)
                {
                    string[] colonnes = line.Split(',');
                    if (item_id == int.Parse(colonnes[0]))
                    {
                        id = item_id;
                        name = colonnes[1];
                        effect1 = colonnes[2];
                        effect2 = colonnes[3];
                        usage = colonnes[4];

                        this.quantity = quantity;
                    }
                }
            }
        }

        static public void UseItem(Item item, bool isCombat = false)
        {
            if((item.usage == "NOTBATTLE" && !isCombat) || (item.usage == "BOTH" && !isCombat))
            {
                Console.WriteLine("Used " + item.name);
            }
            else if (item.usage == "NOTBATTLE" && isCombat)
            {
                Console.WriteLine("Unusable");
            }
            else if ((item.usage == "BATTLE" && isCombat) || (item.usage == "BOTH" && isCombat))
            {
                Console.WriteLine("Used " + item.name);
            }
            else if (item.usage == "BATTLE" && !isCombat)
            {
                Console.WriteLine("Unusable");
            }
        }

        public void JeterItem(Player player, int quantity)
        {
            if (this.quantity - quantity > 0)
            {
                this.quantity -= quantity;
            }
            else
            {
                player.inventory.Remove(this);
            }
        }
    }
}
