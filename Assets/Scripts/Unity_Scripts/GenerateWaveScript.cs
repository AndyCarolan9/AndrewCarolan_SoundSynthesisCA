using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * https://www.youtube.com/watch?v=0KqwmcQqg0s&list=PL3POsQzaCw53p2tA6AWf7_AWgplskR0Vo&index=3
 */
public class GenerateWaveScript : MonoBehaviour
{
    AudioSource source;
    CsoundUnity csound;

    [SerializeField]
    public float amp = 5;

    [SerializeField]
    public float frequency = 10;

    public float[] samples = new float[512];

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        csound = GetComponent<CsoundUnity>();
    }

    // Update is called once per frame
    void Update()
    {
        GetSpectrumData();

        csound.setChannel("frequency", frequency);
        csound.setChannel("amplitude", amp);
    }

    void GetSpectrumData()
    {
        source.GetSpectrumData(samples, 0, FFTWindow.Blackman);
    }
}
