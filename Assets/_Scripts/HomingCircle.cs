using UnityEngine;
using System.Collections;

public class HomingCircle : MonoBehaviour 
{
	public float closeRange = 1;
	public float farRange = 5;
	public float updateDelay = 1.0f;

	private float pixelsToUnits = 2.73f;
	private float updateAfter = 0;
	private float startTime = 0;
	private bool scaleWasSet = false;
	private float length = 0;
	private SpriteRenderer spriteRenderer;

	void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		spriteRenderer.enabled = false;
	}

	void Start()
	{
		startTime = Time.time;
		updateAfter = startTime + updateDelay;
	}

	public void SetColor(Color c)
	{
		spriteRenderer.color = c;
	}

	public void OnDistanceUpdate(float distance)
	{
		if (!scaleWasSet)
		{
			SetScale(farRange);
			scaleWasSet = true;
			spriteRenderer.enabled = true;
		}

		if (Time.time <= updateAfter)
		{
			SetOpacity(Mathf.InverseLerp(startTime, updateAfter, Time.time));
		}
		else
		{
			if (length == 0)
			{
				length = distance;

			}

			if(distance < 0)
				distance = 0;

			float abs = Mathf.Abs(distance / length);
			float scale = Mathf.Lerp(closeRange, farRange, abs);
			SetScale(scale);
			SetOpacity(abs+0.5f);
		}
	}

	void SetScale(float scale)
	{
		transform.localScale = Vector3.one * (scale / pixelsToUnits);
	}

	void SetOpacity(float opacity)
	{
		Color c = spriteRenderer.color;
		c.a = opacity;
		spriteRenderer.color = c;
	}

}
