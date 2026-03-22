using UnityEngine;

public class EnemyDeathState : EnemyBaseState
{
    private float DestroyTime = 5;

    private Rigidbody rb;

    public EnemyDeathState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        rb = stateMachine.Enemy.Rigidbody;
        rb.freezeRotation = false;

        Vector3 direction = (-stateMachine.Enemy.transform.forward + Vector3.up).normalized;
        rb.AddForce(direction * 25, ForceMode.Impulse);
    }
    public override void Tick(float deltaTime)
    {
    }

    public override void Exit()
    {
    }
}
