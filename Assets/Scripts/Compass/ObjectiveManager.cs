using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectiveManager : MonoBehaviour
{
	public List<FishnetSpawner> FishSpots = new List<FishnetSpawner>();
	public List<GameObject> islands = new List<GameObject>();
	[SerializeField] private UnityEvent onComplete = new();
	private int currentIndex = 0;
	public GameObject closestObjective;
	public GameObject tutorialBorder;

    void Start() => closestObjective = islands[currentIndex];

    public void NextObjective()
	{
		TutorialCheck();
        currentIndex++;
		closestObjective = islands[currentIndex];
		Island island = islands[currentIndex].GetComponent<Island>();
		if (island != null)
		{
			island.ToggleActive();
		}
    }
    public void OnCompletedCheck()
    {
        if (currentIndex >= islands.Count - 1)
        {
            onComplete?.Invoke();
        }
    }


    public void ObjectiveCheck(Island island)
	{
		bool isSold = InventoryManager.Instance.SellFish(island.GetFishRequirenment());
		if (isSold)
		{
            NextObjective();
			island.OnCompleted();
			RefreshFishSpots();
		}
	}
	public void TutorialCheck()
	{
		if(currentIndex >= 2)
		{
			if(tutorialBorder != null)
				Destroy(tutorialBorder);
		}
	}

	public void RefreshFishSpots()
	{
		foreach (FishnetSpawner spot in FishSpots)
			spot.RespawnFish();
	}
}
