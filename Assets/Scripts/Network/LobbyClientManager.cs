using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;
using FishNet.Connection;
using TMPro;
using System;
using System.Threading.Tasks;

public class LobbyClientManager : NetworkBehaviour
{
    public UserCredentials userCredentials;

    public TMP_InputField username;
    public TMP_InputField password;

    private LobbyServerManager lobbyServerManager;

    public Task Login()
    {
        return Task.Factory.StartNew(() => {
            lobbyServerManager.UserLogin(username.text, password.text);
        });
        
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        
        lobbyServerManager = GetComponent<LobbyServerManager>();

        lobbyServerManager.RemoveOverworld();
    }


    public async void ButtonPressed()
    {
        if (!IsClient) return;

        await Login();

        if (userCredentials.canJoinGame)
        {
            Debug.Log("ID: " + userCredentials.id);
            lobbyServerManager.LoadOverworld();
        }
    }

    [TargetRpc]
    public void SetCredentials(NetworkConnection client, string id, bool canJoin)
    {
        userCredentials.username = username.text;
        userCredentials.password = password.text;
        userCredentials.id = id;
        userCredentials.canJoinGame = canJoin;
    }

   
}
