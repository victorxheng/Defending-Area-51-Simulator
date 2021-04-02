using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class restartScript : MonoBehaviour {
    
    public void OnClick()
    {
        PlayerPrefs.SetInt("savedScore", 0);
        SceneManager.LoadScene(0);
    }
}
