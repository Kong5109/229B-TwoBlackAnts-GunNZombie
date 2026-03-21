using UnityEngine;

public class Enemy : MonoBehaviour
{
    [field : SerializeField] public Player Target {  get; private set; }
    [field : SerializeField] public Rigidbody Rigidbody {  get; private set; }
    [field: SerializeField] public float MoveSpeed { get; private set; } = 2f;
    [field: SerializeField] public float ChasingRange { get; private set; } = 50f;
    [field: SerializeField] public float AttackRange { get; private set; } = 2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Target ??= FindFirstObjectByType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
