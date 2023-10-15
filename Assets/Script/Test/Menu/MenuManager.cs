using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject menu; // Assign in inspector
    public GameObject ChoosePokemonMenu; // Assign in inspector
    public GameObject GenerateMapMenu; // Assign in inspector
    public GameObject Random; // Assign in inspector
    public bool isShowing;


    // Start is called before the first frame update
    void Start()
    {
        isShowing = true;
    }

    // Update is called once per frame
    void Update()
    {
        ShowMenu();
        MenuHandleOptions();         
    }

    public void ShowMenu()
    {
       if (Input.GetKeyDown(KeyCode.Escape)) {
             isShowing = !isShowing;
             transform.GetComponent<CameraHandler>().HandleShowMenu();
             menu.SetActive(isShowing);
        }
    }

    public void MenuHandleOptions() {
        if (isShowing) {
            if (Input.GetKeyDown(KeyCode.Keypad1))
                ShowChoosePokemon();
            if (Input.GetKeyDown(KeyCode.Keypad2))
                ShowGenerateMap();
        }
    }

    public void ShowChoosePokemon()
    {
        GenerateMapMenu.SetActive(false);
        ChoosePokemonMenu.SetActive(true);
        ChoosePokemonMenu.GetComponent<MenuChoosePokemon>().DrawMenu();
    }
    public void ShowGenerateMap()
    {
        ChoosePokemonMenu.SetActive(false);
        GenerateMapMenu.SetActive(true);
    }

    
    public void ShowRandomPokemon()
    {
        Random.GetComponent<DungeonMain>().TestAddPokemonMenuList();
    }
}
