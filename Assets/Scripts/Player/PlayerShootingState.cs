using UnityEngine;

public class PlayerShootingState : PlayerBaseState
{
    public PlayerShootingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Player.OnPlayerShooting += ShootWeapon;
    }
    public override void Tick(float deltaTime)
    {

    }

    public override void Exit()
    {
        stateMachine.Player.OnPlayerShooting -= ShootWeapon;
    }

    private void ShootWeapon()
    {
        Debug.Log("Gun Shoot");
    }
}
