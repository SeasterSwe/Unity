using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeirdLine : MonoBehaviour
{
    [Header("IdelWave")]
    public float forceDivider = 1.4f;
    public float waveSpeed = 1.2f;
    public float waveFrequancy = 20;
    public float waveDamper = 20;

    [Header("NoiseForIdelWave")]
    public float noiseStreangth = 1f;
    public float noiseWalk = 1f;
    public float noiseMoveSpeed = 1f;

    public int lengthOfLineRenderer = 40;
    void Start()
    {
        LineRenderer lineRenderer = gameObject.GetComponent<LineRenderer>();
        lineRenderer.SetVertexCount(lengthOfLineRenderer);
    }
    void Update()
    {
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        int i = 0;
        while (i < lengthOfLineRenderer)
        {
            float offset = Mathf.Sin(Mathf.Sin((Time.time * waveSpeed) + (i * waveFrequancy)) / waveDamper);
            offset += Mathf.PerlinNoise(transform.position.x * noiseWalk + Time.time * noiseMoveSpeed, Mathf.Sin(Time.time * 0.1f)) * noiseStreangth;
            
            Vector3 pos = new Vector3(transform.position.x + i * 1f, transform.position.y + offset, 0);
            lineRenderer.SetPosition(i, pos);
            i++;
        }
    }
}
