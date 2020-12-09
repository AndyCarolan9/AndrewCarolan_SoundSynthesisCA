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
    #endregion

    private CsoundUnity csound;

    private void Awake()
    {
        csound = GetComponent<CsoundUnity>();

        freqMinValue.text = frequencySlider.minValue.ToString();
        freqMaxValue.text = frequencySlider.maxValue.ToString();

        ampMinValue.text = ampSlider.minValue.ToString();
        ampMaxValue.text = ampSlider.maxValue.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        csound.setChannel("frequency", frequencySlider.value);
        csound.setChannel("amplitude", ampSlider.value);
    }
}
