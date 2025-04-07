using System.Collections.Generic;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
	public List<FishnetSpawner> FishSpots = new List<FishnetSpawner>();
	public List<GameObject> islands = new List<GameObject>();
	private int currentIndex = 0;
	public GameObject closestObjective;

    void Start() => closestObjective = islands[currentIndex];

    public void NextObjective()
	{
		currentIndex++;
		closestObjective = islands[currentIndex];
        islands[currentIndex].GetComponent<Island>().ToggleActive();
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

	public void RefreshFishSpots()
	{
		foreach (FishnetSpawner spot in FishSpots)
			spot.RespawnFish();
	}
}
