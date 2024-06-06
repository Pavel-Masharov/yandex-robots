using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PopupGameEnd : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _tmpResult;
    [SerializeField] private TextMeshProUGUI _tmpInfo;
    [SerializeField] private Button _buttonContinueForAdvertising;
    [SerializeField] private Button _buttonContinue;
    [SerializeField] private Button _buttonReturnToMenu;

    private bool _isShow = false;
    public void ShowPopup(string textResult, string textInfo, bool isGameEnd, bool isVictory = false)
    {
        if (_isShow)
            return;


#if !UNITY_EDITOR
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
#endif


        if (isGameEnd)
        {
            _buttonContinue.gameObject.SetActive(false);

            if(isVictory)
                _buttonContinueForAdvertising.gameObject.SetActive(false);
        }
            
        else
            _buttonContinueForAdvertising.gameObject.SetActive(false);

         

        SetTextResult(textResult);
        SetTextInfo(textInfo);

        gameObject.SetActive(true);


        Time.timeScale = 0;

        _isShow = true;
    }

    public void HidePopup()
    {
        if (!_isShow)
            return;

#if !UNITY_EDITOR
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
#endif


        _buttonContinueForAdvertising.gameObject.SetActive(true);
        _buttonContinue.gameObject.SetActive(true);

        gameObject.SetActive(false);
        _isShow = false;

        SettingsGame.isGamePause = false;

        Time.timeScale = 1;

        GameAudioController.instance.EnableAllSounds();
    }

    public void SetTextResult(string textResult)
    {
        _tmpResult.text = textResult;
    }
    public void SetTextInfo(string textInfo)
    {
        _tmpInfo.text = textInfo;
    }
    public void OnClickButtonReturnToMenu()
    {
        HidePopup();
        SceneManager.LoadScene("MainMenu");
    }
    public void OnClickButtonContinueForAdvertising()
    {
        GameAudioController.instance.DisableAllSounds();

#if UNITY_EDITOR
        HidePopup();

#else
        
        YandexManagerSDK.ShowRewardVideoForContinue();
#endif
    }
    public void OnClickButtonContinue()
    {
        HidePopup();
    }
}
