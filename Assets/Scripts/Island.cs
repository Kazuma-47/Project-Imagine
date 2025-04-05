using UnityEngine;
using UnityEngine.Events;
public class Island : MonoBehaviour
{
    [SerializeField] private int fishRequirenment;
    [SerializeField] private bool active;
    [SerializeField] private UnityEvent<Island> OnCollision = new();

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

    public int GetFishRequirenment()
    {
        return fishRequirenment;
    }

    public void OnCompleted()
    {
        active = !active;
    }
}
