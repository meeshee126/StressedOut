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
            // Set up enemylist
            // areEnemiesReady = true;
        }

        // Time == Night?
        // &&   Enemies Ready?
        if (timer.currentDayTime == Timer.DayTime.Night && areEnemiesReady)
        {
            // Spawn Enemies
            // areEnemiesReady = false;
        }

        // Time == Night?
        // &&   Enemies Dead
        if (timer.currentDayTime == Timer.DayTime.Night && areEnemiesDead)
        {
            // Set Time back to day
            // Empty Enemy List
        }
    }


    /// <summary>
    /// Fills up the enemy List
    /// </summary>
    void FillUpEnemiesList()
    {
        //EnemyList = new GameObject[Random.Range(2, 8)];
        // Set List.Length
        // Run through List.length
            // Set each obj to random enemy (from available enemies List)
    }
    

    /// <summary>
    /// Empties 
    /// </summary>
    void EmptyEnemiesList()
    {
        // Run through List.length
            // Set each obj to null
        // Set List Length back to 0
    }


    void SpawnEnemiesFromList()
    {
        // Run though Available Spots List.length
        // Run though EnemiesList List.length
        // Instatiate each obj (random position)
    }

    Transform PickRandomSpot()
    {
        // Get Available spawn Area Values
        // 
        return null;
    }

    bool EnemiesDeadCheck()
    {
        // Run through
        return false;
    }
}
