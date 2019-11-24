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
    Timer timer;
    bool areEnemiesReady, areEnemiesDead;


    private void Awake()
    {
        gameManager = GameObject.Find("GameManager");
        timer = gameManager.GetComponent<Timer>();
    }

    private void Update()
    {
        // Time == Day?
        if (timer.currentDayTime == Timer.DayTime.Day)
        {
            // Dont do anything
        }

        // Time == Panic?
        // &&   Enemies NOT Ready?
        if (timer.currentDayTime == Timer.DayTime.Panic && !areEnemiesReady)
        {
            FillUpEnemiesList();
            areEnemiesReady = true;
        }

        // Time == Night?
        // &&   Enemies Ready?
        if (timer.currentDayTime == Timer.DayTime.Night && areEnemiesReady)
        {
            SpawnEnemiesFromList();
            areEnemiesReady = false;
        }

        // Time == Night?
        // &&   Enemies Dead
        if (timer.currentDayTime == Timer.DayTime.Night && areEnemiesDead)
        {
            timer.NewDay();
            ResetEnemiesList();
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
    void ResetEnemiesList()
    {
        SetEnemiesList = new GameObject[0];
    }


    void SpawnEnemiesFromList()
    {
        for (int i = 0; i < SetEnemiesList.Length; i++)
        {
            SpawnedEnemiesList[i] = Instantiate(SetEnemiesList[i], PickRandomAvailableSpot() , Quaternion.identity);
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

    bool areEnemiesDeadCheck()
    {
        int enemiesAliveCount = 0;
        for (int i = 0; i < SpawnedEnemiesList.Length; i++)
        {
            if (SpawnedEnemiesList[i].GetComponent<Stats>().health > 0)
            {
                enemiesAliveCount++;
            }
        }

        if (enemiesAliveCount == 0) return true;
        // Run through spawnedlist
            // If (all dead)
            // return true
        return false;
    }
}
