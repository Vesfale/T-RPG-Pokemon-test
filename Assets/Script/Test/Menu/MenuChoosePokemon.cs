using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public static class ButtonExtension
{
    public static void AddEventListener<T>(this Button button, T param, Action<T> OnClick)
    {
        button.onClick.AddListener (delegate() {
            OnClick(param);
        });
    }
}

public class MenuChoosePokemon : MonoBehaviour
{

    public GameObject MainObject;

    List<Pokemon> ListPokemonMenu;

    public void SelectPokemon()
    {
        Debug.Log("Wesh");
    }

    public void InitMenu(List<Pokemon> ListPokemon)
    {
        this.ListPokemonMenu = new List<Pokemon>(ListPokemon);
        this.DrawMenu();
    }

    public void DrawMenu()
    {
        GameObject g;
        
        foreach (Transform child in transform.Find("ScrollPokemon").transform) {
            GameObject.Destroy(child.gameObject);
        }
        foreach (Pokemon PokemonMenu in this.ListPokemonMenu)
            {
                g = Instantiate(EnumMenu.PokemonMenu,transform) as GameObject;
                g.transform.SetParent(transform.Find("ScrollPokemon").transform);
                
                //TODO traduction
                g.transform.Find("NomPokemonBack/NomPokemon").GetComponent <Text> ().text = PokemonMenu.Name[5];
                g.transform.Find("PokemonPortraitBack/PokemonPortrait").GetComponent <Image> ().sprite = Resources.Load<Sprite>("MobSpriteMenu/"+PokemonMenu.PokemonID+"/Normal");
                g.transform.Find("TypePokemon_1").GetComponent <Image> ().sprite = Resources.Load<Sprite>("TypeSprite/"+EnumType.UIType[PokemonMenu.Type1]);
                g.transform.Find("TypePokemon_2").GetComponent <Image> ().sprite = Resources.Load<Sprite>("TypeSprite/"+EnumType.UIType[PokemonMenu.Type2]);
                
                g.GetComponent<Button>().AddEventListener(PokemonMenu, PokemonClick);
            }
    }

    void PokemonClick(Pokemon PokemonMenu)
    {
        //Charger un pokemon
        MainObject.GetComponent<DungeonMain>().TestAddPokemonList(SelectTile.GetMousePosition(),PokemonMenu.PokemonID);;
        PokemonMenu.Log();
    }
}
