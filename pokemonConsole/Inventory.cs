using System;
using System.Collections.Generic;
using pokemonConsole;
namespace inventory;
// Interface pour les objets pouvant être stockes dans l'inventaire
public interface IInventorable
{
    string Name { get; }
}

// Classe de base pour les Pokemon
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

    public Item(string name)
    {
        Name = name;
    }
}

// Classe d'inventaire generique
public class Inventory<T> where T : IInventorable
{
    private List<T> items = new List<T>();

    public void AddItem(T item)
    {
            items.Add(item);
            Console.WriteLine($"{item.Name} a ete ajoute à l'inventaire.");
    }

    public void DisplayInventory()
    {
        Console.WriteLine("Inventaire :");
        foreach (var item in items)
        {
            Console.WriteLine($"- {item.Name}");
        }
    }
}

