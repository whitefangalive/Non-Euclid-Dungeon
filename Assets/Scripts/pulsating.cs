using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;


public class pulsating : MonoBehaviour
{
    Vignette m_Vignette;
    public float maxIntensity = 0.6f;
    public float minIntensity = 0.35f;
    public float frequency = 10;
    public float inbetweenFreq = 1;
    public float intensityReal;

    private bool hasBeatIn = false;
    private bool hasBeatOut = false;
    public AudioSource beatIn;
    public AudioSource beatOut;
    void Start()
    {
        // Create an instance of a vignette
        Vignette tmp;
        if (GetComponent<Volume>().profile.TryGet<Vignette>(out tmp))
        {
            m_Vignette = tmp;
        }
        m_Vignette.intensity.Override(1f);
        // Use the QuickVolume method to create a volume with a priority of 100, and assign the vignette to this volume

    }
    private void Update()
    {
        float x = Time.realtimeSinceStartup * frequency;
        intensityReal = (Mathf.Max(Mathf.Sin((x - 3.4f) / inbetweenFreq), Mathf.Cos((x - 3.4f) / inbetweenFreq)) * maxIntensity);
        m_Vignette.intensity.value = Mathf.Clamp(intensityReal, minIntensity, maxIntensity);
        if (Mathf.Cos((x - 3.4f) / inbetweenFreq) >= 0.9f)
        {
            if (!hasBeatIn)
            {
                beatIn.volume = maxIntensity + 0.35f;
                beatIn.Play();
                hasBeatIn = true;
            }
        }
        if (Mathf.Sin((x - 3.4f) / inbetweenFreq) >= 0.9f)
        {
            if (!hasBeatOut)
            {
                beatOut.volume = maxIntensity + 0.35f;
                beatOut.Play();
                hasBeatOut = true;
            }
        }
        if (intensityReal < 0)
        {
            hasBeatIn = false;
            hasBeatOut = false;
        }

    }
}