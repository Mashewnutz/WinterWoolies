using UnityEngine;
using System.Collections;

public class Glow : MonoBehaviour 
{
	SpriteRenderer spriteRenderer;

	void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	public void OnAccuracyUpdate(float accuracy)
	{
		Color color = spriteRenderer.color;
		color.a = 1 - Mathf.Abs(accuracy);
		spriteRenderer.color = color;
	}
}
