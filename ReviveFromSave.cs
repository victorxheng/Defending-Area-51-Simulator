using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReviveFromSave : MonoBehaviour {

    public Animator ReviveText;
    public Text scoreText;
    // Use this for initialization
    void Start () {
		if(PlayerPrefs.GetInt("savedScore", 0) > 0)
        {
            scoreText.text = "REVIVE  TO SAVED: "+ PlayerPrefs.GetInt("savedScore", 0);
            ReviveText.SetBool("Show", true);
        }
	}
}
