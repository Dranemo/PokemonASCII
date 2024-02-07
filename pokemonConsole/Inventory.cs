using Microsoft.VisualBasic.FileIO;
using NUnit.Framework.Interfaces;
using pokemonConsole;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Xml.Linq;
using Usefull;

namespace inventory
{
    public interface IInventorable
    {
        string Name { get; }
        int Quantity { get; set; }
    }

    // Classe de base pour les Pokémon
    public class PokemonInv : IInventorable
    {
        public string Name { get; set; }
        public int Quantity { get; set; }

        public PokemonInv(string name)
        {
            Name = name;
        }
    }
    public class Item : IInventorable
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Effect1 { get; set; }
        public string Effect2 { get; set; }
        public int Quantity { get; set; }
        public string Category { get; set; }

        public Item(int id, string name, string effect1, string effect2, int quantity)
        {
            ID = id;
            Name = name;
            Effect1 = effect1;
            Effect2 = effect2;
            Quantity = quantity;
        }
        private static List<Item> allItems = new List<Item>();

        public static List<Item> AllItems
        {
            get { return allItems; }
        }

        public static List<Item> LoadItemsFromSaveFile(string saveFilePath)
        {
            List<Item> items = new List<Item>();


            using (TextFieldParser parser = new TextFieldParser(saveFilePath))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");

                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();

                    if (fields.Length >= 5)
                    {
                        if (int.TryParse(fields[0], out int itemId) &&
                            !string.IsNullOrEmpty(fields[1]) &&
                            int.TryParse(fields[4], out int quantity))
                        {
                            string name = fields[1];
                            string effect1 = fields[2];
                            string effect2 = fields[3];

                            if (quantity >= 0)
                            {
                                Item newItem = new Item(itemId, name, effect1, effect2, quantity);
                                items.Add(newItem);
                            }
                        }
                    }
                }
            }

            allItems = items;

            return items;
        }


        public static void SaveQuantitiesToFile(string csvFilePath, List<Item> items)
        {
            List<string> lines = new List<string>();

            foreach (var item in items)
            {
                lines.Add($"{item.ID},{item.Name},{item.Effect1},{item.Effect2},{item.Quantity}");
            }

            File.WriteAllLines(csvFilePath, lines);
        }
    }

    public class Inventory
    {
        private static List<Item> items;

        public static void InitializeItems()
        {
            items = Item.LoadItemsFromSaveFile($"{AdresseFile.FileDirection}\\SaveItemInGame.txt");
        }
        public static void UseItem(string choice)
        {
            Player player = new Player();
            if (items == null)
            {
                Console.WriteLine("Liste d'items non initialisée.");
                return;
            }

            Item itemToUse = items.Find(i => i.Name.Equals(choice, StringComparison.OrdinalIgnoreCase));

            // Initialisez la variable pokemon ici
            Pokemon pokemon = new Pokemon(1, 15, player.id, 1, player.id, player.name);
            if (itemToUse != null)
            {
                Console.WriteLine($"Vous utilisez {itemToUse.Name}...");

                switch (itemToUse.Name)
                {
                    case "POTION":
                        Item potionDetails = Item.AllItems.FirstOrDefault(i => i.Name.Equals("POTION", StringComparison.OrdinalIgnoreCase));

                        if (potionDetails != null)
                        {
                            // Appliquer les effets de la POTION sur le Pokémon
                            int healingAmount = int.Parse(potionDetails.Effect1);
                            pokemon.pvLeft += healingAmount;
                            if (pokemon.pvLeft > pokemon.pv)
                            {
                                pokemon.pvLeft = pokemon.pv;
                            }

                            Console.WriteLine($"Votre Pokémon récupère {healingAmount} points de vie.");
                            Console.WriteLine($"\nLes nouveaux PV du Pokemon du joueur sont = {pokemon.pvLeft}\n");
                        }
                        else
                        {
                            Console.WriteLine("Détails de la POTION introuvables.");
                        }
                        break;
                    case "Medicament":
                        Console.WriteLine("Medicament");
                        break;
                    case "Accelerateur":
                        Console.WriteLine("Accelerateur");
                        break;
                    case "Objet Evolution":
                        Console.WriteLine("Objet Evolution");
                        break;
                    case "Objet de combat":
                        Console.WriteLine("Objet de combat");
                        break;
                    default:
                        Console.WriteLine("Catégorie non reconnue.");
                        break;
                }

                // Décrémenter la quantité de l'objet utilisé
                itemToUse.Quantity--;

                Console.WriteLine($"Effets appliqués avec succès. Quantité restante : {itemToUse.Quantity}");

                // Sauvegarder les quantités dans le fichier
                inventory.Item.SaveQuantitiesToFile($"{AdresseFile.FileDirection}\\SaveItemInGame.txt", inventory.Item.AllItems);
            }
            else
            {
                Console.WriteLine("Objet non trouvé dans l'inventaire.");
            }
        }
    }

}