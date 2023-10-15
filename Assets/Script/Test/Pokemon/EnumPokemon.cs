using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class EnumPokemon
{
    public static float MoveSpeed = 4F;

    public static Dictionary<Vector3, CardinalPoint> Direction = new Dictionary<Vector3, CardinalPoint>()
    {
        {new Vector3(0F, -1F, 0F), CardinalPoint.South},
        {new Vector3(1F, -1F, 0F), CardinalPoint.SouthEast},
        {new Vector3(1F, 0F, 0F), CardinalPoint.East},
        {new Vector3(1F, 1F, 0F), CardinalPoint.NorthEast},
        {new Vector3(0F, 1F, 0F), CardinalPoint.North},
        {new Vector3(-1F, 1F, 0F), CardinalPoint.NorthWest},
        {new Vector3(-1F, 0F, 0F), CardinalPoint.West},
        {new Vector3(-1F, -1F, 0F), CardinalPoint.SouthWest}

    };

    public static Object Prefab = Resources.Load("Prefabs/test/Pokemon");
}

class ActionType
{
    public static string Rotate = "Rotate";
    public static string Attack = "Attack";
    public static string Walk = "Walk";
    public static string Idle = "Idle";
    public static int Tick = 1;
    public static int TickAttack = 4;
}
