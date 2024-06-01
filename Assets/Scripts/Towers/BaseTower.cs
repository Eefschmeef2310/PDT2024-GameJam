using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TowerStage
{
    V1,
    V2,
    V3,
    ASCENDED,
}

public class BaseTower : MonoBehaviour
{
    [SerializeField] private TowerStats TowerStats;
    [SerializeField] private float towerSizeRadius = 0.08f;

    protected bool canFire = true;
    [Tooltip ("Firing every X seconds")]
    protected float fireRate;
    protected float power;
    protected float range;
    protected int towerStageNumber = 1;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, towerSizeRadius);
    }

    public float TowerSizeRadius
    {
        get { return towerSizeRadius; }
    }
    public TowerStage TowerStage
    {
        get
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
                    return TowerStage.ASCENDED;
                default:
                    Debug.Log($"Invalid Level: {towerStageNumber}");
                    return TowerStage.V1;
            }
        }
    }

    protected virtual void Start()
    {
        InitaliseTowerStats();
    }

    protected virtual void Update()
    {
        transform.rotation = Quaternion.identity;
        if (canFire)
        {
            Fire();
        }
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
        towerStageNumber++;
        if (towerStageNumber >= 4)
        {
            towerStageNumber = 4;
        }
        UpdateTowerLevelColour();
    }

    private void UpdateTowerLevelColour()
    {
        switch (towerStageNumber)
        {
            case 4:
                GetComponent<SpriteRenderer>().color = Color.yellow;
                break;
            case 3:
                GetComponent<SpriteRenderer>().color = Color.blue;
                break;
            case 2:
                GetComponent<SpriteRenderer>().color = Color.cyan;
                break;
            case 1:
                GetComponent<SpriteRenderer>().color = Color.white;
                break;
            default:
                Debug.Log("Invalid tower level.");
                break;
        }
    }

    protected virtual void Fire() {
        StartCoroutine(HandleFireRate()); 
    }

    private IEnumerator HandleFireRate()
    {
        canFire = false;
        float nextToFire = fireRate;
        yield return new WaitForSeconds(nextToFire);
        canFire = true;
    }
}
