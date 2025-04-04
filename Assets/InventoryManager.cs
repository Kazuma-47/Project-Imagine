using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public int Fish {get; private set;}

    public void AddFish() => Fish++;

    public void SellFish(int fishAmmount) 
    {
        if (Fish >= fishAmmount)
        Fish -= fishAmmount;
    }

}
