using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    public EnemyIdleState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
    }
    public override void Tick(float deltaTime)
    {
        if (stateMachine.Enemy.Target != null)
        {
            Vector3 startPos = stateMachine.transform.position;
            Vector3 targetPos = stateMachine.Enemy.Target.transform.position;
            if (IsInRange(startPos, targetPos, stateMachine.Enemy.ChasingRange))
            {
                stateMachine.SwitchState(new EnemyChasingState(stateMachine));
                return;
            }
            Debug.Log(IsInRange(startPos, targetPos, stateMachine.Enemy.ChasingRange));
        }
    }

    public override void Exit()
    {
    }

}
