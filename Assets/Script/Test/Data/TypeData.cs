using System.Collections.Generic;

public class EnumType
{
    public static Dictionary<int, string> UIType = new Dictionary<int, string>()
    {
        {0, "Ui_Icon_Type_None"},
        {1, "Ui_Icon_Type_Normal"},
        {2, "Ui_Icon_Type_Fighting"},
        {3, "Ui_Icon_Type_Flying"},
        {4, "Ui_Icon_Type_Poison"},
        {5, "Ui_Icon_Type_Ground"},
        {6, "Ui_Icon_Type_Rock"},
        {7, "Ui_Icon_Type_Bug"},
        {8, "Ui_Icon_Type_Ghost"},
        {9, "Ui_Icon_Type_Steel"},
        {10, "Ui_Icon_Type_Fire"},
        {11, "Ui_Icon_Type_Water"},
        {12, "Ui_Icon_Type_Grass"},
        {13, "Ui_Icon_Type_Electric"},
        {14, "Ui_Icon_Type_Psychic"},
        {15, "Ui_Icon_Type_Ice"},
        {16, "Ui_Icon_Type_Dragon"},
        {17, "Ui_Icon_Type_Dark"},
        {18, "Ui_Icon_Type_Fairy"}
    };
}

public class TypeName : Data
{
    public int TypeID;
    public int LanguageID;
    public string Name;

    public static TypeName FromCsv(string Line)
    {
        string[] values = Line.Split(',');
        TypeName TN = new TypeName();
        
        TN.TypeID = int.Parse(values[0]);
        TN.LanguageID = int.Parse(values[1]);
        TN.Name = values[2];

        return TN;
    }
}