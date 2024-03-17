using Rook;
using Unity.Entities;
using Unity.Mathematics;
using Unity.NetCode;
using Unity.NetCode.Samples.Common;
using UnityEngine;

[UpdateInGroup(typeof(GhostInputSystemGroup))]
public partial struct GatherInputSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<NetworkStreamInGame>();
        state.RequireForUpdate<GameSettings>();
    }

    public void OnUpdate(ref SystemState state)
    {
        foreach (var playerInput in SystemAPI.Query<RefRW<PlayerInput>>().WithAll<GhostOwnerIsLocal>())
        {
            ref var input = ref playerInput.ValueRW;
            input = default;

            // Read input from device:
            {
                input.MovementInput = new float2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
                input.MovementInput += TouchInput.GetStick(TouchInput.StickCode.LeftStick);
                if (TouchInput.GetKey(TouchInput.KeyCode.Left)) input.MovementInput.x -= 1;
                if (TouchInput.GetKey(TouchInput.KeyCode.Right)) input.MovementInput.x += 1;
                if (TouchInput.GetKey(TouchInput.KeyCode.Down)) input.MovementInput.y += 1;
                if (TouchInput.GetKey(TouchInput.KeyCode.Up)) input.MovementInput.y -= 1;

                input.SprintInput = Input.GetAxisRaw("Sprint") > UserConfig.SprintDeadzonePref.Value;
            }

            // Apply local dead-zone values to input we're sending to the server:
            {
                input.MovementInput = RookUtils.ClampStickWithDeadzone(input.MovementInput, UserConfig.MovementDeadzonePref.Value);
                //input.Movement = RookUtils.ClampAxis(input.Movement);
            }
        }
    }
}