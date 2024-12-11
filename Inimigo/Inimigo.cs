using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class Inimigo : NetworkBehaviour
{
    public float moveSpeed = 2f;
    public int health = 3;
    public int damageToPlayer = 1;
    public event System.Action OnEnemyDestroyed;
    private Transform player;





    void Update()
    {
        
        GameObject[]  playerObject = GameObject.FindGameObjectsWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject[Random.Range(0, playerObject.Length)].transform;
        }
        else
        {
            Debug.LogError("Jogador não encontrado.");
        }

        if (player != null)
        {

            Vector2 direction = (player.position - transform.position).normalized;
            transform.position += (Vector3)direction * moveSpeed * Time.deltaTime;
        }
    }

    public void TakeDamage(int damage)
    {

        health -= damage;


        if (health <= 0)
        {
            DieServerRPC();

        }
    }

    [ServerRpc]
    private void DieServerRPC()
    { 
        OnEnemyDestroyed?.Invoke();

     
        Destroy(gameObject);
     
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
        {
            
            Player playerScript = collision.GetComponent<Player>();
            if (playerScript != null)
            {
                playerScript.TakeDamage(damageToPlayer);
            }

            
            Destroy(gameObject);
        }
    }
}
