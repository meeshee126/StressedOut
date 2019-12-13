using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

//Henrik Hafner
public static class SaveSystem
{
	/// <summary>
	/// Save the Datas from the Player
	/// </summary>
	/// <param name="player"></param>
	public static void SavePlayer(Stats player)
	{
		BinaryFormatter formatter = new BinaryFormatter();
		string path = Application.persistentDataPath + "/player.fun";
		FileStream stream = new FileStream(path, FileMode.Create);

		PlayerData data = new PlayerData(player);

		formatter.Serialize(stream, data);
		stream.Close();
	}


	/// <summary>
	/// Load the Datas from the Player
	/// </summary>
	/// <returns></returns>
	public static PlayerData LoadPlayer()
	{
		string path = Application.persistentDataPath + "/player.fun";
		if (true)
		{
			BinaryFormatter formatter = new BinaryFormatter();
			FileStream stream = new FileStream(path, FileMode.Open);

			PlayerData data = formatter.Deserialize(stream) as PlayerData;
			stream.Close();

			return data;
		}
		else
		{
			Debug.LogError("Save file not found in " + path);
			return null;
		}
	}

	/// <summary>
	/// Save the Datas from the Resource Manager
	/// </summary>
	/// <param name="resource"></param>
	public static void SaveResource(ResourceManager resource)
	{
		BinaryFormatter formatter = new BinaryFormatter();
		string path = Application.persistentDataPath + "/resource.fun";
		FileStream stream = new FileStream(path, FileMode.Create);

		ResourceData data = new ResourceData(resource);

		formatter.Serialize(stream, data);
		stream.Close();
	}

	/// <summary>
	/// Load the Datas from the Resource Manager
	/// </summary>
	/// <returns></returns>
	public static ResourceData LoadResource()
	{
		string path = Application.persistentDataPath + "/resource.fun";
		if (true)
		{
			BinaryFormatter formatter = new BinaryFormatter();
			FileStream stream = new FileStream(path, FileMode.Open);

			ResourceData data = formatter.Deserialize(stream) as ResourceData;
			stream.Close();

			return data;
		}
		else
		{
			Debug.LogError("Save file not found in " + path);
			return null;
		}
	}

	/// <summary>
	/// Save the Datas from the Buildings
	/// </summary>
	/// <param name="buildingManager"></param>
	public static void SaveBuilding(BuildingManager buildingManager)
	{
		BinaryFormatter formatter = new BinaryFormatter();
		string path = Application.persistentDataPath + "/building.fun";
		FileStream stream = new FileStream(path, FileMode.Create);

		BuildingData data = new BuildingData(buildingManager);

		formatter.Serialize(stream, data);
		stream.Close();
	}

	/// <summary>
	/// Load the Datas from the Buildings
	/// </summary>
	/// <returns></returns>
	public static BuildingData LoadBuilding()
	{
		string path = Application.persistentDataPath + "/building.fun";
		if (true)
		{
			BinaryFormatter formatter = new BinaryFormatter();
			FileStream stream = new FileStream(path, FileMode.Open);

			BuildingData data = formatter.Deserialize(stream) as BuildingData;
			stream.Close();

			return data;
		}
		else
		{
			Debug.LogError("Save file not found in " + path);
			return null;
		}
	}

	/// <summary>
	/// Save the Datas from the Time
	/// </summary>
	/// <param name="sun"></param>
	public static void SaveTime(Timer timer)
	{
		BinaryFormatter formatter = new BinaryFormatter();
		string path = Application.persistentDataPath + "/time.fun";
		FileStream stream = new FileStream(path, FileMode.Create);

		TimeData data = new TimeData(timer);

		formatter.Serialize(stream, data);
		stream.Close();
	}

	/// <summary>
	/// Load the Datas from the Time
	/// </summary>
	/// <returns></returns>
	public static TimeData LoadTime()
	{
		string path = Application.persistentDataPath + "/time.fun";
		if (true)
		{
			BinaryFormatter formatter = new BinaryFormatter();
			FileStream stream = new FileStream(path, FileMode.Open);

			TimeData data = formatter.Deserialize(stream) as TimeData;
			stream.Close();

			return data;
		}
		else
		{
			Debug.LogError("Save file not found in " + path);
			return null;
		}
	}
    //Michael Schmidt
    /// <summary>
    /// Saves impotertend datas from Sun Script
    /// </summary>
    /// <param name="sun"></param>
    public static void SaveSun(Sun sun)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/sun.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        SunData data = new SunData(sun);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    //Michael Schmidt
    /// <summary>
    /// Load sun Datas
    /// </summary>
    /// <returns></returns>
    public static SunData LoadSun()
    {
        string path = Application.persistentDataPath + "/sun.fun";
        if (true)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SunData data = formatter.Deserialize(stream) as SunData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
