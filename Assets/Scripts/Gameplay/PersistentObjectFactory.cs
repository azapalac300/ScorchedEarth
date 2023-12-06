using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PersistentObjectFactory : ScriptableObject
{
    public GameObject testPrefab;

    public GameObject CreatePersistentObject(SpawnData spawnData)
    {
        return testPrefab;
    }
}
