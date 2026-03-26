using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private EventBus eventBus;
    [SerializeField] private Image reloadImage;
    [SerializeField] private GameObject crossHair;
    [SerializeField] private Image[] crossHairImages;
    [SerializeField] private Color IdleCrosshairColor;
    [SerializeField] private Color ShootCrosshairColor;
    [SerializeField] private GameObject uiCursor;
    [SerializeField] private GameObject raycastGunAmmoHolder;
    [SerializeField] private GameObject[] raycastGunAmmo;
    [SerializeField] private GameObject projectileGunAmmoHolder;
    [SerializeField] private GameObject[] projectileGunAmmo;
    [SerializeField] private Slider progressBar;
    [SerializeField] private GameObject endUI;

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
        eventBus.OnGunShoot += GunShootCrosshairVFX;
        eventBus.OnGameOver += OnEndGame;

        progressBar.maxValue = spawner.EnemyKillToWin;
        progressBar.value = spawner.KillCounter;

        endUI.SetActive(false);
    }

    private void OnDisable()
    {
        eventBus.OnGunUpdate -= SetCurrentGun;
        eventBus.OnGunShoot -= GunShootCrosshairVFX;
        eventBus.OnGameOver -= OnEndGame;

        Time.timeScale = 1f;
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

    private void GunShootCrosshairVFX()
    {
        StartCoroutine(ShootCrosshairRoutine());
    }

    private IEnumerator ShootCrosshairRoutine()
    {
        foreach (var img in crossHairImages)
        {
            img.color = ShootCrosshairColor;
        }
        yield return new WaitForSeconds(0.1f);
        foreach (var img in crossHairImages)
        {
            img.color = IdleCrosshairColor;
        }
    }

    private void OnEndGame()
    {
        Time.timeScale = 0f;
        endUI.SetActive(true);
    }
}
