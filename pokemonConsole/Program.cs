using pokemonConsole;

using System.Collections;
using System.Data;

class AfficherPokemon
{
    static void Main()
    {
        Map.MapPlayer();
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

