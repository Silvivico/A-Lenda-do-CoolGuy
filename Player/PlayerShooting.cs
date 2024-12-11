using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.Netcode;
using UnityEngine;

public class PlayerShooting : NetworkBehaviour
{
    public GameObject tearPrefab; 
    public float tearSpeed; 
    public Transform firePoint; 
    public Rigidbody2D playerRb; 

    public float shootCooldown; 
    private float lastShootTime; 

    void Update()
    {
        if (!IsOwner) { return; }
        if (Time.time - lastShootTime < shootCooldown)
            return;

        
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Shoot(Vector2.up, 0f); 
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            Shoot(Vector2.down, 180f); 
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            Shoot(Vector2.left, 90f); 
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            Shoot(Vector2.right, -90f); 
        }
    }

    void Shoot(Vector2 direction, float rotation)
    {
        
        lastShootTime = Time.time;

        ShootServerRPC(direction, rotation);
    }

    [ServerRpc]
    private void ShootServerRPC(Vector2 _direction, float _rotation)
    {
        GameObject tear = Instantiate(tearPrefab, firePoint.position,
            Quaternion.Euler(0f, 0f, _rotation));

        if (!tear.GetComponent<NetworkObject>().IsSpawned) 
        { tear.GetComponent<NetworkObject>().Spawn(true); }


        Rigidbody2D rb = tear.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
           

            Vector2 finalVelocity = (_direction * tearSpeed) + (playerRb.velocity * 0.5f);

            rb.velocity = finalVelocity;
        }
        else
        {
            Debug.LogError("Rigidbody2D não encontrado no prefab do tear!");
        }
    }


    public void SetShootCooldown(float newCooldown)
    {
        
        if (newCooldown > 0f)
        {
            shootCooldown = newCooldown;
        }
        else
        {
            Debug.LogWarning("Tentativa de definir shootCooldown com um valor inválido: " + newCooldown);
        }
    }

}
