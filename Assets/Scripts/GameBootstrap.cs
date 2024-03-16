using Unity.NetCode;

/// <summary>
/// Bootstrap for Fleet & Empire.
/// </summary>
public class GameBootstrap : ClientServerBootstrap
{
    public override bool Initialize(string defaultWorldName)
    {
        AutoConnectPort = 7979;
        return base.Initialize(defaultWorldName);
    }
}