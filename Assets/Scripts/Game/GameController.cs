using Services.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;
using UnityEngine.Events;
using Unity.AI.Navigation;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private PopupGameEnd _popupGameEnd;
    [SerializeField] private Player _playerDesktopPrefab;
    [SerializeField] private Player _playerPhonePrefab;
    [SerializeField] private EnemyCharacter _enemyCharacterPrefab;
   // [SerializeField] private List<GameMap> _allMaps;
    [SerializeField] private Image _imageLoadingBar;
    [SerializeField] private GameObject _panelLoading;
    [SerializeField] private DotsLoading _dotsLoading;
    [SerializeField] private GameMap _map;

    private Transform _positionPlayer;
    private List<Transform> _listPositionsEnemy;

    private Player _player;

    private IGameMode _gameMode;
    private int _ammountEnemy = 3;
    private int _level = 1;
    private int _quantityKill = 0;

    private Coroutine _coroutineCreateEnemies;

    private NavMeshSurface _surface;

    public bool isSeeRewarded = false;
    private void Awake()
    {
#if !UNITY_EDITOR
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
#endif

       
        //_dotsLoading.StartAnim();

       // _surface = GetComponent<NavMeshSurface>();


        //StartCoroutine(LoadingView(5));
        //_dotsLoading.StartAnim();


        InitializeGame();
    }

    private void Start()
    {
        //string textInfo = _gameMode.GetTextUI(_level, _ammountEnemy, _quantityKill);
        //_player.UpdateInfoUI(textInfo);
    }

    private IEnumerator LoadingView(float timeLoading)
    {
        LoadData();
        CreateMap();
        CreateGameMode();
        //_surface.BuildNavMesh();

        float time = 0;

        while (time < timeLoading)
        {

            yield return null;
            time += Time.deltaTime;
           // Debug.Log(time);
            var percent = time / timeLoading;

            _imageLoadingBar.fillAmount = percent;
        }

        CreatePlayer();
        CreateEnemies();

        string textInfo = _gameMode.GetTextUI(_level, _ammountEnemy, _quantityKill);
        _player.UpdateInfoUI(textInfo);

        _panelLoading.SetActive(false);
    }

    private void InitializeGame()
    {
        LoadData();
        CreateMap();
        CreateGameMode();
        CreatePlayer();
        CreateEnemies();

      // _surface.BuildNavMesh();

       
    }

    private void LoadData()
    {
       // _level = PlayerPrefs.HasKey(ConstantsKey.KEY_CURRENT_LEVEL) ? PlayerPrefs.GetInt(ConstantsKey.KEY_CURRENT_LEVEL) : 1;
        _level = Progress.instance.playerInfo.level;

        _ammountEnemy *= _level;
    }

    private void CreateMap()
    {
        TypeGameMap typeGameMap = SettingsGame.selectedTypeMap;

        //foreach (var item in _allMaps)
        //{
        //    if(item.GetTypeMap() == typeGameMap)
        //    {
        //        var map = Instantiate(item);
        //        map.transform.position = Vector3.zero;
        //        _positionPlayer = map.GetPositionPlayer();
        //        _listPositionsEnemy = map.GetListPositionsEnemy();
        //    }
        //}

        _positionPlayer = _map.GetPositionPlayer();
        _listPositionsEnemy = _map.GetListPositionsEnemy();
    }

    private void CreateGameMode()
    {
        if(SettingsGame.selectedTypeGameMode == TypeGameModes.LevelUp)
        {
            _gameMode = new GameMode_LevelUp(_ammountEnemy);
        }
        else if(SettingsGame.selectedTypeGameMode == TypeGameModes.Survival)
        {
            _gameMode = new GameMode_Survival(_ammountEnemy);
        }
        else
        {
            _gameMode = new GameMode_LevelUp(_ammountEnemy);
        }
    }
    private void CreatePlayer()
    {
        _player = SettingsGame.isPhone ? Instantiate(_playerPhonePrefab) : Instantiate(_playerDesktopPrefab);
        _player.transform.position = _positionPlayer.position;
        _player.InitializePlayer(Defeat, OnPauseGame);

        
    }

    private void CreateEnemies()
    {
        if(_gameMode.TypeGameModes == TypeGameModes.LevelUp)
        {
            for (int i = 0; i < _ammountEnemy; i++)
            {
                CraeteEnemy(_enemyCharacterPrefab, GetPositionSpawnForEnemy(), _player, CheckVictory, i);
            }
        }
        else if(_gameMode.TypeGameModes == TypeGameModes.Survival)
        {
            _coroutineCreateEnemies = StartCoroutine(CoroutineCreateEnemies(0, 3f));
        }
        else
        {
            for (int i = 0; i < _ammountEnemy; i++)
            {
                CraeteEnemy(_enemyCharacterPrefab, GetPositionSpawnForEnemy(), _player, CheckVictory);
            }
        }
       
    }

    private IEnumerator CoroutineCreateEnemies(float delay, float time)
    {
        yield return new WaitForSeconds(delay);

        while (true)
        {
            yield return new WaitForSeconds(time);

            if(!SettingsGame.isGamePause)
                CraeteEnemy(_enemyCharacterPrefab, GetPositionSpawnForEnemy(), _player, CheckVictory);
        }
    }
    private void CraeteEnemy(EnemyCharacter enemyCharacterPrefab, Vector3 spawnPosition, Player player, UnityAction actionCheckVictory, int id = 0)
    {
        EnemyCharacter enemy = Instantiate(enemyCharacterPrefab);
        enemy.transform.position = spawnPosition;
        enemy.InitializeEnemy(player, actionCheckVictory, id);
    }

    private Vector3 GetPositionSpawnForEnemy()
    {
        int index = Random.Range(0, _listPositionsEnemy.Count);
        return _listPositionsEnemy[index].position;
    }


    private void OnPauseGame()
    {
        var textes = _gameMode.GetTextPause(_level, _ammountEnemy, _quantityKill);
        _popupGameEnd.ShowPopup(textes.Item1, textes.Item2, false);
        SettingsGame.isGamePause = true;
    }

    private void CheckVictory()
    {
        _quantityKill++;
        _gameMode.UpdateValues();

        string textInfo = _gameMode.GetTextUI(_level, _ammountEnemy, _quantityKill);
        _player.UpdateInfoUI(textInfo);

        if (_gameMode.CheckVictory())
        {
            Victory();
        }
    }

    private void Victory()
    {
        var textes = _gameMode.GetTextVictory(_level, _ammountEnemy, _quantityKill);
        _popupGameEnd.ShowPopup(textes.Item1, textes.Item2, true, true);

        _level++;

        Progress.instance.playerInfo.level = _level;
        Progress.instance.SaveData();

        SettingsGame.isGamePause = true;
    }
    private void Defeat()
    {
        if(Progress.instance.playerInfo.score < _gameMode.AmmountScore)
        {
            Progress.instance.playerInfo.score = _gameMode.AmmountScore;
            Progress.instance.SaveData();
        }

        var textes = _gameMode.GetTextDefeat(_level, _ammountEnemy, _quantityKill);
        _popupGameEnd.ShowPopup(textes.Item1, textes.Item2, true);

        SettingsGame.isGamePause = true;

    }

    public void ContinueGameAfrerReward()
    {
        isSeeRewarded = true;

        //_popupGameEnd.HidePopup();
        //_player.RestoreHealth(_player.MaxHealth);
        //StartCoroutine(SetTakeDamage(3));
    }

    public void CloseRewardedVideo()
    {
        if(isSeeRewarded)
        {
            _popupGameEnd.HidePopup();
            _player.RestoreHealth(_player.MaxHealth);
            StartCoroutine(SetTakeDamage(3));

            isSeeRewarded = false;
        }
        else
        {

        }   
    }

    private IEnumerator SetTakeDamage(float time)
    {
        yield return new WaitForSeconds(time);
        _player.canTakeDamage = true;
    }
}
