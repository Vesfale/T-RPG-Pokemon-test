using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardinalPoint
{
    South = 7,
    SouthEast = 6,
    East = 5,
    NorthEast = 4,
    North = 3,
    NorthWest = 2,
    West = 1,
    SouthWest = 0
}


public enum TypeTile
{
    None = -1,
    Ground = 0,
    Wall = 1,
    Water = 2
}

public enum SousTypeTile
{
    None = -1,
    Exit = 0,
    Entry = 1,
    Spawn = 2,
    MobSpawn = 3,
    Trainer = 4
}

public class EnumMap 
{
    public static int limit = 3;
    public static string Alt = "Alt";

    public static Dictionary<TypeTile, string> TypeSprite = new Dictionary<TypeTile, string>()
    {
        {TypeTile.Ground, "Ground"},
        {TypeTile.Wall, "Wall"},
        {TypeTile.Water, "Water"}
    };

    //int output = Convert.ToInt32(input, 2);
    public static Dictionary<string, string> SpriteNumber = new Dictionary<string, string>()
    {
        //1
        {"000011011", "0"},
        {"000111111", "1"},
        {"100111111", "1"}, //Not Exist Manual
        {"001111111", "1"}, //Not Exist Manual
        {"000110110", "2"},

        {"011011011", "3"},
        {"111011011", "3"}, //Not Exist Manual
        {"011011111", "3"}, //Not Exist Manual
        {"111111111", "4"},
        {"110110110", "5"},
        {"111110110", "5"}, //Not Exist Manual
        {"110110111", "5"}, //Not Exist Manual

        {"011011000", "6"},
        {"111111000", "7"},
        {"111111100", "7"}, //Not Exist Manual
        {"111111001", "7"}, //Not Exist Manual
        {"110110000", "8"},

        //2
        {"000011010", "9"},
        {"000111000", "10"},
        {"000110010", "11"},

        {"010010010", "12"},
        {"000010000", "13"},
        
        {"010011000", "15"},
        {"010110000", "17"},

        //3
        {"000010010", "19"},
        
        {"000011000", "21"},
        {"010111010", "22"},
        {"000110000", "23"},
        
        {"010010000", "25"},

        //4
        {"000111010", "28"},

        {"010011010", "30"},
        {"010110010", "32"},

        {"010111000", "34"},

        //5
        {"111111010", "37"},
        
        {"110111110", "39"},
        {"011111011", "41"},

        {"010111111", "43"},

        //6
        {"111111110", "45"},
        {"111111011", "46"},

        {"110111111", "48"},
        {"011111111", "49"},

        //7
        {"011011010", "51"},
        {"110110010", "52"},

        {"010011011", "54"},
        {"010110110", "55"},

        //8
        {"000111110", "57"},
        {"000111011", "58"},

        {"110111000", "60"},
        {"011111000", "61"},

        //9
        {"010111011", "63"},
        {"010111110", "64"},

        {"011111010", "66"},
        {"110111010", "67"},

        //10
        {"011111110", "69"},
        {"110111011", "70"}
    };
}
