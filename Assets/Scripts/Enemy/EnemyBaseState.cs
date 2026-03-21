using UnityEngine;

public abstract class EnemyBaseState : State
{
    public EnemyStateMachine stateMachine;

    public EnemyBaseState(EnemyStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    protected bool IsInRange(Vector3 startPos,Vector3 targetPos,float Range)
    {
        float distanceSqr = (targetPos - startPos).sqrMagnitude;
        return distanceSqr < Range * Range;
    }

    protected void FaceTarget(Vector3 targetPos)
    {
        Vector3 lookPos = targetPos - stateMachine.transform.position;
        lookPos.y = 0f;

        stateMachine.transform.rotation = Quaternion.LookRotation(lookPos);
    }

    protected void MoveTo(Vector3 targetPos)
    {
        Vector3 moveDirection = targetPos - stateMachine.transform.position;
        moveDirection.y = 0f;
        moveDirection = moveDirection.normalized;

        stateMachine.Enemy.Rigidbody.linearVelocity = moveDirection * stateMachine.Enemy.MoveSpeed;
    }
}
