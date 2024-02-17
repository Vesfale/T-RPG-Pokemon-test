using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


//ID_cavern == 0 : uniquement sur les Tiles à redefinir
//ID_cavern == -1 : Default setting
//ID_cavern >= 1 : Numéro de caverne

public struct TileDef
{
	public TypeTile Type;
	public SousTypeTile SousType;
	public int ID_cavern;
	public Color Color;
}

public class MapHandler
{
	public int FloorToWall = 4;
	public int WallToFloor = 3;

	System.Random rand = new System.Random();
	
	public TileDef[,] Map;
	public Dictionary<int, List<Vector2Int>> Caverns;
	public int ID_MainCavern;

	public int MapWidth			{ get; set; }
	public int MapHeight		{ get; set; }
	public int PercentAreWalls	{ get; set; }
	public List<CardinalPoint> ListExit	{ get; set; }

	public void CreateWay(CardinalPoint Point, CardinalPoint Dir, int Size)
	{
		Vector2Int Pos;
		Vector2Int PosStart = new Vector2Int();
		
		PosStart = this.PosByCardinalPoint(Point);
		Pos = NearestPoint(Point, PosStart);

	}

	public Vector2Int NearestPoint(CardinalPoint Point, Vector2Int PosStart) 
	{
		Vector2Int Pos = new Vector2Int();

		return Pos;
	}

	public Vector2Int PosByCardinalPoint(CardinalPoint Point)
	{
		Vector2Int Pos;
		
		switch (Point)
		{
			case CardinalPoint.North:
				Pos = new Vector2Int(this.MapWidth / 2, this.MapHeight);
				break;
			case CardinalPoint.NorthEast:
				Pos = new Vector2Int(this.MapWidth, this.MapHeight);
				break;
			case CardinalPoint.East:
				Pos = new Vector2Int(this.MapWidth, this.MapHeight / 2);
				break;
			case CardinalPoint.SouthEast:
				Pos = new Vector2Int(this.MapWidth, 0);
				break;
			case CardinalPoint.South:
				Pos = new Vector2Int(this.MapWidth / 2, 0);
				break;
			case CardinalPoint.SouthWest:
				Pos = new Vector2Int(0, 0);
				break;
			case CardinalPoint.West:
				Pos = new Vector2Int(0, this.MapHeight / 2);
				break;
			case CardinalPoint.NorthWest:
				Pos = new Vector2Int(0, this.MapHeight);
				break;
			default: 
				Pos = new Vector2Int(0, 0);
				break;
		}

		return Pos;
	}
	
	public void IdentifyCaverns()
	{
		int NbCaverns = 0;
		int Size = 0;
		int MaxSize = 0;

		Caverns = new Dictionary<int, List<Vector2Int>>();

		//On reset les cavernes
		for(int column=0, row=0; row <= MapHeight-1; row++)
		{
			for(column = 0; column <= MapWidth-1; column++){
				Map[column,row].ID_cavern = -1;
			}
		}

		for(int column=0, row=0; row <= MapHeight-1; row++)
		{
			for(column = 0; column <= MapWidth-1; column++){
				if ((Map[column,row].Type == TypeTile.Ground || Map[column,row].Type == TypeTile.Exit) && Map[column,row].ID_cavern == -1)
				{
					NbCaverns++;
					Caverns.Add(NbCaverns, new List<Vector2Int>());
					Size = TileCaverns(column,row,NbCaverns, 0);

					if (Size > MaxSize)
					{
						MaxSize = Size;
						this.ID_MainCavern = NbCaverns;
					}
				}
			}
		}
	}

	public int TileCaverns(int x,int y, int ID, int Size)
	{

		if ((Map[x,y].Type != TypeTile.Ground && Map[x,y].Type != TypeTile.Exit) || Map[x,y].ID_cavern != -1)
			return Size;
		Map[x,y].ID_cavern = ID;
		Size++;
		Caverns[ID].Add(new Vector2Int(x, y));

		if (this.MapHeight > y+1)
			Size = TileCaverns(x, y+1, ID, Size);
		if (y-1 >= 0)
			Size = TileCaverns(x, y-1, ID, Size);
		if (this.MapWidth > x+1)
			Size = TileCaverns(x+1, y, ID, Size);
		if (x-1 >= 0)
			Size = TileCaverns(x-1, y, ID, Size);

		return Size;
	}

	public void WallsBetweenCavern()
	{
		bool tmpRes = false;
		bool Res = false;
		List<Vector2Int> ListWall;

		foreach(KeyValuePair<int, List<Vector2Int>> Tiles in this.Caverns)
		{
			foreach(Vector2Int Pos in Tiles.Value)
			{
				ListWall = GetAdjacentWalls(Pos.x,Pos.y,1,1);
				foreach(Vector2Int PosWall in ListWall)
				{
					tmpRes = SearchOtherCaverns(PosWall.x, PosWall.y, 1, 0, Map[Pos.x, Pos.y].ID_cavern);
					tmpRes = SearchOtherCaverns(PosWall.x, PosWall.y, -1, 0, Map[Pos.x, Pos.y].ID_cavern);
					tmpRes = SearchOtherCaverns(PosWall.x, PosWall.y, 0, 1, Map[Pos.x, Pos.y].ID_cavern);
					tmpRes = SearchOtherCaverns(PosWall.x, PosWall.y, 0, -1, Map[Pos.x, Pos.y].ID_cavern);				

					if (Res != true)
						Res = tmpRes;
				}
			}
		}

		if (Res) {
			RandomFillMap(0);
			MakeCaverns(1, 0);
		}
	}

	public bool SearchOtherCaverns(int x,int y, int DirX, int DirY, int ID)
	{
		bool Res = false;
		
		if (IsOutOfBounds(x, y))
			return false;
		if (!IsWall(x, y) && Map[x, y].ID_cavern != ID)
			return true;
		if (IsWall(x, y)) {
			Res = SearchOtherCaverns(x + DirX, y + DirY, DirX, DirY, ID);
			if (Res == true)
				Map[x, y].ID_cavern = 0;
			return Res;
		}
		return false;
	}

	public void DeleteCaverns(int ID = -1)
	{
		foreach(KeyValuePair<int, List<Vector2Int>> Tiles in this.Caverns)
		{
			if ((ID != -1 && ID != Tiles.Key) || Tiles.Key == ID_MainCavern)
				continue;
			
			foreach(Vector2Int Pos in Tiles.Value)
				Map[Pos.x, Pos.y].Type = TypeTile.Wall;
		}

	}
	
	public void MakeCaverns(int nbTurn = 1, int ID = -1)
	{
		for (int nb=0; nb < nbTurn; nb++)
		{
			TileDef[,] MapCaverns = new TileDef[this.MapWidth,this.MapHeight];

			// By initilizing column in the outter loop, its only created ONCE
			for(int column=0, row=0; row <= MapHeight-1; row++)
			{
				for(column = 0; column <= MapWidth-1; column++)
				{
					if (ID == Map[column,row].ID_cavern || ID == -1) {
						MapCaverns[column,row] = InitTileDef(ID);
						MapCaverns[column,row].Type = PlaceWallLogic(column,row);
					}
					else
						MapCaverns[column,row] = Map[column,row];
				}
					
			}
			Map = MapCaverns;
		}
	}
	
	public TypeTile PlaceWallLogic(int x,int y)
	{
		int numWalls = GetAdjacentWalls(x,y,1,1).Count;

		
		if(Map[x,y].Type == TypeTile.Wall)
		{
			if(numWalls < WallToFloor)
				return TypeTile.Ground;
			else if(numWalls >= WallToFloor)
				return TypeTile.Wall;
		}
		else if (Map[x,y].Type == TypeTile.Ground)
			if(numWalls > FloorToWall)
				return TypeTile.Wall;
		return Map[x,y].Type;
	}
	
	public List<Vector2Int> GetAdjacentWalls(int x,int y,int scopeX,int scopeY)
	{
		int startX = x - scopeX;
		int startY = y - scopeY;
		int endX = x + scopeX;
		int endY = y + scopeY;
		
		int iX = startX;
		int iY = startY;
		
		List<Vector2Int> ListWall = new List<Vector2Int>();
		
		for(iY = startY; iY <= endY; iY++) {
			for(iX = startX; iX <= endX; iX++)
			{
				if(!(iX==x && iY==y))
				{
					if(IsWall(iX,iY))
						ListWall.Add(new Vector2Int(iX, iY));
				}
			}
		}
		return ListWall;
	}
	
	bool IsWall(int x,int y)
	{
		// Consider out-of-bound a wall
		if(IsOutOfBounds(x, y))
			return true;
		if(Map[x, y].Type == TypeTile.Wall)
			return true;
		if(Map[x, y].Type == TypeTile.Ground || Map[x, y].Type == TypeTile.Exit)
			return false;

		return false;
	}
	
	bool IsOutOfBounds(int x, int y)
	{
		if( x<0 || y<0 )
			return true;
		else if( x>MapWidth-1 || y>MapHeight-1 )
			return true;
		return false;
	}
	
	public Dictionary<Vector3Int, TileDef> MapToVectorMap()
	{
		Dictionary<Vector3Int, TileDef> ListTile = new Dictionary<Vector3Int, TileDef>();
		
		for(int column=0,row=0; row < MapHeight; row++ ) {
			for( column = 0; column < MapWidth; column++ ) {
				ListTile.Add(new Vector3Int(column, row, 0), Map[column,row]);
			}
		}
		return ListTile;
	}
	
	public void RandomFillMap(int ID = -1)
	{
		// New, empty map
		bool setTile = false;
		
		if (ID == -1)
			Map = new TileDef[MapWidth,MapHeight];

		for(int column=0,row=0; row < MapHeight; row++) {
			for(column = 0; column < MapWidth; column++)
			{
				setTile = false;
				if ((ID != -1 && Map[column,row].ID_cavern == ID) || ID == -1)
					setTile = true;

				if (setTile == true) {
					Map[column,row] = InitTileDef(ID);
					// If coordinants lie on the the edge of the map (creates a border)
					if(column == 0 || row == 0 || column == MapWidth-1 || row == MapHeight-1)
						Map[column,row].Type = TypeTile.Wall;
					else // Else, fill with a wall a random percent of the time
						Map[column,row].Type = RandomPercent(PercentAreWalls);
				}
			}
		}

		//Pos Exit
		if (ID == -1)
			SearchExit();
	}

	private void SearchExit()
	{
		int ColumnExit;
		int RowExit;
		int Size = 3;

		foreach(CardinalPoint Exit in this.ListExit){
			switch (Exit)
			{
				case CardinalPoint.North:
					RowExit = MapHeight;
					ColumnExit = rand.Next(1,MapWidth-1-Size);
					SetExit(ColumnExit, RowExit-1, ColumnExit, RowExit-Size, Size, 0);
					break;
				case CardinalPoint.East:
					RowExit = rand.Next(1,MapHeight-1-Size);
					ColumnExit = MapWidth;
					SetExit(ColumnExit-1, RowExit, ColumnExit-Size, RowExit, 0, Size);
					break;
				case CardinalPoint.South:
					RowExit = 0;
					ColumnExit = rand.Next(1,MapWidth-1-Size);
					SetExit(ColumnExit, RowExit, ColumnExit, RowExit, Size, 0);
					break;
				case CardinalPoint.West:
					RowExit = rand.Next(1,MapHeight-1-Size);
					ColumnExit = 0;
					SetExit(ColumnExit, RowExit, ColumnExit, RowExit, 0, Size);
					break;
				default: 
					break;
			}
		}
	}

	private void SetExit(int ColumnExit, int RowExit, int Column, int Row, int SizeColumn, int SizeRow)
	{
		for(int SizeAhead = 0; SizeAhead < SizeColumn; SizeAhead++) {
			for(int SizeExit = 0; SizeExit < SizeColumn; SizeExit++){
				if (Column+SizeExit < MapWidth && RowExit == Row+SizeAhead && Row+SizeAhead < MapHeight)
					Map[Column+SizeExit,Row+SizeAhead].Type = TypeTile.Exit;
				else if (Column+SizeExit < MapWidth && Row+SizeAhead < MapHeight)
					Map[Column+SizeExit,Row+SizeAhead].Type = TypeTile.Ground;
			}
		}

		for(int SizeAhead = 0; SizeAhead < SizeRow; SizeAhead++) {
			for(int SizeExit = 0; SizeExit < SizeRow; SizeExit++){
				if (Row+SizeExit < MapHeight && ColumnExit == Column+SizeAhead && Column+SizeAhead < MapWidth)
					Map[Column+SizeAhead,Row+SizeExit].Type = TypeTile.Exit;
				else if (Row+SizeExit < MapHeight && ColumnExit+SizeAhead < MapWidth)
					Map[Column+SizeAhead,Row+SizeExit].Type = TypeTile.Ground;
			}
		}
	}

	TypeTile RandomPercent(int percent)
	{
		if(percent>=rand.Next(1,101))
			return TypeTile.Wall;
		return TypeTile.Ground;
	}

	TileDef InitTileDef(int ID = -1)
	{
		TileDef Tile;

		Tile.Type = TypeTile.None;
		Tile.SousType = SousTypeTile.None;
		Tile.ID_cavern = ID;
		Tile.Color = Color.white;

		return Tile;
	}
	
	public MapHandler(int mapWidth, int mapHeight, int percentWalls=40, List<CardinalPoint> Exit = default)
	{
		this.MapWidth = mapWidth;
		this.MapHeight = mapHeight;
		this.PercentAreWalls = percentWalls;
		this.ID_MainCavern = -1;
		if (Exit == null) 
			this.ListExit = new List<CardinalPoint>();
		else
			this.ListExit = new List<CardinalPoint>(Exit);
		this.Map = new TileDef[this.MapWidth,this.MapHeight];
		this.Caverns = new Dictionary<int, List<Vector2Int>>();
	}
}