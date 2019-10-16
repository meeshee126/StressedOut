using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaChange : MonoBehaviour
{
    public Camera mainCamera;
    Vector3 newPosition;
    TimeBehaviour timeBehaviour;

    private void Start()
    {
        timeBehaviour = GameObject.Find("GameManager").GetComponent<TimeBehaviour>();
        newPosition = mainCamera.transform.position;
    }

    void OnTriggerEnter2D(Collider2D enter)
    {
        if (enter.gameObject.tag == "EnterRight")
        {
            newPosition = mainCamera.transform.position + new Vector3(25, 0, 0);
            transform.position = transform.position + new Vector3(5,0,0);
            timeBehaviour.timeCost = TimeBehaviour.TimeCost.highCost;  
        }
        if (enter.gameObject.tag == "EnterLeft")
        {
            newPosition = mainCamera.transform.position + new Vector3(-25, 0, 0);
            transform.position = transform.position + new Vector3(-5, 0, 0);
            timeBehaviour.timeCost = TimeBehaviour.TimeCost.highCost;
        }
        if (enter.gameObject.tag == "EnterUp")
        {
            newPosition = mainCamera.transform.position + new Vector3(0, 15, 0);
            transform.position = transform.position + new Vector3(0, 5.5f, 0);
            timeBehaviour.timeCost = TimeBehaviour.TimeCost.highCost;
        }
        if (enter.gameObject.tag == "EnterDown")
        {
            newPosition = mainCamera.transform.position + new Vector3(0, -15, 0);
            transform.position = transform.position + new Vector3(0, -5.5f, 0);
            timeBehaviour.timeCost = TimeBehaviour.TimeCost.highCost;
        }
    }

    private void Update()
    {
        mainCamera.transform.position += (newPosition - mainCamera.transform.position) * 0.06f;
    }
}
