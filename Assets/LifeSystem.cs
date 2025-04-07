using UnityEngine;
using TMPro;
using UnityEngine.Events;
public class LifeSystem : MonoBehaviour
{
    public static LifeSystem Instance { get; private set; }
    [SerializeField] private UnityEvent onGameOver = new();
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private int lives;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        UpdateLives();
    }

    public void TakeDamage()
    {
        lives--;
        UpdateLives();
    }

    public void UpdateLives()
    {
        livesText.text = "Lives: " + lives;
        if(lives == 0)
        {
            onGameOver?.Invoke();
        }
    }
}