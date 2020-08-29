using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;


public class SerializationManager
{
    public static string SavePath = Application.persistentDataPath + "/saves";
    public static bool Save(string saveName, object saveData)
    {
        

        if (!Directory.Exists(SavePath)) {
            Directory.CreateDirectory(SavePath);
        }
        string path = string.Format("{0}/{1}.save", SavePath, saveName);
        Debug.Log("Save path is " + path);

        BinaryFormatter formatter =  GetBinaryFormatter();
        FileStream file = File.Create(path);
        formatter.Serialize(file, saveData);
        file.Close();

        return true;
    }

    public static object Load(string saveName){
        string path = string.Format("{0}/{1}.save", SavePath, saveName);
        if (!File.Exists(path)) {
            return null;
        }

        BinaryFormatter formatter = GetBinaryFormatter();
        FileStream file = File.Open(path, FileMode.Open);
        try
        {
            
            object saveData = formatter.Deserialize(file);
            file.Close();
            return saveData;

        }
        catch (System.Exception)
        {
            Debug.LogErrorFormat("Error while opening save data from {0}", path);
            file.Close();
            return null;
        }
    }

    public static BinaryFormatter GetBinaryFormatter(){
        BinaryFormatter formatter = new BinaryFormatter();
        SurrogateSelector selector = new SurrogateSelector();
        Vector3SerializationSurrogate vector3surrogate = new Vector3SerializationSurrogate();
        
        selector.AddSurrogate(typeof(Vector3),  new StreamingContext(StreamingContextStates.All), vector3surrogate);
        formatter.SurrogateSelector = selector;

        return formatter;
    }
}
