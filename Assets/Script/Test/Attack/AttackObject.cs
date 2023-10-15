using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackObject : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;    
    private AnimDataAttack AnimSprite;

    public string AttackID;
    public string AttackSubID;

    public void InitAttack(GameObject NewPokemon, Vector3Int Spawn, string ID = "0002", string SubID = "000")
    {
        this.AttackID = ID;
        this.AttackSubID = SubID;
        //this.Position = Spawn;
    }

    void Start()
    {
        this.spriteRenderer = transform.Find("SpriteAttack").GetComponent<SpriteRenderer>();
        transform.Find("SpriteAttack").position += (Vector3.one * 0.5F);

        AnimSprite = new AnimDataAttack();
        AnimSprite.SetAsset(this.AttackID, this.AttackSubID);
    }

    void LateUpdate()
    {
        AnimSprite.Update();
        spriteRenderer.sprite = AnimSprite.GetCurrentSpriteAnim();
    }

    public static AttackObject Create(Vector3Int Spawn, string ID = "0002", string SubID = "000")
    {
        AttackObject CurrentAttack;
        GameObject NewAttack = Instantiate(EnumAttack.Prefab, (Vector3)Spawn, Quaternion.identity) as GameObject;
        
        CurrentAttack = NewAttack.GetComponent<AttackObject>();
        CurrentAttack.InitAttack(NewAttack, Spawn, ID, SubID);

        return CurrentAttack;
    }

}
