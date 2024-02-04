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

        private static List<Item> allItems; // Ajoutez cette variable statique

        // Méthode statique pour charger les données depuis un fichier CSV et stocker dans la variable statique
        public static void LoadAllItemsFromCsv(string csvFilePath)
        {
            allItems = LoadItemsFromCsv(csvFilePath, 1, 43);
        }

        // Propriété statique pour accéder à la liste d'objets
        public static List<Item> AllItems
        {
            get { return allItems; }
        }
    }

    // Classe d'inventaire générique
    public class Inventory<T> where T : IInventorable
    {
        private Dictionary<T, int> itemQuantities = new Dictionary<T, int>();

        public List<T> Items => itemQuantities.Keys.ToList();

        public void AddItem(T item)
        {
            if (itemQuantities.ContainsKey(item))
            {
                itemQuantities[item]++;
            }
            else
            {
                itemQuantities[item] = 1;
            }

            Console.WriteLine($"{item.Name} (Quantité: {itemQuantities[item]}) a été ajouté à l'inventaire.");
        }

        public void DisplayInventory()
        {
            Console.WriteLine("\nInventaire :");
            foreach (var kvp in itemQuantities)
            {
                Console.WriteLine($"- {kvp.Key.Name} (Quantité: {kvp.Value})");
            }
        }
    }
}