using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBullet : MonoBehaviour
{
    private float damage;
    private float speed = 1;
    private int maxHit;
    private Rigidbody2D rb;

    public void InitaliseBullet(float damage, float speed, int maxHit = 1)
    {
        this.damage = damage;
        this.speed = speed;
        this.maxHit = maxHit;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.velocity = transform.up * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<BaseCharacterScript>().TakeDamage(damage);
        }
    }
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
