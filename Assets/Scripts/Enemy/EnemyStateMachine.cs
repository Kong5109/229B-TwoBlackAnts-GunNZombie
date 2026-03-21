using UnityEngine;

public class EnemyStateMachine : StateMachine
{
    [field:SerializeField] public Enemy Enemy {  get; private set; }

    private void Awake()
    {
        Enemy?.GetComponent<Enemy>();
    }

    private void Start()
    {
        SwitchState(new EnemySpawnState(this));
    }
}
