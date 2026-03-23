using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private EventBus eventBus;
    [SerializeField] private Image reloadImage;
    [SerializeField] private GameObject crossHair;
    [SerializeField] private GameObject uiCursor;
    [SerializeField] private GameObject raycastGunAmmoHolder;
    [SerializeField] private GameObject[] raycastGunAmmo;
    [SerializeField] private GameObject projectileGunAmmoHolder;
    [SerializeField] private GameObject[] projectileGunAmmo;
    [SerializeField] private Slider progressBar;

    private EnemySpawner spawner;
    private GunData currentGun;
    private Player currentPlayer;

    private void Awake()
    {
        eventBus = FindAnyObjectByType<EventBus>();
        currentPlayer = FindAnyObjectByType<Player>();
        spawner = FindFirstObjectByType<EnemySpawner>();
    }

    private void OnEnable()
    {
        eventBus.OnGunUpdate += SetCurrentGun;

        progressBar.maxValue = spawner.EnemyKillToWin;
        progressBar.value = spawner.KillCounter;
    }

    private void OnDisable()
    {
        eventBus.OnGunUpdate -= SetCurrentGun;
    }
    private void Update()
    {
        if (currentGun != null)
        {
            if (currentGun.IsReloading)
            {
                float maxValue = currentGun.ReloadTime;
                float value = currentGun.ReloadRemaningTime / maxValue;

                crossHair.SetActive(false);
                reloadImage.gameObject.SetActive(true);
                reloadImage.fillAmount = value;
            }
            else
            {
                reloadImage.gameObject.SetActive(false);
                crossHair.SetActive(true);
            }
        }
        progressBar.value = spawner.KillCounter;
        uiCursor.transform.position = Mouse.current.position.ReadValue();
    }

    private void SetCurrentGun(GunData gunData)
    {
        currentGun = gunData;
        reloadImage.fillAmount = 1;

        if (currentGun.GunType == GunType.RayCastGunType)
        {
            raycastGunAmmoHolder?.SetActive(true);
            projectileGunAmmoHolder?.SetActive(false);


            for (int i = 0; i < raycastGunAmmo.Length; i++)
            {
                if (i < currentGun.CurrentAmmo)
                {
                    raycastGunAmmo[i]?.gameObject.SetActive(true);
                }
                else
                {
                    raycastGunAmmo[i]?.gameObject.SetActive(false);
                }
            }
        }
        else if (currentGun.GunType == GunType.ProjectileGunType)
        {
            raycastGunAmmoHolder?.SetActive(false);
            projectileGunAmmoHolder?.SetActive(true);

            for (int i = 0; i < projectileGunAmmo.Length; i++)
            {
                if (i < currentGun.CurrentAmmo)
                {
                    projectileGunAmmo[i]?.gameObject.SetActive(true);
                }
                else
                {
                    projectileGunAmmo[i]?.gameObject.SetActive(false);
                }
            }
        }
    }
}
