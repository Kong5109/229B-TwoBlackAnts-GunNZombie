using System;
using Unity.Services.LevelPlay;
using UnityEngine;

public class AdsSample : MonoBehaviour
{
    private LevelPlayBannerAd bannerAd;
    public LevelPlayInterstitialAd interstitialAd;
    private LevelPlayRewardedAd rewardedVideoAd;
    bool isAdsEnabled = false;

    public bool IsAdsEnabled => isAdsEnabled;
    public event Action OnInitialized;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LevelPlay.ValidateIntegration();

        LevelPlay.OnInitSuccess += SdkInitCompletedEvent;
        LevelPlay.OnInitFailed += SdkInitFailedEvent;

        LevelPlay.Init(AdConfig.AppKey);
    }

    private void SdkInitFailedEvent(LevelPlayInitError error)
    {
        Debug.Log($"Ads not enable : {error}");
    }

    private void SdkInitCompletedEvent(LevelPlayConfiguration configuration)
    {
        EnableAds();
        isAdsEnabled = true;
        OnInitialized?.Invoke();
    }

    void EnableAds()
    {
        var configBuilder = new LevelPlayBannerAd.Config.Builder();
        configBuilder.SetSize(LevelPlayAdSize.BANNER)
            .SetPosition(LevelPlayBannerPosition.BottomCenter);
        var bannerConfig = configBuilder.Build();

        bannerAd = new LevelPlayBannerAd(AdConfig.BannerAdUnitId, bannerConfig);

        interstitialAd = new LevelPlayInterstitialAd(AdConfig.InterstitialAdUnitId);
        interstitialAd.OnAdLoaded += (adInfo) => Debug.Log("[AdsSample] Interstitial Ad loaded successfully.");
        interstitialAd.OnAdLoadFailed += (error) => Debug.LogWarning($"[AdsSample] Interstitial Ad load failed: {error}");

        rewardedVideoAd = new LevelPlayRewardedAd(AdConfig.RewardedAdUnitId);
    }

    public void LoadBanderAds()
    {
        if (bannerAd != null)
        {
            bannerAd.LoadAd();
        }
        else
        {
            Debug.LogWarning("[AdsSample] Banner Ad is not ready yet.");
        }
    }

    public void ShowBanderAds()
    {
        if (bannerAd != null)
        {
            bannerAd.ShowAd();
        }
        else
        {
            Debug.LogWarning("[AdsSample] Banner Ad is not ready yet.");
        }
    }

    public void HideBanderAds()
    {
        bannerAd?.HideAd();
    }

    public void LoadInterstitialAds()
    {
        if (interstitialAd != null)
        {
            interstitialAd.LoadAd();
        }
        else
        {
            Debug.LogWarning("[AdsSample] Interstitial Ad is not initialized yet.");
        }
    }

    public bool IsInterstitialAdReady()
    {
        return interstitialAd != null && interstitialAd.IsAdReady();
    }

    public void ShowInterstitialAds()
    {
        if (interstitialAd != null)
        {
            if (interstitialAd.IsAdReady())
            {
                interstitialAd.ShowAd();
            }
            else
            {
                Debug.Log("[LevelPlaySample] LevelPlay Interstitial Ad is not ready");
            }
        }
        else
        {
            Debug.LogWarning("[AdsSample] Interstitial Ad is not initialized yet.");
        }
    }

    public void LoadRewardedVideoAds()
    {
        if (rewardedVideoAd != null)
        {
            rewardedVideoAd.LoadAd();
        }
        else
        {
            Debug.LogWarning("[AdsSample] Rewarded Video Ad is not ready yet.");
        }
    }

    public bool IsRewardedVideoReady()
    {
        return rewardedVideoAd != null && rewardedVideoAd.IsAdReady();
    }

    public void ShowRewardedVideoAds()
    {
        if (rewardedVideoAd != null)
        {
            if (rewardedVideoAd.IsAdReady())
            {
                rewardedVideoAd.ShowAd();
            }
            else
            {
                Debug.Log("[LevelPlaySample] LevelPlay Rewarded Video Ad is not ready");
            }
        }
    }

    private void OnDestroy()
    {
        LevelPlay.OnInitSuccess -= SdkInitCompletedEvent;
        LevelPlay.OnInitFailed -= SdkInitFailedEvent;

        bannerAd?.DestroyAd();
        interstitialAd?.DestroyAd();
        rewardedVideoAd?.DestroyAd();
    }
}
