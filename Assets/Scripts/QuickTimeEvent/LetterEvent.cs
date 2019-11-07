using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Michael Schmidt

public class LetterEvent : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    [Header("Spawnfield")]
    [SerializeField]
    Vector3 offset;

    [Header("Letter Configuaritons")]
    [SerializeField]
    //read how many letters will spawn 
    int count;

    [SerializeField]
    //List for all assigned letter prefabs
    List<GameObject> letters = new List<GameObject>();

    TimeBehaviour timeBehaviour;

    GameObject placeHolder;

    [HideInInspector]
    public int correctInput = 0;

    void Start()
    {
        timeBehaviour = GameObject.Find("GameManager").GetComponent<TimeBehaviour>();
    }

    void Update()
    {
        CheckWinSituation();
    }

    void LateUpdate()
    {
        SpawnLetter();
    }

    /// <summary>
    /// Check if QuickTimeEvent is over
    /// </summary>
    void CheckWinSituation()
    {
        //check if all inputs are correct
        if  (correctInput == count)
        {
            Win();
        }
    }

    /// <summary>
    /// Instantiates letter prefab
    /// </summary>
    void SpawnLetter()
    {   
        //only spawns letetr prefab once a time
        if (placeHolder == null && correctInput < count)
        {
            //spawns between an offset area
            Vector3 spawnPosition = player.transform.position + new Vector3(Random.Range(-offset.x / 2, offset.x / 2),
                                                                            Random.Range(-offset.y / 2, offset.y / 2), 0);

            placeHolder = Instantiate(letters[Random.Range(0, letters.Count)], spawnPosition, Quaternion.identity, this.transform);
        }
    }

    /// <summary>
    /// If QuickTimeEvent is a win situation
    /// </summary>
    void Win()
    {
        Debug.Log("Won QuickTimeEvent");

        //low cost for time behaviour
        timeBehaviour.timeCost = TimeBehaviour.TimeCost.LowCost;

        QuitLetterEvent();
    }

    /// <summary>
    /// If QuickTimeEvent is a lose situation
    /// </summary>
    public void Fail()
    {
        Debug.Log("Lose QuickTimeEvent");

        //middle cost for time behaviour
        timeBehaviour.timeCost = TimeBehaviour.TimeCost.MiddleCost;

        QuitLetterEvent();
    }

    /// <summary>
    /// Close QuickTimeEvent
    /// </summary>
    void QuitLetterEvent()
    {
        //reset player input for next Letter event
        correctInput = 0;
        
        //enable player script
        GameObject.Find("Player").GetComponent<Player>().enabled = true;

        //disable this event (script)
        this.enabled = false;
    }

    /// <summary>
    /// draw offset area for configuration in inspector
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(player.transform.position, offset);
    }
}
