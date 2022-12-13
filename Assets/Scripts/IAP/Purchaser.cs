using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class Purchaser : MonoBehaviour
{
    //При успешной покупке
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
    //Удаление рекламы
    private void RemoveAds()
    {
        PlayerPrefs.SetInt("removeads", 1);
        Debug.Log("Purchase: removeads");
    }

    //Покупка 100 монет
    private void Add100Coins()
    {
        Managers.Coin.AddCoins(Managers.Auth.GetID(), 100);
        Debug.Log("Purchse: get 100 coins");
        Messenger.Broadcast(GameEvent.SHOW_ALL);
    }
}
