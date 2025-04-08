using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    private Coroutine queueCoroutine;
    [SerializeField] private AudioClip audioClip;

    private void Start()
    {
        if(audioClip != null)
        {
            PlayTrackImmediate(audioClip);
        }
    }
    public void PlayTrackImmediate(AudioClip newTrack)
    {
        if (newTrack == null)
        {
            print("Tried to play null track.");
            return;
        }

        if (queueCoroutine != null)
            StopCoroutine(queueCoroutine);

        audioSource.Stop();
        audioSource.clip = newTrack;
        audioSource.loop = true;
        audioSource.Play();
    }
    public void PlayTrackAfterCurrent(AudioClip newTrack)
    {
        if (newTrack == null)
        {
            print("Tried to queue null track.");
            return;
        }

        if (queueCoroutine != null)
            StopCoroutine(queueCoroutine);

        queueCoroutine = StartCoroutine(QueueTrack(newTrack));
    }

    private IEnumerator QueueTrack(AudioClip newTrack)
    {
        if (!audioSource.isPlaying || audioSource.clip == null)
        {
            PlayTrackImmediate(newTrack);
            yield break;
        }

        audioSource.loop = false;

        while (audioSource.isPlaying)
        {
            yield return null;
        }

        PlayTrackImmediate(newTrack);
    }

    public void StopMusic()
    {
        if (queueCoroutine != null)
            StopCoroutine(queueCoroutine);

        audioSource.Stop();
        audioSource.clip = null;
    }
}

