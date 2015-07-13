using UnityEngine;
using System.Collections;

public class Projectile : BaseGameObject
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
    new void Update()
    {
        base.Update();
        if (projectileType == ProjectileTypes.player)
        {
            transform.position += transform.up * movementSpeed * Time.deltaTime;
        }
        else
        {
            transform.position += transform.up * movementSpeed * Time.deltaTime;
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
        //Destroy(gameObject);
    }
}
