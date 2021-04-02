using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoardMenu : MonoBehaviour
{
    public Animator LeaderboardAnim;
    public Animator leaderboardButtonAnim;

    // Use this for initialization
    void Start()
    {
        uploadSuccessfulText = true;
        //PlayerPrefs.SetInt("hs", 0);
        if (PlayerPrefs.GetString("YourName", "No Name") != "No Name")
            nameEnter.text = PlayerPrefs.GetString("YourName", "No Name");
        //PlayerPrefs.SetString("YourName", "No Name");
    }

    public void showLeaderboard()
    {
        LeaderboardAnim.SetBool("Show", true);
        leaderboardButtonAnim.SetBool("Show", false);

        if (PlayerPrefs.GetString("YourName", "No Name") != "No Name")
            nameEnter.text = PlayerPrefs.GetString("YourName", "No Name");

        uploadSuccessfulText = false;
        dl.LoadScores(true);
    }
    public void back()
    {
        LeaderboardAnim.SetBool("Show", false);
        leaderboardButtonAnim.SetBool("Show", true);
    }

    public Text leaderboardText;
    public Text outputText;

    private string SubmitName;
    private string checkName;
    public InputField nameEnter;

    public DreamloLeaderboard dl;
    private int repeats;
    public bool uploadSuccessfulText;


    public void validateName()
    {
        checkName = nameEnter.text;
        uploadSuccessfulText = true;
        outputText.text = "PROCESSING...";

        if (checkName.Equals(""))
        {
            outputText.text = "ENTER A NAME";

        }
        else if (checkName.Length > 18)
        {
            outputText.text = "TOO MANY CHARACTERS";
        }
        else if (checkName.Equals(PlayerPrefs.GetString("YourName", "No Name")))
        {
            outputText.text = "UPLOADING...";
            UploadScores();
        }
        else
        {
            int AsciiCheck = 0;
            for (int i = 0; i < checkName.Length; i++)
            {
                if (checkName[i] < ' ' || (checkName[i] > '!' && checkName[i] < '0') || (checkName[i] > '9' && checkName[i] < 'A') || checkName[i] > 'z' || (checkName[i] > 'Z' && checkName[i] < 'a'))//insert ascii chart
                {
                    AsciiCheck = 1;
                }
            }
            if (AsciiCheck == 1)
            {
                outputText.text = "CONTAINS INVALID CHARACTERS";
            }
            else
            {
                outputText.text = "LOADING...";
                dl.LoadScores(false);
            }

        }
    }
    public void formatScores(bool refresh)
    {
        repeats = 0;
        List<DreamloLeaderboard.Score> scoreList = dl.ToListHighToLow();
        List<DreamloLeaderboard.Score> scoreList2 = dl.ToListHighToLow2();
        string lbText = formatList(scoreList, refresh, "WORLD  RECORDS - HARD\n");
        string lb2Text = formatList(scoreList2, refresh, "WORLD  RECORDS - EASY\n");
        if (PlayerPrefs.GetInt("gameMode", 0) == 0)
        {
            leaderboardText.text = lb2Text;
        }
        else
        {
            leaderboardText.text = lbText;
        }
        if (!refresh)
        {
            if (repeats > 0)
            {
                outputText.text = "NAME TAKEN";
            }
            else
            {
                outputText.text = "UPLOADING...";
                dl.DeletePrevious(PlayerPrefs.GetString("YourName", "No Name"));
            }
        }

    }

    public void UploadScores()
    {
        string s = checkName;
        s = s.Replace("a", "A");
        s = s.Replace("b", "B");
        s = s.Replace("c", "C");
        s = s.Replace("d", "D");
        s = s.Replace("e", "E");
        s = s.Replace("f", "F");
        s = s.Replace("g", "G");
        s = s.Replace("h", "H");
        s = s.Replace("i", "I");
        s = s.Replace("j", "J");
        s = s.Replace("k", "K");
        s = s.Replace("l", "L");
        s = s.Replace("m", "M");
        s = s.Replace("n", "N");
        s = s.Replace("o", "O");
        s = s.Replace("p", "P");
        s = s.Replace("q", "Q");
        s = s.Replace("r", "R");
        s = s.Replace("s", "S");
        s = s.Replace("t", "T");
        s = s.Replace("u", "U");
        s = s.Replace("v", "V");
        s = s.Replace("w", "W");
        s = s.Replace("x", "X");
        s = s.Replace("y", "Y");
        s = s.Replace("z", "Z");
        checkName = s;
        nameEnter.text = checkName;
        PlayerPrefs.SetString("YourName", checkName);
        dl.AddScore(checkName);
    }

    private string formatList(List<DreamloLeaderboard.Score> scoreList, bool refresh, string startMessage)
    {
        int count = 0;
        string ReturnString = startMessage;
        if (scoreList.Count < 1)
        {
            ReturnString = "NO INTERNET CONNECTION";
            return ReturnString;
        }
        foreach (DreamloLeaderboard.Score currentScore in scoreList)
        {
            count++;
            ReturnString += count + ". ";
            ReturnString += string.Format("{0:n0}", currentScore.score) + " - ";

            ReturnString += Clean(currentScore.playerName) + " ";
            ReturnString += "\n";
            if (!refresh)
            {
                if (checkName.Equals(Clean(currentScore.playerName)))
                {
                    repeats++;
                }
            }
        }
        return ReturnString;
    }
    public void updateScore()
    {
        dl.AddScore(PlayerPrefs.GetString("YourName", "No Name"));
    }
    string Clean(string s)
    {
        s = s.Replace("/", "");
        s = s.Replace("|", "");
        s = s.Replace("+", " ");
        return s;

    }
}
