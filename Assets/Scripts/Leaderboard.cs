using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

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
	public static bool TrySaveScore(Score newScore)
    {
		if (TryLoadScore(out Score oldScore) && oldScore.time < newScore.time)
		{
			return false;
		}
		Save(newScore);
		return true;
	}
	public static bool TryLoadScore(out Score score)
	{
		if (PlayerPrefs.HasKey("save"))
		{
			score = Load();
			return true;
		}
		score = new Score();
		return false;
	}
	public static string FormatTime(float time)
	{
		int seconds = Mathf.FloorToInt(time);
		int milliseconds = (int)((time - seconds) * 10000);
		return $"{seconds}.{ milliseconds}";
	}


	private static void Save(Score newScore)
	{
		XmlSerializer xml = new XmlSerializer(typeof(Score));
		StringWriter writer = new StringWriter();

		xml.Serialize(writer, newScore);

		PlayerPrefs.SetString("save", writer.ToString());
	}
	private static Score Load()
	{
		XmlSerializer xml = new XmlSerializer(typeof(Score));
		StringReader reader = new StringReader(PlayerPrefs.GetString("save"));

		return (Score)xml.Deserialize(reader);
	}
}
