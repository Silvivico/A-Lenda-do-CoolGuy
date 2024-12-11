using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Bala : NetworkBehaviour
{
    public float lifetime = 10f;
    public float drag = 0.1f;
    public float minimumSpeed = 0.5f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        if (rb != null && rb.velocity.magnitude > 0)
        {
            rb.velocity *= (1 - drag * Time.deltaTime);
        }

        if (rb != null && rb.velocity.magnitude < minimumSpeed)
        {
            Destroy(gameObject);
        }
    }



    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Enemy"))
        {
            Inimigo enemy = collision.GetComponent<Inimigo>();
            if (enemy != null)
            {
                enemy.TakeDamage(1);
            }

            Destroy(gameObject);
        }
    }
}
