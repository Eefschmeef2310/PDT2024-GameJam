using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : BaseCharacterScript
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>().AddExperience();
            Destroy(gameObject);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<BaseCharacterScript>().TakeDamage(10);
        }
    }
}
