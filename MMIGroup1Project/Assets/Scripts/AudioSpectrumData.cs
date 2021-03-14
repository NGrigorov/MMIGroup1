using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioListener))]
public class AudioSpectrumData : MonoBehaviour
{
    private GameObject thesiaObj;
    private Material material;

    //private List<Vector3> pos = new List<Vector3>();
    private const int SAMPLES = 256;
    private float[] freqAmps = new float[SAMPLES * 4];
    private int avgIndex = 0;

    void Start()
    {
        thesiaObj = GameObject.Find("Thesia");
        material = thesiaObj.GetComponent<Renderer>().material;
    }

    void Update()
    {
        float[] spectrum = new float[SAMPLES * 8];

        AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);

        float freqResolution = 24000.0f / (SAMPLES * 8);

        float maxAmp = 0;

        //pos.Clear();
        for (int i = 0; i < SAMPLES; i++)
        {
            float freq = i * freqResolution;
            float freqAmp = spectrum[i];
            if (freqAmp > maxAmp) maxAmp = freqAmp;

            //pos.Add(new Vector3(i / 64.0f - 1, -1, 0));
            //pos.Add(new Vector3(i / 64.0f - 1, -1 + freqAmp * 5, 0));
            //pos.Add(new Vector3(0, 0.6f + freqAmp * 3, 0));
            freqAmps[avgIndex * SAMPLES + i] = freqAmp;
        }
        avgIndex++;
        if (avgIndex == 4) avgIndex = 0;

        //lineRenderer.startWidth = 0.02f;
        //lineRenderer.endWidth = 0.02f;
        //lineRenderer.SetPositions(pos.ToArray());
        //lineRenderer.useWorldSpace = true;
        //lineRenderer.positionCount = pos.ToArray().Length;
        //lineRenderer.loop = false;

        //thesiaObj.transform.position = new Vector3(0, maxAmp * 10, 0);
    }

    void OnPostRender()
    {
        Color darkGreen = new Color(0, 0.5f, 0, 1);

        GL.PushMatrix();
        material.SetPass(0);
        GL.LoadIdentity();

        GL.Begin(GL.QUADS);
        
        GL.Color(darkGreen);
        float rot = 0;
        for (int i = 0; i < SAMPLES; i++)
        {
            float amp = 0;
            for (int a = 0; a < 4; a++)
            {
                amp += 0.6f + Mathf.Min(freqAmps[a * SAMPLES + i] * 3, 0.5f);// Mathf.Max(0, Mathf.Log(freqAmps[a * SAMPLES + i] * 3) + 1);
            }
            amp /= 4;
            //amp = Mathf.Log(amp) + 1;

            float x = amp * Mathf.Cos(rot);
            float y = amp * Mathf.Sin(rot);

            GL.Vertex3(0.5f * Mathf.Cos(rot), 0.5f * Mathf.Sin(rot), 0);
            GL.Vertex3(x, y, 0);

            rot -= (2 * Mathf.PI) / SAMPLES;

            x = amp * Mathf.Cos(rot);
            y = amp * Mathf.Sin(rot);
            
            GL.Vertex3(x, y, 0);
            GL.Vertex3(0.5f * Mathf.Cos(rot), 0.5f * Mathf.Sin(rot), 0);
        }

        GL.End();

        GL.PopMatrix();
    }
}
