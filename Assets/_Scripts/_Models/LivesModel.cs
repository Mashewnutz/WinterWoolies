using UnityEngine;
using System.Collections;

public class LivesModel : MonoBehaviour 
{
	public static string ARROW_LAUNCHED = "arrow.launched";
	public static string ARROW_BROKEN = "arrow.broken";
	public static string CLEAN = "arrow.clean";

	public static string COUNT_CHANGED = "arrow.countChanged";
	public static string OUT_OF_ARROWS = "arrow.outOfArrows";

	public int maxArrows = 6;
	public int arrows = 6;
	public int activeArrows = 0;

	void Awake()
	{
		Messenger.AddListener(ARROW_LAUNCHED, OnArrowLaunched);
		Messenger.AddListener(ARROW_BROKEN, OnArrowBroken);
		Messenger.AddListener(CLEAN, OnClean);
	}

	void OnArrowLaunched()
	{
		--arrows;
		++activeArrows;
		Messenger.Broadcast(COUNT_CHANGED, arrows);
	}

	void OnArrowBroken()
	{
		--activeArrows;
		if (arrows == 0 && activeArrows == 0)
		{
			Messenger.Broadcast(OUT_OF_ARROWS);
		}
	}

	void OnClean()
	{
		arrows = maxArrows;
		activeArrows = 0;
		Messenger.Broadcast(COUNT_CHANGED, arrows);
	}
}
