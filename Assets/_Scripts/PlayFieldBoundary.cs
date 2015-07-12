using UnityEngine;
using System.Collections;

public class PlayFieldBoundary : MonoBehaviour
{

    public PlayFieldBoundary opposingBoundary;
    public bool isSideBoundary = false;

    void OnTriggerEnter2D(Collider2D collidingObject)
    {
        if (isSideBoundary)
        {
            //collidingObject.transform.position = new Vector2(opposingBoundary.transform.position.x, collidingObject.transform.position.y);
        }
        else
        {
            //collidingObject.transform.position = new Vector2(collidingObject.transform.position.x, opposingBoundary.transform.position.y);
        }
        //Debug.Log(collidingObject.name);
    }

    void OnCollisionEnter2D(Collision2D collidingObject)
    {
        //Debug.Log(collidingObject.gameObject.name);
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(this.collider2D.bounds.size.x, this.collider2D.bounds.size.y, 0f));
    }
}
