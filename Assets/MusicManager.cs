using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    private GameObject player;
    private string CurrentSceneName;
    private AudioSource CurrentPlayingMusic;

    public float bossDistanceRequired = 25f;
    public float normalEnemyDistanceRequired = 10f;

    public AudioSource StartMusic;
    public AudioSource GameplayNormalMusic;
    public AudioSource GameplayCombatMusic;
    public AudioSource BossMusic;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("HeadCollider");
        CurrentSceneName = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) 
        {
            player = GameObject.Find("HeadCollider");
            CurrentSceneName = SceneManager.GetActiveScene().name;
        }
        if (CurrentSceneName == "StartScene")
        {
            switchMusic(StartMusic, 1);
        }
        else if (GetClosestGameObjectOfName(true) < bossDistanceRequired)
        {
            switchMusic(BossMusic, 1);
        }
        else if (GetClosestGameObjectOfName(false) < normalEnemyDistanceRequired)
        {
            switchMusic(GameplayCombatMusic, 0.5f);
        }
        else 
        {
            switchMusic(GameplayNormalMusic, 1.5f);
        }
    }

    public static IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            if (start <= 0) 
            {
                audioSource.Stop();
            }
            yield return null;
        }
        yield break;
    }

    private void switchMusic(AudioSource to, float fadeTime)
    {
        if (CurrentPlayingMusic != null) 
        {
            StartCoroutine(StartFade(CurrentPlayingMusic, fadeTime, 0));
            if (CurrentPlayingMusic.volume <= 0)
            {
                CurrentPlayingMusic.Stop();
            }
        }
        
        to.Play();
        to.volume = 0;
        StartCoroutine(StartFade(to, fadeTime, 1));

        CurrentPlayingMusic = to;
    }
    private float GetClosestGameObjectOfName(bool boss)
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Enemy");

        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = player.transform.position;
        foreach (GameObject t in objects)
        {
            if (t != null && ((boss && t.name == "Boss") || !boss))
            {
                float dist = Vector3.Distance(t.transform.position, currentPos);
                if (dist < minDist && t.transform.gameObject != gameObject)
                {
                    tMin = t.transform;
                    minDist = dist;
                }
            }
        }
        return minDist;
    }
}