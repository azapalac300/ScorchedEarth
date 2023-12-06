using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Xml.Serialization;

[CreateAssetMenu]
public class DataSerializer : ScriptableObject
{
    string path { get { return Directory.GetCurrentDirectory(); } }
    string userData { get { return @"\User_Data"; } }
    public void SerializeUserData(Dictionary<string, string> userIDs, Dictionary<string, string> userCredentials)
    {
        
        SaveData saveData = new SaveData();
        saveData.userIdData = JsonUtility.ToJson(userIDs); 
        saveData.credentialsData = JsonUtility.ToJson(userCredentials);

        TextWriter writer = null;

        Debug.Log(path);
        
        try
        {
            var serializer = new XmlSerializer(typeof(SaveData));
            writer = new StreamWriter(path + userData, true);
            serializer.Serialize(writer, saveData);
        }
        finally
        {
            if (writer != null)
            {
                writer.Close();
            }
        }
    }
}
