using UnityEngine;
using System.Collections;

public class ArrowHitTarget : MonoBehaviour 
{
	public static void Execute(Target target, Arrow arrow, float accuracy)
	{
		Game.GetObjectManager().SpawnRaindropExplode(target.raindrop.transform, accuracy > 0.0f);
		
		target.raindrop.Die();
		target.Die ();
		
		Raindrop raindrop = Game.GetSpawner().GetNextTarget(arrow.transform.position);
		AssignTargetCommand.Execute(arrow, raindrop);
		Messenger.Broadcast<float>(Game.ADD_SCORE, accuracy);

		if (accuracy == 0.0f)
		{
			Messenger.Broadcast(Messages.SHOUT, "SNOWDELICIOUS!");
		}
	}
}
