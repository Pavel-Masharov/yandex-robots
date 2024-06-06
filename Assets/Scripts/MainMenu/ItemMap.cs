using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ItemMap : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textTitle;
    [SerializeField] private Image _image;

    public TypeGameMap TypeMap { get; private set; }

    private UnityAction _actionUpdateMaps;

    public void InitializeMap(GameMapData gameModeData, UnityAction actionUpdateMaps)
    {
        _textTitle.text = gameModeData.nameMap;
        _image.sprite = gameModeData.sprite;
        TypeMap = gameModeData.typeMap;
        _actionUpdateMaps = actionUpdateMaps;

        GetComponent<Button>().onClick.AddListener(ActivateMap);
    }

    public void ActivateMap()
    {
        GetComponent<Image>().color = Color.green;
        SettingsGame.selectedTypeMap = TypeMap;
        _actionUpdateMaps?.Invoke();
    }

    public void DeactivateMap()
    {
        GetComponent<Image>().color = Color.white;
    }
}
