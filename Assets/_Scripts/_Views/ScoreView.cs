using UnityEngine;
using System.Collections;

public class ScoreView : MonoBehaviour 
{
	public TextMesh textMesh;

	void Awake () 
	{
		Messenger.AddListener<int> (ScoreModel.CHANGED, OnScoreChange);
	}

	void Start()
	{
		if (!textMesh)
			textMesh = GetComponent<TextMesh>();
	}
	
	void OnScoreChange(int score)
	{
		textMesh.text = "" + score;
	}
}
