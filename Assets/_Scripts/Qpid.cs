using UnityEngine;
using System.Collections;

public class Qpid : MonoBehaviour 
{
	public static string ARROW_LAUNCHED = "qpid.arrowLaunched";
	public static string RELOADED = "qpid.reloaded";
	public static string AUTO_LAUNCH = "qpid.autoLaunch";
	public static string CLEAN = "qpid.clean";

	public Transform launchPosition;

	public float floatSpeed = 5.0f;
	public float floatDistance = 5.0f;

	public bool triggeredByPlayer = false;
	public bool readyToShoot = true;

	private Vector3 startPosition;

	void Awake()
	{
		Messenger.AddListener(AUTO_LAUNCH, TryShooting);
		Messenger.AddListener(CLEAN, OnClean);
	}

	void Start()
	{
		startPosition = transform.position;
	}

	void OnTapDown()
	{
		if (!triggeredByPlayer)
		{
			TryShooting();
		}
	}

	void TryShooting()
	{
		Animator animator = GetComponent<Animator>();
		if (readyToShoot)
		{
			readyToShoot = false;
			triggeredByPlayer = true;
			animator.SetBool("Shoot", true);
			
			StartCoroutine(Reload ());
		}
	}
	
	void Update()
	{
		float offset = Mathf.Cos (Time.time * floatSpeed) * floatDistance;
		Vector3 newPosition = startPosition + Vector3.up * offset;
		transform.position = newPosition;
	}

	void OnShoot()
	{
		Animator animator = GetComponent<Animator>();
		animator.SetBool("Shoot", false);

		Arrow arrow = Game.GetObjectManager().SpawnArrow(launchPosition);
		Messenger.Broadcast<Arrow>(ARROW_LAUNCHED, arrow);

		Messenger.Broadcast(QpidTapText.HIDE);
	}

	void OnClean()
	{
		triggeredByPlayer = false;
	}

	IEnumerator Reload()
	{
		yield return new WaitForSeconds(0.5f);
		readyToShoot = true;
		Messenger.Broadcast(RELOADED);
	}
}
