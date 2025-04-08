using System.Collections;
using UnityEditor;
using UnityEngine;

public class SpawnSerpent : MonoBehaviour
{
    public static SpawnSerpent Instance { get; private set; }
    private string[] animations = { "Attack", "Idle", "Roar" };

    [SerializeField] private GameObject Serpent;
    [SerializeField] private GameObject player;
    [SerializeField] private float radius;
    [SerializeField] private bool debug;
    private float spawnInterval;
    [HideInInspector] public bool SerpentActive;
    public bool CanAttack;
    [SerializeField] private float minSpawnDistance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    { 
        StartCoroutine(SpawnObjects()); 
    }

    private IEnumerator SpawnObjects()
    {
        while (true)
        {
            if (CanAttack && !SerpentActive)
            {
                SerpentActive = true;
                Vector3 spawnPosition = GetRandomPositionAroundPlayer();
                GameObject serpent = Instantiate(Serpent, spawnPosition, Quaternion.identity);
                serpent.GetComponent<Serpent>().serpentbehaviour.PlayAnimation(GetRandomAnimation());
                yield return null;
            }
            else
            {
                yield return null;
            }
        }
    }

    private Vector3 GetRandomPositionAroundPlayer()
    {
        // Only spawn in a cone in front of the player
        float angle = Random.Range(-90f, 90f); // Front half
        float distance = Random.Range(minSpawnDistance, radius); // Ensure it's not too close

        Quaternion rotation = Quaternion.Euler(0, angle, 0);
        Vector3 direction = rotation * player.transform.forward;

        Vector3 spawnOffset = direction.normalized * distance;
        Vector3 spawnPosition = player.transform.position + spawnOffset;
        spawnPosition.y = -5f;

        return spawnPosition;
    }

    public void SerpentAnimationOver()
    {
        SerpentActive = false;
        StartCoroutine(SpawnObjects());
    }

    private string GetRandomAnimation()
    {
        int index = Random.Range(0, animations.Length);
        return animations[index];
    }


    public void SetCanAttack(bool value) => CanAttack = value;

    private void OnDrawGizmos()
    {
        if (!debug || player == null) return;

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(player.transform.position, 0.3f); 

        Vector3 forwardOffset = player.transform.forward.normalized * 2f; 
        Vector3 spawnCenter = player.transform.position + forwardOffset;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(spawnCenter, radius); 

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(player.transform.position, spawnCenter);
    }

}
