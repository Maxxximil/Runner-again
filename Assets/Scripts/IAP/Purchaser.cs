using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class Purchaser : MonoBehaviour
{
    //��� �������� �������
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
    //�������� �������
    private void RemoveAds()
    {
        PlayerPrefs.SetInt("removeads", 1);
        Debug.Log("Purchase: removeads");
    }

    //������� 100 �����
    private void Add100Coins()
    {
        Managers.Coin.AddCoins(Managers.Auth.GetID(), 100);
        Debug.Log("Purchse: get 100 coins");
        Messenger.Broadcast(GameEvent.SHOW_ALL);
    }
}
