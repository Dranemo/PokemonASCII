using pokemonConsole;

class AfficherPokemon
{
    static void Main()
    {
        Pokemon pokemon = GeneratePokemon.generatePokemon(1, 25);

        Console.WriteLine("name = " + pokemon.getName());
        Console.WriteLine("Level = " + pokemon.getLevel());
        Console.WriteLine("pv = " + pokemon.getPv());
        Console.WriteLine("atk = " + pokemon.getAtk());
        Console.WriteLine("def = " + pokemon.getDef());
        Console.WriteLine("Spd = " + pokemon.getSpd());
        Console.WriteLine("Spe = " + pokemon.getSpe());

    }
}

