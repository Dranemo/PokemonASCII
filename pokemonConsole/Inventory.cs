using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

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

        public Item(int id, string name, string effect1, string effect2, int quantity)
        {
            ID = id;
            Name = name;
            Effect1 = effect1;
            Effect2 = effect2;
            Quantity = quantity;
        }

        public static List<Item> AllItems
        {
            get { return allItems; }
        }

        private static List<Item> allItems;

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
}
