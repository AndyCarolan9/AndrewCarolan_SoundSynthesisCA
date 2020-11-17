using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCsoundScript : MonoBehaviour
{
    [SerializeField]
    public float amp = 5;

    [SerializeField]
    public float frequency = 10;

    private CsoundUnity csound;

    // Start is called before the first frame update
    void Start()
    {
        csound = GetComponent<CsoundUnity>();
    }

    // Update is called once per frame
    void Update()
    {
        csound.setChannel("frequency", frequency);   
        csound.setChannel("amplitude", amp);   
    }
}
