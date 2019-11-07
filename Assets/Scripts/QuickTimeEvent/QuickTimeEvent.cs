using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickTimeEvent : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    [Header("Spawnfield")]
    [SerializeField]
    Vector3 offset;

    [Header("Letter Configuaritons")]
    [SerializeField]
    int count;

    [SerializeField]
    List<GameObject> letters = new List<GameObject>();

    [HideInInspector]
    public int correctInput;

    GameObject placeHolder;

    TimeBehaviour timeBehaviour;

    void Start()
    {
        correctInput = 0;
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

    void CheckWinSituation()
    {
        if  (correctInput == count)
        {
            Win();
        }
    }

    void SpawnLetter()
    {   
        if (placeHolder == null && correctInput < count)
        {
            Vector3 spawnPosition = player.transform.position + new Vector3(Random.Range(-offset.x / 2, offset.x / 2),
                                                                            Random.Range(-offset.y / 2, offset.y / 2), 0);

            placeHolder = Instantiate(letters[Random.Range(0, letters.Count)], spawnPosition, Quaternion.identity, this.transform);
        }
    }

    void Win()
    {
        Debug.Log("Won QuickTimeEvent");
        timeBehaviour.timeCost = TimeBehaviour.TimeCost.lowCost;
        QuitQuickTimeEvent();
    }

    public void Faile()
    {
        Debug.Log("Lose QuickTimeEvent");
        timeBehaviour.timeCost = TimeBehaviour.TimeCost.middleCost;
        QuitQuickTimeEvent();
    }

    void QuitQuickTimeEvent()
    {
        correctInput = 0;
        GameObject.Find("Player").GetComponent<Player>().enabled = true;
        this.enabled = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(player.transform.position, offset);
    }
}
