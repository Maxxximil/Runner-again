using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CurrentUserSession : MonoBehaviour
{
    [SerializeField] private Text _userName;

    private void Awake()
    {
        Messenger<string>.AddListener("USER_NAME", DisplayUser);
    }

    private void OnDestroy()
    {
        Messenger<string>.RemoveListener("USER_NAME", DisplayUser);

    }

    //private void OnDisable()
    //{
    //    Messenger<string>.RemoveListener("USER_NAME", DisplayUser);

    //}

    //private void OnEnable()
    //{
    //    Messenger<string>.AddListener("USER_NAME", DisplayUser);

    //}

    private void DisplayUser(string name)
    {
        Debug.Log("Name: " + name);
        _userName.text = name;
    }

}
