using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Firestore;

[FirestoreData]
public struct CharacterData
{
    [FirestoreProperty]
    public string Name { get; set; }

    [FirestoreProperty]
    public string Description { get; set; }

    [FirestoreProperty]
    public int Atack { get; set; }

    [FirestoreProperty]
    public int Deffence { get; set; }
}

public class FirestoreEXample : MonoBehaviour
{
    [SerializeField] private string _characterPath = "character_sheets/one_cool_dude";

    [SerializeField] private InputField _nameField;
    [SerializeField] private InputField _descriptionField;
    [SerializeField] private InputField _attackField;
    [SerializeField] private InputField _deffenceField;

    [SerializeField] private Button _submitButton;

    private void Start()
    {
        _submitButton.onClick.AddListener(() =>
        {
            var characterData = new CharacterData
            {
                Name = _nameField.text,
                Description = _descriptionField.text,
                Atack = int.Parse(_attackField.text),
                Deffence = int.Parse(_deffenceField.text),
            };
            var firestore = FirebaseFirestore.DefaultInstance;
            firestore.Document(_characterPath).SetAsync(characterData);
        });
    }


}
