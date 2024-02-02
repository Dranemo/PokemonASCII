using System;

public class Entity
{
    public int PositionX;
    public int PositionY;
}

public class NPC : Entity
{
    public string Name { get; set; }
    public string Dialogue { get; set; }
    public string Map { get; set; }
    public char Sprite { get; set; }
    public NPC(int positionX, int positionY, string name, string dialogue, string map, char sprite)
    {
        PositionX = positionX;
        PositionY = positionY;
        Name = name;
        Dialogue = dialogue;
        Map = map;
        Sprite = sprite;
    }
    public void DisplayMap()
    {
        string[] lines = File.ReadAllLines($"C:\\Users\\agathelier\\Desktop\\C-Pokemon\\pokemonConsole\\Assets\\Maps\\{Map}");
        Console.WriteLine("test");
        Thread.Sleep(1000);
        Console.SetCursorPosition(0, 0); // Réinitialisez la position du curseur au début

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

        Console.SetCursorPosition(0, lines.Length); // Déplacez le curseur après la fin de la carte

    }
}
public class Pokeball : Entity
{
    public Pokeball(int positionX, int positionY, int id_pokemon, string map)
    {
        Console.WriteLine(id_pokemon);
    }
}
