using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTestMap : MonoBehaviour
{
    Map Dungeon;

    // Start is called before the first frame update
    void Start()
    {
        Dungeon = new Map();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void NewMap()
    {
        Dungeon.ClearMap();
        Dungeon.MakeMap(50, 50, 40);
    }    

    public void NewFreshMap()
    {
        Dungeon.ClearMap();
        Dungeon.Genere(50, 50, 40);
    }

    public void DeleteCaverns()
    {
        Dungeon.DeleteCaverns();
        Dungeon.ClearMap();
        Dungeon.DrawMap();
    }
    
    public void SelectWall()
    {
        Dungeon.ConnectCaverns();
        Dungeon.ClearMap();
        Dungeon.DrawMap();
    }

}
