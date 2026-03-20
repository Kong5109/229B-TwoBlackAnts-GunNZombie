using UnityEngine;

public enum GunType
{
    RayCastGunType,
    ProjectileGunType
}

public class Weapon : MonoBehaviour
{
    [Header("Guns")]
    [SerializeField] private GunData[] guns;

    [Header("RayCast Settings")]
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float raycastRange = 100f;
    [SerializeField] private LayerMask hitLayer;

    [Header("Projectile Settings")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float projectileSpeed = 20f;

    [Header("Reload Settings")]
    [SerializeField] private float reloadTime = 1.5f;

    public GunData CurrentGunData { get; private set; }
    private int currentGunIndex = 0;

    private void Start()
    {
        foreach (var gun in guns)
        {
            gun.Initialize();
            gun.gunObject.SetActive(false);
        }

        if (guns.Length > 0)
        {
            currentGunIndex = 0;
            CurrentGunData = guns[currentGunIndex];
            CurrentGunData.gunObject.SetActive(true);
        }
    }

    // --- Switch Gun ---------------------------------
    public void SwitchGun()
    {
        if (guns.Length <= 1) return;

        CurrentGunData.gunObject.SetActive(false);

        currentGunIndex = (currentGunIndex + 1) % guns.Length;
        CurrentGunData = guns[currentGunIndex];

        CurrentGunData.gunObject.SetActive(true);
    }

    // --- Shoot ---------------------------------
    public void Shoot()
    {
        if (CurrentGunData.IsReloading) { return; }
        if (CurrentGunData.CurrentAmmo <= 0) { return; }

        switch (CurrentGunData.gunType)
        {
            case GunType.RayCastGunType: ShootRayCast(); break;
            case GunType.ProjectileGunType: ShootProjectile(); break;
        }

        CurrentGunData.UseAmmo();
    }

    private void ShootRayCast()
    {
        if (playerCamera == null) return;
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        if (Physics.Raycast(ray, out RaycastHit hit, raycastRange, hitLayer))
            Debug.Log($"[RayCast] Hit: {hit.collider.name}");
    }

    private void ShootProjectile()
    {
        if (projectilePrefab == null || firePoint == null) return;
        GameObject p = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        if (p.TryGetComponent<Rigidbody>(out var rb))
            rb.linearVelocity = firePoint.forward * projectileSpeed;
    }

    // --- Reload ---------------------------------
    public void Reload()
    {
        CurrentGunData.StartReload(this, reloadTime);
    }
}