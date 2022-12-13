using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Информация по внутриигровым покупкам
public class IAPInfo : MonoBehaviour
{
    public static IAPInfo Instance;

    [SerializeField] private GameObject removeAdsButton;
    [SerializeField] private Text coinsTexts;

    private void Start()
    {
        Instance = this;
        UpdateCoinsText();
        UpdateRemaoveAdsButton();
    }

    //Кнопка удаления рекламы
    public void UpdateRemaoveAdsButton()
    {
        bool removeAds = PlayerPrefs.GetInt("removeads") == 1;
        removeAdsButton.SetActive(!removeAds);
    }

    //Кнопка доп монеток
    public void UpdateCoinsText()
    {
        int coins = PlayerPrefs.GetInt("coins");
        coinsTexts.text = "Coins: " + coins;
    }
}
