using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBase : MonoBehaviour
{
    public bool doBuild;
    public bool isBuild;
    public bool doUpgrade;

    public GameObject Wood;
    public GameObject Stone;

    public int health = 100;

    void Update()
    {
        if (health == 0)
        {
            isBuild = false;

            if (Wood.gameObject.activeSelf == true)
            {
                Wood.gameObject.SetActive(false);
            }
            else if (Stone.gameObject.activeSelf == true)
            {
                Stone.gameObject.SetActive(false);
            }
        }

        BeginnBuild();
        Repair();
    }

    void OnTriggerEnter2D(Collider2D buildRadius)
    {
        if (buildRadius.gameObject.tag == "BuildRadius")
        {
            doBuild = true;
        }
    }

    void OnTriggerExit2D(Collider2D buildRadius)
    {
        if (buildRadius.gameObject.tag == "BuildRadius")
        {
            doBuild = false;
        }
    }

    void BeginnBuild()
    {
        if (doBuild == true && isBuild != true && Input.GetKeyDown(KeyCode.F))
        {
            Wood.gameObject.SetActive(true);
            doUpgrade = true;
            isBuild = true;
        }

        else if (doBuild == true && doUpgrade == true && Input.GetKeyDown(KeyCode.F))
        {
            Wood.gameObject.SetActive(false);
            Stone.gameObject.SetActive(true);
        }
    }

    void Repair()
    {
        if (health == 0 && Input.GetKeyDown(KeyCode.F))
        {
            health = 100;
        }
    }
}
