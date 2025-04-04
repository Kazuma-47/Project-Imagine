using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }
    public int Fish { get; private set; }

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

    public void AddFish() => Fish++;

    public bool SellFish(int fishAmount)
    {
        if (Fish >= fishAmount)
        {
            Fish -= fishAmount;
            return true;
        }
        else
        {
            return false;
        }
    }
}
