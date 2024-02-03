
﻿using pokemonConsole;

using System.Collections;
using System.Data;
﻿using System;
using System.Collections.Generic;
using inventory;

class Program
{

    static void Main()
    {
        // Charger les objets avec un ID de 1 à 4 du CSV
        List<Item> items = Item.LoadItemsFromCsv("C:\\Users\\GolfOcean33\\OneDrive\\Bureau\\Nouveau dossier\\pokemonConsole\\GameFiles\\CSV\\item.csv", 1, 43);

        // Créer un inventaire et ajouter les objets
        Inventory<Item> inventory = new Inventory<Item>();
        foreach (var item in items)
        {
            inventory.AddItem(item);
        }

        // Afficher l'inventaire
        inventory.DisplayInventory();
        MainMenu.Start();
    }



}

namespace Usefull
{
    class Functions
    {
        public static void ClearInputBuffer()
        {
            while (Console.KeyAvailable)
            {
                Console.ReadKey(intercept: true);
            }
        }
    }

    class AdresseFile
    {
        public static string FileDirection = "C:\\Users\\GolfOcean33\\OneDrive\\Bureau\\Nouveau dossier\\pokemonConsole\\GameFiles\\";
    }
}