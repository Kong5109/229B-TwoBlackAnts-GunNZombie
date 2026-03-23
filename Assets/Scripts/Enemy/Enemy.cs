using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public event Action OnTakeDamage;
    public event Action OnDeath;

    [field: SerializeField] public Player Target { get; private set; }
    [field: SerializeField] public Rigidbody Rigidbody { get; private set; }

    [field: SerializeField] public float MoveSpeed { get; private set; } = 1f;
    [field: SerializeField] public float ChasingRange { get; private set; } = 50f;
    [field: SerializeField] public float AttackRange { get; private set; } = 2f;
    [field: SerializeField] public int Health { get; private set; } = 100;

    private float DestroyTime = 5;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Target ??= FindFirstObjectByType<Player>();
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health > 0)
        {
            OnTakeDamage?.Invoke();
        }
        else
        {
            OnDeath?.Invoke();
            Destroy(gameObject, DestroyTime);
        }
    }
}
