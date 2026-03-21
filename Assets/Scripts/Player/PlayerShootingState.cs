using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShootingState : PlayerBaseState
{
    public PlayerShootingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Player.OnPlayerShooting += ShootWeapon;

        stateMachine.Player.WeaponHolder.gameObject.SetActive(true);
    }
    public override void Tick(float deltaTime)
    {
        SetGunLookAt(GetRayCastMousePoint());

        if (stateMachine.Player.IsShootingMode == false)
        {
            stateMachine.SwitchState(new PlayerIdleState(stateMachine));
            return;
        }
    }

    public override void Exit()
    {
        stateMachine.Player.OnPlayerShooting -= ShootWeapon;

        stateMachine.Player.WeaponHolder.gameObject.SetActive(false);
    }

    private void SetGunLookAt(Vector3 pos)
    {
        stateMachine.Player.WeaponHolder.transform.LookAt(pos);
    }

    private Vector3 GetRayCastMousePoint()
    {
        Ray camRay = stateMachine.Player.Camera.ScreenPointToRay(Mouse.current.position.ReadValue());
        Physics.Raycast(camRay, out RaycastHit hit, float.MaxValue);
        return hit.point;
    }

    private void ShootWeapon()
    {
        Debug.Log("Gun Shoot");
    }
}
