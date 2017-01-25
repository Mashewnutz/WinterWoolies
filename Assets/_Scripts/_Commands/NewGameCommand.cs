using UnityEngine;
using System.Collections;

public class NewGameCommand
{
	public static void Execute(int level, Game.GameType gameType)
	{
		Messenger.Broadcast(Messages.CLEAN);
		Messenger.Broadcast(QpidTapText.CLEAN);
		Messenger.Broadcast(Qpid.CLEAN);
		Messenger.Broadcast(LivesModel.CLEAN);
		Messenger.Broadcast(ScoreModel.CLEAN);
		Messenger.Broadcast(OwnCamera.SHOW_LEVEL_SCENE);

		Game.Instance().SetLevel(level, gameType);
	}
}
