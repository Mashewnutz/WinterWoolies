using UnityEngine;
using System.Collections;

public class ArrowMissedTarget : MonoBehaviour 
{
	public static void Execute(Target target, Arrow arrow, float accuracy)
	{
		target.raindrop.taken = false;
		
		arrow.Die ();
		target.Die ();

		Messenger.Broadcast (Messages.SAY, accuracy >= 1 ? "WAY TOO SOON" : "YOU FORGOT TO TAP");
		Messenger.Broadcast (Game.TRY_LAUNCH_ARROW);
	}
}
