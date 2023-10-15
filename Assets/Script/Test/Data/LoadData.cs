using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Data{}

public class GlobalData
{
    public static LoadData DataBase = new LoadData();
}

public class LoadData
{
    public List<PokemonName> DataPokemonName;
    public List<PokemonType> DataPokemonType;
    public List<TypeName> DataTypeName;

    public LoadData()
    {
        this.LoadAll();
    }

    public List<Data> LoadDataCSV(string path, Func<string, Data> FromCsv)
    {
        string[] CsvLines = Resources.Load<TextAsset>(path).ToString().Split('\n');
        CsvLines = CsvLines.Where(x => !string.IsNullOrEmpty(x)).ToArray();

        return CsvLines.Skip(1).Select(x => FromCsv(x)).ToList().Cast<Data>().ToList();
    }
                                           

    public void LoadAll()
    {
        DataPokemonName = this.LoadDataCSV("Data/Csv/pokemon_species_names", PokemonName.FromCsv).Cast<PokemonName>().ToList();
        DataPokemonType = this.LoadDataCSV("Data/Csv/pokemon_types", PokemonType.FromCsv).Cast<PokemonType>().ToList();
        DataTypeName = this.LoadDataCSV("Data/Csv/type_names", TypeName.FromCsv).Cast<TypeName>().ToList();
    }


    public void test()
    {
        foreach (PokemonName x in DataPokemonName)
            if (x.LanguageID == 5)
                Debug.Log(x.Name);
    }
}
