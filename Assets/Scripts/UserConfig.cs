using Rook;
using Unity.Collections;
using UnityEngine;

/// <summary>
/// Config controlled by the user themselves.
/// </summary>
public static class UserConfig
{
    /// <inheritdoc cref="Values.MovementDeadzone"/>>
    public static RookPref<float> MovementDeadzonePref => RookPref<float>.GetOrCreate(nameof(Values.MovementDeadzone), 0.2f, RookPrefs.PrefStorageLocation.UnityPlayerPref);
    public static RookPref<float> SprintDeadzonePref => RookPref<float>.GetOrCreate(nameof(Values.SprintDeadzone), 0.5f, RookPrefs.PrefStorageLocation.UnityPlayerPref);
    
    public static Values AsValues() => new Values
    {
        MovementDeadzone = MovementDeadzonePref.Value,
        SprintDeadzone = SprintDeadzonePref.Value,
    };

    public struct Values
    {
        public float MovementDeadzone;
        public float SprintDeadzone;
    }
}