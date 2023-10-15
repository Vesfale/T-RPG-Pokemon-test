using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Linq;

public class PokemonObject : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;    
    private AnimDataPokemon AnimSprite;
    public string Pokemon_ID;
    public LayerMask WhatStopMovement;
    public Transform MovePoint;
    public bool InitPos = true;
    
    //private Vector3Int Position;
    

    public void InitPokemon(GameObject NewPokemon, Vector3Int Spawn, string ID = "0000")
    {
        this.Pokemon_ID = ID;
        //this.Position = Spawn;
    }

    // Start is called before the first frame update
    void Start()
    {
        MovePoint.parent = null;
        this.spriteRenderer = transform.Find("SpritePokemon").GetComponent<SpriteRenderer>();
        transform.Find("SpritePokemon").position += (Vector3.one * 0.5F);

        string XmlDoc = Resources.Load<TextAsset>("MobSprite/"+this.Pokemon_ID+"/AnimData").ToString();
        AnimSprite = AnimDataPokemon.FromXml(XDocument.Parse(XmlDoc));
        AnimSprite.InitAnimData();
        AnimSprite.SetAsset(this.Pokemon_ID);
        AnimSprite.SetAction(ActionType.Idle);
        AnimSprite.SetDirection(CardinalPoint.South);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, MovePoint.position, EnumPokemon.MoveSpeed * Time.deltaTime);
        AnimSprite.Update();
        spriteRenderer.sprite = AnimSprite.GetCurrentSpriteAnim();
    }

    void OnMouseOver()
    {
        Debug.Log(Pokemon_ID);
    }

    void OnMouseDown()
    {
        if(InitPos == false) {
            InitPos = true;
            MovePoint.position = transform.position;
        }
        if (GameObject.Find("Main").GetComponent<DungeonMain>().OnMouseDown(this))
            Playable(!transform.GetComponent<Player>().enabled);
    }

    public void Playable(bool Playable)
    {
        transform.GetComponent<Player>().enabled = Playable;
    }

    public void Movement(Vector3 Direction)
    {
        if (Vector3.Distance(transform.position, MovePoint.position) <= 0.05F)
        {
            if (!Physics2D.OverlapCircle(MovePoint.position + Direction+ ((Vector3.up + Vector3.right) * 0.5F), 0.2F, WhatStopMovement))
                MovePoint.position += Direction;
            if (EnumPokemon.Direction.ContainsKey(Direction))
            {
                AnimSprite.SetAction(ActionType.Walk);
                AnimSprite.SetDirection(EnumPokemon.Direction[Direction]);
            }
        }
    }

    public void SetAction(string Type)
    {
        AnimSprite.SetAction(Type);
    }

    public static PokemonObject Create(Vector3Int Spawn, string ID = "0000")
    {
        PokemonObject CurrentPokemon;
        GameObject NewPokemon = Instantiate(EnumPokemon.Prefab, (Vector3)Spawn, Quaternion.identity) as GameObject;
        
        CurrentPokemon = NewPokemon.GetComponent<PokemonObject>();
        CurrentPokemon.InitPokemon(NewPokemon, Spawn, ID);

        return CurrentPokemon;
    }
}
