using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public event Action OnPlayerShooting;

    [field:SerializeField] public InputReader InputReader { get; private set; }
    [field:SerializeField] public Weapon Weapon { get; private set; }
    [field: SerializeField] public EventBus EventBus { get; private set; }
    private void Awake()
    {
        InputReader?.GetComponent<InputReader>();
        Weapon?.GetComponent<Weapon>();

        EventBus = FindAnyObjectByType<EventBus>();
    }
}
