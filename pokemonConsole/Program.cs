using pokemonConsole;

using System.Collections;

class AfficherPokemon
{
    static void Main()
    {
        Pokemon pokemon = GeneratePokemon.generatePokemon(6, 25);

        Console.WriteLine("name = " + pokemon.getName());
        Console.WriteLine("Level = " + pokemon.getLevel());

        for(int i = 0; i < pokemon.getListType().Count; i++) 
        {
            Console.WriteLine($"Type {i+1} = {pokemon.getListType()[i]}");
        }

        Console.WriteLine("pv = " + pokemon.getPv());
        Console.WriteLine("atk = " + pokemon.getAtk());
        Console.WriteLine("def = " + pokemon.getDef());
        Console.WriteLine("Spe = " + pokemon.getSpe());
        Console.WriteLine("Spd = " + pokemon.getSpd());

    }
}

