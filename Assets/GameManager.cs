using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform EndGoalParent;
    public List<Transform> possibleEndGoals;

    public Transform treasure;

    int totalHazards;
    public Transform HazardParent;
    public List<Transform> possibleHazards;
    public GameObject hazardPrefab;

    public Transform SpawnParent;
    public List<Transform> possibleSpawns;

    public int playerLevel;

    public float currentRoundTimer;
    public float minimumLevelCompletionTime;
    public float currentLevelCompletionTime;

    bool waitingForNextRound;
    float newRoundTimer;
    float timeBetweenRounds = 5.0f;

    public GameObject player;

    public UIController uiController;

    public AudioSource playerSounds;

    public AudioClip celebrate;
    public AudioClip hangover;

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
        currentRoundTimer = currentLevelCompletionTime;
        totalHazards = possibleHazards.Count;
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
            currentRoundTimer -= Time.deltaTime;
            if (currentRoundTimer < 0)
                GameOver();

        }
    }


    private void CommenceNewRound()
    {
        MoveTreasure();
        SpawnAnObstacle();
        playerLevel++;
        waitingForNextRound = false;

        var newSpawnIndex = Random.Range(0, possibleSpawns.Count);
        player.transform.position = possibleSpawns[newSpawnIndex].position;

        player.GetComponent<PlayerController>().NewRoundOrRestart();
        playerSounds.PlayOneShot(hangover);
    }
    public void WinCondition()
    {
        newRoundTimer = 0;
        currentRoundTimer = currentLevelCompletionTime;
        waitingForNextRound = true;
        playerSounds.PlayOneShot(celebrate);
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
        currentRoundTimer = currentLevelCompletionTime;
        player.GetComponent<PlayerController>().NewRoundOrRestart();

        uiController.ConfigureRestart();
        possibleHazards.Clear();
        foreach (Transform hazard in HazardParent)
        {
            possibleHazards.Add(hazard);
        }
        foreach (var loc in possibleHazards)
        {
            foreach (Transform child in loc)
            {
                GameObject.Destroy(child.gameObject);
            }
        }

    }
    private bool CheckPlayerInMap()
    {
        var halfMapSize = mapSize / 2;
        if (player.transform.position.x > halfMapSize || player.transform.position.x < -halfMapSize
             || player.transform.position.z > halfMapSize || player.transform.position.z < -halfMapSize)
            return false;
        return true;
    }

    private void MoveTreasure()
    {
        var newTreasureLocation = Random.Range(0, possibleEndGoals.Count);
        treasure.SetParent(possibleEndGoals[newTreasureLocation]);
        treasure.localPosition = Vector3.zero;
    }
    private void SpawnAnObstacle()
    {
        if (playerLevel < totalHazards && possibleHazards.Count !=0)
        {
            var newLocation = Random.Range(0, possibleHazards.Count);
            GameObject go = GameObject.Instantiate(hazardPrefab, possibleHazards[newLocation]);
            possibleHazards.RemoveAt(newLocation);
        }
    }
}
