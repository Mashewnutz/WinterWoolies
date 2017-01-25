using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour 
{

	// Use this for initialization
	void Start () {
		Messenger.AddListener("sss", Update);	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
