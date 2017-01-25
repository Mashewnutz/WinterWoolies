using UnityEngine;
using System.Collections;

public class PlayButton : MonoBehaviour 
{
	public static string GAME_START = "Play.Button";
	public int level = 1;
	public Game.GameType gameType;
	
	void OnTapDown() {
		Messenger.Broadcast (GAME_START, level, gameType);
	}
}
