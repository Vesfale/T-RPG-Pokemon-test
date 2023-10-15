using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMap
{
    Vector3Int MinPos;
    Vector3Int MaxPos;
    Dictionary<Vector3Int, char> Dungeon;

    public GenerateMap ()
    {
        MinPos = new Vector3Int(int.MaxValue, int.MaxValue, 0);
        MaxPos = new Vector3Int(int.MinValue, int.MinValue, 0);

        Dungeon = new Dictionary<Vector3Int, char>();
    }

    public void CreateRoom(int x, int y, int width, int height)
    {
        Vector3Int PosTile;

        for (int i = x; i <= (x + width); i++)
        {
            for (int j = y; j <= (y + height); j++)
            {
                PosTile = new Vector3Int(i, j, 0);
                Dungeon[PosTile] = '0';
                
                //DOTO
                MinPos = Vector3Int.Min(MinPos, PosTile);
                MaxPos = Vector3Int.Max(MaxPos, PosTile);
            }
        }
    }

    public void CreateLimitMap()
    {
        Vector3Int PosTile;

        MinPos = MinPos + (Vector3Int.down * EnumMap.limit) + (Vector3Int.left * EnumMap.limit);
        MaxPos = MaxPos + (Vector3Int.up * EnumMap.limit) + (Vector3Int.right * EnumMap.limit);
        for (int i = MinPos.x; i <= (MaxPos.x); i++)
        {
            for (int j = MinPos.y; j <= MaxPos.y; j++)
            {
                PosTile = new Vector3Int(i, j, 0);
                if (!Dungeon.ContainsKey(PosTile))
                    Dungeon[PosTile] = '1';
            }
        }
    }

    public Dictionary<Vector3Int, char> CreateDungeon()
    {
        
        this.CreateRoom(-10,-4,19,7);
        // Voir pour utiliser la création de limite au début pour éviter de vérifier si ça existe
        this.CreateLimitMap();
        return Dungeon;
    }
}
