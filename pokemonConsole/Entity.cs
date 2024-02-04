using pokemonConsole;
using System;
using Usefull;

public class Entity
{
    public string name { get; set; }

    public int PositionX { get; set; }
    public int PositionY { get; set; }
    public char sprite { get; set; }

    public string map { get; set; }
    public char actuallPositionChar { get; set;  }
}

public class NPC : Entity
{
    public string dialogue { get; private set; }


    public NPC(string name_, string dialogue_, char sprite_, string map_, int x, int y, char actualPosition)
    {
        name = name_;
        dialogue = dialogue_;
        sprite = sprite_;

        map = map_;

        PositionX = x;
        PositionY = y;

        actuallPositionChar = actualPosition;
    }
}

public class Pokeball : Entity
{
    public int id_pokemon;

    public Pokeball(int id_, string map_, int x, int y, char actualPosition)
    {
        name = Pokemon.GetNom(id_);
        id_pokemon = id_;
        sprite = '°';

        map = map_;

        PositionX = x;
        PositionY = y;

        actuallPositionChar = actualPosition;
    }
}
