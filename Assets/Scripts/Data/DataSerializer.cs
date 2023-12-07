using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Xml.Serialization;
using System;
using Unity.VisualScripting;

[CreateAssetMenu(fileName = "Data Serializer", menuName = "SE Assets/Data Serializer", order = 1)]
public class DataSerializer : ScriptableObject
{
    [SerializeField]
    public SaveData saveData;
   // string path { get { return (Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SE_Data"); } }
    private string userData { get { return "User_Data"; } }

    private string ID_Data { get { return "User_IDs"; } }

    private string Credentials_Data { get { return "User_Credentials"; } }

    public void SerializeUserData(string userName, string userPassword, string userID )
    {

        saveData.usernames.Add(userName);
        saveData.passwords.Add(userPassword);
        saveData.ids.Add(userID);

        string userDataString = JsonUtility.ToJson(saveData);

        PlayerPrefs.SetString(userData, userDataString);

        //PlayerPrefs.SetString(Credentials_Data, credentialsData);

        /*
        TextWriter writer = null;

        Directory.CreateDirectory(path + userData);

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
        }*/
    }

    public void ClearData()
    {
        saveData.usernames.Clear();
        saveData.passwords.Clear();
        saveData.ids.Clear();

        string userDataString = JsonUtility.ToJson(saveData);

        PlayerPrefs.SetString(userData, userDataString);
    }

    public void DeserializeUserData(ref Dictionary<string, string> userIds, ref Dictionary<string, string> userCredentials)
    {
        saveData = JsonUtility.FromJson<SaveData>(PlayerPrefs.GetString(userData));

        if (saveData != null) {

            try
            {
                for (int i = 0; i < saveData.usernames.Count; i++)
                {

                    userIds.Add(saveData.usernames[i], saveData.ids[i]);
                    userCredentials.Add(saveData.usernames[i], saveData.passwords[i]);
                }
            }catch(System.ArgumentOutOfRangeException e)
            {
                saveData = new SaveData();
                saveData.ids = new List<string>();
                saveData.passwords = new List<string>();
                saveData.usernames = new List<string>();
            }
        }
        else
        {
            saveData = new SaveData();
            saveData.ids = new List<string>();
            saveData.passwords = new List<string>();
            saveData.usernames = new List<string>();
        }
    }
}
