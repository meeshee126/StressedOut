using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Henrik Hafner
public class GenerateBase : MonoBehaviour
{
	public bool doBuild;
	public bool isBuild;
	public bool doRepair;

    string buildPhase;

    public GameObject Wood;
    public GameObject Stone;
	public GameObject Iron;
	public GameObject Ruin;
    public SpriteRenderer mySprite;
    public GameObject buildFX;

	ResourceManager resourceManager;
	TimeBehaviour timeBehaviour;

	public int maxHealth = 200;
    public int health = 200;

	private void Start()
    {
		resourceManager = GameObject.Find("GameManager").GetComponent<ResourceManager>();

		timeBehaviour = GameObject.Find("GameManager").GetComponent<TimeBehaviour>();

        mySprite.enabled = false;
    }

    void Update()
    {
		// Check if the building is destroyed and then disable all game object that make up the building
		if (health == 0)
		{
			doRepair = true;

			Ruin.gameObject.SetActive(true);
			Wood.gameObject.SetActive(false);
			Stone.gameObject.SetActive(false);
			Iron.gameObject.SetActive(false);
		}
		else if (health < maxHealth)
		{
			doRepair = true;
		}

        // Check if the building has to be built first or if it has to be repaired
        if (doRepair == true && doBuild == true)
        {
            Repair();
        }
        else
        {
            BeginnBuild();
        }
	}

    /// <summary>
    /// The Triggers find the radius of the player and check if the radius is in contact with the building. 
    /// </summary>
    /// <param name="player"></param>
    void OnTriggerEnter2D(Collider2D player)
    {
        if (player.gameObject.tag == "Player")
        {
            doBuild = true;
        }
    }

	/// <summary>
	/// The trigger also makes sure that you see the building you want to build and the resources you need to build
	/// </summary>
	/// <param name="player"></param>
	private void OnTriggerStay2D(Collider2D player)
    {
		if (player.gameObject.tag == "Enemy" && isBuild == true && health > 0)
		{
			health -= 1;
		}

		if (isBuild == false && player.gameObject.tag == "Player")
        {
			mySprite.enabled = true;
            mySprite.color = new Color(1f, 1f, 1f, .5f);
        }
        else if (isBuild == true && player.gameObject.tag == "Player")
        {
            mySprite.enabled = false;
        }

		//Make Resource Costs for build
        if (Wood.gameObject.activeSelf == false && Stone.gameObject.activeSelf == false && Iron.gameObject.activeSelf == false && player.gameObject.tag == "Player" && doRepair == false)
        {
            resourceManager.ResourceCosts("Wood_Chunk", "-10");
        }
		else if (Wood.gameObject.activeSelf == true && Stone.gameObject.activeSelf == false && Iron.gameObject.activeSelf == false && player.gameObject.tag == "Player" && doRepair == false)
		{
			resourceManager.ResourceCosts("Wood_Chunk", "-20");
			resourceManager.ResourceCosts("Stone_Chunk", "-10");
		}
		else if (Wood.gameObject.activeSelf == false && Stone.gameObject.activeSelf == true && Iron.gameObject.activeSelf == false && player.gameObject.tag == "Player" && doRepair == false)
		{
			resourceManager.ResourceCosts("Wood_Chunk", "-10");
			resourceManager.ResourceCosts("Stone_Chunk", "-10");
			resourceManager.ResourceCosts("Iron_Chunk", "-10");
		}

		//Make Resource Costs for repair
		if (Wood.gameObject.activeSelf == true && Stone.gameObject.activeSelf == false && Iron.gameObject.activeSelf == false && player.gameObject.tag == "Player" && doRepair == true)
		{
			resourceManager.ResourceCosts("Wood_Chunk", "-5");
			resourceManager.ResourceCosts("Stone_Chunk", "");
		}
		else if (Wood.gameObject.activeSelf == false && Stone.gameObject.activeSelf == true && Iron.gameObject.activeSelf == false && player.gameObject.tag == "Player" && doRepair == true)
		{
			resourceManager.ResourceCosts("Wood_Chunk", "-10");
			resourceManager.ResourceCosts("Stone_Chunk", "-5");
		}
		else if (Wood.gameObject.activeSelf == false && Stone.gameObject.activeSelf == false && Iron.gameObject.activeSelf == true && player.gameObject.tag == "Player" && doRepair == true)
		{
			resourceManager.ResourceCosts("Wood_Chunk", "-5");
			resourceManager.ResourceCosts("Stone_Chunk", "-5");
			resourceManager.ResourceCosts("Stone_Chunk", "-5");
		}
	}

    void OnTriggerExit2D(Collider2D player)
    {
        if (player.gameObject.tag == "Player")
        {
            doBuild = false;

            mySprite.enabled = false;

			resourceManager.ResourceCosts("Wood_Chunk", "");
			resourceManager.ResourceCosts("Stone_Chunk", "");
			resourceManager.ResourceCosts("Iron_Chunk", "");
		}
    }

    //Build and upgrade buildings when the requirements are met in order to activate the respective gameobject.
    void BeginnBuild()
    {
        if (doBuild == true && isBuild != true && Input.GetKeyDown(KeyCode.F) && resourceManager.wood >= 10)
        {
			resourceManager.AddResource("Wood", -10);

			Wood.gameObject.SetActive(true);
            isBuild = true;

            buildPhase = "Wood";

            if (buildFX != null) Instantiate(buildFX, this.transform.position, Quaternion.identity);

			timeBehaviour.timeCost = timeBehaviour.crafting;

		}

        else if (doBuild == true && isBuild == true && Stone.gameObject.activeSelf == false && Iron.gameObject.activeSelf == false && Input.GetKeyDown(KeyCode.F) && resourceManager.wood >= 20 && resourceManager.stone >= 10 && health == maxHealth)
        {
			resourceManager.AddResource("Wood", -20);
			resourceManager.AddResource("Stone_Chunk", -10);

			Wood.gameObject.SetActive(false);
            Stone.gameObject.SetActive(true);

			health = 400;
			maxHealth = 400;

            buildPhase = "Stone";

            if (buildFX != null) Instantiate(buildFX, this.transform);

            timeBehaviour.timeCost = timeBehaviour.crafting;
		}

		else if (doBuild == true && isBuild == true && Stone.gameObject.activeSelf == true && Iron.gameObject.activeSelf == false && Input.GetKeyDown(KeyCode.F) && resourceManager.wood >= 10 && resourceManager.stone >= 10 && resourceManager.iron >= 10 && health == maxHealth)
		{
			resourceManager.AddResource("Wood", -10);
			resourceManager.AddResource("Stone_Chunk", -10);
			resourceManager.AddResource("Iron_Chunk", -10);

			Stone.gameObject.SetActive(false);
			Iron.gameObject.SetActive(true);

			health = 600;
			maxHealth = 600;

			buildPhase = "Iron";

			if (buildFX != null) Instantiate(buildFX, this.transform);

			timeBehaviour.timeCost = timeBehaviour.crafting;
		}
	}

    // Check if the building has been destroyed and check which was the last one to fix the right building again.
    void Repair()
    {
		if (health == 0 && Input.GetKeyDown(KeyCode.F))
        {
            health = maxHealth;

            Ruin.gameObject.SetActive(false);

            if (buildPhase == "Wood" && resourceManager.wood >= 10)
            {
				resourceManager.AddResource("Wood", -5);

				Wood.gameObject.SetActive(true);
                doRepair = false;
            }
            else if (buildPhase == "Stone" && resourceManager.stone >= 10 && resourceManager.wood >= 5)
            {
				resourceManager.AddResource("Wood", -10);
				resourceManager.AddResource("Stone_Chunk", -5);

				Stone.gameObject.SetActive(true);
                doRepair = false;
            }
			else if (buildPhase == "Iron" && resourceManager.iron >= 5 && resourceManager.stone >= 5 && resourceManager.wood >= 5)
			{
				resourceManager.AddResource("Wood", -5);
				resourceManager.AddResource("Stone_Chunk", -5);
				resourceManager.AddResource("Iron_Chunk", -5);

				Iron.gameObject.SetActive(true);
				doRepair = false;
			}

			timeBehaviour.timeCost = timeBehaviour.crafting;
		}
		else if (health < maxHealth && Input.GetKeyDown(KeyCode.F))
		{
			if (buildPhase == "Wood" && resourceManager.wood >= 10)
			{
				health = maxHealth;

				resourceManager.AddResource("Wood", -5);

				doRepair = false;
			}
			else if (buildPhase == "Stone" && resourceManager.stone >= 10 && resourceManager.wood >= 5)
			{
				health = maxHealth;

				resourceManager.AddResource("Wood", -10);
				resourceManager.AddResource("Stone_Chunk", -5);

				doRepair = false;
			}
			else if (buildPhase == "Iron" && resourceManager.iron >= 5 && resourceManager.stone >= 5 && resourceManager.wood >= 5)
			{
				health = maxHealth;

				resourceManager.AddResource("Wood", -5);
				resourceManager.AddResource("Stone_Chunk", -5);
				resourceManager.AddResource("Iron_Chunk", -5);

				doRepair = false;
			}

			timeBehaviour.timeCost = timeBehaviour.crafting;
		}
	}
}
