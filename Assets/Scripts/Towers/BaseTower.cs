using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTower : MonoBehaviour
{
    [SerializeField] private TowerStats TowerStats;


    protected bool canFire = true;
    protected float fireRate;
    protected float power;
    protected float range;
    protected int towerStageNumber = 1;

    public TowerStage GetTowerStage()
    {
        switch (towerStageNumber)
        {
            case 1:
                return TowerStage.V1;
            case 2:
                return TowerStage.V2;
            case 3:
                return TowerStage.V3;
            case 4:
                return TowerStage.Ascended;
            default:
                Debug.Log($"Invalid Level: {towerStageNumber}");
                return TowerStage.V1;
        }
    }

    protected virtual void Start()
    {
        InitaliseTowerStats();
    }

    protected virtual void Update()
    {
        if (canFire)
        {
            Fire();
        }
    }

    protected virtual void Fire() {
        StartCoroutine(FireRateHandler()); 
    }

    private IEnumerator FireRateHandler()
    {
        canFire = false;
        float nextToFire = 1 / fireRate;
        yield return new WaitForSeconds(nextToFire);
        canFire = true;
    }

    private void InitaliseTowerStats()
    {
        if (TowerStats == null)
        {
            Debug.LogError($"Tower Stats for {name} is missing!");
            return;
        }

        power = TowerStats.power;
        fireRate = TowerStats.fireRate;
        range = TowerStats.range;
    }

    public void LevelTower()
    {
        if (towerStageNumber == 4)
            return;
        towerStageNumber++;
    }
}

public enum TowerStage
{
    V1,
    V2,
    V3,
    Ascended,
}
