using UnityEngine;

public class EnemyChasingState : EnemyBaseState
{
    public EnemyChasingState(EnemyStateMachine stateMachine) : base(stateMachine)
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
            if (IsInRange(startPos, targetPos, stateMachine.Enemy.AttackRange))
            {
                stateMachine.SwitchState(new EnemyAttackState(stateMachine));
                return;
            }

            FaceTarget(stateMachine.Enemy.Target.transform.position);
            MoveTo(stateMachine.Enemy.Target.transform.position);
        }
        else
        {
            stateMachine.SwitchState(new EnemyIdleState(stateMachine));
            return;
        }
    }

    public override void Exit()
    {
    }
}
