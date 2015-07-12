using UnityEngine;
using System.Collections;

public class GamefieldCollider : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D collidingObject)
    {
        Destroy(collidingObject.gameObject);
    }

}
