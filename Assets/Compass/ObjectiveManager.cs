using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
	public GameObject player;
	public GameObject objectivePrefab;
	public float objectivesCount;
	public List<GameObject> objectives = new List<GameObject>();
	public GameObject closestObjective;

    void Start()
    {
        SpawnRandom(objectivePrefab);
    }

	public void UpdateClosestObjective()
	{	
		float shortestDistance = 1000;
		foreach (var objective in objectives)
		{
			float distance = Vector3.Distance(objective.transform.position, player.transform.position);
			if (distance < shortestDistance)
			{
				shortestDistance = distance;
				closestObjective = objective;
			}
		}
	}

	private void SpawnRandom(GameObject prefab)
	{
		for (int i = 0; i < objectivesCount; i++)
		{
			GameObject instance = Instantiate(prefab, new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10)), Quaternion.identity, transform);
			objectives.Add(instance);
		}
	}
}
