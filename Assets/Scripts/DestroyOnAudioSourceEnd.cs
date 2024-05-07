using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnAudioSourceEnd : MonoBehaviour
{
    private AudioSource m_AudioSource;
    private void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
        Destroy(gameObject, m_AudioSource.clip.length);
    }
}
