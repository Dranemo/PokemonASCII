using System;
using System.Collections.Generic;
using pokemonConsole;

class AfficherPokemon
{
    static void AfficherDetailsPokemon(Pokemon pokemon)
    {
        Console.WriteLine("Name = " + pokemon.getName());
        Console.WriteLine("Level = " + pokemon.getLevel());
        Console.WriteLine("PV = " + pokemon.getPv());
        Console.WriteLine("ATK = " + pokemon.getAtk());
        Console.WriteLine("DEF = " + pokemon.getDef());
        Console.WriteLine("SPE = " + pokemon.getSpe());
        Console.WriteLine("SPD = " + pokemon.getSpd());

        foreach (string type in pokemon.getListType())
        {
            Console.WriteLine($"Type = {type}");
        }
        Console.WriteLine("");
    }
    static void Main()
    {


        //pokemon.AfficherDetailsPokemon();

        //Console.WriteLine();

        //pokemon.Evolution();
        //pokemon.AfficherDetailsPokemon();

        //Console.WriteLine();

        //pokemon.Evolution();
        //pokemon.AfficherDetailsPokemon();

        Pokemon pokemon = GeneratePokemon.generatePokemon(27, 25);
        Pokemon pokemonAdverse = GeneratePokemon.generatePokemon(95, 25);

        AfficherDetailsPokemon(pokemon);
        AfficherDetailsPokemon(pokemonAdverse);

        while (pokemon.Pv > 0 && pokemonAdverse.Pv > 0)
        {
            // Afficher les détails des Pokémon
            AfficherDetailsPokemon(pokemon);
            AfficherDetailsPokemon(pokemonAdverse);

            // Demander à l'utilisateur d'entrer son action
            Console.WriteLine("Entrez 'Attaque' pour attaquer : ");
            string modeAttaque = Console.ReadLine();

            // Vérifier l'action de l'utilisateur
            if (modeAttaque.ToLower() == "attaque")
            {
                float multiplicateurType1 = CalculerMultiplicateur1(pokemon.getListType()[0], pokemonAdverse.getListType()[0]);
                Console.WriteLine($"Multiplicateur type 1 = {multiplicateurType1}");
                if (pokemon.getListType().Count > 1 && !string.IsNullOrEmpty(pokemon.getListType()[1]) && pokemonAdverse.getListType().Count > 1 && !string.IsNullOrEmpty(pokemonAdverse.getListType()[1]))
                {
                    float multiplicateurType2 = CalculerMultiplicateur2(pokemon.getListType()[1], pokemonAdverse.getListType()[1]);
                    Console.WriteLine($"Multiplicateur type 2 = {multiplicateurType2}");
                }


                float PvRestantPokemonJoueur = CalculerDegatSubitPokemonJoueur(pokemon.Pv, pokemonAdverse.getAtk());
                float PvRestantPokemonAdverse = CalculerDegatSubitPokemonAdverse(pokemonAdverse.Pv, pokemon.getAtk());
                

                // Mise à jour des PV du Pokémon du joueur
                pokemon.Pv = (int)PvRestantPokemonJoueur;

                // Mise à jour des PV du Pokémon de l'adversaire
                pokemonAdverse.Pv = (int)PvRestantPokemonAdverse;

                Console.WriteLine($"Les nouveaux PV du Pokemon du joueur sont = {pokemon.Pv}");
                Console.WriteLine($"Les nouveaux PV du Pokemon de l'adversaire sont = {pokemonAdverse.Pv}");
            }
            else
            {
                Console.WriteLine("Action non reconnue. Essayez 'Attaque'.");
            }

            // À ce stade, la boucle s'arrête car l'un des Pokémon a 0 PV ou moins
            if (pokemon.Pv <= 0)
            {
                Console.WriteLine("Le Pokemon du joueur a perdu !");
            }
            if (pokemonAdverse.Pv <= 0)
            {
                Console.WriteLine("Le Pokemon de l'adversaire a perdu !");
            }
        }
    }

    static float CalculerMultiplicateur(string typePokemon, string typeAdverse)
    {
        Dictionary<string, Dictionary<string, float>> multiplicateurs = new Dictionary<string, Dictionary<string, float>>()
    {
        {"NORMAL", new Dictionary<string, float>() {{"ROCHE", 0.5f}, {"SPECTRE", 0f}, {"default", 1f}}},
        {"FEU", new Dictionary<string, float>() {{"FEU", 0.5f}, {"EAU", 0.5f}, {"ROCHE", 0.5f}, {"DRAGON", 0.5f}, {"PLANTE", 2f}, {"GLACE", 2f}, {"INSECTE", 2f}, {"default", 1f}}},
        {"EAU", new Dictionary<string, float>() {{"EAU", 0.5f}, {"PLANTE", 0.5f}, {"DRAGON", 0.5f}, {"FEU", 2f}, {"SOL", 2f}, {"ROCHE", 2f}, {"default", 1f}}},
        {"PLANTE", new Dictionary<string, float>() {{"FEU", 0.5f}, {"PLANTE", 0.5f}, {"POISON", 0.5f}, { "VOL", 0.5f }, { "INSECTE", 0.5f }, { "DRAGON", 0.5f }, { "EAU", 2f}, {"SOL", 2f}, {"ROCHE", 2f}, {"default", 1f}}},
        {"ELECTRIK", new Dictionary<string, float>() {{"PLANTE", 0.5f}, {"ELECTRIK", 0.5f}, {"DRAGON", 0.5f}, {"EAU", 2f}, {"VOL", 2f}, {"SOL", 0f}, {"default", 1f}}},
        {"GLACE", new Dictionary<string, float>() {{"EAU", 0.5f}, {"GLACE", 0.5f}, {"PLANTE", 2f}, {"SOL", 2f}, {"VOL", 2f}, {"DRAGON", 2f}, {"default", 1f}}},
        {"COMBAT", new Dictionary<string, float>() {{"POISON", 0.5f}, {"VOL", 0.5f}, {"PSY", 0.5f}, {"INSECTE", 0.5f}, {"NORMAL", 2f}, {"GLACE", 2f}, { "ROCHE", 2f }, { "SPECTRE", 0f }, { "default", 1f}}},
        {"POISON", new Dictionary<string, float>() {{"POISON", 0.5f}, {"SOL", 0.5f}, {"ROCHE", 0.5f}, {"SPECTRE", 0.5f}, {"PLANTE", 2f}, {"INSECTE", 2f}, {"default", 1f}}},
        {"SOL", new Dictionary<string, float>() {{"PLANTE", 0.5f}, {"INSECTE", 0.5f}, {"FEU", 2f}, {"ELECTRIK", 2f}, {"POISON", 2f}, {"ROCHE", 2f}, { "VOL", 0f }, { "default", 1f}}},
        {"VOL", new Dictionary<string, float>() {{"ELECTRIK", 0.5f}, {"ROCHE", 0.5f}, {"PLANTE", 2f}, {"COMBAT", 2f}, {"INSECTE", 2f}, {"default", 1f}}},
        {"PSY", new Dictionary<string, float>() {{"PSY", 0.5f}, {"COMBAT", 2f}, {"POISON", 2f}, {"default", 1f}}},
        {"INSECTE", new Dictionary<string, float>() {{"FEU", 0.5f}, { "COMBAT", 0.5f }, { "VOL", 0.5f }, { "SPECTRE", 0.5f }, { "PLANTE", 2f}, {"POISON", 2f}, { "PSY", 2f }, { "default", 1f}}},
        {"ROCHE", new Dictionary<string, float>() {{"COMBAT", 0.5f}, {"SOL", 0.5f}, {"FEU", 2f}, { "GLACE", 2f }, { "VOL", 2f }, { "INSECTE", 2f }, { "default", 1f}}},
        {"SPECTRE", new Dictionary<string, float>() {{"SPECTRE", 2f}, {"NORMAL", 0f}, { "PSY", 0f }, { "default", 1f}}},
        {"DRAGON", new Dictionary<string, float>() {{"DRAGON", 2f}, { "default", 1f}}},
    };

        if (multiplicateurs.ContainsKey(typePokemon))
        {
            if (multiplicateurs[typePokemon].ContainsKey(typeAdverse))
            {
                return multiplicateurs[typePokemon][typeAdverse];
            }
            else
            {
                return multiplicateurs[typePokemon]["default"];
            }
        }
        else
        {
            return 1f;
        }
    }

    static float CalculerMultiplicateur1(string type1Pokemon, string type1Adverse)
    {
        return CalculerMultiplicateur(type1Pokemon, type1Adverse);
    }

    static float CalculerMultiplicateur2(string type2Pokemon, string type2Adverse)
    {
        return CalculerMultiplicateur(type2Pokemon, type2Adverse);
    }

    static float CalculerDegatSubitPokemonJoueur(int pvPokemonJoueur, int atkPokemonAdverse)
    {
        float PvRestantPokemonJoueur = pvPokemonJoueur - atkPokemonAdverse;
        if (PvRestantPokemonJoueur < 0)
        {
            PvRestantPokemonJoueur = 0;
        }

        return PvRestantPokemonJoueur;
    }

    static float CalculerDegatSubitPokemonAdverse(int pvPokemonAdverse, int atkPokemonJoueur)
    {
        float PvRestantPokemonAdverse = pvPokemonAdverse - atkPokemonJoueur;
        if (PvRestantPokemonAdverse < 0)
        {
            PvRestantPokemonAdverse = 0;
        }

        return PvRestantPokemonAdverse;
    }
}
