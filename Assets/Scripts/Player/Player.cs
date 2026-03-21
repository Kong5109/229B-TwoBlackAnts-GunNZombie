using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public event Action OnPlayerShooting;

    [field:SerializeField] public InputReader InputReader { get; private set; }
    [field:SerializeField] public Weapon Weapon { get; private set; }
    [field: SerializeField] public EventBus EventBus { get; private set; }
    [field: SerializeField] public GameObject WeaponHolder { get; private set; }
    [field: SerializeField] public bool IsShootingMode { get; private set; } = false;
    public Camera Camera { get; private set; }
    private void Awake()
    {
        InputReader?.GetComponent<InputReader>();
        Weapon?.GetComponent<Weapon>();

        EventBus = FindAnyObjectByType<EventBus>();
    }
    private void Start()
    {
        Camera = Camera.main;
    }

    private void Update()
    {
        Debug.DrawRay(WeaponHolder.transform.position, WeaponHolder.transform.forward * 100, Color.red);
    }
}
