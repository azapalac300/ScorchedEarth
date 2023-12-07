using FishNet.Connection;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public struct SpawnData
{
    public Vector3 Position;
    public Quaternion Rotation;

    public string Name;
    public string BaseAssetName;


    public string OwnerID;

}

public enum DamageType
{
    Ballistic, Explosive, Fire, Ice, Poison, Radiation, Lightning
}

public static class SE_Resources
{

    //Play channels

    //Server-Server channels
    public static Action<NetworkConnection> PlayerJoined;


    //Client-Server channels
    public static Action<SpawnData> PersistentSpawnRequest; 

}


public interface Selectable
{
    public void ResolveSelection(string selectorID);
}

public interface Destructible
{
    public int CurrentHP { get; }
    public int TotalHP { get; }

    public void TakeDamage(int damage, DamageType damageType);
}

[Serializable]
public class SaveData
{
    [SerializeField]
    public List<string> usernames;

    [SerializeField]
    public List<string> passwords;

    [SerializeField]
    public List<string> ids;
}