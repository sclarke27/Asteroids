using UnityEngine;
using System.Collections;

public class LoseCollider : MonoBehaviour
{

    private LevelManager levelManager;
    private GameData gameData;
    public AudioSource powerupDestructionSound;

    // Use this for initialization
    void Start()
    {
        levelManager = GameObject.FindObjectOfType<LevelManager>();
        gameData = GameObject.FindObjectOfType<GameData>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Ball")
        {
            gameData.LoseOneLife();
            if (gameData.GetPlayerRemainingLives() <= 0)
            {
                levelManager.LoadLevel("StopScreen");
            }
            else
            {
                levelManager.ResetPlayer();
            }
        }
        else
        {
            if (powerupDestructionSound != null)
            {
                powerupDestructionSound.volume = gameData.GetSFXVolume();
                powerupDestructionSound.Play();
            }
            Destroy(collision.gameObject);
        }

    }
}
