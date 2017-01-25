using UnityEngine;
using System.Collections;

public class ObjectManager : MonoBehaviour 
{
	public Arrow[] arrowPrefabs;
	public GameObject brokenArrowPrefab;
	public GameObject raindropExplodePrefab;
	public Target targetPrefab;

	private int lastSpawnedArrowIndex = 0;

	public Arrow SpawnArrow(Transform origin)
	{
		Arrow arrow = Instantiate(arrowPrefabs[lastSpawnedArrowIndex], origin.position, origin.rotation) as Arrow;
		arrow.transform.parent = transform;

		if (++lastSpawnedArrowIndex == arrowPrefabs.Length)
			lastSpawnedArrowIndex = 0;

		return arrow;
	}

	public GameObject SpawnRaindropExplode(Transform origin, bool smallExplosion)
	{
		GameObject raindropExplode = Instantiate(raindropExplodePrefab, origin.position, origin.rotation) as GameObject;
		raindropExplode.transform.parent = transform;
		raindropExplode.transform.localScale = origin.localScale;
		if (smallExplosion)
			raindropExplode.transform.localScale *= 0.5f;

		return raindropExplode;
	}

	public Target SpawnTarget(Arrow arrow, Raindrop raindrop)
	{		
		Target target = Instantiate (targetPrefab, raindrop.transform.position, raindrop.transform.rotation) as Target;
		target.transform.parent = transform;
		target.arrow = arrow;
		target.raindrop = raindrop;
		target.transform.localScale = raindrop.transform.localScale;
		target.SetColor(arrow.GetColor ());

		return target;
	}

	public GameObject SpawnBrokenArrow(Arrow arrow)
	{
		float brokenArrowForce = 150.0f;
		GameObject brokenArrow = Instantiate(brokenArrowPrefab, arrow.transform.position, arrow.transform.rotation) as GameObject;
		brokenArrow.transform.parent = transform;
		brokenArrow.rigidbody2D.AddForce (transform.right * brokenArrowForce);

		brokenArrow.GetComponent<SpriteRenderer>().color = arrow.GetColor ();

		return brokenArrow;
	}
}
