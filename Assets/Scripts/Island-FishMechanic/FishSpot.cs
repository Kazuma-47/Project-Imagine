using UnityEngine;

public class FishSpot : MonoBehaviour
{
    private FishnetSpawner spawner;

    private void Awake()
    {
        spawner = GetComponentInParent<FishnetSpawner>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            InventoryManager.Instance.AddFish();
            UIHandler.Instance.UpdateUI();
            spawner.RemoveFish(gameObject);
        }    
    }
}
