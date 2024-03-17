using Unity.Entities;
using Unity.Mathematics;
using Unity.NetCode;
using UnityEngine;

[GhostComponent(PrefabType=GhostPrefabType.AllPredicted)]
public struct PlayerInput : IInputComponentData
{
    public float2 MovementInput;
    public bool SprintInput;
}

[DisallowMultipleComponent]
public class PlayerAuthoring : MonoBehaviour
{
    class CubeInputBaking : Unity.Entities.Baker<PlayerAuthoring>
    {
        public override void Bake(PlayerAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent<PlayerInput>(entity);
        }
    }
}