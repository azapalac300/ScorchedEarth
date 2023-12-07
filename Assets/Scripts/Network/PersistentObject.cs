using FishNet.Connection;
using FishNet.Object;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using FishNet.Object.Synchronizing;

public class PersistentObject : NetworkBehaviour, Selectable
{
    [SyncVar]
    public string ownerID;

    public bool CheckOwnerID(string id, NetworkConnection conn = null)
    {
        if(id == ownerID) return true;

        return false;
    }

    public void ResolveSelection(string id)
    {
        if (id == ownerID)
        {
            Debug.Log("You are the owner");
        }
        else
        {
            Debug.Log("You are not the owner");
        }
    }

}
