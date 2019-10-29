using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBase : MonoBehaviour
{
    bool doBuild;
    bool isBuild;
    bool doRepair;

    string buildPhase;

    public GameObject Wood;
    public GameObject Stone;

    public int health = 100;

    public SpriteRenderer mySprite;

    private void Start()
    {
        mySprite.enabled = false;
    }

    void Update()
    {
        // Check if the building is destroyed and then disable all game object that make up the building
        if (health == 0)
        {
            doRepair = true;

            Wood.gameObject.SetActive(false);
            Stone.gameObject.SetActive(false);
        }

        // Check if the building has to be built first or if it has to be repaired
        if (doRepair == true)
        {
            Repair();
        }
        else
        {
            BeginnBuild();
        }
    }

    /// <summary>
    /// Find the radius of the player and check if the radius is in contact with the building
    /// </summary>
    /// <param name="buildRadius"></param>
    void OnTriggerEnter2D(Collider2D buildRadius)
    {
        if (buildRadius.gameObject.tag == "BuildRadius")
        {
            doBuild = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isBuild == false)
        {
            mySprite.enabled = true;
            mySprite.color = new Color(1f, 1f, 1f, .5f);
        }
        else if (isBuild == true)
        {
            mySprite.enabled = false;
        }
    }

    void OnTriggerExit2D(Collider2D buildRadius)
    {
        if (buildRadius.gameObject.tag == "BuildRadius")
        {
            doBuild = false;

            mySprite.enabled = false;
        }
    }

    //Build and upgrade buildings when the requirements are met in order to activate the respective gameobject.
    void BeginnBuild()
    {
        if (doBuild == true && isBuild != true && Input.GetKeyDown(KeyCode.F))
        {
            Wood.gameObject.SetActive(true);
            isBuild = true;

            buildPhase = "Wood";
        }

        else if (doBuild == true && isBuild == true && Input.GetKeyDown(KeyCode.F))
        {
            Wood.gameObject.SetActive(false);
            Stone.gameObject.SetActive(true);

            buildPhase = "Stone";
        }
    }

    // Check if the building has been destroyed and check which was the last one to fix the right building again.
    void Repair()
    {
        if (health == 0 && Input.GetKeyDown(KeyCode.F))
        {
            health = 100;

            if (buildPhase == "Wood")
            {
                Wood.gameObject.SetActive(true);
                doRepair = false;
            }
            else if (buildPhase == "Stone")
            {
                Stone.gameObject.SetActive(true);
                doRepair = false;
            }
        }
    }
}
