using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour
{

    public Asteroid childAsteroid;
    public int minChildCount = 2;
    public int maxChildCount = 4;
    public int scoreValue = 1000;

    public bool destroyed = false;
    public bool finishDestruction = false;


    private bool childAsteroidsSpawned = false;
    private int childCount = 0;
    private GameData gameData;
    //private Animator animator;

    // Use this for initialization
    void Start()
    {
        gameData = GameObject.FindObjectOfType<GameData>();
        this.rigidbody2D.velocity = new Vector2(Random.Range(-2f, 2f), Random.Range(-2f, 2f));
        this.rigidbody2D.AddTorque(Random.Range(-20f, 20f));
        childCount = Random.Range(minChildCount, maxChildCount);
    }

    // Update is called once per frame
    void Update()
    {
        if (destroyed)
        {
            gameData.AddPlayerScore(scoreValue);
            if (childAsteroid != null && !childAsteroidsSpawned)
            {
                for (int i = 0; i < childCount; i++)
                {
                    Vector2 newPos = new Vector2();
                    float childOffset = 0.5f;
                    switch (i)
                    {
                        case 0:
                            newPos = transform.position + transform.up * childOffset;
                            break;
                        case 1:
                            newPos = transform.position + (transform.up * -1) * childOffset;
                            break;
                        case 2:
                            newPos = transform.position + (transform.right * -1) * childOffset;
                            break;
                        case 3:
                            newPos = transform.position + transform.right * childOffset;
                            break;
                    }
                    Asteroid newAsteroid1 = Instantiate(childAsteroid, newPos, transform.rotation) as Asteroid;
                    newAsteroid1.transform.parent = transform.parent;
                }
                childAsteroidsSpawned = true;
            }
            this.rigidbody2D.Sleep();
        }
        if (finishDestruction)
        {
            Destroy(gameObject);
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


    void OnTriggerEnter2D(Collider2D collidingObject)
    {
        this.GetComponent<Animator>().SetTrigger("Destroyed");
    }
}
