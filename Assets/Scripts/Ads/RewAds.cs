using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
//Реклама за вознаграждение
public class RewAds : MonoBehaviour
{
    //Айди
    private string _rewardedUnitAd = "ca-app-pub-3940256099942544/5224354917";
    //Награда
    public int Reward = 50;

    private RewardedAd _rewardedAd;
    //Загрузка
    private void OnEnable()
    {
        _rewardedAd = new RewardedAd(_rewardedUnitAd);
        AdRequest adRequest = new AdRequest.Builder().Build();
        _rewardedAd.LoadAd(adRequest);
        _rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
    }
    //Добавление монет после просмотра
    private void HandleUserEarnedReward(object sender, Reward e)
    {
        Managers.Coin.AddCoins(Managers.Auth.GetID(), Reward);
    }
    //Показ рекламы
    public void ShowAd()
    {
        if (_rewardedAd.IsLoaded())
        {
            _rewardedAd.Show();
        }
    }
}
