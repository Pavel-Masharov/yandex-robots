using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ItemMode : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textTitle;
    [SerializeField] private Image _image;

    public TypeGameModes TypeMode { get; private set; }

    private UnityAction _actionUpdateMode;

    public void InitializeMode(GameModeData gameModeData, UnityAction actionUpdateMode)
    {
        _textTitle.text = gameModeData.nameMode;
        _image.sprite = gameModeData.sprite;
        TypeMode = gameModeData.typeMode;
        _actionUpdateMode = actionUpdateMode;

        GetComponent<Button>().onClick.AddListener(ActivateMode);
    }

    public void ActivateMode()
    {
        GetComponent<Image>().color = Color.green;
        SettingsGame.selectedTypeGameMode = TypeMode;
        _actionUpdateMode?.Invoke();
    }

    public void DeactivateMode()
    {
        GetComponent<Image>().color = Color.white;
    }


}
