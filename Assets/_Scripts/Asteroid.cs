using UnityEngine;
using System.Collections;

public class Asteroid : BaseGameObject
{

    public Asteroid childAsteroid;
    public int minChildCount = 2;
    public int maxChildCount = 4;
    public int scoreValue = 1000;



    private bool childAsteroidsSpawned = false;
    private int childCount = 0;
    //private Animator animator;

    // Use this for initialization
    new void Start()
    {
        base.Start();
        this.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-2f, 2f), Random.Range(-2f, 2f));
        this.GetComponent<Rigidbody2D>().AddTorque(Random.Range(-20f, 20f));
        childCount = Random.Range(minChildCount, maxChildCount);
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
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
            this.GetComponent<Rigidbody2D>().Sleep();
        }
        if (finishDestruction)
        {
            DestroyGameObject();
        }


    }


    void OnTriggerEnter2D(Collider2D collidingObject)
    {
        this.GetComponent<Animator>().SetTrigger("Destroyed");
        Destroy(collidingObject.gameObject);
    }
}
