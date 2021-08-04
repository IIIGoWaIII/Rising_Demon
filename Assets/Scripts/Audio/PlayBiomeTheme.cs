using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBiomeTheme : MonoBehaviour
{
    private bool muteTheme = false;
    private float startTime;
    private float fadeOutTime = 1f;
    private float volume;

    private AudioSource audioSource;
    private Collider2D coll;

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        coll = gameObject.GetComponent<Collider2D>();
        volume = audioSource.volume;
    }

    void Update()
    {
        if(muteTheme)
        {
            float t = (Time.time - startTime) / fadeOutTime;
            float newVolume = volume;
            newVolume = Mathf.Lerp(volume, 0f, t);
            audioSource.volume = newVolume;
            print(newVolume);

            if(newVolume == 0)
            {
                audioSource.Stop();
                muteTheme = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Theme Changer"))
        {
            audioSource.Play();
            audioSource.volume = volume;
            muteTheme = false;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Theme Changer"))
        {
            startTime = Time.time;
            muteTheme = true;
        }
    }
}
