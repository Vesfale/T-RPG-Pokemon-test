using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class MainAttack : MonoBehaviour
{

    public int Min;
    public int Max;
    // Start is called before the first frame update
    void Start()
    {
        List<string> directories;
        List<string> Subdirectories;

        int X = 0;
        int Y = 0;
        int ind = 0;

        directories = MainAttack.GetDirectories("D:/Unity Project/Pokemon DM Test/Assets/Resources/AttackSprite/move_VFX/");
        foreach (string directorie in directories)
        {
            Subdirectories = MainAttack.GetDirectories("D:/Unity Project/Pokemon DM Test/Assets/Resources/AttackSprite/move_VFX/"+directorie.Split('/').Last()+"/");
            if (ind >= Min)
            {
                X += 3;
                Y = 0;
                foreach (string Subdirectorie in Subdirectories)
                {
                    if (Subdirectorie.Split('/').Last() != "pieces")
                    {
                        Debug.Log(directorie.Split('/').Last() + " = " + Subdirectorie.Split('/').Last());
                        AttackObject.Create(new Vector3Int(X, Y, 0), directorie.Split('/').Last(), Subdirectorie.Split('/').Last());
                        Y += 3;
                    }
                    
                }
            }
            ind++;
            if (ind >= Max)
                break;
        }
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static List<string> GetDirectories(string path, string searchPattern = "*",SearchOption searchOption = SearchOption.TopDirectoryOnly)
    {
        if (searchOption == SearchOption.TopDirectoryOnly)
            return Directory.GetDirectories(path, searchPattern).ToList();

        var directories = new List<string>(GetDirectories(path, searchPattern));

        for (var i = 0; i < directories.Count; i++)
            directories.AddRange(GetDirectories(directories[i], searchPattern));

        return directories;
    }
}
