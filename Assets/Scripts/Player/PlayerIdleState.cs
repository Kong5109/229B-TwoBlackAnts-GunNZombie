using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {

    }
    public override void Tick(float deltaTime)
    {
        if (stateMachine.Player.IsShootingMode)
        {
            stateMachine.SwitchState(new PlayerShootingState(stateMachine));
            return;
        }
    }

    public override void Exit()
    { 

    }

}
