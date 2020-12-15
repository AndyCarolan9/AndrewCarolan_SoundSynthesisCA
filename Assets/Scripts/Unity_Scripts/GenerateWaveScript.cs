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

    float[] samples = new float[512];

    List<Vector2> points = new List<Vector2>();

    public UILineRenderer lineRenderer;

    float timeBetweenUpdate = 0.15f;
    float lastUpdateTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //GetSpectrumData();

        //MakeFrequencyBands();


        if(timeBetweenUpdate + lastUpdateTime <= Time.time || lastUpdateTime == 0)
        {
            lastUpdateTime = Time.time;
            GetClipData();
            CreatePointsList();

            lineRenderer.points = points;
            lineRenderer.SetVerticesDirty();
        }        
    }

    void GetClipData()
    {
        source.GetOutputData(samples, 1);
    }

    void CreatePointsList()
    {
        points.Clear();

        for(int i = 0; i < samples.Length; i++)
        {
            points.Add(new Vector2(i - 256, samples[i]));
        }
    }
}
