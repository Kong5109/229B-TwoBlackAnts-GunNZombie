using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class GunData : MonoBehaviour
{
    [field: Header("Gun Attrubute")]
    public GameObject gunObject;
    public GunType gunType;
    public int maxAmmo = 30;

    [field: Header("Gun Data")]
    [field: SerializeField] public int CurrentAmmo { get; private set; }
    [field: SerializeField] public bool IsReloading { get; private set; }
    [field: SerializeField] public float ReloadRemaningTime { get; private set; } = 1f;

    public void Initialize()
    {
        CurrentAmmo = maxAmmo;
        IsReloading = false;
    }

    public void StartReload(float reloadTime)
    {
        if (IsReloading || CurrentAmmo == maxAmmo) return;
        ReloadRemaningTime = reloadTime;
        IsReloading = true;
    }

    private void Update()
    {
        if (IsReloading == false) { return; }

        if (ReloadRemaningTime < 0)
        {
            CurrentAmmo = maxAmmo;
            IsReloading = false;
            return;
        }

        ReloadRemaningTime -= Time.deltaTime;
    }

    public void UseAmmo()
    {
        if (CurrentAmmo > 0) CurrentAmmo--;
    }
}