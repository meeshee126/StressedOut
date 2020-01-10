using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyOnPress : MonoBehaviour
{
    public GameObject[] SpawnedEnemiesList;
    public GameObject[] AvailableSpotsList;
    public GameObject[] SetEnemiesList;

    GameObject gameManager;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadPlus) || Input.GetKeyDown(KeyCode.Plus))
        {
            SpawnEnemiesFromList();
        }
        #region commented stuff
        //// Time == Panic?
        //// &&   Enemies NOT Ready?
        //if (worldTimer.currentDayTime == Timer.DayTime.Panic && !areEnemiesReady)
        //{
        //    FillUpEnemiesList();
        //    areEnemiesReady = true;
        //}

        // Time == Night?
        // &&   Enemies Ready?
        #endregion
    }


    #region commented stuff
    ///// <summary>
    ///// Fills up the enemy List
    ///// </summary>
    //void FillUpEnemiesList()
    //{
    //    SetEnemiesList = new GameObject[Random.Range(2, 8)];
    //    SpawnedEnemiesList = new GameObject[SetEnemiesList.Length];
    //    for (int i = 0; i < SetEnemiesList.Length; i++)
    //    {
    //        SetEnemiesList[i] = AvailableEnemiesList[Random.Range(0, AvailableEnemiesList.Length)];
    //    }
    //}
    #endregion


    private void SpawnEnemiesFromList()
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

    //private void KillSpawnedEnemies()
    //{
        
    //    for (int i = 0; i < SpawnedEnemiesList.Length; i++)
    //    {
    //        Destroy(SpawnedEnemiesList[i]);
    //    }
    //}

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
}
