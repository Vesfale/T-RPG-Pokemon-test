using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine; 
using UnityEngine.Tilemaps;

public class Map
{
    string TextureName;
    Tilemap Ground;
    Tilemap Wall;
    Tilemap Water;
    Dictionary<string, Sprite> SpriteMap;
    Dictionary<Vector3Int, TileDef> ListTile;

    MapHandler Dungeon;

    // Start is called before the first frame update
    public Map()
    {
        this.SetTileMap();
        //this.SetMapSprite("Treeshroud Forest");
        this.SetMapSprite("Station Pass");
    }

    public void SetTileMap()
    {
        this.Ground = GameObject.Find("Ground").GetComponent<Tilemap>();
        this.Wall = GameObject.Find("Wall").GetComponent<Tilemap>();
        this.Water = GameObject.Find("Water").GetComponent<Tilemap>();
    }

    public void SetMapSprite(string Texture)
    {
        Sprite[] SpriteMapTile;

        this.TextureName = Texture;
        SpriteMapTile = Resources.LoadAll<Sprite>("MapSprite/"+this.TextureName);
        
        this.SpriteMap = new Dictionary<string, Sprite>();
        this.SpriteMap.Clear();
        foreach(Sprite SpriteTile in SpriteMapTile)
            this.SpriteMap[SpriteTile.name] = SpriteTile;
    }

    public void MakeMap(int Width, int Height, int PercentWalls)
    {  
        Dungeon = new MapHandler(Width, Height, PercentWalls);
        Dungeon.RandomFillMap();
        Dungeon.MakeCaverns(2);

        this.DrawMap();
    }

    public void Genere(int Width, int Height, int PercentWalls, List<CardinalPoint> ListExit = default)
    {  
        Dungeon = new MapHandler(Width, Height, PercentWalls, ListExit);
        Dungeon.RandomFillMap();
        Dungeon.MakeCaverns(2);
        this.ConnectCaverns();
        //this.DeleteCaverns();
        this.DrawMap();
    }


    public void DeleteCaverns()
    {
        if (Dungeon != null)
        {
            Dungeon.IdentifyCaverns();
            Dungeon.DeleteCaverns();
        }
    }

    public void ConnectCaverns()
    {
        if (Dungeon != null)
        {
            Dungeon.IdentifyCaverns();
            Dungeon.WallsBetweenCavern();
        }
    }

    public char SameTileType(Vector3Int PosTile, TypeTile TileType)
    {
        if (this.ListTile.ContainsKey(PosTile))
            if (this.ListTile[PosTile].Type == TileType)
                return '1';
        return '0';
    }

    public void ClearMap()
    {
        if (this.ListTile != null)
        {
            foreach (KeyValuePair<Vector3Int, TileDef> Tile in this.ListTile)
            {
                this.Ground.SetTile(Tile.Key, null);
                this.Water.SetTile(Tile.Key, null);
                this.Wall.SetTile(Tile.Key, null);
            }
        }
         
   }


    public void DrawMap()
    {
        string FormatSprite;
        string FormatSpriteReal;
        string FormatSpriteLow;
        char SameTile;
        
        this.ListTile = Dungeon.MapToVectorMap();

        foreach (KeyValuePair<Vector3Int, TileDef> Tile in this.ListTile)
        {
            FormatSprite = "";
            FormatSpriteLow = "";

            SameTile = this.SameTileType(Tile.Key + Vector3Int.up + Vector3Int.left, Tile.Value.Type);
            FormatSprite += SameTile;
            FormatSpriteLow += '0';
            SameTile = this.SameTileType(Tile.Key + Vector3Int.up, Tile.Value.Type);
            FormatSprite += SameTile;
            FormatSpriteLow += SameTile;
            SameTile = this.SameTileType(Tile.Key + Vector3Int.up + Vector3Int.right, Tile.Value.Type);
            FormatSprite += SameTile;
            FormatSpriteLow += '0';
            SameTile = this.SameTileType(Tile.Key + Vector3Int.left, Tile.Value.Type);
            FormatSprite += SameTile;
            FormatSpriteLow += SameTile;
            FormatSprite += '1'; // Tile.Key + Vector3Int.zero
            FormatSpriteLow += '1'; // Tile.Key + Vector3Int.zero
            SameTile = this.SameTileType(Tile.Key + Vector3Int.right, Tile.Value.Type);
            FormatSprite += SameTile;
            FormatSpriteLow += SameTile;
            SameTile = this.SameTileType(Tile.Key + Vector3Int.down + Vector3Int.left, Tile.Value.Type);
            FormatSprite += SameTile;
            FormatSpriteLow += '0';
            SameTile = this.SameTileType(Tile.Key + Vector3Int.down, Tile.Value.Type);
            FormatSprite += SameTile;
            FormatSpriteLow += SameTile;
            SameTile = this.SameTileType(Tile.Key + Vector3Int.down + Vector3Int.right, Tile.Value.Type);
            FormatSprite += SameTile;
            FormatSpriteLow += '0';

            FormatSpriteReal = this.GetFormatSprite(FormatSprite);
            if(EnumMap.SpriteNumber.ContainsKey(FormatSpriteReal))
                this.DrawTile(Tile.Key, Tile.Value, FormatSpriteReal);
            else
                this.DrawTile(Tile.Key, Tile.Value, FormatSpriteLow);
        }
    }

    public void DrawTile(Vector3Int Position, TileDef Tile, string FormatSpriteReal)
    {
        string TypeSprite = EnumMap.TypeSprite[Tile.Type];
        Tile TileObject = ScriptableObject.CreateInstance<Tile>();

        if (Tile.Type == TypeTile.Exit)
            TileObject.color = Color.red;
        else
            TileObject.color = Tile.Color;
        TileObject.sprite = this.SpriteMap[this.TextureName+"_"+TypeSprite+"_"+EnumMap.SpriteNumber[FormatSpriteReal]];
        if (TypeSprite == EnumMap.TypeSprite[TypeTile.Ground] || TypeSprite == EnumMap.TypeSprite[TypeTile.Exit])
            this.Ground.SetTile(Position, TileObject);
        else if (TypeSprite == EnumMap.TypeSprite[TypeTile.Water])
            this.Water.SetTile(Position, TileObject);
        else
            this.Wall.SetTile(Position, TileObject);
    }

    public string GetFormatSprite(string FormatSpriteReal)
    {
        string FormatSprite = FormatSpriteReal;
        char[] EacheCase = FormatSpriteReal.ToCharArray();
        bool Res = true;
        int MaxSame = 0;
        int Same = 0;
        int Pos = 0;
        
        foreach (KeyValuePair<string, string> Format in EnumMap.SpriteNumber) {
            Pos = 0;
            Same = 0;
            Res = true;
            foreach(int c in Format.Key) {
                if (c == '0'){
                    Pos++;
                    continue;
                }
                if (EacheCase[Pos] != c) {
                    Res = false;
                    break;
                }
                else
                    Same++;
                Pos++;
            }

            if (Res == true && MaxSame < Same) {
                MaxSame = Same;
                FormatSprite = Format.Key;
            }
        }

        return FormatSprite;
    }
}
