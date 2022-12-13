using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

//Класс для реклам между забегами
public class InterAds : MonoBehaviour
{
    private InterstitialAd _interstitialAd;
    //Айди межстраничной рекламы
    private string _interstitalUnitId = "ca-app-pub-3940256099942544/1033173712";

    //Загрузка рекламы
    private void OnEnable()
    {
        _interstitialAd = new InterstitialAd(_interstitalUnitId);
        AdRequest adRequest = new AdRequest.Builder().Build();
        _interstitialAd.LoadAd(adRequest);
    }
    //Показ рекламы
    public void ShowAd()
    {
        if (_interstitialAd.IsLoaded())
        {
            _interstitialAd.Show();
        }
    }
}
