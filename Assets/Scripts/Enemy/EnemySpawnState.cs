using UnityEngine;

public class EnemySpawnState : EnemyBaseState
{
    private float spawnIdleDelay = 2f;
    public EnemySpawnState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
    }
    public override void Tick(float deltaTime)
    {
        if (spawnIdleDelay < 0)
        {
            stateMachine.SwitchState(new EnemyIdleState(stateMachine));
        }

        spawnIdleDelay -= deltaTime;
    }

    public override void Exit()
    {

    }

}
