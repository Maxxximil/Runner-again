using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectebleItems : MonoBehaviour
{
    [SerializeField] private string itemName;
    [SerializeField] public Transform place;

    //��� ����� ������� ��� ���������� ����������
    private void OnTriggerEnter(Collider other)
    {
        this.gameObject.SetActive(false);
        Messenger.Broadcast("ADD_COINS");
    }
}
