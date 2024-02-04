using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;

namespace inventory
{
    // Interface pour les objets pouvant être stockés dans l'inventaire
    public interface IInventorable
    {
        string Name { get; }
        int Quantity { get; set; }
    }

    // Classe de base pour les Pokémon
    public class PokemonInv : IInventorable
    {
        public string Name { get; set; }
        public int Quantity { get; set; } // Ajoutez cette propriété

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
        public static List<Item> LoadItemsFromCsv(string csvFilePath, int startId, int endId, string saveItemFilePath)
        {
            List<Item> items = new List<Item>();

            // Charger les quantités depuis le fichier SaveItem.txt
            Dictionary<string, int> quantities = LoadQuantitiesFromFile(saveItemFilePath);

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

                                // Utiliser la quantité depuis le fichier SaveItem.txt
                                if (quantities.TryGetValue(name, out int quantity))
                                {
                                    Item newItem = new Item(name, effect1, effect2, quantity);
                                    items.Add(newItem);
                                }
                            }
                        }
                    }
                }
            }

            return items;
        }

        // Nouvelle méthode pour sauvegarder les quantités dans un fichier
        public static void SaveQuantitiesToFile(string filePath, List<Item> items)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var item in items)
                {
                    writer.WriteLine($"{item.Name},{item.Quantity}");
                }
            }
        }

        // Ajoutez cette méthode pour charger les quantités depuis le fichier SaveItem.txt
        private static Dictionary<string, int> LoadQuantitiesFromFile(string filePath)
        {
            Dictionary<string, int> quantities = new Dictionary<string, int>();

            if (File.Exists(filePath))
            {
                foreach (string line in File.ReadLines(filePath))
                {
                    string[] parts = line.Split(',');
                    if (parts.Length == 2 && int.TryParse(parts[1], out int quantity))
                    {
                        string itemName = parts[0];
                        quantities[itemName] = quantity;
                    }
                }
            }

            return quantities;
        }

        private static List<Item> allItems; // Ajoutez cette variable statique

        // Méthode statique pour charger les données depuis un fichier CSV et stocker dans la variable statique
        public static void LoadAllItemsFromCsv(string csvFilePath, string saveItemFilePath)
        {
            allItems = LoadItemsFromCsv(csvFilePath, 1, 43, saveItemFilePath);
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
                Console.WriteLine($"{item.Name} (Quantité: {itemQuantities[item]}) a été ajouté à l'inventaire.");
            }
            else
            {
                Console.WriteLine($"L'objet {item.Name} n'est pas présent dans l'inventaire.");
            }
        }

        public void DisplayInventory()
        {
            Console.WriteLine("\nInventaire :");
            foreach (var kvp in itemQuantities)
            {
                Console.WriteLine($"- {kvp.Key.Name} (Quantité: {kvp.Value})");
            }
        }

        public void SaveQuantitiesToFile(string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var kvp in itemQuantities)
                {
                    writer.WriteLine($"{kvp.Key.Name},{kvp.Value}");
                }
            }
        }

        public void LoadQuantitiesFromFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                itemQuantities.Clear();

                foreach (string line in File.ReadLines(filePath))
                {
                    string[] parts = line.Split(',');
                    if (parts.Length == 2 && int.TryParse(parts[1], out int quantity))
                    {
                        T item = Items.FirstOrDefault(i => i.Name == parts[0]);
                        if (item != null)
                        {
                            item.Quantity = quantity;
                            itemQuantities[item] = quantity;
                        }
                    }
                }
            }
        }
    }
}
