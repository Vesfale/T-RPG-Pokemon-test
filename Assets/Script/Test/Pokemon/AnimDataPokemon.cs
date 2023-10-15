using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using System.Xml.Linq;

[XmlRoot("AnimData")]
public class AnimDataPokemon : AnimData
{
    public int ShadowSize { get; set; }
    public string IDAsset;

    [XmlArray("Anims")]
    [XmlArrayItem("Anim", typeof(AnimPokemon))]
    public List<AnimPokemon> Anims { get; set; }

    private CardinalPoint Direction { get; set; }

    public static AnimDataPokemon FromXml (XDocument xd)
    {
        XmlSerializer s = new XmlSerializer(typeof(AnimDataPokemon));
        return (AnimDataPokemon)s.Deserialize(xd.CreateReader());
    }

    public void InitAnimData()
    {
        this.Tick = ActionType.Tick;
    }

    public void SetAsset(string Pokemon_ID, string Type = "Idle")
    { 
        AnimPokemon AnimAssetType;
        IDAsset = Pokemon_ID;
        if (Type == "ALL") {
            this.Anims.ForEach(delegate(AnimPokemon AnimAsset) {
                AnimAsset.SetAsset(Pokemon_ID);
            });
        }
        else
        {
            AnimAssetType = this.Anims.Find(x => x.Name.Equals(Type));
            if (AnimAssetType != null)
                AnimAssetType.SetAsset(Pokemon_ID);
        }
    }

    public void SetAction(string Type)
    {
        if (this.CurrentAnim != null)
            if (this.CurrentAnim.Name != Type)
                this.ResetAnim();

        this.CurrentAnim = this.Anims.Find(x => x.Name.Equals(Type));
         if (this.CurrentAnim != null)
                this.CurrentAnim.SetAsset(IDAsset, Type);
    }

    public void SetDirection(CardinalPoint Type)
    {
        if (Direction != Type)
            Direction = Type;
    }


    public Sprite GetCurrentSpriteAnim()
    {
        if (this.CurrentAnim != null)
            return this.CurrentAnim.GetSpriteAnim(this.Frame, (int)Direction);
        return null;
    }
}


