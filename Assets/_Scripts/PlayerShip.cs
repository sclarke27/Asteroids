using UnityEngine;
using System.Collections;

public class PlayerShip : MonoBehaviour
{

    public float shipScreenOffset = 0.172f;

    //public float maxVelocity = 15.0f;
    //public float acclerationAmount = 0.1f;
    //public float declerationAmount = 0.1f;
    public Animator shipAnimator;
    public GameObject defaultProjectile;
    public int maxHealth = 100;
    private float thrustAmount = 500f;
    private float rotationAmount = 270f;
    private int currentHealth;

    private GameData gameData;



    // Use this for initialization
    void Start()
    {
        gameData = GameObject.FindObjectOfType<GameData>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameData.IsGamePaused())
        {

            //rotate left
            if (gameData.IsLeftPaddledown() && !gameData.IsRightPaddledown())
            {
                //shipAnimator.SetBool("MoveLeft", true);
                //shipAnimator.SetBool("MoveRight", false);
                this.transform.Rotate(0, 0, rotationAmount * Time.deltaTime);
            }

            //rotate right
            if (gameData.IsRightPaddledown() && !gameData.IsLeftPaddledown())
            {
                //shipAnimator.SetBool("MoveLeft", false);
                //shipAnimator.SetBool("MoveRight", true);
                this.transform.Rotate(0, 0, (rotationAmount * -1) * Time.deltaTime);
            }

            //fire thrusters
            if (!gameData.IsRightPaddledown() && !gameData.IsLeftPaddledown() && gameData.IsUpPaddledown())
            {
                transform.rigidbody2D.AddForce(transform.up * thrustAmount * Time.deltaTime);
                shipAnimator.SetBool("MoveLeft", false);
                shipAnimator.SetBool("MoveRight", false);
                shipAnimator.SetBool("FiringThrusters", true);

            }

            //do nothing
            if (!gameData.IsRightPaddledown() && !gameData.IsLeftPaddledown() && !gameData.IsUpPaddledown())
            {
                shipAnimator.SetBool("MoveLeft", false);
                shipAnimator.SetBool("MoveRight", false);
                shipAnimator.SetBool("FiringThrusters", false);

            }

        }
        if (transform.position.y < 0 || transform.position.x < 0 || transform.position.y > 12 || transform.position.x > 16)
        {
            //Destroy(gameObject);
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

    }

    void OnCollisionEnter2D(Collision2D collidingObject)
    {
        Debug.Log(collidingObject.gameObject.layer);

        if (collidingObject.gameObject.layer == 8)
        {
            Debug.Log("boom");
            gameData.LoseOneLife();
            gameData.SetPlayerReady(false);
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collidingObject)
    {
        if (collidingObject.name.IndexOf("Projectile") >= 0)
        {
            Projectile projectile = collidingObject.gameObject.GetComponent<Projectile>() as Projectile;
            currentHealth = currentHealth - projectile.GetDestructionAmount();
            Debug.Log("player hit: " + projectile.GetDestructionAmount());

            if (currentHealth > 0)
            {
                shipAnimator.SetTrigger("TookDamage");
            }
            else
            {
                
                /*
                Destroy(gameObject);
                if (EnemyShip.enemyCount <= 0)
                {
                    levelManager.ShowLevelComplete();
                }
                 */
            }
        }
    }

    public void FireProjectile()
    {
        Instantiate(defaultProjectile, transform.position, transform.rotation);
    }

}
