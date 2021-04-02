using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideInstructTxt : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if(PlayerPrefs.GetString("YourName", "No Name") != "NoName")
        {
            gameObject.SetActive(false);
        }
	}
}
