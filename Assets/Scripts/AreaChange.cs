using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaChange : MonoBehaviour
{
    public Camera mainCamera;
    Vector3 newPosition;
    TimeBehaviour timeBehaviour;

    public int posX = 0;
    public int posY = 0;

    private void Start()
    {
        timeBehaviour = GameObject.Find("GameManager").GetComponent<TimeBehaviour>();
        newPosition = mainCamera.transform.position;
    }

    void OnTriggerEnter2D(Collider2D enter)
    {
        if (enter.gameObject.tag == "EnterRight" && mainCamera.transform.position.x == posX)
        {
            newPosition = mainCamera.transform.position + new Vector3(25, 0, 0);
            transform.position = transform.position + new Vector3(5,0,0);
            posX += 25;

            timeBehaviour.timeCost = TimeBehaviour.TimeCost.highCost;  
        }
        if (enter.gameObject.tag == "EnterLeft" && mainCamera.transform.position.x == posX)
        {
            newPosition = mainCamera.transform.position + new Vector3(-25, 0, 0);
            transform.position = transform.position + new Vector3(-5, 0, 0);
            posX -= 25;

            timeBehaviour.timeCost = TimeBehaviour.TimeCost.highCost;
        }
        if (enter.gameObject.tag == "EnterUp" && mainCamera.transform.position.y == posY)
        {
            newPosition = mainCamera.transform.position + new Vector3(0, 15, 0);
            transform.position = transform.position + new Vector3(0, 5.5f, 0);
            posY += 15;

            timeBehaviour.timeCost = TimeBehaviour.TimeCost.highCost;
        }
        if (enter.gameObject.tag == "EnterDown" && mainCamera.transform.position.y == posY)
        {
            newPosition = mainCamera.transform.position + new Vector3(0, -15, 0);
            transform.position = transform.position + new Vector3(0, -5.5f, 0);
            posY -= 15;

            timeBehaviour.timeCost = TimeBehaviour.TimeCost.highCost;
        }
    }

    private void Update()
    {
        if (Vector3.Distance(mainCamera.transform.position, newPosition) > 0.03f)
        {
            mainCamera.transform.position += (newPosition - mainCamera.transform.position) * 0.15f;
        }
        else
        {
            mainCamera.transform.position = newPosition;
        }
    }
}
