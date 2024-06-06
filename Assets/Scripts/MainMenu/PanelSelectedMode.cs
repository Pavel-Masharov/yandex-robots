using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class PanelSelectedMode : MonoBehaviour
{
    [SerializeField] private ItemMode _itemModePrefab;
    [SerializeField] private Transform _parentItems;
    [SerializeField] private TextMeshProUGUI _textTitle;

    private List<ItemMode> _listItemModes = new List<ItemMode>();
    private UnityAction _actionUpdateInfo;
    public void Initialize(string textTitle, List<GameModeData> gameModes, UnityAction actionUpdateInfo)
    {
        _textTitle.text = textTitle;

        foreach (var item in gameModes)
        {
            ItemMode mode = Instantiate(_itemModePrefab, _parentItems);
            mode.InitializeMode(item, UpdatePanelModes);

            _listItemModes.Add(mode);
        }

        _actionUpdateInfo = actionUpdateInfo;


        _listItemModes[0].ActivateMode();
    }

    public void UpdatePanelModes()
    {
        _actionUpdateInfo?.Invoke();

        foreach (var item in _listItemModes)
        {
            if (item.TypeMode == SettingsGame.selectedTypeGameMode)
                continue;

            item.DeactivateMode();
        }
    }
}
