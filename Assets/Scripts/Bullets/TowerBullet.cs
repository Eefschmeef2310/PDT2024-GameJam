using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBullet : MonoBehaviour
{
    private float damage;
    [SerializeField] private float speed = 1;
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<BaseCharacterScript>().TakeDamage(damage);
            RemoveOneMultiHit();
        }
    }

    private void RemoveOneMultiHit()
    {
        maxHit--;
        if (maxHit <= 0)
        {
            OnBecameInvisible();
        }
    }
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
