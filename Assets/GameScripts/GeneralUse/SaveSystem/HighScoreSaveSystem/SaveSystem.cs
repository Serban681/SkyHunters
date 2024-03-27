using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class HighScoreSaveSystem
{
    public static void SaveData()
	{
		BinaryFormatter formatter = new BinaryFormatter();
		string path = Application.persistentDataPath + "/highscore.data";

		FileStream stream = new FileStream(path, FileMode.Create);

		HighScoreData data = new HighScoreData();

		formatter.Serialize(stream, data);
		stream.Close();
	}

	public static HighScoreData LoadData()
	{
		string path = Application.persistentDataPath + "/player.data";
		if (File.Exists(path))
		{
			BinaryFormatter formatter = new BinaryFormatter();
			FileStream stream = new FileStream(path, FileMode.Open);

			HighScoreData data = formatter.Deserialize(stream) as HighScoreData;
			stream.Close();

			return data;
		}
		else
		{
			return null;
		}
	}
}
