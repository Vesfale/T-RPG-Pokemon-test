using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimData
{

    public int Tick;
    public int Frame;
    private int DurationFrame;
    private int TickPerFrame;

    public Anim CurrentAnim;

    public void ResetAnim()
    {
        this.Frame = 0;
        this.DurationFrame = 0;
        this.TickPerFrame = 0;
    }

    private void ChangeTickPerFrame()
    {
        if (!(this.TickPerFrame >= Tick))
            this.TickPerFrame++;
        else
            this.ChangeDurationFrame();
    }

    private void ChangeDurationFrame()
    {
        this.TickPerFrame = 0;
        this.DurationFrame += 2;
        if (this.CurrentAnim != null)
            if (this.CurrentAnim.Name== ActionType.Walk && EnumPokemon.MoveSpeed > 4F)
                this.DurationFrame++;
        if (this.CurrentAnim != null)
            if (this.CurrentAnim.DurationEnd(this.Frame, this.DurationFrame))
                this.ChangeFrame();
    }
        
    private void ChangeFrame()
    {
        this.Frame++;
        this.DurationFrame = 0;
        if (this.CurrentAnim.AnimationEnd(this.Frame))
            this.ResetAnim();
    }
    
    public void Update()
    {
        this.ChangeTickPerFrame();
    }
}
