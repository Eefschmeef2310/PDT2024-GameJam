using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldTower : BaseTower, ITower
{
    private BaseCharacterScript playerScript;
    [SerializeField] private float ascendedShieldBreakRadius = 100;

    protected override void Start()
    {
        base.Start();
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<BaseCharacterScript>();
    }

    protected override void Fire()
    {
        base.Fire();
        TowerStageFire();
    }

    private void TowerStageFire()
    {
        switch (towerStageNumber)
        {
            case 4:
                Ascended();
                goto case 3;
            case 3:
                V3();
                break;
            case 2:
                V2();
                break;
            case 1:
                V1();
                break;
            default:
                Debug.Log("Invalid tower level.");
                break;
        }
    }

    // Apply shield amount
    public void V1()
    {
        playerScript.AddShield(power);
    }

    // Apply shield amount + shield break damage
    public void V2()
    {
        playerScript.AddShield(power, power);
    }

    // Apply shield amount + shield break damage + freeze status effect
    public void V3()
    {
        playerScript.AddShield(power, power, StatusEffects.FREEZE);
    }

    // Set shield break radius to ascendedShieldBreakRadius value
    public void Ascended()
    {
        playerScript.ShieldBreakEffectSize = ascendedShieldBreakRadius;
    }
}