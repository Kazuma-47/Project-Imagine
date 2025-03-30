using System.Collections.Generic;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
	public GameObject player;
	public GameObject objectivePrefab;
	public float FishSpotCount;
	public List<GameObject> FishSpots = new List<GameObject>();
	public List<GameObject> islands = new List<GameObject>();
	private int currentIndex = 0;
	public GameObject closestObjective;

    void Start()
    {
        SpawnRandom(objectivePrefab);
		closestObjective = islands[currentIndex];
    }

	public void GetNextIsland()
	{
		currentIndex++;
		closestObjective = islands[currentIndex];
	}

	private void SpawnRandom(GameObject prefab)
	{
		for (int i = 0; i < FishSpotCount; i++)
		{
			GameObject instance = Instantiate(prefab, new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10)), Quaternion.identity, transform);
			FishSpots.Add(instance);
		}
	}
}
