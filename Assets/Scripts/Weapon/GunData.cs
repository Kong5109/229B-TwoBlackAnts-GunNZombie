using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class GunData : MonoBehaviour
{
    [field: Header("Gun Attrubute")]
    [field: SerializeField] public GameObject GunObject { get; private set; }
    [field: SerializeField] public GunType GunType { get; private set; }
    [field: SerializeField] public int MaxAmmo { get; private set; } = 10;
    [field: SerializeField] public int WeaponDamage { get; private set; } = 30;
    [field: SerializeField] public float ReloadTime { get; private set; } = 1.5f;

    [field: Header("Gun Data")]
    [field: SerializeField] public int CurrentAmmo { get; private set; }
    [field: SerializeField] public bool IsReloading { get; private set; }
    [field: SerializeField] public float ReloadRemaningTime { get; private set; } = 1f;

    private Weapon weapon;
    public void Initialize()
    {
        weapon = GetComponent<Weapon>();
        CurrentAmmo = MaxAmmo;
        IsReloading = false;
    }

    public void StartReload()
    {
        if (IsReloading || CurrentAmmo == MaxAmmo) return;

        ReloadRemaningTime = ReloadTime;
        IsReloading = true;
    }

    private void Update()
    {
        if (IsReloading == false) { return; }

        if (ReloadRemaningTime < 0)
        {
            CurrentAmmo = MaxAmmo;
            IsReloading = false;
            weapon.UpdateGun();
            weapon.TryReloadFinish(this);
            return;
        }

        ReloadRemaningTime -= Time.deltaTime;
    }

    public void UseAmmo()
    {
        if (CurrentAmmo > 0) CurrentAmmo--;
    }
}