using System;
using Unity.Entities;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// Singleton!
/// </summary>
[Serializable]
public struct GameSettings : IComponentData
{
    public Entity Player;
    /// <summary>Units per second.</summary>
    public float PlayerMovementSpeed; 
    /// <summary>Units per second.</summary>
    public float PlayerMovementSpeedSprinting;


}

[DisallowMultipleComponent]
public class GameSettingsAuthoring : MonoBehaviour
{
    [FormerlySerializedAs("Cube")]
    public GameObject Player;
    public GameSettings Settings;

    class NetCubeSpawnerBaker : Baker<GameSettingsAuthoring>
    {
        public override void Bake(GameSettingsAuthoring authoring)
        {
            var component = authoring.Settings;
            component.Player = GetEntity(authoring.Player, TransformUsageFlags.Dynamic);
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, component);
        }
    }
}

