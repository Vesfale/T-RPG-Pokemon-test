using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Anim
{
    public string Name { get; set; }

    public abstract void SetAsset(string AttackID, string AttackSubID);
    public abstract bool DurationEnd(int Frame, int DurationFrame);
    public abstract bool AnimationEnd(int Frame);
    public abstract Sprite GetSpriteAnim(int Frame, int DurationFrame);
}
