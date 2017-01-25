using UnityEngine;
using System.Collections;

public class Raindrop : MonoBehaviour {

	public static string DESTROYED = "raindrop.destroyed";
	public Vector3 targetPosition;
	public float spawnSpeed = 2;
	public float hoverAmount = 0.01f;
	public float hoverSpeedY = 1;
	public float hoverSpeedX = 1;
	public bool taken = false;

	private float lifetime = 0;

	// Use this for initialization
	void Start () {
		lifetime = 0;
	}
	
	void Update () 
	{
		lifetime += Time.deltaTime;

		Vector3 delta = targetPosition - transform.position;
		transform.position += delta * Time.deltaTime * spawnSpeed;

		transform.position += new Vector3 (Mathf.Sin (lifetime * hoverSpeedX) * hoverAmount, Mathf.Sin (lifetime * hoverSpeedY) * hoverAmount, 0);
	}

	public void Die() 
	{
		Messenger.Broadcast (DESTROYED, this);
		Destroy (this.gameObject);
	}
}
