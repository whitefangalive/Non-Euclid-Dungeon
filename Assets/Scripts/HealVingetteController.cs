using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;


public class HealVingetteController : MonoBehaviour
{
    Vignette m_Vignette;
    public float Intensity = 0f;
    private float realIntensity;
    public float speed = 1f;
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
    // Update is called once per frame
    void Update()
    {
        if (realIntensity > 0)
        {
            GetComponent<Volume>().priority = 2;
        }
        else 
        {
            GetComponent<Volume>().priority = 0;
        }
        realIntensity = Mathf.Lerp(0, Intensity, Time.timeSinceLevelLoad);

        m_Vignette.intensity.value = realIntensity;
    }

    public void playVignette(float strength) 
    {
        Intensity = strength;
    }
}
