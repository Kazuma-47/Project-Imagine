using System.Collections;
using UnityEngine;

public class SpawnAttacks : MonoBehaviour
{
    [SerializeField] private GameObject objectToSpawn; // Prefab to spawn
    [SerializeField] private float spawnRadius = 5f; // How far from the player to spawn
    [SerializeField] private float spawnInterval = 2f; // How often to spawn

    private void Start()
    {
        StartCoroutine(SpawnObjects());
    }

    private IEnumerator SpawnObjects()
    {
        while (true)
        {
            Vector3 spawnPosition = GetRandomPositionAroundPlayer();
            Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private Vector3 GetRandomPositionAroundPlayer()
    {
        Vector2 randomCircle = Random.insideUnitCircle * spawnRadius;
        Vector3 spawnPosition = new Vector3(randomCircle.x, 0, randomCircle.y); // Assuming a flat plane (XZ)
        return transform.position + spawnPosition;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}
