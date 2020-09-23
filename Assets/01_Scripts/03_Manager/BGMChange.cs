using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMChange : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip audioClipBOSS;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClipBOSS;
        audioSource.Play();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BgmChange()
    {
        audioSource.clip = audioClipBOSS;
        audioSource.Play();
        Debug.Log(audioSource);
    }
}
