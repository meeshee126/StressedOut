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
}
