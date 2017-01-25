using UnityEngine;
using System.Collections;

public class OwnCamera : MonoBehaviour 
{
	public static string SHOW_MAIN_SCENE = "CameraController.ShowMainScene";
	public static string SHOW_LEVEL_SCENE = "CameraController.ShowLevelScene";
	public static string SHOW_END_SCENE = "CameraController.ShowEndScene";

	public Transform titleScenePosition;
	public Transform levelScenePosition;
	public Transform endScenePosition;
	public Vector3 offset = new Vector3(0, 0, -10);
	
	public float transitionTime = 1.0f;
	
	private Vector3 startPosition;
	private Vector3 targetPosition;
	private float transitionProgress = 1.0f;
	
	public void GoToMainScene()
	{
		MoveTo(titleScenePosition.position + offset);
	}
	
	public void GoToLevelScene()
	{
		MoveTo(levelScenePosition.position + offset);
	}
	
	public void GoToEndScene()
	{
		MoveTo(endScenePosition.position + offset);
	}
	
	void Awake()
	{
		Messenger.AddListener(SHOW_MAIN_SCENE, GoToMainScene);
		Messenger.AddListener(SHOW_LEVEL_SCENE, GoToLevelScene);
		Messenger.AddListener(SHOW_END_SCENE, GoToEndScene);
		GoToMainScene();
	}
	
	void Start()
	{
		startPosition = titleScenePosition.position + offset;
		targetPosition = titleScenePosition.position + offset;
	}
	
	void Update () 
	{
		if (transitionProgress < 1.0f)
		{
			transitionProgress += Time.deltaTime / transitionTime;
			transform.position = Vector3.Lerp(startPosition, targetPosition, transitionProgress);
		}
	}
	
	void MoveTo(Vector3 position)
	{
		startPosition = transform.position;
		targetPosition = position;
		transitionProgress = 0.0f;
	}
}
