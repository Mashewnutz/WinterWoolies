using UnityEngine;
using System.Collections;

public class QpidTapText : MonoBehaviour 
{
	public static string SHOW = "qpidTapText.show";
	public static string HIDE = "qpidTapText.hide";
	public static string CLEAN = "qpidTapText.clean";

	public int tapToDisplay = 3;
	public int tapDisplayed = 0; 

	public float bounceDistance = 0.25f;
	public float bounceSpeed = 0.3f;
	private Vector3 startPosition;

	void Awake()
	{
		Messenger.AddListener(SHOW, OnShow);
		Messenger.AddListener(HIDE, OnHide);
		Messenger.AddListener(CLEAN, OnClean);

		startPosition = transform.position;
	}

	void OnShow()
	{
		if (++tapDisplayed < tapToDisplay)
		{
			renderer.enabled = true;
		}
	}

	void OnHide()
	{
		renderer.enabled = false;
	}

	void OnClean()
	{
		tapDisplayed = 0;
	}

	void Update()
	{
		float offset = Mathf.Cos(Mathf.Deg2Rad * Time.time * bounceSpeed);
		offset *= offset;
		offset *= bounceDistance;
		transform.position = startPosition + Vector3.up * offset;
	}
}
