using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour 
{
	public static string TRY_LAUNCH_ARROW = "Game.TryLaunchArrow";
	public static string ADD_SCORE = "Game.AddScore";
	
	public enum GameType
	{
		AutoAim,
		ManualAim
	};

	public GameObject snowflakePrefab;

	public int minimumArrows = 3;
	public GameType gameType = GameType.AutoAim;

	public LivesModel livesModel;

	private static Game instance;
	public static Game Instance()
	{
		return instance;
	}

	public static RaindropSpawner spawner;
	public static RaindropSpawner GetSpawner()
	{
		return spawner;
	}

	private static ObjectManager objectManager;
	public static ObjectManager GetObjectManager()
	{
		return objectManager;
	}
	
	void Awake()
	{
		instance = this;
		objectManager = GetComponent<ObjectManager>();

		livesModel = FindObjectOfType(typeof(LivesModel)) as LivesModel;
		spawner = FindObjectOfType(typeof(RaindropSpawner)) as RaindropSpawner;

		Messenger.AddListener<Arrow>(Qpid.ARROW_LAUNCHED, ArrowLaunchedCommand.Execute);
		Messenger.AddListener<Target, Arrow, float>(Target.ARROW_HIT_TARGET, ArrowHitTarget.Execute);
		Messenger.AddListener<Target, Arrow, float>(Target.ARROW_MISSED_TARGET, ArrowMissedTarget.Execute);
		Messenger.AddListener<int, GameType>(PlayButton.GAME_START, NewGameCommand.Execute);

		Messenger.AddListener(Qpid.RELOADED, TryLaunchingArrow);
		Messenger.AddListener(LivesModel.OUT_OF_ARROWS, OnGameOver);
		Messenger.AddListener(Game.TRY_LAUNCH_ARROW, TryLaunchingArrow);
		Messenger.AddListener<float>(Game.ADD_SCORE, OnAddScore);
	}

	void OnGameOver()
	{
		Messenger.Broadcast(OwnCamera.SHOW_END_SCENE);
	}

	void TryLaunchingArrow()
	{
		if (livesModel.activeArrows < minimumArrows && livesModel.arrows > 0)
		{
			Messenger.Broadcast(Qpid.AUTO_LAUNCH);
		}
	}

	void OnAddScore(float accuracy)
	{
		int value = (accuracy == 0 ? 200 : (int)(1 - accuracy) * 100);
		if (livesModel.activeArrows == 3)
			value *= 15;
		else if (livesModel.activeArrows == 2)
			value *= 5;

		Messenger.Broadcast(ScoreModel.ADD, value);
	}

	public void SetLevel(int level, GameType gameType)
	{
		minimumArrows = level;
		this.gameType = gameType;
	}
}
