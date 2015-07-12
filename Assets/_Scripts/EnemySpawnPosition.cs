using UnityEngine;
using System.Collections;

public class EnemySpawnPosition : MonoBehaviour
{

    public EnemySpawner.EnemyTypes enemyType;

    public EnemySpawner.EnemyTypes GetEnemyType()
    {
        return enemyType;
    }

    public void SetEnemyType(EnemySpawner.EnemyTypes type)
    {
        enemyType = type;
    }


    void OnDrawGizmos()
    {
        switch (enemyType)
        {
            case EnemySpawner.EnemyTypes.Large:
                Gizmos.DrawWireSphere(transform.position, 0.5f);
                break;
            case EnemySpawner.EnemyTypes.Medium:
                Gizmos.DrawWireSphere(transform.position, 0.3f);
                break;

            case EnemySpawner.EnemyTypes.Small:
                Gizmos.DrawWireSphere(transform.position, 0.25f);
                break;
        }
        
    }
}
