using UnityEngine;
using UnityEngine.Events;
public class Island : MonoBehaviour
{
    private int fishRequirenment;
    private UnityEvent OnCollision = new UnityEvent();

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {

        }
    }
}
