using FishNet.Connection;
using FishNet.Managing.Scened;
using FishNet.Object;
using System.Collections;
using System.Collections.Generic;

public class LobbyServerManager : NetworkBehaviour
{
    public DataSerializer serializer;


    private Dictionary<string, string> userIDs;
    public Dictionary<string, string> userCredentials;



    public override void OnStartServer()
    {

        base.OnStartServer();

        userIDs = new Dictionary<string, string>();
        userCredentials = new Dictionary<string, string>();


        SceneLoadData sld = new SceneLoadData("Overworld");
        SceneManager.LoadGlobalScenes(sld);

        sld = new SceneLoadData("Login");
        SceneManager.LoadGlobalScenes(sld);

        serializer.SerializeUserData(userIDs, userCredentials);
    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        RemoveOverworld();
    }

    public void ButtonPressed()
    {
        if(IsClient) LoadOverworld();
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
        Resources.PlayerJoined?.Invoke(conn);
    }

}
