using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class DungeonMain : MonoBehaviour
{
    private bool isShowing;


    Map Dungeon;
    SelectTile SelectMap;
    List<PokemonObject> ListPokemon;
    List<Pokemon> ListPokemonMenu;
    PokemonObject Player1;

    // Start is called before the first frame update
    void Start()
    {
        Dungeon = new Map();
        SelectMap = new SelectTile();
        ListPokemon = new List<PokemonObject>();

        //TestAddPokemonList();

        ListPokemonMenu = new List<Pokemon>();
        TestAddPokemonMenuList();
    }

    public void TestAddPokemonList()
    {
        ListPokemon.Add(PokemonObject.Create(new Vector3Int(0, 0, 0), "0006"));
        ListPokemon.Add(PokemonObject.Create(new Vector3Int(2, 0, 0), "0003"));
        ListPokemon.Add(PokemonObject.Create(new Vector3Int(4, 0, 0), "0009"));
        ListPokemon.Add(PokemonObject.Create(new Vector3Int(6, 0, 0), "0007"));

        Player1 = ListPokemon.Find(x => x.Pokemon_ID.Equals("0006"));
        Player1.Playable(true);
    }

    public void TestAddPokemonList(Vector3Int MousePos, string ID_Pokemon)
    {
        
        ListPokemon.Add(PokemonObject.Create(MousePos, ID_Pokemon));
        if(Player1 != null)
        {
            if (Player1.InitPos == false)
            {
                ListPokemon.Remove(Player1);
                Destroy(Player1.MovePoint.gameObject);
                Destroy(Player1.gameObject);
            }
            Player1.Playable(false);
        }
        Player1 = ListPokemon.Last();
        Player1.InitPos = false;
        Player1.Playable(true);
    }

    public void TestAddPokemonMenuList()
    {  
        int ID_Pokemon = 0;
        System.Random rand = new System.Random();

        ListPokemonMenu.Clear();
        ID_Pokemon = rand.Next(1,493);
        ListPokemonMenu.Add(new Pokemon(ID_Pokemon.ToString().PadLeft(4, '0')));
        ID_Pokemon = rand.Next(1,493);
        ListPokemonMenu.Add(new Pokemon(ID_Pokemon.ToString().PadLeft(4, '0')));
        ID_Pokemon = rand.Next(1,493);
        ListPokemonMenu.Add(new Pokemon(ID_Pokemon.ToString().PadLeft(4, '0')));
        ID_Pokemon = rand.Next(1,493);
        ListPokemonMenu.Add(new Pokemon(ID_Pokemon.ToString().PadLeft(4, '0')));
        ID_Pokemon = rand.Next(1,493);
        ListPokemonMenu.Add(new Pokemon(ID_Pokemon.ToString().PadLeft(4, '0')));
        ID_Pokemon = rand.Next(1,493);
        ListPokemonMenu.Add(new Pokemon(ID_Pokemon.ToString().PadLeft(4, '0')));
        ID_Pokemon = rand.Next(1,493);
        ListPokemonMenu.Add(new Pokemon(ID_Pokemon.ToString().PadLeft(4, '0')));
        ID_Pokemon = rand.Next(1,493);
        ListPokemonMenu.Add(new Pokemon(ID_Pokemon.ToString().PadLeft(4, '0')));
        ID_Pokemon = rand.Next(1,493);
        ListPokemonMenu.Add(new Pokemon(ID_Pokemon.ToString().PadLeft(4, '0')));

        isShowing = GameObject.Find("ChoosePokemon");
        if (GameObject.Find("ChoosePokemon"))
            GameObject.Find("ChoosePokemon").GetComponent<MenuChoosePokemon>().InitMenu(ListPokemonMenu);
    }


    // Update is called once per frame
    void Update()
    {
        SelectMap.MouseOver(ListPokemon);
        if(Player1 != null)
        {
            if (Player1.InitPos == false) {
                SelectTile.FollowMouse(ref Player1);
            }
        }
    }

    public bool OnMouseDown(PokemonObject SelectedPokemon)
    {
        Player1.Playable(false);
        Player1 = SelectedPokemon;

        return SelectMap.Active;
    }
}
