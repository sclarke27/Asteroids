using UnityEngine;
using System.Collections;

public class PlayerShip : BaseGameObject
{

    public float shipScreenOffset = 0.172f;

    //public float maxVelocity = 15.0f;
    //public float acclerationAmount = 0.1f;
    //public float declerationAmount = 0.1f;
    public Animator shipAnimator;
    public GameObject defaultProjectile;
    //public int maxHealth = 100;
    private float thrustAmount = 750f;
    private float rotationAmount = 150f;
    //private int currentHealth;

    



    // Use this for initialization
    new void Start()
    {
        base.Start();
        //currentHealth = maxHealth;
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
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
                transform.GetComponent<Rigidbody2D>().AddForce(transform.up * thrustAmount * Time.deltaTime);
                //shipAnimator.SetBool("MoveLeft", false);
                //shipAnimator.SetBool("MoveRight", false);
                shipAnimator.SetBool("FiringThrusters", true);

            }

            //do nothing
            if (!gameData.IsRightPaddledown() && !gameData.IsLeftPaddledown() && !gameData.IsUpPaddledown())
            {
                //shipAnimator.SetBool("MoveLeft", false);
                //shipAnimator.SetBool("MoveRight", false);
                shipAnimator.SetBool("FiringThrusters", false);

            }

        }
        if (finishDestruction)
        {
            DestroyGameObject();
        }


    }

    new public void DestroyGameObject()
    {
        gameData.LoseOneLife();
        gameData.SetPlayerReady(false);
        base.DestroyGameObject();
        
    }

    void OnCollisionEnter2D(Collision2D collidingObject)
    {
        if (collidingObject.gameObject.layer == 8)
        {
            shipAnimator.SetTrigger("Destroyed");
        }
    }

    /*
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
                
            }
        }
    }
    */

    public void FireProjectile()
    {
        Instantiate(defaultProjectile, transform.position, transform.rotation);
    }

}
