using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBase : MonoBehaviour
{
    public bool yourVar;

    public GameObject Wall;

    void Update()
    {
        if (yourVar == true && Input.GetKeyDown(KeyCode.F))
        {
            Wall.gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D wall)
    {
        if (wall.gameObject.tag == "Wall")
        {
            yourVar = true;
        }
    }
}
