using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public struct Player: IComponentData
{

}

public class PlayerAuthouring : MonoBehaviour
{
    class Baker : Baker<PlayerAuthouring>
    {
        public override void Bake(PlayerAuthouring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent<Player>(entity);
        }
    }
}
