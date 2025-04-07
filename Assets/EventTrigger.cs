using UnityEngine;
using UnityEngine.Events;
public class EventTrigger : MonoBehaviour
{
    [SerializeField] private UnityEvent onTrigger = new();

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            onTrigger?.Invoke();
        }
    }
}
