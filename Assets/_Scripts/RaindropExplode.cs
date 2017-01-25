using UnityEngine;
using System.Collections;

public class RaindropExplode : MonoBehaviour 
{
	public GameObject snowflakePrefab;

	void Done()
	{
		for (int i = 0; i < Game.Instance().livesModel.activeArrows; ++i)
		{
			GameObject snowflake = Instantiate(snowflakePrefab, transform.position, transform.rotation) as GameObject;
			snowflake.transform.localScale = transform.localScale;
		}
		Destroy (gameObject);
	}
}
