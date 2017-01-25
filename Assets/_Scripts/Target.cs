using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour 
{
	public static string ARROW_HIT_TARGET = "Target.ArrowHitTarget";
	public static string ARROW_MISSED_TARGET = "Target.ArrowMissedTarget";

	public float closeRange = 0.3f;
	public float farRange = 1.0f;

	public Transform closeRing;
	public Transform farRing;
	
	public Arrow arrow;
	public Raindrop raindrop;
	public float accuracy;

	public bool wasInRange = false;

	private HomingCircle hominCircle;
	private Glow glow;
	private float pixelsToUnits = 2.56f;

	static int count = 0;

	public void Die()
	{
		Destroy (gameObject);
	}

	void OnTapDown()
	{
		ArrowCheck ();
	}

	void ArrowCheck()
	{
		if (accuracy >= 1 || accuracy <= -1)
			Messenger.Broadcast<Target, Arrow, float>(ARROW_MISSED_TARGET, this, arrow, accuracy);
		else
			Messenger.Broadcast<Target, Arrow, float>(ARROW_HIT_TARGET, this, arrow, accuracy);
	}

	public void SetColor(Color color)
	{
		hominCircle.SetColor(color);
	}

	void Awake () 
	{
		name = "Target" + count++;
		hominCircle = GetComponentInChildren<HomingCircle>();

		SetScale (closeRing, closeRange * 2);
		SetScale (farRing, farRange * 2);
		SetScale(GetComponentInChildren<Tap>().transform, farRange * 2);
	}
	
	void Update()
	{
		if (!arrow)
			return;

		transform.position = raindrop.transform.position;

		float distance = ToTarget().magnitude;
		float absoluteAccuracy = GetAbsoluteAccuracy(distance);
		float sign = GetDirectionSign();

		accuracy = absoluteAccuracy * sign;
		if (accuracy == 0.0f) {
			arrow.isAiming = false;
			wasInRange = true;
		}

		Debug.Log ("distance: " + distance + " accuracy: " + accuracy + " wasInRange: " + wasInRange);

		if (accuracy <= -1.0f &&  wasInRange) {
			ArrowCheck ();
		}

		hominCircle.OnDistanceUpdate(distance * sign);
	}

	void SetScale(Transform transform, float scale)
	{
		transform.localScale = Vector3.one * (scale / pixelsToUnits);
	}

	float GetDirectionSign()
	{
		return Mathf.Sign(Vector3.Dot(arrow.transform.right, ToTarget()));
	}

	Vector3 ToTarget()
	{
		Vector3 thisPosition = new Vector3(transform.position.x,
		                                   transform.position.y);
		Vector3 otherPosition = new Vector3(arrow.transform.position.x,
		                                    arrow.transform.position.y);
		return thisPosition - otherPosition;
	}

	float GetAbsoluteAccuracy(float distance)
	{
		if (distance >= farRange)
			return 1;
		if (distance <= closeRange)
			return 0;

		return Mathf.InverseLerp(closeRange, farRange, distance);
	}
}
