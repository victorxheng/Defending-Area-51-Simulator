using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class AdManager : MonoBehaviour {

    public Text outputText;
    public GameObject gameOver;

    public GameObject player;
    public GameObject playAudio;

    public Text reviveOutputText;
    
#if UNITY_ANDROID
    string gameID = "3244059";
#elif UNITY_IOS
        string gameID = "3244058";
#endif
    bool testMode = false;


    void Awake ()
    {
        PlayerPrefs.SetInt("revive", 0);
        outputText.text = "revive  with  ad";
        //Advertisement.Initialize(gameID, testMode);
    }

    public void ShowReward(string zone = "")
    {
        outputText.text = "LOADING...";
        if (string.Equals(zone, ""))
            zone = null;

        ShowOptions options2 = new ShowOptions();
        options2.resultCallback = AdCallbackhandler2;

        if (Advertisement.IsReady(zone))
        {
            Advertisement.Show(zone, options2);
            outputText.text = "SHOWING...";
        }
        else
        {
            outputText.text = "FAILED  TO  LOAD,  TRY  AGAIN";
        }
    }
    void AdCallbackhandler2(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                outputText.text = "AD COMPLETE";
                GameObject audio = Instantiate(playAudio);
                audio.GetComponent<AudioPlayer>().playSound = "reward";
                StartCoroutine(revive());
                break;
            case ShowResult.Skipped:
                outputText.text = "AD  SKIPPED";
                break;
            case ShowResult.Failed:
                outputText.text = "AD  FAILED";
                break;
        }
    }

    public Animator leaderboardButtonAnim;

    IEnumerator revive()
    {
        Time.timeScale = 1;

        PlayerPrefs.SetInt("gameOver", 0);
        gameOver.SetActive(false);
        player.transform.position = new Vector3(0, 4, -1);
        player.SetActive(true);

        SpawnScript ss = player.GetComponent<SpawnScript>();
        ss.revive();
        PlayerScript ps = player.GetComponent<PlayerScript>();
        ps.revive();

        PlayerPrefs.SetInt("revive", 1);
        yield return new WaitForSeconds(Time.deltaTime * 5);
        PlayerPrefs.SetInt("revive", 0);

        leaderboardButtonAnim.SetBool("Show", false);
    }

    public Text submitNameText;
    public LeaderBoardMenu lbm;

    public void validateLeaderboardName(string zone = "")
    {
        if (string.Equals(zone, ""))
            zone = null;

        ShowOptions options = new ShowOptions();
        options.resultCallback = AdCallbackhandler;

        if (Advertisement.IsReady(zone))
        {
            Advertisement.Show(zone, options);
            submitNameText.text = "SHOWING...";
        }
        else
        {
            submitNameText.text = "FAILED  TO  LOAD,  TRY  AGAIN";
        }
    }

    void AdCallbackhandler(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                submitNameText.text = "AD COMPLETE";
                GameObject audio = Instantiate(playAudio);
                audio.GetComponent<AudioPlayer>().playSound = "reward";
                lbm.validateName();
                break;
            case ShowResult.Skipped:
                submitNameText.text = "AD  SKIPPED";
                break;
            case ShowResult.Failed:
                submitNameText.text = "AD  FAILED";
                break;
        }
    }

    public void ReviveFromSaved(string zone = "")
    {
        if (string.Equals(zone, ""))
            zone = null;

        ShowOptions options = new ShowOptions();
        options.resultCallback = AdCallbackhandler3;

        if (Advertisement.IsReady(zone))
        {
            Advertisement.Show(zone, options);
            reviveOutputText.text = "SHOWING...";
        }
        else
        {
            reviveOutputText.text = "FAILED  TO  LOAD,  TRY  AGAIN";
        }
    }

    void AdCallbackhandler3(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                reviveOutputText.text = "AD COMPLETE";
                GameObject audio = Instantiate(playAudio);
                audio.GetComponent<AudioPlayer>().playSound = "reward";
                PlayerPrefs.SetInt("kills", PlayerPrefs.GetInt("savedScore", 0));

                SpawnScript ss = player.GetComponent<SpawnScript>();
                ss.reviveSaved();
                PlayerScript ps = player.GetComponent<PlayerScript>();
                ps.revive();

                break;
            case ShowResult.Skipped:
                reviveOutputText.text = "AD  SKIPPED";
                break;
            case ShowResult.Failed:
                reviveOutputText.text = "AD  FAILED";
                break;
        }
    }

    


}
