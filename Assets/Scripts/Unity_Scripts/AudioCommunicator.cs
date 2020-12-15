using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioCommunicator : MonoBehaviour
{
    #region UI
    public Slider frequencySlider;
    public Text freqMinValue;
    public Text freqMaxValue;

    public Slider ampSlider;
    public Text ampMinValue;
    public Text ampMaxValue;

    public Text freqValue;
    public Text ampValue;
    #endregion

    #region Random Wave Variables
    public int randFreq;
    public int randAmp;
    public int soundToPlay;
    #endregion

    public bool allowInput = false;

    private CsoundUnity csound;

    private void Awake()
    {
        csound = GetComponent<CsoundUnity>();

        freqMinValue.text = frequencySlider.minValue.ToString();
        freqMaxValue.text = frequencySlider.maxValue.ToString();

        ampMinValue.text = ampSlider.minValue.ToString();
        ampMaxValue.text = ampSlider.maxValue.ToString();
    }

    private void OnEnable()
    {
        EventSystem.current.OnGenerateNewWave += SetupRandomVariables; // subscribe to event
        EventSystem.current.OnStartPlayerInput += AllowInput;
    }

    private void OnDisable()
    {
        EventSystem.current.OnGenerateNewWave -= SetupRandomVariables; // unsubscribe to event
        EventSystem.current.OnStartPlayerInput -= AllowInput;
    }

    // Update is called once per frame
    void Update()
    {
        if(randFreq == 0 || randAmp == 0)
        {
            SetupRandomVariables();
        }

        if(allowInput)
        {
            csound.setChannel("frequency", frequencySlider.value);
            csound.setChannel("amplitude", ampSlider.value);

            freqValue.text = frequencySlider.value.ToString();
            ampValue.text = ampSlider.value.ToString();
        }
        else
        {
            if (!csound.enabled) csound.enabled = true;

            csound.setChannel("sound", soundToPlay);
            csound.setChannel("frequency", randFreq);
            csound.setChannel("amplitude", randAmp);
        }
    }

    void SetupRandomVariables()
    {
        randFreq = (int)Random.Range(frequencySlider.minValue, frequencySlider.maxValue);
        randAmp = (int)Random.Range(ampSlider.minValue, ampSlider.maxValue);
        soundToPlay = Random.Range(0, 2); // set to 2 because it is an exclusive value. Random should return 0 or 1

        EventSystem.current.StartWaveCapture(); // capture the wave now that it is made
    }

    void AllowInput()
    {
        frequencySlider.enabled = true; // set to false in editor 
        ampSlider.enabled = true;
        allowInput = true;
    }
}
