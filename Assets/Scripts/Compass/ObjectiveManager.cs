using System.Collections.Generic;
using UnityEngine;

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
		//closestObjective = islands[currentIndex];
    }

	public void GetNextIsland()
	{
		currentIndex++;
		closestObjective = islands[currentIndex];
	}
}
