using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public struct Score
{
	public string name;
	public float time;


	public Score(string name, float time)
    {
		this.name = name;
		this.time = time;
	}
}


public static class Leaderboard
{
	public static void SaveScore(Score newScore)
    {
		BinaryFormatter formatter = new BinaryFormatter();
		string path = Application.persistentDataPath + "/leaderboard.sav";

		FileStream stream = new FileStream(path, FileMode.Create);

		formatter.Serialize(stream, newScore);
	}
	public static Score LoadScore()
	{
		string path = Application.persistentDataPath + "/leaderboard.sav";

		if (File.Exists(path))
        {
			BinaryFormatter formatter = new BinaryFormatter();
			FileStream stream = new FileStream(path, FileMode.Open);
			Score score = (Score)formatter.Deserialize(stream);
			stream.Close();

			return score;
		}
		return new Score("null", 0);
	}
	public static string FormatTime(float time)
	{
		int seconds = Mathf.FloorToInt(time);
		int milliseconds = (int)((time - seconds) * 10000);
		return $"{seconds}.{ milliseconds}";
	}
}
