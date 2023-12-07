using FishNet.Connection;
using FishNet.Object;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Managing.Scened;

public class PersistentObjectSpawner : NetworkBehaviour
{
    public PersistentObjectFactory factory;
    public GameObject spawnedObjects;

    // Start is called before the first frame update
    void Start()
    {
        SE_Resources.PersistentSpawnRequest += (data) =>
        {
            SpawnPersistentObject(data);
        };
    }

    [ServerRpc(RequireOwnership = false)]
    public void SpawnPersistentObject(SpawnData spawnData)
    {
        GameObject prefab = factory.CreatePersistentObject(spawnData);

        UnityEngine.SceneManagement.Scene overworld = SceneManager.GetScene("Overworld");
        GameObject obj = Instantiate(prefab, spawnData.Position, spawnData.Rotation);
        PersistentObject persistentObject = obj.GetComponent<PersistentObject>();
        persistentObject.ownerID = spawnData.OwnerID;


        ServerManager.Spawn(obj, null, overworld);

        obj.transform.SetParent(spawnedObjects.transform);

        SetupForObservers(persistentObject, spawnData);
    }


    [ObserversRpc]
    private void SetupForObservers(PersistentObject persistentObject, SpawnData spawnData)
    {
        persistentObject.gameObject.transform.SetParent(spawnedObjects.transform);
    }

}
