using UnityEngine;

public class EnemyStateMachine : StateMachine
{
    [field:SerializeField] public Enemy Enemy {  get; private set; }

    private void Awake()
    {
        Enemy?.GetComponent<Enemy>();
    }

    private void OnEnable()
    {
        Enemy.OnTakeDamage += EnterTakeDamageState;
        Enemy.OnDeath += EnterDeathState;
    }

    private void OnDisable()
    {
        Enemy.OnTakeDamage -= EnterTakeDamageState;
        Enemy.OnDeath -= EnterDeathState;
    }


    private void Start()
    {
        SwitchState(new EnemySpawnState(this));
    }

    public void EnterDeathState()
    {
        SwitchState(new EnemyDeathState(this));
    }

    public void EnterTakeDamageState()
    {
        Enemy.AudioSource.PlayOneShot(Enemy.SFXHurt);
        Enemy.HealthBar.value = Enemy.Health;
    }
}
