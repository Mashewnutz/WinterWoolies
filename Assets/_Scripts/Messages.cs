using UnityEngine;
using System.Collections;

public class Messages : MonoBehaviour 
{
	public static string SAY = "messages.say";
	public static string SHOUT = "messages.shout";
	public static string CLEAN = "messages.clean";

	public TextMessage textMessagePrefab;
	public int messagesToDisplay = 5;
	private int messagesDisplayed = 0;

	void Awake()
	{
		Messenger.AddListener<string>(SAY, OnSay);
		Messenger.AddListener<string>(SHOUT, SpawnText);
		Messenger.AddListener(CLEAN, OnClean);
	}

	void OnSay(string text)
	{
		if (messagesDisplayed++ < messagesToDisplay)
		{
			SpawnText(text);
		}
	}

	void SpawnText(string text)
	{
		TextMessage message = Instantiate(textMessagePrefab, transform.position, transform.rotation) as TextMessage;
		message.transform.parent = transform;
		message.SetText(text);
	}

	void OnClean()
	{
		messagesDisplayed = 0;
	}
}
