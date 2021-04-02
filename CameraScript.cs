using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraScript : MonoBehaviour {

    public GameObject player;
    private Vector3 offset;
    public Text kills;
    public Text highscore;
    public Text outputText;
    public GameObject deathPanel;
     bool called = false;
    public Animator leaderboardButtonAnim;
    public DreamloLeaderboard dl;
    public GameObject playAudio;
    public NotificationScript ns;
    void Awake()
    {
        Time.timeScale = 1;
        deathPanel.SetActive(false);
        offset = new Vector3(0, 0, -10);

            if (PlayerPrefs.GetInt("gameMode", 0) == 0)
            highscore.text = PlayerPrefs.GetInt("recordEasy", 0) + "";
        else
            highscore.text = PlayerPrefs.GetInt("recordHard", 0) + "";
    }
    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
        kills.text = PlayerPrefs.GetInt("kills", 0)+"";
        

        if (PlayerPrefs.GetInt("gameOver", 0) == 1)
        {
            if (!called)
            {

                    PlayerPrefs.SetInt("savedScore", PlayerPrefs.GetInt("kills", 0));
                PlayerPrefs.SetInt("Deaths", PlayerPrefs.GetInt("Deaths", 0)+1);
                ns.showNotification();

                int hs;
                if (PlayerPrefs.GetInt("gameMode", 0) == 0)
                    hs = PlayerPrefs.GetInt("recordEasy", 0);
                else
                    hs = PlayerPrefs.GetInt("recordHard", 0);

                if (PlayerPrefs.GetInt("kills", 0) > hs)
                {
                    if (PlayerPrefs.GetInt("gameMode", 0) == 0)
                    {
                        PlayerPrefs.SetInt("recordEasy", PlayerPrefs.GetInt("kills", 0));
                        highscore.text = PlayerPrefs.GetInt("recordEasy", 0) + "";
                    }
                    else
                    {
                        PlayerPrefs.SetInt("recordHard", PlayerPrefs.GetInt("kills", 0));
                        highscore.text = PlayerPrefs.GetInt("recordHard", 0) + "";
                    }

                    if (PlayerPrefs.GetString("YourName", "No Name") != "No Name")
                        dl.AddScore(PlayerPrefs.GetString("YourName", "No Name"));
                    dl.LoadScores(true);
                }
                StartCoroutine(death());
            }
        }
        else
        {
            called = false;
        }
    }
    IEnumerator death()
    {
        called = true;
        outputText.text = "revive  with  ad";
        GameObject audio = Instantiate(playAudio);
        audio.GetComponent<AudioPlayer>().playSound = "rip";
        yield return new WaitForSeconds(1);
        Time.timeScale = 0;
        leaderboardButtonAnim.SetBool("Show", true);
        deathPanel.SetActive(true);
    }



}
