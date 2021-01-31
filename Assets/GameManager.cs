using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform EndGoalParent;
    public List<Transform> possibleEndGoals;

    public Transform HazardParent;
    public List<Transform> possibleHazards;

    public Transform SpawnParent;
    public List<Transform> possibleSpawns;

    public int playerLevel;

    float currentRoundTimer;
    public float minimumLevelCompletionTime;
    public float currentLevelCompletionTime;

    bool waitingForNextRound;
    float newRoundTimer;
    float timeBetweenRounds = 5.0f;

    public GameObject player;

    public UIController uiController;

    public float mapSize;

    void Start()
    {
        foreach (Transform goal in EndGoalParent)
        {
            possibleEndGoals.Add(goal);
        }
        foreach (Transform hazard in HazardParent)
        {
            possibleHazards.Add(hazard);
        }
        foreach (Transform spawn in SpawnParent)
        {
            possibleSpawns.Add(spawn);
        }
    }
    void Update()
    {
        if (!CheckPlayerInMap())
            GameOver();
        if (waitingForNextRound)
        {
            newRoundTimer += Time.deltaTime;
            if (newRoundTimer > timeBetweenRounds)
                CommenceNewRound();

        }
        else
        {
            currentRoundTimer += Time.deltaTime;
            if (currentRoundTimer > currentLevelCompletionTime)
                GameOver();

        }
    }


    private void CommenceNewRound()
    {
        playerLevel++;
        waitingForNextRound = false;

        var newSpawnIndex = Random.Range(0, possibleSpawns.Count);
        player.transform.position = possibleSpawns[newSpawnIndex].position;

        player.GetComponent<PlayerController>().NewRoundOrRestart();
    }
    public void WinCondition()
    {
        newRoundTimer = 0;
        currentRoundTimer = 0;
        waitingForNextRound = true;
    }

    public void GameOver()
    {
        //display UI for new 
        ResetGame();
    }

    public void ResetGame()
    {
        playerLevel = 0;
        player.transform.position = Vector3.zero;
        currentRoundTimer = 0;
        player.GetComponent<PlayerController>().NewRoundOrRestart();

        uiController.ConfigureRestart();

    }
    private bool CheckPlayerInMap()
    {
        var halfMapSize = mapSize / 2;
        if (player.transform.position.x > halfMapSize || player.transform.position.x < -halfMapSize
             || player.transform.position.z > halfMapSize || player.transform.position.z < -halfMapSize)
            return false;
        return true;
    }
}
