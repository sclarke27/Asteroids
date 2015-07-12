using UnityEngine;
using System.Collections;

public class EnemyShip : MonoBehaviour
{

    public GameObject defaultProjectile;
    public int maxHealth = 200;
    private float firingYOffset = -0.25f;
    private Animator shipAnimator;
    public bool fire = false;
    public bool destroy = false;

    public static int enemyCount = 0;
    private int currentHealth;

    private LevelManager levelManager;
    private GameData gameData;

    // Use this for initialization
    void Start()
    {
        levelManager = GameObject.FindObjectOfType<LevelManager>();
        gameData = GameObject.FindObjectOfType<GameData>();
        shipAnimator = GetComponent<Animator>();
        currentHealth = maxHealth;
        enemyCount++;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameData.IsGamePaused())
        {
            if (destroy)
            {
                Destroy(gameObject);
                return;
            }
            int randVal = Random.Range(0, 1000);
            if (randVal == 50)
            {
                FireProjectile();
                Debug.Log("fire");
            }
            if (fire)
            {
                DoProjectileFiring();
                fire = false;
            }
        }
    }

    public void FireProjectile()
    {
        shipAnimator.SetTrigger("FireWeapons");
        //Instantiate(defaultProjectile, transform.position, Quaternion.identity);
    }

    public void DoProjectileFiring()
    {

        Instantiate(defaultProjectile, new Vector3(transform.position.x, transform.position.y + firingYOffset, transform.position.z), Quaternion.identity);
    }

    void OnTriggerEnter2D(Collider2D collidingObject)
    {
        if (collidingObject.name.IndexOf("Projectile") >= 0 && !destroy)
        {
            Projectile projectile = collidingObject.gameObject.GetComponent<Projectile>() as Projectile;
            currentHealth = currentHealth - projectile.GetDestructionAmount();
            Debug.Log("enemy hit: " + projectile.GetDestructionAmount());

            if (currentHealth > 0)
            {
                shipAnimator.SetTrigger("TookDamage");
            }
            else
            {
                shipAnimator.SetTrigger("Destroyed");
                enemyCount--;
                if (EnemyShip.enemyCount <= 0)
                {
                    levelManager.ShowLevelComplete();
                }
            }
        }
    }
}
