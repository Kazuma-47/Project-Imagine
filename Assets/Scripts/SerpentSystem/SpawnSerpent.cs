using System.Collections;
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
        Vector3 spawnPosition = new Vector3(randomCircle.x, 0, randomCircle.y); 
        return player.transform.position + spawnPosition;
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
        if (debug)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(player.transform.position, radius);
        }
    }
}
