using UnityEngine;
using TMPro;

public class LifeSystem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private int lives;

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
        livesText.text = "lives: " + lives;
    }
}
