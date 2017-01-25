using UnityEngine;
using System.Collections;

public class Tap : MonoBehaviour 
{
	public void TapDown()
	{
		transform.parent.SendMessage("OnTapDown",SendMessageOptions.DontRequireReceiver);
	}

	public void TapUp()
	{
		transform.parent.SendMessage("OnTapUp",SendMessageOptions.DontRequireReceiver);
	}
}
