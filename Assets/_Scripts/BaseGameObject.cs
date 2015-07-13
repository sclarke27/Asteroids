using UnityEngine;
using System.Collections;

public class BaseGameObject : MonoBehaviour
{

    public GameData gameData;
    public Vector2 minScreenBounds;
    public Vector2 maxScreenBounds;

    public bool destroyed = false;
    public bool finishDestruction = false;


    // Use this for initialization
    public void Start()
    {
        gameData = GameObject.FindObjectOfType<GameData>();
    }


    // Update is called once per frame
    public void Update()
    {
        minScreenBounds = gameData.GetMinScreenBounds();
        maxScreenBounds = gameData.GetMaxScreenBounds();
        if (transform.position.y < minScreenBounds.y || transform.position.x < minScreenBounds.x || transform.position.y > maxScreenBounds.y || transform.position.x > maxScreenBounds.x)
        {
            if (transform.position.y < minScreenBounds.y)
            {
                transform.position = new Vector2(transform.position.x, maxScreenBounds.y);
            }
            if (transform.position.y > maxScreenBounds.y)
            {
                transform.position = new Vector2(transform.position.x, minScreenBounds.y);
            }
            if (transform.position.x < minScreenBounds.x)
            {
                transform.position = new Vector2(maxScreenBounds.x, transform.position.y);
            }
            if (transform.position.x > maxScreenBounds.x)
            {
                transform.position = new Vector2(minScreenBounds.x, transform.position.y);
            }
        }
    }

    public void DestroyGameObject()
    {
       Destroy(gameObject);
    }
}
