using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using System.Xml.Linq;

[XmlRoot("Anim")]
public class AnimPokemon : Anim
{
    public int Index { get; set; }
    public int FrameWidth { get; set; }
    public int FrameHeight { get; set; }
    public int RushFrame { get; set; }
    public int HitFrame { get; set; }
    public int ReturnFrame { get; set; }
    
    [XmlArray("Durations")]
    [XmlArrayItem("Duration", typeof(int))]
    public List<int> Durations { get; set; }

    private Sprite SpriteAnim;
    private Sprite SpriteOffset;
    private Sprite SpriteShadow;

    public AnimPokemon()
    {
    }

    override
    public void SetAsset(string PokemonID, string SubID = "")
    {
        SpriteAnim = Resources.Load<Sprite>("MobSprite/"+PokemonID+"/"+this.Name+"-Anim");
        SpriteOffset = Resources.Load<Sprite>("MobSprite/"+PokemonID+"/"+this.Name+"-Offsets");
        SpriteShadow = Resources.Load<Sprite>("MobSprite/"+PokemonID+"/"+this.Name+"-Shadow");
    }

    override
    public bool AnimationEnd(int Frame)
    {
        if (Frame >= (Durations.Count))
            return true;
        return false;
    }

    override
    public bool DurationEnd(int Frame, int DurationFrame)
    {
        if (DurationFrame >= this.Durations[Frame])
            return true;
        return false;
    }

    override
    public Sprite GetSpriteAnim(int Frame, int Direction)
    {
        var rect = new Rect(this.FrameWidth * Frame, this.FrameHeight * Direction, this.FrameWidth, this.FrameHeight);
        return Sprite.Create(this.SpriteAnim.texture, rect, Vector2.one * 0.5F, 24);
    }
}