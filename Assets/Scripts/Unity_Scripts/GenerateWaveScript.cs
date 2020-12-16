using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Generates the wave created by the player
/// </summary>
public class GenerateWaveScript : MonoBehaviour
{
    AudioSource source;

    float[] samples = new float[512];

    List<Vector2> points = new List<Vector2>();

    public UILineRenderer lineRenderer;

    float timeBetweenUpdate = 0.2f;
    float lastUpdateTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Attach to events
    /// </summary>
    private void OnEnable()
    {
        EventSystem.current.OnSolvedWave += DisableLineRenderer;
    }

    /// <summary>
    /// Detach from event
    /// </summary>
    private void OnDisable()
    {
        EventSystem.current.OnSolvedWave -= DisableLineRenderer;
    }

    // Update is called once per frame
    void Update()
    {
        if(timeBetweenUpdate + lastUpdateTime <= Time.time || lastUpdateTime == 0)
        {
            lastUpdateTime = Time.time;
            source.GetOutputData(samples, 1);
            CreatePointsList();

            lineRenderer.points = points;
            lineRenderer.SetVerticesDirty();
        }        
    }

    /// <summary>
    /// Turns a float array into a list of vector 2.
    /// 256 is taken away from the index because 0 in the line renderer is in the centre. Used to span the line across the screen
    /// </summary>
    /// <param name="samples">output data of the audio</param>
    /// <returns></returns>
    void CreatePointsList()
    {
        points.Clear();

        for(int i = 0; i < samples.Length; i++)
        {
            points.Add(new Vector2(i - 256, samples[i]));
        }
    }

    /// <summary>
    /// Sets the points of the line renderer to be null to stop it drawing
    /// </summary>
    void DisableLineRenderer()
    {
        lineRenderer.points = null;
        EventSystem.current.GenerateNewWave();
    }
}
