using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioListener))]
public class AudioSpectrumData : MonoBehaviour
{
    private GameObject thesiaObj;
    private Material material;

    // Number of frequency samples
    public const int SAMPLES = 64;

    private const int smoothingFactor = 4;
    private float[] freqAmps = new float[SAMPLES * smoothingFactor];
    private int avgIndex = 0;
    private float maxAmp = 0;

    void Start()
    {
        thesiaObj = GameObject.Find("Thesia");
        material = thesiaObj.GetComponent<Renderer>().material;
    }

    void Update()
    {
        maxAmp = 0;
        float[] spectrum = new float[SAMPLES * 8];

        AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);

        float freqResolution = 24000.0f / (SAMPLES * 8);

        for (int i = 0; i < SAMPLES; i++)
        {
            float freq = i * freqResolution;
            float freqAmp = spectrum[i];
            if (freqAmp > maxAmp) maxAmp = freqAmp;

            freqAmps[avgIndex * SAMPLES + i] = freqAmp;
        }
        avgIndex++;
        if (avgIndex == smoothingFactor) avgIndex = 0;
    }

    private void RenderCircle(float x, float y, float radius, Color color)
    {
        GL.Begin(GL.TRIANGLES);
        GL.Color(color);
        for (int i = 0; i < 32; i++)
        {
            float rot1 = (i / 32.0f) * (Mathf.PI * 2);
            float rot2 = ((i+1) / 32.0f) * (Mathf.PI * 2);
            GL.Vertex3(x, y, 0);
            GL.Vertex3(x + radius * Mathf.Cos(-rot1), y + radius * Mathf.Sin(-rot1), 0);
            GL.Vertex3(x + radius * Mathf.Cos(-rot2), y + radius * Mathf.Sin(-rot2), 0);
        }
        GL.End();
    }

    private void RenderStraight(float x1, float x2, float y1, float y2)
    {
        GL.TexCoord2(0, 0);
        GL.Vertex3(x1, y1, 0);
        GL.TexCoord2(0, 1);
        GL.Vertex3(x1, y2, 0);

        GL.TexCoord2(1, 1);
        GL.Vertex3(x2, y2, 0);
        GL.TexCoord2(1, 0);
        GL.Vertex3(x2, y1, 0);
    }

    private void RenderCurved(float x1, float x2, float y1, float y2, float amp, float range)
    {
        float offset = Mathf.PI * 0.5f + range * 0.5f;

        x1 = -x1 * range + offset;
        x2 = -x2 * range + offset;

        float radius = 1.2f;

        float lowRadius = Mathf.Max(radius - amp, 0.5f);

        GL.TexCoord2(0, 0);
        GL.Vertex3(lowRadius * Mathf.Cos(x1), lowRadius * Mathf.Sin(x1), 0);
        GL.TexCoord2(0, 1);
        GL.Vertex3((radius + amp) * Mathf.Cos(x1), (radius + amp) * Mathf.Sin(x1), 0);

        GL.TexCoord2(1, 1);
        GL.Vertex3((radius + amp) * Mathf.Cos(x2), (radius + amp) * Mathf.Sin(x2), 0);
        GL.TexCoord2(1, 0);
        GL.Vertex3(lowRadius * Mathf.Cos(x2), lowRadius * Mathf.Sin(x2), 0);
    }

    private void RenderFrequency()
    {
        Color darkGreen = new Color(0, 0.5f, 0, 1);

        GL.PushMatrix();
        material.SetPass(0);
        GL.LoadIdentity();
        float aspect = (float) Screen.width / Screen.height;
        var proj = Matrix4x4.Ortho(0, 10, 0, 10, -1, 100);
        GL.LoadProjectionMatrix(proj);

        GL.PushMatrix();
        GL.LoadIdentity();
        var model = Matrix4x4.Translate(new Vector3(3, 3, 0));
        GL.MultMatrix(model);

        RenderCircle(0, 0, 2, new Color(34 / 255.0f, 34 / 255.0f, 59 / 255.0f));
        RenderCircle(0, 0, maxAmp, new Color(72 / 255.0f, 191 / 255.0f, 227 / 255.0f));

        GL.Begin(GL.QUADS);

        float div = SAMPLES;
        float offset = 0.2f / SAMPLES;

        for (int i = 0; i < SAMPLES; i++)
        {
            // Compute moving average amplitude of frequency samples
            float amp = 0;
            for (int a = 0; a < smoothingFactor; a++)
            {
                amp += Mathf.Min(freqAmps[a * SAMPLES + i] * 10 + 0.1f, 1.0f);// Mathf.Max(0, Mathf.Log(freqAmps[a * SAMPLES + i] * 3) + 1);
            }
            amp /= smoothingFactor;

            float x1 = (i / div) + offset;
            float x2 = ((i + 1) / div) - offset;
            float y1 = -amp;
            float y2 = amp;

            //RenderStraight(x1, x2, y1, y2);
            RenderCurved(x1, x2, y1, y2, amp, 4);
        }

        GL.End();
        GL.PopMatrix();
        GL.PopMatrix();
    }

    void OnPostRender()
    {
        RenderFrequency();
    }
}
