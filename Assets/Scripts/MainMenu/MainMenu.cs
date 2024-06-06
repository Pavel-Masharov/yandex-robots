using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    [SerializeField] SettingsGameData _settingsGameData;

    [SerializeField] private PanelSelectedMode _panelSelectedMode;
    [SerializeField] private PanelSelectedMap _panelSelectedMap;

    [SerializeField] private Button _buttonStart;
    [SerializeField] private Button _buttonControl;
    [SerializeField] private Button _otherMyGame;

    [SerializeField] private TextMeshProUGUI _textInfo;

    [SerializeField] private PanelControll _panelControl;

    [SerializeField] private GameObject _panelLoading;
    [SerializeField] private Image _imageLoading;

    [SerializeField] private DotsLoading _dotsLoading;

    private string _domen = "";
    private void Awake()
    {
        _buttonStart.onClick.AddListener(OnClickStartGame);
        _buttonControl.onClick.AddListener(OnClickControl);
    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

#if UNITY_EDITOR


#else
        GetDomen();
        GetCurentLang();

        YandexManagerSDK.ShowAdv();
#endif

        InitializeMainMenu();
    }

    private void InitializeMainMenu()
    {
        string textTitlePanelModes = "Выберите режим игры";
        _panelSelectedMode.Initialize(textTitlePanelModes, _settingsGameData.GetListAllGameModeData(), UpdateTextInfo);


        string textTitlePanelMaps = "Выберите карту";
        _panelSelectedMap.Initialize(textTitlePanelMaps, _settingsGameData.GetListAllGameMapData());

        _panelControl.Initialize();

        _buttonStart.onClick.AddListener(OnClickStartGame);
        _buttonControl.onClick.AddListener(OnClickControl);
        _otherMyGame.onClick.AddListener(OnClickOtherMyGames);
    }

    private void OnClickStartGame()
    {
        GameAudioController.instance.PlayClickButton();
        _dotsLoading.StartAnim();
        StartCoroutine(LoadYourAsyncScene());

    }
    private void OnClickControl()
    {
        GameAudioController.instance.PlayClickButton();
        _panelControl.OpenPanel();

    }
    private void OnClickOtherMyGames()
    {
        GameAudioController.instance.PlayClickButton();

        string link = "https://yandex." + _domen + "/games/developer?name=UralPlay#redir-data=%7B%22http_ref%22%3A%22https%253A%252F%252Fyandex.ru%252Fgames%252F%2523app%253D235903%22%2C%22rn%22%3A926082638%7D";

        Application.OpenURL(link);
    }

    private IEnumerator LoadYourAsyncScene()
    {
        _panelLoading.SetActive(true);

        string nameGameScene = "";
        if (SettingsGame.selectedTypeMap == TypeGameMap.City)
        {
            nameGameScene = "Game-Sity";
        }
        else if(SettingsGame.selectedTypeMap == TypeGameMap.Mars)
        {
            nameGameScene = "Game-Mars";
        }
        else
        {
            nameGameScene = "Game-Pluton";
        }

        // SceneManager.LoadScene(nameGameScene);
        yield return SceneManager.LoadSceneAsync(nameGameScene);

        //while (!asyncLoad.isDone)
        //{
        //    _imageLoading.fillAmount = asyncLoad.progress;
        //    yield return null;
        //}

       // yield return null;
    }


    private void UpdateTextInfo()
    {
        if(SettingsGame.selectedTypeGameMode == TypeGameModes.LevelUp)
        {
            int level = Progress.instance.playerInfo.level;
            string text = level + " уровень";
            _textInfo.text = text;
        }
        else if(SettingsGame.selectedTypeGameMode == TypeGameModes.Survival)
        {
            int score = Progress.instance.playerInfo.score;
            string text = score + " очков";
            _textInfo.text = text;
        }

    }

    private void GetDomen()
    {
        string curDomen = YandexManagerSDK.GetCurentTLD();
        _domen = curDomen;
        Debug.Log("ДОМЕН " + curDomen);     
    }

    private void GetCurentLang()
    {
        string curLang = YandexManagerSDK.GetCurentLang();
        Debug.Log("Язык " + curLang);    
    }
}
