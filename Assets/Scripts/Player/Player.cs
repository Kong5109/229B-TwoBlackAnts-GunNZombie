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
        EventBus ??= FindAnyObjectByType<EventBus>();
    }
    private void Start()
    {
        Camera = Camera.main;

        EventBus.RaiseGameStart();
    }

    private void OnEnable()
    {
        InputReader.OnAttacking += Shoot;
        InputReader.OnChangingWeapon += ChangeWeapon;
    }

    private void OnDisable()
    {
        InputReader.OnAttacking -= Shoot;
        InputReader.OnChangingWeapon -= ChangeWeapon;
    }
    private void Update()
    {
        Debug.DrawRay(WeaponHolder.transform.position, WeaponHolder.transform.forward * 100, Color.red);
    }

    private void Shoot()
    {
        Weapon.Shoot();
    }

    private void ChangeWeapon()
    {
        Weapon.SwitchGun();
    }
}
