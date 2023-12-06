using FishNet;
using FishNet.Managing;
using FishNet.Object;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ConnectionType
{
    Client, Server, Host
}
public class Startup : NetworkBehaviour {

    public ConnectionType connectionType;
    // Start is called before the first frame update
    void Awake()
    {
        if(connectionType == ConnectionType.Client)
        {
            InstanceFinder.ClientManager.StartConnection();
        }else if(connectionType == ConnectionType.Server)
        {
            InstanceFinder.ServerManager.StartConnection();
        }
        else
        {
            InstanceFinder.ClientManager.StartConnection();
            InstanceFinder.ServerManager.StartConnection();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
