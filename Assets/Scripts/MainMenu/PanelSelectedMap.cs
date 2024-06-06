using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PanelSelectedMap : MonoBehaviour
{
    [SerializeField] private ItemMap _itemMapPrefab;
    [SerializeField] private Transform _parentItems;
    [SerializeField] private TextMeshProUGUI _textTitle;

    private List<ItemMap> _listItemMaps = new List<ItemMap>();
    public void Initialize(string textTitle, List<GameMapData> gameMaps)
    {
        _textTitle.text = textTitle;

        foreach (var item in gameMaps)
        {
            ItemMap map = Instantiate(_itemMapPrefab, _parentItems);
            map.InitializeMap(item, UpdatePanelMap);

            _listItemMaps.Add(map);
        }


        _listItemMaps[0].ActivateMap();
    }


   

    public void UpdatePanelMap()
    {

        foreach (var item in _listItemMaps)
        {
            if (item.TypeMap == SettingsGame.selectedTypeMap)
                continue;

            item.DeactivateMap();
        }
    }
}
