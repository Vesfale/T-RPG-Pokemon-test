using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimAttack : Anim
{
    
    private SpriteRenderer spriteRenderer;   
    private Sprite[] SpriteAnim;

    public AnimAttack()
    {
    }

    override
    public void SetAsset(string AttackID, string AttackSubID)
    {
        SpriteAnim = Resources.LoadAll<Sprite>("AttackSprite/move_VFX/"+AttackID+"/"+AttackSubID+"/");
    }

    override
    public bool AnimationEnd(int Frame)
    {
        if (Frame >= (SpriteAnim.Length))
            return true;
        return false;
    }

    override
    public bool DurationEnd(int Frame, int DurationFrame)
    {
        return true;
    }

    override
    public Sprite GetSpriteAnim(int Frame, int Direction = 0)
    {
        return SpriteAnim[Frame];
    }

}
