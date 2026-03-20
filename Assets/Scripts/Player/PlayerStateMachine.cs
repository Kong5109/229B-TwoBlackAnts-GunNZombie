using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    [field : SerializeField] public Player Player {  get; private set; }

    private void Awake()
    {
        Player?.GetComponent<Player>();
    }

    void Start()
    {
        SwitchState(new PlayerIdleState(this));
    }
}
