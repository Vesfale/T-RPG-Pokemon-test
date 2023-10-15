using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnumSelect
{
    public static string SpriteFolder = "ObejctSprite";
    public static string SpriteSelect = "Select";
    public static string SelectEmpty = "Select_0";
    public static string SelectObject = "Select_1";
}

public class SelectTile
{
    static Grid Grid = GameObject.Find("Grid").GetComponent<Grid>();
    static Tilemap Select = GameObject.Find("Select").GetComponent<Tilemap>();

    string TextureName;

    public bool Active;

    Dictionary<string, Sprite> SpriteMap;

    Vector3Int PositionOver;

    public SelectTile()
    {
        this.SetMapSprite(EnumSelect.SpriteSelect);
        this.Active = true;
    }


    public void MouseOver(List<PokemonObject> ListPokemon)
    {
        Debug.Log("Active = "+ListPokemon);
        if (this.Active == true)
        {
            Vector3Int Position = GetMousePosition();
            Tile Tile = ScriptableObject.CreateInstance<Tile>();
            
            if (ListPokemon.Find(x => x.transform.position.Equals(Position)) == null)
                Tile.sprite = this.SpriteMap[EnumSelect.SelectEmpty];
            else
                Tile.sprite = this.SpriteMap[EnumSelect.SelectObject];
            if (!(PositionOver == null))
                if (Position != PositionOver)
                    Select.SetTile(this.PositionOver, null);
            Select.SetTile(Position, Tile);
            this.PositionOver = Position;
        }
        else
            Select.SetTile(this.PositionOver, null);

    }

    public void SetMapSprite(string Texture)
    {
        Sprite[] SpriteMapTile;

        this.TextureName = Texture;
        SpriteMapTile = Resources.LoadAll<Sprite>(EnumSelect.SpriteFolder+"/"+this.TextureName);
        
        this.SpriteMap = new Dictionary<string, Sprite>();
        this.SpriteMap.Clear();
        foreach(Sprite SpriteTile in SpriteMapTile)
            this.SpriteMap[SpriteTile.name] = SpriteTile;
    }

    public static Vector3Int GetMousePosition () {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return Grid.WorldToCell(mouseWorldPos);
    }

    public static void FollowMouse(ref PokemonObject MyObject)
    {
        MyObject.transform.position = GetMousePosition();
    }

}
