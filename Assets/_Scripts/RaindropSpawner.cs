using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RaindropSpawner : MonoBehaviour {

	public Raindrop raindropPrefab;
	public GameObject raindropPositions;
	public GameObject raindropSpawnPositions;
	public float minimumDistance = 0;
	public float smallestRaindrop = 0.5f;
	public float bigestRaindrop = 2.0f;
	public int initialNumberOfRaindrops = 3;

	public List<Raindrop> raindrops = new List<Raindrop>();

		void Start () {
		for (int i = 0; i < initialNumberOfRaindrops; ++i) {
			Vector3 finalPos = GetRaindropPosition ();
			SpawnRaindrop (finalPos, finalPos);
		}
	}

	[ContextMenu("Hide Spawn Points")]
	public void HideSpawnPoints()
	{
		GameObjectsUtils.ToggleRenderer(transform, false);
	}

	[ContextMenu("Show Spawn Points")]
	public void ShowSpawnPoints()
	{
		GameObjectsUtils.ToggleRenderer(transform, true);
	}

	void Awake() {
		Messenger.AddListener<Raindrop> (Raindrop.DESTROYED, OnRaindropDestroyed);
	}

	Vector3 GetRaindropPosition(){
		if (raindrops.Count == 0)
			return raindropPositions.transform.GetChild(0).position;

		int numberSpawnPositions = raindropPositions.transform.childCount;
		float distance = 0;
		Vector3 position;
		do {
			int spawnIndex = Random.Range (0, numberSpawnPositions - 1);
			position = raindropPositions.transform.GetChild (spawnIndex).transform.position;
			distance = FindClosestDistance(position);
		} while(distance < minimumDistance);
		return position;
	}

	Vector3 GetRaindropSpawnPosition(Vector3 finalPos){
		float closest = float.MaxValue;
		Vector3 spawnPoint = new Vector3(0,0,0);
		for (int i = 0; i < raindropSpawnPositions.transform.childCount; ++i) {
			Transform spawnPos = raindropSpawnPositions.transform.GetChild(i);
			if((spawnPos.position-finalPos).magnitude < closest)
			{
				closest = (spawnPos.position-finalPos).magnitude;
				spawnPoint = spawnPos.position;
			}
		}
		return spawnPoint;
	}

	void SpawnRaindrop(Vector3 start, Vector3 destination) {
		Raindrop raindrop = Instantiate (raindropPrefab, start, Quaternion.identity) as Raindrop;
		raindrop.targetPosition = destination;
		float scale = Random.Range (smallestRaindrop, bigestRaindrop);
		raindrop.transform.localScale = new Vector3 (scale, scale, 1);
		raindrops.Add (raindrop);
	}

	void OnRaindropDestroyed(Raindrop raindrop) {
		Vector3 finalPos = GetRaindropPosition ();
		SpawnRaindrop (GetRaindropSpawnPosition(finalPos), finalPos);
		raindrops.Remove (raindrop);
	}

	float FindClosestDistance(Vector3 position)
	{
		float minDistance = float.MaxValue;
		foreach(Raindrop raindrop in raindrops){
			float distance = (raindrop.gameObject.transform.position - position).magnitude;
			if(distance < minDistance)
				minDistance = distance;
		}
		return minDistance;
	}

	public Raindrop GetNextTarget(Vector3 position)
	{
		foreach (Raindrop raindrop in raindrops) 
		{
			float distance = (raindrop.transform.position-position).magnitude;
			if (!raindrop.taken && distance > minimumDistance)
			{
				return raindrop;
			}
		}
		int index = Random.Range (0, raindrops.Count - 1);
		return raindrops[index];
	}
}
