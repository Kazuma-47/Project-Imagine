using System.Collections.Generic;
using UnityEngine;

public class FishnetSpawner : MonoBehaviour
{
    [SerializeField] private GameObject fishSpot;
    [SerializeField] private float radius;
    [SerializeField] int fishAmount;
    private List<GameObject> fishList = new();
    [SerializeField] private bool debug;

    private void Start()
    {
        SpawnRandomFish();
    }
    public void SpawnRandomFish()
    {
        for (int i = 0; i < fishAmount; i++)
        {
            Vector3 position = Random.insideUnitSphere * radius;
            position.y = 0;
            GameObject fish = Instantiate(fishSpot, position, Quaternion.identity, transform);
            fishList.Add(fish);
        }
    }

    public void RespawnFish()
    {
        for (int i = fishList.Count; i < fishAmount; i++)
        {
            Vector3 position = Random.insideUnitSphere * radius;
            position.y = 0;
            GameObject fish = Instantiate(fishSpot, position, Quaternion.identity, transform);
            fishList.Add(fish);
        }
    }

    public void RemoveFish(GameObject fish)
    {
        if (fishList.Contains(fish))
        {
            fishList.Remove(fish);
            Destroy(fish);
        }  
    }

    private void OnDrawGizmos()
    {
        if (debug)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}
