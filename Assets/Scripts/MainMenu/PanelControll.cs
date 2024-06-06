using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelControll : MonoBehaviour
{
    [SerializeField] private Button _buttonClose;
    [SerializeField] private GameObject _desctopInfo, _phoneInfo;
    public void Initialize()
    {
        _buttonClose.onClick.AddListener(ClosePanel);

        if (SettingsGame.isPhone)
        {
            _desctopInfo.SetActive(false);
        }
        else
        {
            _phoneInfo.SetActive(false);
        }
    }

    private void ClosePanel()
    {
        GameAudioController.instance.PlayClickButton();
        gameObject.SetActive(false);
    }

    public void OpenPanel()
    {
        gameObject.SetActive(true);
    }
   
}
