using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;


public struct PlayerSpawner: IComponentData
{
    public Entity playerPrefab;
}

[DisallowMultipleComponent]
public class PlayerSpawnerAuthoring : MonoBehaviour
{

    public GameObject playerPrefab;

    class Baker : Baker<PlayerSpawnerAuthoring>
    {
        public override void Bake(PlayerSpawnerAuthoring authoring)
        {
            PlayerSpawner component = default(PlayerSpawner);
            component.playerPrefab = GetEntity(authoring.playerPrefab, TransformUsageFlags.Dynamic);
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, component);
        } 
    }
}
