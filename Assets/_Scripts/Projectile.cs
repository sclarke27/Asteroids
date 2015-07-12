using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{

    public enum ProjectileTypes
    {
        player,
        enemy
    }

    public ProjectileTypes projectileType = ProjectileTypes.player;
    public float movementSpeed = 10f;
    public int destructionAmount = 100;
    public float projectileTimeout = 1f;
    private float projectileDeltaTime = 0f; 

    // Update is called once per frame
    void Update()
    {
        if (projectileType == ProjectileTypes.player)
        {
            transform.position += transform.up * movementSpeed * Time.deltaTime;
        }
        else
        {
            transform.position += transform.up * movementSpeed * Time.deltaTime;
        }
        if (transform.position.y < 0 || transform.position.x < 0 || transform.position.y > 12 || transform.position.x > 16)
        {
            if (transform.position.y < 0)
            {
                transform.position = new Vector2(transform.position.x, 12f);
            }
            if (transform.position.y > 12)
            {
                transform.position = new Vector2(transform.position.x, 0f);
            }
            if (transform.position.x < 0)
            {
                transform.position = new Vector2(16f, transform.position.y);
            }
            if (transform.position.x > 16)
            {
                transform.position = new Vector2(0f, transform.position.y);
            }

        }
        projectileDeltaTime += Time.deltaTime;
        if (projectileDeltaTime >= projectileTimeout)
        {
            Destroy(gameObject);
        }
    }

    public int GetDestructionAmount()
    {
        return destructionAmount;
    }

    void OnTriggerEnter2D(Collider2D collidingObject) 
    {
        Destroy(gameObject);
    }
}
