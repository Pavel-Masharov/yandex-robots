using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SettingsGameData", menuName = "ScriptableObjects/SettingsGameData")]

public class SettingsGameData : ScriptableObject
{
    [Header("For Modes")] [SerializeField] private List<GameModeData> _listAllGameModeData;
    [Header("For Maps")] [SerializeField] private List<GameMapData> _listAllGameMapData;
    public List<GameModeData> GetListAllGameModeData()
    {
        return _listAllGameModeData;
    }

    public List<GameMapData> GetListAllGameMapData()
    {
        return _listAllGameMapData;
    }
}

[System.Serializable]
public class GameModeData
{
    public string nameMode;
    public Sprite sprite;
    public TypeGameModes typeMode;
}

[System.Serializable]
public class GameMapData
{
    public string nameMap;
    public Sprite sprite;
    public TypeGameMap typeMap;
}