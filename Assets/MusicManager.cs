using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    private GameObject player;
    private string CurrentSceneName;
    [SerializeField]
    private AudioSource CurrentPlayingMusic;
    private AudioSource PreviouslyPlayingMusic;

    public float bossDistanceRequired = 25f;
    public float normalEnemyDistanceRequired = 10f;

    public AudioSource StartMusic;
    public AudioSource GameplayNormalMusic;
    public AudioSource GameplayCombatMusic;
    public AudioSource BossMusic;

    private enum State {
        START,
        BOSS,
        COMBAT,
        NORMAL,
        NONE
    }
    private State currentState = State.NONE;
    private State previousState;

    public float maxMusicVolume = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("HeadCollider");
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) 
        {
            player = GameObject.Find("HeadCollider");
            
        }
        CurrentSceneName = SceneManager.GetSceneByBuildIndex(1).name;
        Debug.Log(CurrentSceneName);
        previousState = currentState;
        if (CurrentSceneName == "StartScene")
        {
            currentState = State.START;
        }
        else if (GetClosestGameObjectOfName(true) < bossDistanceRequired)
        {
            currentState = State.BOSS;
        }
        else if (GetClosestGameObjectOfName(false) < normalEnemyDistanceRequired)
        {
            currentState = State.COMBAT;
        }
        else 
        {
            currentState = State.NORMAL;
        }


        switch (currentState)
        {
            case State.START:
                switchMusic(StartMusic, 5);
                break;
            case State.BOSS:
                switchMusic(BossMusic, 6);
                break;
            case State.COMBAT:
                switchMusic(GameplayCombatMusic, 6);
                break;
            case State.NORMAL:
                switchMusic(GameplayNormalMusic, 6);
                break;
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
            yield return null;
        }
        yield break;
    }

    private void switchMusic(AudioSource to, float fadeTime)
    {
        if (previousState != currentState)
        {
            PreviouslyPlayingMusic = CurrentPlayingMusic;
            if (!to.isPlaying) 
            { 
                to.Play();
            }
            
            to.volume = 0.01f;
        }
        else
        {
            if (PreviouslyPlayingMusic != null && PreviouslyPlayingMusic.isPlaying)
            {
                StartCoroutine(StartFade(PreviouslyPlayingMusic, fadeTime, 0));
            }
            if (to.isPlaying)
            {
                StartCoroutine(StartFade(to, fadeTime, maxMusicVolume));
                CurrentPlayingMusic = to;
            }
        }
        
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