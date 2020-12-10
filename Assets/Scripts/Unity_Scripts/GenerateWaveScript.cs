using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/*
 * https://www.youtube.com/watch?v=0KqwmcQqg0s&list=PL3POsQzaCw53p2tA6AWf7_AWgplskR0Vo&index=3
 */
public class GenerateWaveScript : MonoBehaviour
{
    AudioSource source;

    public float[] samples = new float[512];

    public float[] freqBand;

    public Image[] freqBand_Images;

    public float startScale = 1;
    public float scaleMultiplier = 10;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();

        freqBand = new float[freqBand_Images.Length];
    }

    // Update is called once per frame
    void Update()
    {
        //GetSpectrumData();

        //MakeFrequencyBands();

        GetClipData();
    }

    void GetClipData()
    {
        source.GetOutputData(samples, 1);
    }

    void GetSpectrumData()
    {
        source.GetSpectrumData(samples, 0, FFTWindow.Blackman);
    }

    void MakeFrequencyBands()
    {
        int count = 0;

        for(int i = 0; i < freqBand.Length; i++)
        {
            float average = 0;
            int sampleCount = (int)Mathf.Pow(2, i) * 2;

            if(i == 7)
            {
                sampleCount += 2;
            }

            for(int j = 0; j < sampleCount; j++)
            {
                average += samples[count] * (count + 1);
                count++;
            }

            average /= count;

            freqBand[i] = average * 10;
        }

        for (int k = 0; k < freqBand_Images.Length; k++)
        {
            freqBand_Images[k].transform.localScale = new Vector3(
                freqBand_Images[k].transform.localScale.x,
                (freqBand[k] * scaleMultiplier) + startScale,
                freqBand_Images[k].transform.localScale.x);

            Debug.Log(freqBand_Images[k].transform.localScale);
        }
    }
}
