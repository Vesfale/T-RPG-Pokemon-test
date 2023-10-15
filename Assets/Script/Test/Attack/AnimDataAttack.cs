using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimDataAttack : AnimData
{
    
    public AnimDataAttack()
    {
        CurrentAnim = new AnimAttack();
        this.Tick = ActionType.TickAttack;
    }

    public void SetAsset(string AttackID, string AttackSubID)
    {
        this.CurrentAnim.SetAsset(AttackID, AttackSubID);
    }

    public Sprite GetCurrentSpriteAnim()
    {
        if (this.CurrentAnim != null)
            return this.CurrentAnim.GetSpriteAnim(this.Frame, 0);
        return null;
    }
}
