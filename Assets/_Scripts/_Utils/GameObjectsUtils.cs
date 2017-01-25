using UnityEngine;
using System.Collections;

public static class GameObjectsUtils 
{
	public static void ToggleRenderer(Transform parent, bool isEnabled)
	{
		foreach (Transform child in parent)
		{
			if (child.renderer)
				child.renderer.enabled = isEnabled;

			ToggleRenderer(child.transform, isEnabled);
		}
	}

}
