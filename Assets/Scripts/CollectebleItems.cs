using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectebleItems : MonoBehaviour
{
    [SerializeField] private string itemName;

    private void OnTriggerEnter(Collider other)
    {
        Managers.Coin.AddCoins(1);
        Destroy(this.gameObject);
        Messenger.Broadcast("ADD_COINS");
    }
}
