using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTower : MonoBehaviour
{
    [SerializeField] private TowerStats TowerStats;

    protected float power;
    protected float fireRate;
    protected float range;

    protected virtual void Start()
    {
        InitaliseTowerStats();
    }

    private void InitaliseTowerStats()
    {
        if (TowerStats == null)
        {
            Debug.LogError($"Tower Stats for {name} is missing!");
        }

        power = TowerStats.power;
        fireRate = TowerStats.fireRate;
        range = TowerStats.range;
    }
}
