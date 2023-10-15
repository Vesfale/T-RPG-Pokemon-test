public class PokemonName : Data
{
    public int PokemonID;
    public int LanguageID;
    public string Name;
    public string Genus;

    public static PokemonName FromCsv(string Line)
    {
        string[] values = Line.Split(',');
        PokemonName PN = new PokemonName();
        
        PN.PokemonID = int.Parse(values[0]);
        PN.LanguageID = int.Parse(values[1]);
        PN.Name = values[2];
        PN.Genus = values[3];

        return PN;
    }
}

public class PokemonType : Data
{
    public int PokemonID;
    public int TypeID;
    public int Slot;

    public static PokemonType FromCsv(string Line)
    {
        string[] values = Line.Split(',');
        PokemonType PT = new PokemonType();
        
        PT.PokemonID = int.Parse(values[0]);
        PT.TypeID = int.Parse(values[1]);
        PT.Slot = int.Parse(values[2]);

        return PT;
    }
}