using UnityEngine;

public class TimeCremental : MonoBehaviour
{
    public float Timer = 0;
    private pulsating pulsator;
    public float freqMultipler;
    public float inbetweenFreqMultiplier;
    public float min = 1;
    public float max = 20;

    private PlayerData playerdata;
    // Start is called before the first frame update
    void Start()
    {
        pulsator = GameObject.Find("PostEffectsController").GetComponent<pulsating>();
        playerdata = GameObject.Find("Player").GetComponent<PlayerData>();
    }

    // Update is called once per frame
    void Update()
    {
        Timer = (playerdata.maxHealth - playerdata.health) * (200 / (playerdata.maxHealth - 1));
        pulsator.minIntensity = (0.35f / Mathf.Clamp((playerdata.maxHealth - 1), 1, Mathf.Infinity)) * (playerdata.maxHealth - playerdata.health);
        pulsator.maxIntensity = (0.65f / Mathf.Clamp((playerdata.maxHealth - 1), 1, Mathf.Infinity)) * (playerdata.maxHealth - playerdata.health);
        pulsator.frequency = (Mathf.Floor(Mathf.Clamp((Timer * freqMultipler), 0, max) * 10) / 10) + Mathf.Clamp(playerdata.maxHealth - playerdata.health, 0, min);
        pulsator.inbetweenFreq = Mathf.Clamp((1 / (1 + (Timer * inbetweenFreqMultiplier))), 0, 1);

    }
}