using UnityEngine;
using System.Collections;

public abstract class PanelBaseClass : MonoBehaviour
{

    public GameData gameData;
    public LevelManager levelManager;


    // Use this for initialization
    public void Start()
    {
        gameData = GameObject.FindObjectOfType<GameData>();
        levelManager = GameObject.FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    public void Update()
    {

    }

    public bool IsPanelActive()
    {
        return gameObject.activeInHierarchy;
    }


}
