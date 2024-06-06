using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
public class AIManger : MonoBehaviour
{
    [SerializeField] private GameObject _mapPrefab;
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private AIEnemy _enemyPrefab;

    public NavMeshSurface surface;

    void Start()
    {
        var map = Instantiate(_mapPrefab);

        surface = GetComponent<NavMeshSurface>();
        surface.BuildNavMesh();

        var player = Instantiate(_playerPrefab);
        var enemy = Instantiate(_enemyPrefab);

        enemy.Move(player.transform);

       
    }

   
}
