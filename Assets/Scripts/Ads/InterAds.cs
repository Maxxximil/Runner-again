using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
public class InterAds : MonoBehaviour
{
    private InterstitialAd _interstitialAd;

    private string _interstitalUnitId = "ca-app-pub-3940256099942544/1033173712";

    private void OnEnable()
    {
        _interstitialAd = new InterstitialAd(_interstitalUnitId);
        AdRequest adRequest = new AdRequest.Builder().Build();
        _interstitialAd.LoadAd(adRequest);
    }

    public void ShowAd()
    {
        if (_interstitialAd.IsLoaded())
        {
            _interstitialAd.Show();
        }
    }
}