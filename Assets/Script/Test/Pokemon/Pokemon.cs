using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pokemon
{

    public string PokemonID;
    public Dictionary<int, string> Name;
    public Dictionary<int, string> Genus;
    public int Type1;
    public Dictionary<int, string> TypeName1;
    public int Type2;
    public Dictionary<int, string> TypeName2;
    

    public Pokemon()
    {
        this.InitVar();
    }

    public Pokemon(string ID)
    {
        this.InitVar();
        InitPokemon(ID);
    }

    public void InitVar()
    {
        Name = new Dictionary<int, string>();
        Genus = new Dictionary<int, string>();
        TypeName1 = new Dictionary<int, string>();
        TypeName2 = new Dictionary<int, string>();
        
        PokemonID = "0000";
        Name[5] = "Oeuf";
        Type1 = 0;
        TypeName1[5] = "Aucun";
        Type2 = 0;
        TypeName2[5] = "Aucun";
    }

    public void InitPokemon(string ID)
    {
        int DataPokemonID = int.Parse(ID);

        this.PokemonID = ID;

        List<PokemonName> PokemonNames = GlobalData.DataBase.DataPokemonName.FindAll(x => x.PokemonID.Equals(DataPokemonID));

        foreach (PokemonName PN in PokemonNames)
        {
            Name[PN.LanguageID] = PN.Name;
            Genus[PN.LanguageID] = PN.Genus;
        }

        List<PokemonType> PokemonTypes = GlobalData.DataBase.DataPokemonType.FindAll(x => x.PokemonID.Equals(DataPokemonID));

        if (PokemonTypes.Count >= 1)
            Type1 = PokemonTypes[0].TypeID;
        if (PokemonTypes.Count >= 2)
            Type2 = PokemonTypes[1].TypeID;


        List<TypeName> TypeNames = GlobalData.DataBase.DataTypeName.FindAll(x => x.TypeID.Equals(Type1));

        foreach (TypeName TN in TypeNames)
            TypeName1[TN.LanguageID] = TN.Name;

        TypeNames = GlobalData.DataBase.DataTypeName.FindAll(x => x.TypeID.Equals(Type2));

        foreach (TypeName TN in TypeNames)
            TypeName2[TN.LanguageID] = TN.Name;
    }

    public void Log()
    {
        Debug.Log("Name = "+this.Name[5]);
        Debug.Log("Genus = "+this.Genus[5]);
        Debug.Log("Type1 = "+this.TypeName1[5]);
        Debug.Log("Type2 = "+this.TypeName2[5]);
    }
}
