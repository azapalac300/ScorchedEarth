using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Persistent Object Factory", menuName = "SE Assets/Persistent Object Factory", order = 2)]
public class PersistentObjectFactory : ScriptableObject
{
    public GameObject testPrefab;

    public GameObject CreatePersistentObject(SpawnData spawnData)
    {
        return testPrefab;
    }
}
