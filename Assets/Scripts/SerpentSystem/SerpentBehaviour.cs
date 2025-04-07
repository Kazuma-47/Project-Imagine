using System;
using UnityEngine;

public class SerpentBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject parentObject;
    [SerializeField] private Animator animator;

    public void PlayAnimation(string AnimationName)
    {
        switch (AnimationName)
        {
            case "Attack":
                animator.SetBool("Attack", true);
                break;
            case "Idle":
                animator.SetBool("Idle", true);
                break;
            case "Roar":
                animator.SetBool("Roar", true);
                break;
            default:
                break;
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            LifeSystem.Instance.TakeDamage();
        }
    }
}
