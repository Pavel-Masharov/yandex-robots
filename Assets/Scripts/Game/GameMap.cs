using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMap : MonoBehaviour
{

    [SerializeField] private TypeGameMap _typeMap;
    [SerializeField] private Transform _positionPlayer;
    [SerializeField] private List<Transform> _listPositionsEnemy;
    public List<Transform> GetListPositionsEnemy()
    {
        return _listPositionsEnemy;
    }

    public Transform GetPositionPlayer()
    {
        return _positionPlayer;
    }

    public TypeGameMap GetTypeMap()
    {
        return _typeMap;
    }
}
