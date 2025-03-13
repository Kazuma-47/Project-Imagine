using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [SerializeField] private float timerDurration;
    private float currentTimer;
    public bool isFinished;
    [SerializeField] private UnityEvent onFinshed = new();

    private void Start()
    {
        currentTimer = timerDurration;
    }

    private void Reset()
    {
        currentTimer = timerDurration;
        isFinished = false;
    }

    private void Update()
    {
        timerDurration -= Time.deltaTime;   
       if(timerDurration < 0 && !isFinished)
        {
            isFinished = true;
            onFinshed?.Invoke();
        }
    }
}
