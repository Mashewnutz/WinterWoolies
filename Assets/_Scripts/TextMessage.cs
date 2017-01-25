using UnityEngine;
using System.Collections;

public class TextMessage : MonoBehaviour 
{
	public float lifeTime = 1.0f;
	public float liftSpeed = 10;
	public float fadeDelay = 0.25f;
	private TextMesh textMesh;


	public void SetText(string text)
	{
		if (!textMesh)
			textMesh = GetComponent<TextMesh>();
		textMesh.text = text;
	}

	void Update () 
	{
		if (fadeDelay > 0) {
			fadeDelay -= Time.deltaTime;
			return;
		}

		lifeTime -= Time.deltaTime;

		Color c = textMesh.color;
		c.a = lifeTime + fadeDelay;
		textMesh.color = c;

		transform.position += Vector3.up * liftSpeed * Time.deltaTime;

		if (lifeTime < 0)
			Destroy(gameObject);
	}
}
