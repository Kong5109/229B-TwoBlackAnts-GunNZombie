using UnityEngine;

public static class AdConfig
{
    public static string AppKey => GetAppKey();
    public static string BannerAdUnitId => GetBannerAdUnitID();
    public static string InterstitialAdUnitId => GetInterstitialAdUnitID();
    public static string RewardedAdUnitId => GetRewardedAdUnitID();

    static string GetAppKey()
    {
        return "28c3de40d";
    }

    static string GetBannerAdUnitID()
    {
        return "o602tiapppds4f0w";
    }

    static string GetInterstitialAdUnitID()
    {
        return "ogo2oe8utaiiinal";
    }

    static string GetRewardedAdUnitID()
    {
        return "cc5i5gs61hwwh61v";
    }
}