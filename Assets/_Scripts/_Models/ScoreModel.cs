using UnityEngine;
using System.Collections;

public class ScoreModel : MonoBehaviour 
{
	public static string ADD = "scoreModel.add";
	public static string CLEAN = "scoreModel.clean";

	public static string CHANGED = "scoreModel.changed";

	public int score = 0;

	void Awake()
	{
		Messenger.AddListener<int>(ADD, OnAddScore);
		Messenger.AddListener(CLEAN, OnClean);
	}

	void OnAddScore(int amount)
	{
		score += amount;
		Messenger.Broadcast<int>(CHANGED, score);
	}

	void OnClean()
	{
		score = 0;
		Messenger.Broadcast<int>(CHANGED, score);
	}
}
