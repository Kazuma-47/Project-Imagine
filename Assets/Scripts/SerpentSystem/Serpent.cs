using UnityEngine;

public class Serpent : MonoBehaviour
{
    public SerpentBehaviour serpentbehaviour;

    public void AnimationEnded()
    {
        Destroy(gameObject);
        SpawnSerpent.Instance.SerpentAnimationOver();
    }
}
