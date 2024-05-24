using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericTower : BaseTower, ITower
{
    [SerializeField] GameObject bulletGameObject;
    [SerializeField] float bulletSpeed;

    private int multiHit = 2;
    private float posOffset = 0.1f;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void Fire()
    {
        TowerStageAttack();

        base.Fire();
    }

    private void TowerStageAttack()
    {
        switch (towerStageNumber)
        {
            case 4:
                Ascended();
                goto case 3;
            case 3:
                V3();
                goto case 2;
            case 2:
                V2();
                goto case 1;
            case 1:
                V1();
                break;
            default:
                Debug.Log("Invalid tower level.");
                break;
        }
    }

    public void V1()
    {
        Vector2 posForward = transform.position + transform.up * posOffset;
        CreateBullet(posForward, Quaternion.identity);
    }

    public void V2()
    {
        Vector2 posLeft = transform.position + -transform.right * posOffset;
        Quaternion rotLeft = Quaternion.Euler(0, 0, 90);
        CreateBullet(posLeft, rotLeft);

        Vector2 posRight = transform.position + transform.right * posOffset;
        Quaternion rotRight = Quaternion.Euler(0, 0, -90);
        CreateBullet(posRight, rotRight);
    }

    public void V3()
    {
        Vector2 posBehind = transform.position + -transform.up * posOffset;
        Quaternion behindRotation = Quaternion.Euler(0, 0, 180);
        CreateBullet(posBehind, behindRotation);
    }

    public void Ascended() 
    { 
        // Penatration bullet is created in Create Bullet
    }

    private void CreateBullet(Vector3 position, Quaternion rotataion)
    {
        GameObject bullet = Instantiate(bulletGameObject, position, rotataion);
        if (towerStageNumber == 4)
        {
            bullet.GetComponent<TowerBullet>().InitaliseBullet(power, bulletSpeed, multiHit);
        }
        else
        {
            bullet.GetComponent<TowerBullet>().InitaliseBullet(power, bulletSpeed);
        }
    }
}
