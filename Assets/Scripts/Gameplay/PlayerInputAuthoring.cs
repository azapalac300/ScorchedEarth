using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.NetCode;
using Unity.Entities;
using Unity.Burst;

[GhostComponent(PrefabType = GhostPrefabType.AllPredicted)]
public struct PlayerInput: IInputComponentData
{
    public int Horizontal;
    public int Vertical;
   
}

public struct PlayerSpeed: IComponentData
{
    public float Speed;
}

[DisallowMultipleComponent]
public class PlayerInputAuthoring : MonoBehaviour
{
   class Baking: Baker<PlayerInputAuthoring>
    {
        public override void Bake(PlayerInputAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent<PlayerInput>(entity);
            AddComponent<PlayerSpeed>(entity);
        }
    }
}

[UpdateInGroup(typeof(GhostInputSystemGroup))]
public partial struct SamplePlayerInput: ISystem
{

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {

        bool left = Input.GetKey("left");
        bool right = Input.GetKey("right");
        bool down = Input.GetKey("down");
        bool up = Input.GetKey("up");

        foreach (var playerInput in SystemAPI.Query<RefRW<PlayerInput>>().WithAll<GhostOwnerIsLocal>())
        {
            
            playerInput.ValueRW = default;
            if (left)
                playerInput.ValueRW.Horizontal -= 1;
            if (right)
                playerInput.ValueRW.Horizontal += 1;
            if (down)
                playerInput.ValueRW.Vertical -= 1;
            if (up)
                playerInput.ValueRW.Vertical += 1;
        }

    }


}