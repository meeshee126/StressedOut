using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Michael Schmidt

public class LetterEvent : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    [Header("Spawnarea")]
    [SerializeField]
    Vector3 offset;

    [Header("Letter Configuaritons")]
    [SerializeField]
    //read how many letters will spawn 
    int count;

    [Header("Audio")]
    [SerializeField]
    GameObject winSFX;
    [SerializeField]
    GameObject loseSFX;

    [SerializeField]
    //List for all assigned letter prefabs
    List<GameObject> letters = new List<GameObject>();

    Timer timer;
    TimeBehaviour timeBehaviour;

    GameObject placeHolder;

    [HideInInspector]
    public GameObject gatherSound;

    [HideInInspector]
    public int correctInput = 0;

    [HideInInspector]
    public bool won, failed;

    void Start()
    {
        timer = GameObject.Find("GameManager").GetComponent<Timer>();
        timeBehaviour = GameObject.Find("GameManager").GetComponent<TimeBehaviour>();
    }

    void Update()
    {
        CheckDayTime();
        CheckWinSituation();
    }

    void LateUpdate()
    {
        SpawnLetter();
    }

    /// <summary>
    /// Chceck if daytime switch to panic mode while gathering
    /// </summary>
    void CheckDayTime()
    {
        //abort gathering when daytime switches to panic mode
        if (timer.currentDayTime != Timer.DayTime.Day)
        {
            QuitLetterEvent();
        }
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
        timeBehaviour.timeCost = timeBehaviour.winQuickTimeEvent;

        //play sound when winning event
        if (winSFX != null) Instantiate(winSFX, player.transform.position, Quaternion.identity);

        QuitLetterEvent();

        won = true;
    }

    /// <summary>
    /// If QuickTimeEvent is a lose situation
    /// </summary>
    public void Fail()
    {
        Debug.Log("Lose QuickTimeEvent");

        //middle cost for time behaviour
        timeBehaviour.timeCost = timeBehaviour.loseQuickTimeEvent;

        //player sound when losing event
        if (loseSFX != null) Instantiate(loseSFX, player.transform.position, Quaternion.identity);

        QuitLetterEvent();

        failed = true;
        Debug.Log("Failed!");
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
