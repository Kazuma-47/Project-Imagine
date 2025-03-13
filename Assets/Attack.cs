using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private GameObject activeAsset;
    private bool active;
    private Collider target;

    public void ToggleActive()
    {
        active = !active;
        activeAsset.SetActive(active);
        if (target != null)
            CheckHit(target);
    }
    
    public void DeleteAttack()
    {
        Destroy(gameObject);
    }
    private void CheckHit(Collider objects)
    {
        
        if (objects.CompareTag("Player"))
        {
            if (active)
            {
                objects.GetComponent<LifeSystem>().TakeDamage();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        target = other; 
        CheckHit(target);
    }

    private void OnTriggerExit(Collider other)
    {
        target = null;
    }
}
