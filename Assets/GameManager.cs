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


    public float minimumLevelCompletionTime;
    public float currentLevelCompletionTime;

    bool waitingForNextRound;
    float newRoundTimer;
    float timeBetweenRounds = 5.0f;

    public GameObject player;

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
        if (waitingForNextRound)
        {
            newRoundTimer += Time.deltaTime;
            if (newRoundTimer > timeBetweenRounds)
                CommenceNewRound();

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
        waitingForNextRound = true;
    }

    public void GameOver()
    {
        //display UI for new 
    }

    public void ResetGame()
    {
        playerLevel = 0;
        player.transform.position = Vector3.zero;
        player.GetComponent<PlayerController>().NewRoundOrRestart();

    }
}
