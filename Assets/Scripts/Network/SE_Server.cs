using FishNet.Connection;
using FishNet.Object;
using FishNet.Managing.Scened;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SE_Server : NetworkBehaviour
{
    public GameObject playerPrefab;



    public GameObject spawnedObjects;

    // Start is called before the first frame update
    void Start()
    {
        if (IsServer)
        {
            SE_Resources.PlayerJoined += (c) => { 
                SpawnPlayer(playerPrefab, c);
            };
        }
    }


    public void SpawnPlayer(GameObject playerPrefab, NetworkConnection conn)
    {
        UnityEngine.SceneManagement.Scene overworld = SceneManager.GetScene("Overworld");
        GameObject g = Instantiate(playerPrefab);
        ServerManager.Spawn(g, conn, overworld);
        g.transform.SetParent(spawnedObjects.transform);
        SetSpawnedObjectParent(g);
    }


    [ObserversRpc]
    private void SetSpawnedObjectParent(GameObject spawnedObject)
    {
        spawnedObject.transform.SetParent(spawnedObjects.transform);
    }
}
