using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionAudio : MonoBehaviour {
    public GameObject playAudio;
	// Use this for initialization
	void Start () {
        GameObject audio = Instantiate(playAudio);
        audio.GetComponent<AudioPlayer>().playSound = "hit";
    }
}
