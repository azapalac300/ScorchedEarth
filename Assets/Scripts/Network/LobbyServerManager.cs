using FishNet.Connection;
using FishNet.Managing.Scened;
using FishNet.Object;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyServerManager : NetworkBehaviour
{
    public DataSerializer serializer;


    private Dictionary<string, string> userIDs;
    public Dictionary<string, string> userCredentials;

    public float serializeDataInterval;
    private float serializeDataTimer;

    private LobbyClientManager clientManager;

    public override void OnStartServer()
    {

        base.OnStartServer();

        clientManager = GetComponent<LobbyClientManager>();

        userIDs = new Dictionary<string, string>();
        userCredentials = new Dictionary<string, string>();
        serializer.DeserializeUserData(ref userIDs, ref userCredentials);

        SceneLoadData sld = new SceneLoadData("Overworld");
        SceneManager.LoadGlobalScenes(sld);

        sld = new SceneLoadData("Login");
        SceneManager.LoadGlobalScenes(sld);

    }


    [ServerRpc(RequireOwnership = false)]
    public void RemoveOverworld(NetworkConnection conn = null)
    {
        UnityEngine.SceneManagement.Scene overworld = SceneManager.GetScene("Overworld");
        NetworkConnection[] connections = { conn };
        SceneManager.RemoveConnectionsFromScene(connections, overworld);
    }

    [ServerRpc (RequireOwnership = false)]
    public void LoadOverworld(NetworkConnection conn = null)
    {
        //Get Scene handles
        UnityEngine.SceneManagement.Scene overworld = SceneManager.GetScene("Overworld");
        UnityEngine.SceneManagement.Scene login = SceneManager.GetScene("Login");

        //Add player (Client) to Overworld and remove them from the login screen
        NetworkConnection[] connections = { conn };
        SceneManager.AddConnectionToScene(conn, overworld);
        SceneManager.RemoveConnectionsFromScene(connections, login);
        SE_Resources.PlayerJoined?.Invoke(conn);
    }

    public void Update()
    {
        serializeDataTimer += Time.deltaTime;

        if (serializeDataTimer >= serializeDataInterval)
        {
           
            serializeDataTimer = 0;
        }
        
    }




    [ServerRpc(RequireOwnership = false)]
    public void UserLogin(string username, string password, NetworkConnection client = null)
    {
        if(!userIDs.ContainsKey(username))
        {
            AddNewUser(username, password, client);
        }
        else
        {
            AddReturningUser(username, password, client);
        }
    }


    void AddNewUser(string username, string password, NetworkConnection client)
    {
        userCredentials.Add(username, password);

        string userID = System.Guid.NewGuid().ToString();
        userIDs.Add(username, userID);

        serializer.SerializeUserData(username, password, userID);

        clientManager.SetCredentials(client, userID, true);

    }

    void AddReturningUser(string username, string password, NetworkConnection client) {

        if (userCredentials[username] == password)
        {
            clientManager.SetCredentials(client, userIDs[username], true);
        }
        else
        {
            //Display error message
            clientManager.SetCredentials(client, userIDs[username], false);
        }
    }
    
}
