using UnityEngine;
using System.Collections;

public class SnowFlake : MonoBehaviour 
{
	public float force = 5.0f;
	public float torque = 5.0f;
	private bool applyOnce = false;
	
	void Update () 
	{
		if (!applyOnce) {
			rigidbody2D.AddForce (transform.right * Random.Range (-force, force));
			rigidbody2D.AddTorque (Random.Range (-torque, torque));
			applyOnce = true;
		}
	}

	void FixedUpdate()
	{
		if (transform.position.y < -7.0 && transform.position.y > -12.0)
		{
			rigidbody2D.AddForce (new Vector3(0.15f,0,0));
			
		}
	}

	void OnClean()
	{
		Destroy (gameObject, 2);
	}

}
