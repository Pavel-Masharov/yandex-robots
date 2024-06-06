using Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTarget : MonoBehaviour, ITakeDamage
{
    //public float _health => 100;

    public float MaxHealth => 100;

    public void TakeDamage(float damage)
    {
        Debug.Log("TakeDamage-test");
    }
}
