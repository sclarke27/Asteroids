using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{

    public GameObject BasicAsteroid;
    public float spawnAreaWidth = 10f;
    public float spawnAreaHeight = 5f;
    public int minStartingAsteroids = 1;
    public int maxStartingAsteroids = 1;

    private float offsetWidth;
    private float offsetHeight;
    private GameData gameData;
    private LevelManager levelManager;
    private int totalStartingAsteroids = 0;


    public enum EnemyTypes
    {
        Large,
        Medium,
        Small
    }

    // Use this for initialization
    void Start()
    {
        gameData = GameObject.FindObjectOfType<GameData>();
        levelManager = GameObject.FindObjectOfType<LevelManager>();

        totalStartingAsteroids = Random.Range(minStartingAsteroids, maxStartingAsteroids);

        offsetWidth = spawnAreaWidth / 2;
        offsetHeight = spawnAreaHeight / 2;

        float minX = transform.position.x - offsetWidth;
        float maxX = transform.position.x + offsetWidth;
        float minY = transform.position.y - offsetHeight;
        float maxY = transform.position.y + offsetHeight;

        for (int i = 0; i < totalStartingAsteroids; i++)
        {
            GameObject newAsteroid = Instantiate(BasicAsteroid, new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY)), transform.rotation) as GameObject;
            newAsteroid.transform.parent = transform;
        }

        
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(spawnAreaWidth, spawnAreaHeight, 0f));
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameData.IsGamePaused())
        {
            if (transform.childCount == 0)
            {
                levelManager.ShowLevelComplete();
            }
        }
    }
}
