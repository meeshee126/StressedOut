using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
	[HideInInspector] public GenerateBase[] building;
	public GameObject[] buildingObj;

    private void Start()
	{
        building = new GenerateBase[buildingObj.Length];

        for (int i = 0; i < buildingObj.Length; i++)
		{
			building[i] = buildingObj[i].GetComponent<GenerateBase>();
		}
	}

	/// <summary>
	/// Make sure ALL buildings are saved
	/// </summary>
	public void SaveController()
	{
		SaveSystem.SaveBuilding(this);
	}

	/// <summary>
	/// Make sure ALL buildings are loaded
	/// </summary>
	public void LoadController()
	{
		BuildingData data = SaveSystem.LoadBuilding();

		for (int i = 0; i < buildingObj.Length; i++)
		{
			building[i].health = data.health[i];

			if (data.isActive[i] == 0)
			{
				building[i].Wood.SetActive(false);
				building[i].Stone.SetActive(false);
				building[i].Iron.SetActive(false);
				building[i].Ruin.SetActive(false);

				building[i].isBuild = false;
				building[i].doRepair = false;
			}
			else if (data.isActive[i] == 1)
			{
				building[i].Wood.SetActive(true);
				building[i].Stone.SetActive(false);
				building[i].Iron.SetActive(false);
				building[i].Ruin.SetActive(false);

				building[i].isBuild = true;
				building[i].doRepair = false;
			}
			else if (data.isActive[i] == 2)
			{
				building[i].Wood.SetActive(false);
				building[i].Stone.SetActive(true);
				building[i].Iron.SetActive(false);
				building[i].Ruin.SetActive(false);

				building[i].isBuild = true;
				building[i].doRepair = false;
			}
			else if (data.isActive[i] == 3)
			{
				building[i].Wood.SetActive(false);
				building[i].Stone.SetActive(false);
				building[i].Iron.SetActive(false);
				building[i].Ruin.SetActive(true);

				building[i].isBuild = true;
				building[i].doRepair = true;
			}
			else if (data.isActive[i] == 4)
			{
				building[i].Wood.SetActive(false);
				building[i].Stone.SetActive(false);
				building[i].Iron.SetActive(true);
				building[i].Ruin.SetActive(false);

				building[i].isBuild = true;
				building[i].doRepair = false;
			}
		}
	}
}
