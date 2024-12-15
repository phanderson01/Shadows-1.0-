using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audio_play : MonoBehaviour
{
    AudioSource aud;
    // Start is called before the first frame update
    void Start()
    {
        aud = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void walking_sound()
    {
        aud.Play();
    }

    public void stop_sound()
    {
        aud.Pause();
    }

}
