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

    private void Start()
    {
        islandText.text = "Fish Required: " + fishRequirenment.ToString();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (active)
        {
            if (collision.transform.CompareTag("Player"))
            {
                OnCollision?.Invoke(this);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (active)
        {
            if (other.transform.CompareTag("Player"))
            {
                print("player safe");
                SpawnSerpent.Instance.SetCanAttack(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (active)
        {
            if (other.transform.CompareTag("Player"))
            {
                print("player in danger");
                SpawnSerpent.Instance.SetCanAttack(true);
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
