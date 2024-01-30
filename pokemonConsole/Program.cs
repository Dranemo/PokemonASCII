using pokemonConsole;

using System.Collections;

class AfficherPokemon
{
    static void Main()
    {
        Pokemon pokemon = GeneratePokemon.generatePokemon(4, 60);

        pokemon.AfficherDetailsPokemon();

        Console.WriteLine();

        pokemon.Evolution();
        pokemon.AfficherDetailsPokemon();

        Console.WriteLine();

        pokemon.Evolution();
        pokemon.AfficherDetailsPokemon();
    }
}

