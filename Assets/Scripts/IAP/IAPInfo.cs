using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class IAPInfo : MonoBehaviour
{
    public static IAPInfo Instance;

    [SerializeField] private GameObject removeAdsButton;
    [SerializeField] private Text coinsTexts;

    private void Start()
    {
        Instance = this;
        //PlayerPrefs.SetInt("removeads", 0);
        UpdateCoinsText();
        UpdateRemaoveAdsButton();
    }

    public void UpdateRemaoveAdsButton()
    {
        bool removeAds = PlayerPrefs.GetInt("removeads") == 1;
        removeAdsButton.SetActive(!removeAds);
    }

    public void UpdateCoinsText()
    {
        int coins = PlayerPrefs.GetInt("coins");
        coinsTexts.text = "Coins: " + coins;
    }
}
