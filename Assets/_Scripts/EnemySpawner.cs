using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{

    public GameObject BasicAsteroid;
    public int minStartingAsteroids = 1;
    public int maxStartingAsteroids = 1;

    private float offsetWidth;
    private float offsetHeight;
    private GameData gameData;
    private LevelManager levelManager;
    private int totalStartingAsteroids = 0;
    private float spawnAreaWidth = 10f;
    private float spawnAreaHeight = 5f;


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
        Vector2 maxScreenBounds = gameData.GetMaxScreenBounds();
        spawnAreaWidth = maxScreenBounds.x * 2;
        spawnAreaHeight = maxScreenBounds.y * 2;

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
