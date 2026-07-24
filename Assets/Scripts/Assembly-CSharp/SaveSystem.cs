public static class SaveSystem
{
	private static string savePath = global::UnityEngine.Application.persistentDataPath + "/savefile.dat";

	private static string secretKey = "jfuiwhf89732y4fhu9sdh";

	public static void Save(SaveData data)
	{
		string contents = EncryptDecrypt(global::UnityEngine.JsonUtility.ToJson(data), secretKey);
		if (global::System.IO.File.Exists(savePath))
		{
			string destFileName = savePath + ".bak";
			global::System.IO.File.Copy(savePath, destFileName, overwrite: true);
		}
		global::System.IO.File.WriteAllText(savePath, contents);
	}

	public static SaveData Load()
	{
		string path = savePath + ".bak";
		SaveData saveData = AttemptLoad(savePath);
		if (saveData == null && global::System.IO.File.Exists(path))
		{
			global::UnityEngine.Debug.LogWarning("Main save corrupt/missing, using backup...");
			saveData = AttemptLoad(path);
		}
		if (saveData == null)
		{
			global::UnityEngine.Debug.Log("No savefile found, creating new...");
			saveData = new SaveData();
			Save(saveData);
		}
		return saveData;
	}

	public static bool SubmitHighscore(int score)
	{
		SaveData saveData = Load();
		if (saveData.highscores.Length >= 10 && score <= global::System.Linq.Enumerable.Min(saveData.highscores, (HighscoreEntry e) => e.score))
		{
			return false;
		}
		HighscoreEntry element = new HighscoreEntry
		{
			score = score,
			date = global::System.DateTime.Now.ToString("MM/dd/yy hh:mm tt")
		};
		saveData.highscores = global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Take(global::System.Linq.Enumerable.OrderByDescending(global::System.Linq.Enumerable.Append(saveData.highscores, element), (HighscoreEntry e) => e.score), 10));
		Save(saveData);
		return true;
	}

	private static SaveData AttemptLoad(string path)
	{
		try
		{
			if (!global::System.IO.File.Exists(path))
			{
				return null;
			}
			return global::UnityEngine.JsonUtility.FromJson<SaveData>(EncryptDecrypt(global::System.IO.File.ReadAllText(path), secretKey));
		}
		catch (global::System.Exception ex)
		{
			global::UnityEngine.Debug.LogError("Failed to load " + path + ": " + ex.Message);
			return null;
		}
	}

	private static string EncryptDecrypt(string textToProcess, string key)
	{
		global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder();
		for (int i = 0; i < textToProcess.Length; i++)
		{
			stringBuilder.Append((char)(textToProcess[i] ^ key[i % key.Length]));
		}
		return stringBuilder.ToString();
	}
}
