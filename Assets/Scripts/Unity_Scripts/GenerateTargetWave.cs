using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateTargetWave : MonoBehaviour
{
    AudioSource source;

    List<List<Vector2>> pointLists = new List<List<Vector2>>(); // the output data of the generated sound over a certain time

    float timeBetweenUpdate = 0.15f;
    float lastUpdateTime = 0;

    public UILineRenderer lineRenderer;

    int index = 0;
    int max = 10;

    int lastPointSet = 0;

    bool captureWave = false;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        EventSystem.current.OnStartWaveCapture += StartWaveCapture;
    }

    private void OnDisable()
    {
        EventSystem.current.OnStartWaveCapture -= StartWaveCapture;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeBetweenUpdate + lastUpdateTime <= Time.time || lastUpdateTime == 0)
        {
            lastUpdateTime = Time.time;

            if (index >= max)
            {
                StartPlayerInput();

                lineRenderer.points = pointLists[lastPointSet];
                lineRenderer.SetVerticesDirty();

                lastPointSet++;

                if(lastPointSet >= pointLists.Count)
                {
                    lastPointSet = 0;
                }
            }
            else
            {
                if(captureWave)
                {
                    float[] newSamples = new float[512];

                    source.GetOutputData(newSamples, 1);
                    var points = CreatePointsList(newSamples);

                    pointLists.Add(points);

                    lineRenderer.points = points;
                    lineRenderer.SetVerticesDirty();

                    index++;
                }
            }
        }
    }

    List<Vector2> CreatePointsList(float[] samples)
    {
        List<Vector2> points = new List<Vector2>();

        for (int i = 0; i < samples.Length; i++)
        {
            points.Add(new Vector2(i - 256, samples[i]));
        }

        return points;
    }

    void StartWaveCapture()
    {
        captureWave = true;
        pointLists.Clear();
    }

    void StartPlayerInput()
    {
        captureWave = false;
        EventSystem.current.StartPlayerInput();
    }
}
