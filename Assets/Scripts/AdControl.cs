using System.Collections;
using UnityEngine;

public class AdControl : MonoBehaviour
{
    public AdsSample AdsSample;
    public bool autorunInitAds;

    private void Awake()
    {
        if (AdsSample == null)
        {
            AdsSample = GetComponent<AdsSample>();
        }
    }

    public void OnEnable()
    {
        if (AdsSample == null)
        {
            AdsSample = GetComponent<AdsSample>();
        }

        if (AdsSample != null)
        {
            if (AdsSample.IsAdsEnabled)
            {
                ShowAndLoadAds();
            }
            else
            {
                AdsSample.OnInitialized += OnAdsInitialized;
            }

            if (autorunInitAds)
            {
                StartCoroutine(PlayAdRoutine());
            }
        }
    }

    public IEnumerator PlayAdRoutine()
    {
        // 1. รอจนกว่า AdsSample และ LevelPlay SDK จะ Initialize สำเร็จ
        while (AdsSample == null || !AdsSample.IsAdsEnabled)
        {
            yield return null;
        }

        // 2. ถ้าโฆษณายังไม่พร้อม ให้เริ่มโหลดโฆษณา
        if (!AdsSample.IsInterstitialAdReady())
        {
            AdsSample.LoadInterstitialAds();
        }

        // 3. รอจนกว่าโฆษณาจะโหลดเสร็จสมบูรณ์ (IsAdReady == true)
        float timeout = 10f;
        while (!AdsSample.IsInterstitialAdReady() && timeout > 0f)
        {
            timeout -= Time.unscaledDeltaTime;
            yield return null;
        }

        // 4. เมื่อพร้อมแล้ว สั่งแสดงโฆษณา Interstitial 1 ครั้ง
        if (AdsSample.IsInterstitialAdReady())
        {
            AdsSample.ShowInterstitialAds();
        }
        else
        {
            Debug.LogWarning("[AdControl] Interstitial Ad โหลดไม่สำเร็จหรือหมดเวลา Timeout (10s)");
        }
    }

    public void OnDisable()
    {
        StopAllCoroutines();

        if (AdsSample != null)
        {
            AdsSample.OnInitialized -= OnAdsInitialized;
            if (AdsSample.IsAdsEnabled)
            {
                AdsSample.HideBanderAds();
            }
        }
    }

    private void OnAdsInitialized()
    {
        if (AdsSample != null)
        {
            AdsSample.OnInitialized -= OnAdsInitialized;
            ShowAndLoadAds();
        }
    }

    private void ShowAndLoadAds()
    {
        AdsSample.LoadBanderAds();
        AdsSample.ShowBanderAds();
        AdsSample.LoadRewardedVideoAds();
    }
}
