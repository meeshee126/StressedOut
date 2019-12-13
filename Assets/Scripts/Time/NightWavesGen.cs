using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightWavesGen : MonoBehaviour
{
    public GameObject[] AvailableEnemiesList;
    public GameObject[] SpawnedEnemiesList;
    public GameObject[] AvailableSpotsList;
    public GameObject[] SetEnemiesList;

    GameObject gameManager;
    Timer worldTimer;
    bool areEnemiesReady;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager");
        worldTimer = gameManager.GetComponent<Timer>();
    }

    private void Update()
    {
        if (worldTimer.dayOver)
            return;

        // Time == Day?
        if (worldTimer.currentDayTime == Timer.DayTime.Day)
        {
            // Dont do anything
        }

        // Time == Panic?
        // &&   Enemies NOT Ready?
        if (worldTimer.currentDayTime == Timer.DayTime.Panic && !areEnemiesReady)
        {
            FillUpEnemiesList();
            areEnemiesReady = true;
        }

        // Time == Night?
        // &&   Enemies Ready?
        if (worldTimer.currentDayTime == Timer.DayTime.Night && areEnemiesReady)
        {
            SpawnEnemiesFromList();
            areEnemiesReady = false;
        }

        // Time == Night?
        // &&   Enemies Dead
        if (worldTimer.currentDayTime == Timer.DayTime.Night && areEnemiesDeadCheck())
        {
         
            
            for (int i = 0; i < SpawnedEnemiesList.Length; i++)
                Destroy(SpawnedEnemiesList[i]);
            ResetEnemiesLists();
            worldTimer.dayOver = true;
            Debug.Log("DayOver");
            // Set Time back to day
            // Empty Enemy List
        }
    }


    /// <summary>
    /// Fills up the enemy List
    /// </summary>
    void FillUpEnemiesList()
    {
        SetEnemiesList = new GameObject[Random.Range(2, 8)];
        SpawnedEnemiesList = new GameObject[SetEnemiesList.Length];
        for (int i = 0; i < SetEnemiesList.Length; i++)
        {
            SetEnemiesList[i] = AvailableEnemiesList[Random.Range(0, AvailableEnemiesList.Length)];
        }
    }
    

    /// <summary>
    /// Empties 
    /// </summary>
    void ResetEnemiesLists()
    {
        SetEnemiesList = new GameObject[0];
        SpawnedEnemiesList = new GameObject[0];
        Debug.Log("Reseted Lists");
    }


    void SpawnEnemiesFromList()
    {
        for (int i = 0; i < SetEnemiesList.Length; i++)
        {
            Vector3 randomSpot = PickRandomAvailableSpot();
            SpawnedEnemiesList[i] = Instantiate(SetEnemiesList[i], randomSpot, Quaternion.identity);
        }

        // Run though Available Spots List.length
        // Run though EnemiesList List.length
            // Instatiate each obj (random position)
    }

    Vector3 PickRandomAvailableSpot()
    {
        int field = Random.Range(0, AvailableSpotsList.Length);

        Vector3 spotInField = new Vector3(
            Random.Range(
            AvailableSpotsList[field].transform.position.x -
            (AvailableSpotsList[field].GetComponent<BoxCollider2D>().size.x / 2),
            AvailableSpotsList[field].transform.position.x +
            (AvailableSpotsList[field].GetComponent<BoxCollider2D>().size.x / 2)),
            Random.Range(
            AvailableSpotsList[field].transform.position.y -
            (AvailableSpotsList[field].GetComponent<BoxCollider2D>().size.y / 2),
            AvailableSpotsList[field].transform.position.y +
            (AvailableSpotsList[field].GetComponent<BoxCollider2D>().size.y / 2)));
        //    AvailableEnemiesList
            // Set spot to new available spot position
        // Get Available spawn Area Values
        return spotInField;
    }

    public bool areEnemiesDeadCheck()
    {
        int enemiesAliveCount = 0;
        for (int i = 0; i < SpawnedEnemiesList.Length; i++)
        {
            if (SpawnedEnemiesList[i] != null)
                if (SpawnedEnemiesList[i].GetComponent<Stats>().health > 0)
                {
                    enemiesAliveCount++;
                }
        }

        if (enemiesAliveCount == 0)
        {
            return true;
        }


        // Run through spawnedlist
        // If (all dead)
        // return true
        return false;
    }
}
