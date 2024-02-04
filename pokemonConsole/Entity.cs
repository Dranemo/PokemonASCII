using System;
using Usefull;

public class Entity
{
    public int PositionX { get; set; }
    public int PositionY { get; set; }
}

public class NPC : Entity
{
    public string Name { get; set; }
    public string Dialogue { get; set; }
    public string Map { get; set; }
    public char Sprite { get; set; }


    public NPC(string name, string dialogue, string map, char sprite)
    {
        Name = name;
        Dialogue = dialogue;
        Map = map;
        Sprite = sprite;
    }


    public void DisplayMap()
    {

        string[] lines = File.ReadAllLines($"{AdresseFile.FileDirection}Assets\\Maps\\{Map}");
        Console.WriteLine("test");
        Thread.Sleep(1000);
        Console.SetCursorPosition(0, 0); // Reinitialisez la position du curseur au debut

        for (int i = 0; i < lines.Length; i++)
        {
            char[] chars = lines[i].ToCharArray();

            if (i == PositionY)
            {
                Console.SetCursorPosition(PositionX, i);
                Console.Write(Sprite);
            }
            else
            {
                Console.SetCursorPosition(0, i);
                Console.Write(new string(chars));
            }
        }

        Console.SetCursorPosition(0, lines.Length); // Deplacez le curseur après la fin de la carte

    }
}





public class Pokeball : Entity
{
    public Pokeball(int positionX, int positionY, int id_pokemon, string map)
    {
        Console.WriteLine(id_pokemon);
    }
}
