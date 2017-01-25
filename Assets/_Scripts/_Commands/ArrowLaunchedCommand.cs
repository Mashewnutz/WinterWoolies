using UnityEngine;
using System.Collections;

public class ArrowLaunchedCommand : MonoBehaviour 
{
	public static void Execute(Arrow arrow)
	{
		Raindrop raindrop = Game.GetSpawner().GetNextTarget(arrow.gameObject.transform.position);
		AssignTargetCommand.Execute(arrow, raindrop);
		
		Messenger.Broadcast(LivesModel.ARROW_LAUNCHED);
		Messenger.Broadcast(Game.TRY_LAUNCH_ARROW);
	}

}
