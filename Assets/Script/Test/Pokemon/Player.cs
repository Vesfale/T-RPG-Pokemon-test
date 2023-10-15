using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PokemonObject PlayerPokemon;

    // Start is called before the first frame update
    void Start()
    {
        this.PlayerPokemon = transform.GetComponent<PokemonObject>();
    }

    // Update is called once per frame
    void Update()
    {
        this.ChangeStatus();
        this.Movement();
    }

    void Movement()
    {
        Vector3 Direction = Vector3.zero;

        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1F)
            Direction.x = Input.GetAxisRaw("Horizontal");
        if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1F)
            Direction.y = Input.GetAxisRaw("Vertical");

        if (PlayerPokemon != null)
            PlayerPokemon.Movement(Direction);
    }
    
    void ChangeStatus()
    {
        if (Input.GetKeyDown(KeyCode.T))
            this.PlayerPokemon.SetAction(ActionType.Rotate);
        else if (Input.GetKeyDown(KeyCode.Y))
            this.PlayerPokemon.SetAction(ActionType.Idle);
        else if (Input.GetKeyDown(KeyCode.E))
            this.PlayerPokemon.SetAction(ActionType.Walk);
        else if (Input.GetKeyDown(KeyCode.R))
            this.PlayerPokemon.SetAction(ActionType.Attack);
    }
}
