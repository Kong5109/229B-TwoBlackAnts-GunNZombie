using System.Collections;
using UnityEngine;

[System.Serializable]
public class GunData
{
    public GameObject gunObject;
    public GunType gunType;
    public int maxAmmo = 30;

    public int CurrentAmmo { get; private set; }
    public bool IsReloading { get; private set; }
    private Coroutine reloadCoroutine;

    public void Initialize()
    {
        CurrentAmmo = maxAmmo;
        IsReloading = false;
    }

    public void StartReload(MonoBehaviour owner, float reloadTime)
    {
        if (IsReloading || CurrentAmmo == maxAmmo) return;
        reloadCoroutine = owner.StartCoroutine(ReloadCoroutine(reloadTime));
    }

    private IEnumerator ReloadCoroutine(float reloadTime)
    {
        IsReloading = true;
        yield return new WaitForSeconds(reloadTime);
        CurrentAmmo = maxAmmo;
        IsReloading = false;
    }

    public void UseAmmo()
    {
        if (CurrentAmmo > 0) CurrentAmmo--;
    }
}