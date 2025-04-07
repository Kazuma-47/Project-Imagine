using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class Island : MonoBehaviour
{
    [SerializeField] private int fishRequirenment;
    [SerializeField] private bool active;
    [SerializeField] private UnityEvent<Island> OnCollision = new();
    [SerializeField] private TextMeshProUGUI islandText;
    [SerializeField] private GameObject islandRequirementObject;
    [SerializeField] private bool tutorialIsland;

    private void Start()
    {
        islandText.text = "Fish Required: " + fishRequirenment.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (active)
        {
            if (other.transform.CompareTag("Player"))
            {
                OnCollision?.Invoke(this);
                if (!tutorialIsland)
                {
                    SpawnSerpent.Instance.SetCanAttack(false);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (active)
        {
            if (other.transform.CompareTag("Player"))
            {
                OnCollision?.Invoke(this);
                if (!tutorialIsland)
                {
                    SpawnSerpent.Instance.SetCanAttack(true);
                }
                
            }
        }
    }

    public void ToggleActive() => active = !active;
    public int GetFishRequirenment()
    {
        return fishRequirenment;
    }

    public void OnCompleted()
    {
        active = !active;
    }
    private void Update()
    {
        if (active)
        {
            islandRequirementObject.SetActive(true);
        }
        else
        {
            islandRequirementObject.SetActive(false);
        }
    }
}
