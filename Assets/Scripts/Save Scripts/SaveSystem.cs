using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    // Start is called before the first frame update
    public static void SavePlayer (Player player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.soul";
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveFile data = new SaveFile(player);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static SaveFile LoadPlayer()
    {

        string path = Application.persistentDataPath + "/player.soul";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveFile  data= formatter.Deserialize(stream) as SaveFile;

            stream.Close();

            return data;

            
        }
        else
        {
            Debug.Log("Save File not found");
            //Mess around with this in the menu UI
            return null;
        }
    }

    public static void CreateFile(int fileNum)
    {

    }
}
