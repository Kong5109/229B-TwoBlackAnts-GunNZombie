using UnityEngine;

public enum GunType
{
    RayCastGunType,
    ProjectileGunType
}

public class Weapon : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private Player player;

    [Header("Guns")]
    [SerializeField] private GunData[] guns;

    [Header("RayCast Settings")]
    [SerializeField] private float raycastRange = 100f;
    [SerializeField] private GameObject hitVFX;
    [SerializeField] private GameObject shootVFX;
    [SerializeField] private Transform shootVFXPoint;
    [SerializeField] private LayerMask hitLayer;

    [Header("Projectile Settings")]
    [SerializeField] private GameObject projectileShootVFX;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float bulletAcceleration = 80f;

    [field: SerializeField] public GunData CurrentGunData { get; private set; }
    private int currentGunIndex = 0;

    private void Start()
    {
        foreach (var gun in guns)
        {
            gun.Initialize();
            gun.GunObject.SetActive(false);
        }

        if (guns.Length > 0)
        {
            currentGunIndex = 0;
            CurrentGunData = guns[currentGunIndex];
            CurrentGunData.GunObject.SetActive(true);
        }

        player.EventBus.RaiseGunUpdate(CurrentGunData);
    }

    // --- Switch Gun ---------------------------------
    public void SwitchGun()
    {
        if (guns.Length <= 1) return;

        CurrentGunData.GunObject.SetActive(false);

        currentGunIndex = (currentGunIndex + 1) % guns.Length;
        CurrentGunData = guns[currentGunIndex];

        CurrentGunData.GunObject.SetActive(true);
        UpdateGun();
    }

    #region Shoot
    // --- Shoot ---------------------------------
    public void Shoot()
    {
        if (CurrentGunData.IsReloading) { return; }
        if (CurrentGunData.CurrentAmmo <= 0) { return; }

        switch (CurrentGunData.GunType)
        {
            case GunType.RayCastGunType: ShootRayCast(); break;
            case GunType.ProjectileGunType: ShootProjectile(); break;
        }

        DoPlayerShootAnimation();
        CurrentGunData.UseAmmo();
        if (CurrentGunData.CurrentAmmo <= 0)
        {
            Reload();
        }

        player.EventBus.RaiseGunShoot();
        UpdateGun();
    }

    private void ShootRayCast()
    {
        if (player == null) return;

        //Ray ray = player.Camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        Vector3 startPos = this.transform.position;
        Vector3 direction = this.transform.forward;
        if (Physics.Raycast(startPos, direction, out RaycastHit hit, raycastRange, hitLayer))
        {
            GameObject obj = Instantiate(hitVFX, hit.point, Quaternion.identity);
            Destroy(obj, 5f);

            obj = Instantiate(shootVFX, shootVFXPoint.position , Quaternion.identity);
            Destroy(obj, 5f);

            if (hit.collider.TryGetComponent<Enemy>(out Enemy enemy))
            {
                enemy.TakeDamage(CurrentGunData.WeaponDamage);
            }
        }
    }

    private void ShootProjectile()
    {
        if (projectilePrefab == null || shootPoint == null) return;

        GameObject bullet = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);
        DamageDealer damage = bullet.GetComponent<DamageDealer>();
        damage.SetDamage(CurrentGunData.WeaponDamage);

        if (bullet.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            float mass = rb.mass;
            float force = mass * bulletAcceleration;
            rb.AddForce(force * shootPoint.forward, ForceMode.Impulse);
        }

        GameObject obj = Instantiate(projectileShootVFX, shootPoint.position, Quaternion.identity);
        Destroy(obj, 5f);
    }
    #endregion
    // --- Reload ---------------------------------
    public void Reload()
    {
        CurrentGunData.StartReload();
    }

    public void UpdateGun()
    {
        player.EventBus.RaiseGunUpdate(CurrentGunData);
    }

    private void DoPlayerShootAnimation()
    {
        //player.Animator.Play("Shoot", 0, 0f);
        player.Animator.CrossFadeInFixedTime("Shoot", 0.1f);
    }
}