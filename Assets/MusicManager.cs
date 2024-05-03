using System;
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
    public bool bossDead = false;

    public AudioSource StartMusic;
    public AudioSource GameplayNormalMusic;
    public AudioSource GameplayCombatMusic;
    public AudioSource BossMusic;
    public AudioSource VictoryMusic;

    private enum State {
        START,
        BOSS,
        COMBAT,
        NORMAL,
        VICTORY,
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
        else if (bossDead) 
        {
            currentState = State.VICTORY;
        }
        else
        {
            currentState = State.NORMAL;
        }


        switch (currentState)
        {
            case State.START:
                switchMusic(StartMusic, 2);
                break;
            case State.BOSS:
                switchMusic(BossMusic, 3);
                break;
            case State.COMBAT:
                switchMusic(GameplayCombatMusic, 2);
                break;
            case State.NORMAL:
                switchMusic(GameplayNormalMusic, 2);
                break;
            case State.VICTORY:
                switchMusic(VictoryMusic, 1);
                break;
        }
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
                float currentTime = 0;
                float start = PreviouslyPlayingMusic.volume;
                if (currentTime < fadeTime)
                {
                    currentTime += Time.deltaTime;
                    PreviouslyPlayingMusic.volume = Mathf.Lerp(start, 0, currentTime / fadeTime);
                }
            }
            if (to.isPlaying)
            {
                float currentTime = 0;
                float start = to.volume;
                if (currentTime < fadeTime) 
                {
                    currentTime += Time.deltaTime;
                    to.volume = Mathf.Lerp(start, maxMusicVolume, currentTime / fadeTime);
                }
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