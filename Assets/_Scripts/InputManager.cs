using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour 
{
	private Tap tap;
	void Update()
	{
		Tap currentTap = FindTapObject();

		if (Input.GetMouseButtonUp(0) && tap)
		{
			tap.TapUp();
			tap = null;
		}
		else if (Input.GetMouseButtonDown(0))
		{
			if (tap)
				tap.TapUp();

			tap = currentTap;
			if (tap)
				tap.TapDown();
		}
		else if (tap && tap != currentTap)
		{
			tap.TapUp();
			tap = currentTap;

			if (tap)
				tap.TapDown();
		}
	}

	Tap FindTapObject()
	{
		float dist = transform.position.z - Camera.main.transform.position.z;
		var screenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, dist);
		var worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
		RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);			
		if (hit.collider != null)
		{
			return hit.collider.GetComponent<Tap>();
		}
		return null;
	}
}
