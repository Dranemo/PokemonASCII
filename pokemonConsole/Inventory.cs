using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;

namespace inventory
{
    // Interface pour les objets pouvant être stockés dans l'inventaire
    public interface IInventorable
    {
        string Name { get; }
    }

    // Classe de base pour les Pokémon
    public class PokemonInv : IInventorable
    {
        public string Name { get; set; }

        public PokemonInv(string name)
        {
            Name = name;
        }
    }

    // Classe de base pour les objets
    public class Item : IInventorable
    {
        public string Name { get; set; }
        public string Effect1 { get; set; }
        public string Effect2 { get; set; }
        public int Quantity { get; set; }

        public Item(string name, string effect1, string effect2, int quantity)
        {
            Name = name;
            Effect1 = effect1;
            Effect2 = effect2;
            Quantity = quantity;
        }

        // Méthode statique pour lire les données depuis un fichier CSV
        public static List<Item> LoadItemsFromCsv(string csvFilePath, int startId, int endId)
        {
            List<Item> items = new List<Item>();

            using (TextFieldParser parser = new TextFieldParser(csvFilePath))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");

                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();

                    // Assurez-vous que le tableau a suffisamment de champs
                    if (fields.Length >= 4)
                    {
                        // Parsez l'ID du champ
                        if (int.TryParse(fields[0], out int itemId))
                        {
                            // Vérifiez si l'ID est dans la plage spécifiée
                            if (itemId >= startId && itemId <= endId)
                            {
                                string name = fields[1];
                                string effect1 = fields[2];
                                string effect2 = fields[3];

                                // Créer un nouvel objet Item avec une quantité aléatoire entre 0 et 20
                                Random random = new Random();
                                int quantity = random.Next(21);

                                Item newItem = new Item(name, effect1, effect2, quantity);
                                items.Add(newItem);
                            }
                        }
                    }
                }
            }

            return items;
        }
    }

    // Classe d'inventaire générique
    public class Inventory<T> where T : Item, IInventorable
    {
        private List<T> items = new List<T>();

        public void AddItem(T item)
        {
            items.Add(item);
            Console.WriteLine($"{item.Name} (Quantité: {item.Quantity}) a été ajouté à l'inventaire.");
        }

        public void DisplayInventory()
        {
            Console.WriteLine("\nInventaire :");
            foreach (var item in items)
            {
                if (item.Quantity > 0)
                {
                    Console.WriteLine($"- {item.Name} (Quantité: {item.Quantity})");
                }
            }
        }
    }


}