using System;
using Unity.Entities;
using Unity.NetCode;


[UnityEngine.Scripting.Preserve]
public class GameBootstrap: ClientServerBootstrap
{
    public override bool Initialize(string defaultWorldName)
    {
        AutoConnectPort = 1990;
        return base.Initialize(defaultWorldName);
    }
}
