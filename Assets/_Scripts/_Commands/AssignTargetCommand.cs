using UnityEngine;
using System.Collections;

public class AssignTargetCommand
{
	public static void Execute(Arrow arrow, Raindrop raindrop)
	{
		Game.GetObjectManager().SpawnTarget(arrow, raindrop);
		
		raindrop.taken = true;
		
		arrow.target = raindrop;
		arrow.isAiming = true;
	}
}
