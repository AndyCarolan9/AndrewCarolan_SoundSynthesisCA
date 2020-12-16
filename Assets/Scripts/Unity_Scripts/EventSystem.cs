using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystem : MonoBehaviour
{
    public static EventSystem current;

    private void Awake()
    {
        current = this;
    }

    public event Action OnGenerateNewWave; // calls function to create new random wave
    public event Action OnStartWaveCapture; // starts the playing and capture of target wave data
    public event Action OnStartPlayerInput; // Allow the player to input data
    public event Action OnSolvedWave;

    public void GenerateNewWave()
    {
        OnGenerateNewWave?.Invoke();
    }

    public void StartWaveCapture()
    {
        OnStartWaveCapture?.Invoke();
    }

    public void StartPlayerInput()
    {
        OnStartPlayerInput?.Invoke();
    }

    public void SolvedWave()
    {
        OnSolvedWave?.Invoke();
    }
}
