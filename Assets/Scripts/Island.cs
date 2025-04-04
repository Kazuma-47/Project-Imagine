using UnityEngine;
using UnityEngine.Events;
public class Island : MonoBehaviour
{
    private int fishRequirenment;
    private UnityEvent <int> OnCollision = new UnityEvent<int>();

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            OnCollision?.Invoke(fishRequirenment);
        }
    }
}
