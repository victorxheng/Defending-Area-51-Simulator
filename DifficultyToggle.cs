using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyToggle : MonoBehaviour {

    public Image Easy;
    public Image Hard;

    public Text EasyT;
    public Text HardT;

    public Text record;

    public Animator ReviveButton;

    void Start ()
    {
        buttonUpdate();
    }
    private void buttonUpdate()
    {
        Easy.color = new Color(Easy.color.r, Easy.color.g, Easy.color.b, 0.5f);
        EasyT.color = new Color(EasyT.color.r, EasyT.color.g, EasyT.color.b, 0.5f);
        Hard.color = new Color(Hard.color.r, Hard.color.g, Hard.color.b, 0.5f);
        HardT.color = new Color(HardT.color.r, HardT.color.g, HardT.color.b, 0.5f);


        if (PlayerPrefs.GetInt("gameMode", 0) == 0)
        {
            Easy.color = new Color(Easy.color.r, Easy.color.g, Easy.color.b, 1);
            EasyT.color = new Color(EasyT.color.r, EasyT.color.g, EasyT.color.b, 1);
        }
        else
        {
            Hard.color = new Color(Hard.color.r, Hard.color.g, Hard.color.b, 1);
            HardT.color = new Color(HardT.color.r, HardT.color.g, HardT.color.b, 1);
        }
    }

    public void onEasy()
    {
        PlayerPrefs.SetInt("gameMode", 0);
        buttonUpdate();
            record.text = PlayerPrefs.GetInt("recordEasy", 0) + "";

        PlayerPrefs.SetInt("savedScore", 0);
        ReviveButton.SetBool("Show", false);
    }
    public void onHard()
    {
        PlayerPrefs.SetInt("gameMode", 1);
        buttonUpdate();
        record.text = PlayerPrefs.GetInt("recordHard", 0) + "";

        PlayerPrefs.SetInt("savedScore", 0);
        ReviveButton.SetBool("Show", false);
    }
}
