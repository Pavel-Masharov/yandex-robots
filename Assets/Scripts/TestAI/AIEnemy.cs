using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AIEnemy : MonoBehaviour
{
    [SerializeField] private Transform _target;


    public NavMeshAgent agent;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(_target.position);
    }

    
    public void Move(Transform target)
    {
        _target = target;
        agent.SetDestination(_target.position);
    }
}
