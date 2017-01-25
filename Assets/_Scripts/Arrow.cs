using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour 
{
	public float baseSpeed = 10;
	public float incrementSpeed = 0.5f;
	public float rotateSpeed = 10;
	public float aimLock = 0.05f;

	public bool isAiming = true;

	public Raindrop target;

	public Color GetColor()
	{
		return GetComponentInChildren<SpriteRenderer>().color;
	}

	public void Die()
	{
		Game.GetObjectManager().SpawnBrokenArrow(this);
		Messenger.Broadcast(LivesModel.ARROW_BROKEN);
		Destroy (gameObject);
	}
	
	void Update()
	{
		if (!target)
			return;

		if (isAiming)
			Aim();
		Move();
	}
	
	public void Aim()
	{
		Vector3 targetPosition = target.transform.position;
		float dot = Vector3.Dot ((targetPosition - transform.position).normalized,
		                         transform.up.normalized);

		if (Mathf.Abs(dot) > aimLock)
			RotateTowards(dot);
		else
			LookAt(targetPosition);
	}

	void RotateTowards(float dot)
	{
		float angle = rotateSpeed * Time.deltaTime * Mathf.Sign(dot);
		transform.Rotate (Vector3.forward, angle);
	}

	void LookAt(Vector3 point)
	{
		Vector3 direction = target.transform.position - transform.position;

		float angle = Mathf.Atan2 (direction.y, direction.x) - Mathf.Atan2(transform.right.y, transform.right.x);
		angle *= Mathf.Rad2Deg;
		transform.Rotate (Vector3.forward, angle);
	}

	void LookAt(float dot)
	{
		float alpha = Mathf.Acos(dot);
		transform.Rotate(Vector3.forward, alpha);
	}

	void Move()
	{
		Vector3 offset = transform.right * (Time.deltaTime * baseSpeed);
		transform.position += offset;
	}

	public void IncreaseSpeed()
	{
		baseSpeed += incrementSpeed;
	}

}
