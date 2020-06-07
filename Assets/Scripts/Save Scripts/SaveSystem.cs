using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public static class SaveSystem
{
    // Start is called before the first frame update
    public static void SavePlayer (Player player, int num)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        SaveFile data = new SaveFile(player);
        string path = "";
        switch (num)
        {
            case 0:
                path = Application.persistentDataPath + "/player.pure";
                break;
            case 1:
                path = Application.persistentDataPath + "/player.impure";
                break;
            case 2:
                path = Application.persistentDataPath + "/player.hallowed";
                break;
        }
        //path = Application.persistentDataPath + "/player.soul";
        FileStream stream = new FileStream(path, FileMode.Create);


        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static SaveFile LoadPlayer(int fileNum)
    {

        //string path = Application.persistentDataPath + "/player.soul";
        string path = "";
        switch (fileNum)
        {
            case 0:
                path = Application.persistentDataPath + "/player.pure";
                break;
            case 1:
                path = Application.persistentDataPath + "/player.impure";
                break;
            case 2:
                path = Application.persistentDataPath + "/player.hallowed";
                break;


        }
        Debug.Log(path);
   //     path = Application.persistentDataPath + "/player.soul";
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
        if (fileNum == 0)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            SaveFile data = new SaveFile(fileNum);
            string path = Application.persistentDataPath + "/player.pure";
            FileStream stream = new FileStream(path, FileMode.Create);


            formatter.Serialize(stream, data);
            stream.Close();
        }
        else if (fileNum == 1)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            SaveFile data = new SaveFile(fileNum);
            string path = Application.persistentDataPath + "/player.impure";
            FileStream stream = new FileStream(path, FileMode.Create);


            formatter.Serialize(stream, data);
            checkExistence(1);
            stream.Close();
        }
        else
        {
            BinaryFormatter formatter = new BinaryFormatter();
            SaveFile data = new SaveFile(fileNum);
            string path = Application.persistentDataPath + "/player.hallowed";
            FileStream stream = new FileStream(path, FileMode.Create);


            formatter.Serialize(stream, data);
            stream.Close();
        }
    }

    public static bool checkExistence(int num)
    {
        string path = "";
        switch (num)
        {
            case 0:
                path = Application.persistentDataPath + "/player.pure";
                break;
            case 1:
                path = Application.persistentDataPath + "/player.impure";
                break;
            case 2:
                path = Application.persistentDataPath + "/player.hallowed";
                break;
        }
        Debug.Log(path);
        return File.Exists(path);
    }
}
