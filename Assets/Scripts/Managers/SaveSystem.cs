using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem
{
    public static void Save(int highscore)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/pump_rage.data";
        FileStream stream = new FileStream(path, FileMode.Create);

        //SaveData data = new SaveData(saveData);
        
        formatter.Serialize(stream, highscore);
        stream.Close();
    }

    public static int Load()
    {
        string path = Application.persistentDataPath + "/pump_rage.data";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            int highscore = (int)formatter.Deserialize(stream);
            
            stream.Close();

            return highscore;
        }
        else
        {
            Debug.LogError("Save file not found in: " + path);
            return 0;
        }
    }
}
