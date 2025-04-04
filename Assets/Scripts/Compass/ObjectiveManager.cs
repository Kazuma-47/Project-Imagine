using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ObjectiveManager : MonoBehaviour
{
	public GameObject player;
	public GameObject objectivePrefab;
	public List<GameObject> FishSpots = new List<GameObject>();
	public List<GameObject> islands = new List<GameObject>();
	private int currentIndex = 0;
	public GameObject closestObjective;

    void Start()
    {
		closestObjective = islands[currentIndex];
	}

	public void NextObjective()
	{
		currentIndex++;
		closestObjective = islands[currentIndex];
	}

	public void ObjectiveCheck(Island island)
	{
		bool isSold = InventoryManager.Instance.SellFish(island.GetFishRequirenment());
		if (isSold)
		{
            NextObjective();
			island.OnCompleted();
        }
		

	}
}
