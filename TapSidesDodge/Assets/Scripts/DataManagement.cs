using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class DataManagement : MonoBehaviour
{

	public static DataManagement datamanagement;
	public int tokensHighscore;
	public int totalCollectedTokens;

	public string bestTime;

	void Awake()
	{
		Environment.SetEnvironmentVariable ("MONO_REFLECTION_SERIALIZER", "yes");
		datamanagement = this;
		DontDestroyOnLoad (gameObject);
	}

	public void SaveData()
	{
		
		BinaryFormatter binForm = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/gameInfo.dat");
		gameData data = new gameData ();
		data.tokensHighScore = tokensHighscore;
		data.totalCollectedTokens = totalCollectedTokens;
		data.bestTime = bestTime;
		binForm.Serialize (file, data);
		file.Close ();
	}

	public void LoadData()
	{
		
		if (File.Exists (Application.persistentDataPath + "/gameInfo.dat")) 
		{
			BinaryFormatter binForm = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/gameInfo.dat", FileMode.Open);
			gameData data = (gameData)binForm.Deserialize (file);
			file.Close ();
			tokensHighscore = data.tokensHighScore;
			totalCollectedTokens = data.totalCollectedTokens;
			bestTime = data.bestTime;
		}
	}
}

[Serializable]
class gameData{
public int tokensHighScore;
public int totalCollectedTokens;
public string bestTime;
}
