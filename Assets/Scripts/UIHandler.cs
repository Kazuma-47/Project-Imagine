using UnityEngine;
using TMPro;

public class UIHandler : MonoBehaviour
{
    public static UIHandler Instance { get; private set; }
    [SerializeField] private TextMeshProUGUI fishScore;

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
    }

    private void Start() => UpdateUI();

    public void UpdateUI()
    {
        fishScore.text = "Caught fish: " + InventoryManager.Instance.Fish;
    }
}
