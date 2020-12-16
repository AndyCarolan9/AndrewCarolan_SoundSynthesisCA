using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script for renderering and recorded the target wave over a certain time. For the line renderer to constantly update the audio wave it needs the updated data.
/// However to get this, the audio needs to be playing. To avoid mixing two different audio waves, the values of the wave are recorded for a inital time. 
/// Then the stored waves are used to update the line renderer.
/// </summary>
public class GenerateTargetWave : MonoBehaviour
{
    AudioSource source;

    List<List<Vector2>> pointLists = new List<List<Vector2>>(); // the output data of the generated sound over a certain time

    float timeBetweenUpdate = 0.2f;
    float lastUpdateTime = 0;

    public UILineRenderer lineRenderer;

    int amountCaptured = 0; // amount of wave lists captured
    int max = 10; // max limit of wave lists to store

    int lastPointSet = 0; // currently displayed list

    bool captureWave = false; // used for events. 

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Subscribe to event
    /// </summary>
    private void OnEnable()
    {
        EventSystem.current.OnStartWaveCapture += StartWaveCapture;
    }

    /// <summary>
    /// Unsubscribe from event
    /// </summary>
    private void OnDisable()
    {
        EventSystem.current.OnStartWaveCapture -= StartWaveCapture;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeBetweenUpdate + lastUpdateTime <= Time.time || lastUpdateTime == 0) // reduce the amount of times the line renderer updates
        {
            lastUpdateTime = Time.time;

            if (amountCaptured >= max)
            {
                StartPlayerInput(); // sets the sliders to be active, if not already

                lineRenderer.points = pointLists[lastPointSet]; // update points for line renderer
                lineRenderer.SetVerticesDirty(); // make the line renderer update

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

                    source.GetOutputData(newSamples, 1); // Gets the output data of the audio. Frequency and amplitude
                    var points = CreatePointsList(newSamples); 

                    pointLists.Add(points);

                    lineRenderer.points = points;
                    lineRenderer.SetVerticesDirty();

                    amountCaptured++;
                }
            }
        }
    }

    /// <summary>
    /// Turns a float array into a list of vector 2.
    /// 256 is taken away from the index because 0 in the line renderer is in the centre. Used to span the line across the screen
    /// </summary>
    /// <param name="samples">output data of the audio</param>
    /// <returns></returns>
    List<Vector2> CreatePointsList(float[] samples)
    {
        List<Vector2> points = new List<Vector2>();

        for (int i = 0; i < samples.Length; i++)
        {
            points.Add(new Vector2(i - 256, samples[i]));
        }

        return points;
    }

    /// <summary>
    /// Event Method. Starts/restarts the wave capture.
    /// </summary>
    void StartWaveCapture()
    {
        captureWave = true;
        pointLists.Clear();
        amountCaptured = 0;
        lastPointSet = 0;
    }

    /// <summary>
    /// Wave has played long enough for the game to get accurate data
    /// Can allow the player to begin interactions
    /// </summary>
    void StartPlayerInput()
    {
        captureWave = false;
        EventSystem.current.StartPlayerInput();
    }
}
