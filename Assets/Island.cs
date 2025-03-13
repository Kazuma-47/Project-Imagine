using UnityEngine;
using UnityEngine.Events;

public class Island : MonoBehaviour
{
    [SerializeField] private UnityEvent<GameObject> unityEvent = new();

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            unityEvent?.Invoke(gameObject);
        }
    }

    public void Removed()
    {
     Destroy(gameObject);
    }
}
