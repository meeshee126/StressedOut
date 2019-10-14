using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaChange : MonoBehaviour
{
    public Camera MainCamera;

    void OnTriggerEnter2D(Collider2D enter)
    {
        if (enter.gameObject.tag == "EnterRight")
        {
            MainCamera.transform.position = MainCamera.transform.position + new Vector3(25, 0, 0);
            transform.position = transform.position + new Vector3(5,0,0);
        }
        if (enter.gameObject.tag == "EnterLeft")
        {
            MainCamera.transform.position = MainCamera.transform.position + new Vector3(-25, 0, 0);
            transform.position = transform.position + new Vector3(-5, 0, 0);
        }
        if (enter.gameObject.tag == "EnterUp")
        {
            MainCamera.transform.position = MainCamera.transform.position + new Vector3(0, 15, 0);
            transform.position = transform.position + new Vector3(0, 5, 0);
        }
        if (enter.gameObject.tag == "EnterDown")
        {
            MainCamera.transform.position = MainCamera.transform.position + new Vector3(0, -15, 0);
            transform.position = transform.position + new Vector3(0, -5, 0);
        }
    }
}
