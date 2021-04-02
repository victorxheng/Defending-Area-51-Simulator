using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour {
    public AudioClip hit;
    public AudioClip rip;
    public AudioClip shoot;
    public AudioClip lvlUp;
    public AudioClip flap;
    public AudioSource source;

    public string playSound;

    private void Start()
    {
        source.volume = 1;
        source.pitch = 1;
        if (playSound == "hit")
        {
            source.clip = hit;
            source.Play();
        }
        if (playSound == "rip")
        {
            source.clip = rip;
            source.Play();
        }
        if (playSound == "reward")
        {
            source.clip = lvlUp;
            source.Play();
        }
        if (playSound == "flap")
        {
            source.clip = flap;
            source.Play();
        }
        if (playSound == "shoot")
        {
            source.clip = shoot;
            source.volume = 0.3f;
            source.Play();
        }
        if (playSound == "place")
        {
            source.clip = shoot;
            source.volume = 0.8f;
            source.pitch = 3;
            source.Play();
        }

        StartCoroutine(selfDestruct());
    }

    IEnumerator selfDestruct()
    {
        yield return new WaitForSeconds(0.7f);
        Destroy(gameObject);
    }
}
