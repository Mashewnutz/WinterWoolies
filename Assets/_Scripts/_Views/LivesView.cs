using UnityEngine;
using System.Collections;

public class LivesView : MonoBehaviour 
{
	void Awake () 
	{
		Messenger.AddListener<int> (LivesModel.COUNT_CHANGED, OnLivesCountChange);
	}
	
	void OnLivesCountChange(int count)
	{
		int i = 0;
		foreach (Transform child in transform)
		{
			child.gameObject.SetActive(i++ < count);
		}
	}
}
