using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioCommunicator : MonoBehaviour
{
    public Slider frequencySlider;
    public Slider ampSlider;

    private CsoundUnity csound;

    private void Awake()
    {
        csound = GetComponent<CsoundUnity>();
    }

    // Update is called once per frame
    void Update()
    {
        csound.setChannel("frequency", frequencySlider.value);
        csound.setChannel("amplitude", ampSlider.value);
    }
}
