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
        StartCoroutine(TimerCoroutine()); 
    }

    private IEnumerator TimerCoroutine()
    {
        while (true)
        {
            if (CanAttack && !SerpentActive)
            {
                float spawnInterval = GetRandomInterval();
                yield return new WaitForSeconds(spawnInterval);

                if (!SerpentActive && CanAttack)
                {
                    StartCoroutine(SpawnObjects());
                    SerpentActive = true;
                }
            }
            else
            {
                yield return null; 
            }
        }
    }

    private IEnumerator SpawnObjects()
    {
            Vector3 spawnPosition = GetRandomPositionAroundPlayer();
            GameObject serpent = Instantiate(Serpent, spawnPosition, Quaternion.identity);
            serpent.GetComponent<Serpent>().serpentbehaviour.PlayAnimation(GetRandomAnimation());
            yield return null;
    }

    private Vector3 GetRandomPositionAroundPlayer()
    {
        Vector2 randomCircle = Random.insideUnitCircle * radius;
        Vector3 offset = new Vector3(randomCircle.x, 0, randomCircle.y);

        Vector3 forwardOffset = player.transform.forward.normalized * 2f;

        Vector3 spawnPosition = player.transform.position + offset + forwardOffset;
        spawnPosition.y = 0; 
        return spawnPosition;
    }

    public void SerpentAnimationOver()
    {
        SerpentActive = false;
    }

    private string GetRandomAnimation()
    {
        int index = Random.Range(0, animations.Length);
        return animations[index];
    }
    
    private float GetRandomInterval()
    {
        return Random.Range(7f, 10f);
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
