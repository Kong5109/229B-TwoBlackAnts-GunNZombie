using UnityEngine;
using System.Collections;
public class EnemyDeathState : EnemyBaseState
{
    private float DestroyTime = 5;

    private Rigidbody rb;

    public EnemyDeathState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Enemy.HealthBar.gameObject.SetActive(false);
        stateMachine.Enemy.StartRagdoll();
        stateMachine.Enemy.StartDeathVFX();

        rb = stateMachine.Enemy.Rigidbody;
        rb.freezeRotation = false;

        return;
        /*Vector3 direction = (-stateMachine.Enemy.transform.forward + Vector3.up).normalized;
        rb.AddForce(direction * 25, ForceMode.Impulse);*/
    }
    public override void Tick(float deltaTime)
    {
        
    }

    public override void Exit()
    {
    }
}
