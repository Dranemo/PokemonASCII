using pokemonConsole;
using System;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using Usefull;

class Entity
{
    public string name { get; set; }

    public int PositionX { get; set; }
    public int PositionY { get; set; }
    public char sprite { get; set; }

    public string map { get; set; }
    public char actuallPositionChar { get; set;  }



    public virtual void Function(Player player) { }

    protected Entity(string name, int positionX, int positionY, char sprite, string map, char actuallPositionChar)
    {
        this.name = name;
        PositionX = positionX;
        PositionY = positionY;
        this.sprite = sprite;
        this.map = map;
        this.actuallPositionChar = actuallPositionChar;
    }
}

class Pokeball : Entity
{
    public int id_pokemon;
    public bool taken;

    public Pokeball(int id_, string map_, int x, int y, char actualPosition) : base(Pokemon.GetNom(id_), x, y, 'o', map_, actualPosition)
    {
        id_pokemon = id_;
        taken = false;
    }

    public override void Function(Player player) 
    {
        player.addPokemonToParty(new Pokemon(id_pokemon, 5, player.id, 1, player.id, player.name));
        taken = true;
    }
}
