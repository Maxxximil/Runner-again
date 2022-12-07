using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class Purchaser : MonoBehaviour
{
    public void OnPurchessedComplite(Product product)
    {
        switch (product.definition.id)
        {
            case "com.Max.Runner.removeads":
                RemoveAds();
                break;
            case "com.Max.Runner.100coins":
                Add100Coins();
                break;
        }
    }

    private void RemoveAds()
    {
        PlayerPrefs.SetInt("removeads", 1);
        Debug.Log("Purchase: removeads");
    }

    private void Add100Coins()
    {
        //int coins = PlayerPrefs.GetInt("coins");
        //int coins = Managers.Coin.GetCoins(Managers.Auth.GetUser());
        //coins += 100;
        //PlayerPrefs.SetInt("coins", coins);
        Managers.Coin.AddCoins(Managers.Auth.GetID(), 100);
        Debug.Log("Purchse: get 100 coins");
        Messenger.Broadcast(GameEvent.SHOW_ALL);
    }
}
