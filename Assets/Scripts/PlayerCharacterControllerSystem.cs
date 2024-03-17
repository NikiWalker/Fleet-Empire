using Rook;
using Unity.Entities;
using Unity.Mathematics;
using Unity.NetCode;
using Unity.Transforms;
using Unity.Collections;
using Unity.Burst;
using Unity.Core;
using GameSettings_1 = GameSettings;

[UpdateInGroup(typeof(PredictedSimulationSystemGroup))]
[BurstCompile]
public partial struct PlayerCharacterControllerSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<GameSettings>();
        state.RequireForUpdate<NetworkTime>();
        var builder = new EntityQueryBuilder(Allocator.Temp)
                      .WithAll<Simulate>()
                      .WithAll<PlayerInput>()
                      .WithAllRW<LocalTransform>();

        var query = state.GetEntityQuery(builder);
        state.RequireForUpdate(query);
    }
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var moveJob = new PlayerCharacterControllerJob
        {
            GameSettings = SystemAPI.GetSingleton<GameSettings>(),
            Time = SystemAPI.Time,
        };
        state.Dependency = moveJob.ScheduleParallel(state.Dependency);
    }

    [BurstCompile]
    [WithAll(typeof(Simulate))]
    partial struct PlayerCharacterControllerJob : IJobEntity
    {
        public GameSettings GameSettings;
        public float FixedCubeSpeed;
        public TimeData Time;

        public void Execute(PlayerInput playerInput, ref LocalTransform trans)
        {
            // Sanitize inputs:
            playerInput.MovementInput = RookUtils.ClampStickWithDeadzone(playerInput.MovementInput, float.Epsilon);

            // Movement:
            bool isSprinting = playerInput.SprintInput;
            float moveSpeed = isSprinting ? GameSettings.PlayerMovementSpeedSprinting : GameSettings.PlayerMovementSpeed;
            var positionChange = playerInput.MovementInput * (moveSpeed * Time.DeltaTime);
            trans.Position += new float3(positionChange.x, 0, positionChange.y);
        }

    }
}
