using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//Очистка данных в полях ввода
public class NullifyFormData : MonoBehaviour
{
    [SerializeField] private TMP_InputField[] _inputFields;

    private void OnDisable()
    {
        for (int i = 0; i < _inputFields.Length; i++)
        {
            _inputFields[i].text = null;
        }
    }
}
