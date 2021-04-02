using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationScript : MonoBehaviour {

    public Text title;
    public Text content;
    public Animator notificationAnimator;
    public List<string> titleTexts = new List<string>();
    public List<string> contentTexts = new List<string>();
    private void Start()
    {
        titleTexts.Add("");
        contentTexts.Add("");
        titleTexts.Add("REVIVE  WITH  AN  AD");
        contentTexts.Add("VIEW  AN  AD,  INSTALL  ITS  GAME,  AND BECOME  ALIVE  ONCE  MORE");
        titleTexts.Add("");
        contentTexts.Add("");
        titleTexts.Add("RATE  FIVE  STARS");
        contentTexts.Add("HELP  SPREAD  AREA  51  AWARENESS");
        titleTexts.Add("");
        contentTexts.Add("");
        titleTexts.Add("INSTALL  AN  AD'S  GAME");
        contentTexts.Add("A  FEW  SIMPLE  CLICKS  HELPS  FEED  THE DEVELOPER. GAIN INSTANT KARMA");
        titleTexts.Add("");
        contentTexts.Add("");
        titleTexts.Add("SHARE  WITH  YOUR  FRIENDS");
        contentTexts.Add("PLAY  WITH  YOUR  FRIENDS  TO  PREPARE  FOR  THE  RAID");
        titleTexts.Add("");
        contentTexts.Add("");
        titleTexts.Add("GAIN INSTANT KARM");
        contentTexts.Add("REMEMBER  TO  INSTALL  AN  AD'S  GAME  FOR  AREA  51  RAID  SUPPORT");
        titleTexts.Add("");
        contentTexts.Add("");
        titleTexts.Add("REMINDER");
        contentTexts.Add("AREA  51  RAID  ON  SEPTEMBER  20TH");
        titleTexts.Add("");
        contentTexts.Add("");
        titleTexts.Add("GAME  FACT");
        contentTexts.Add("BULLETS  TRAVEL  FASTER  ON  HARD  MODE");
        titleTexts.Add("");
        contentTexts.Add("");
        titleTexts.Add("");
        contentTexts.Add("");
        titleTexts.Add("GAMERS  HELPING  GAMERS");
        contentTexts.Add("INSTALL  THE  GAME  FROM  AN  AD  TO  CONTRIBUTE TO  OUR  COMMUNITY");
        titleTexts.Add("");
        contentTexts.Add("");
        titleTexts.Add("GAME  FACT");
        contentTexts.Add("RUNNERS  DIE  FROM  MINES  OR  FLYING  OUT  OF  THE  WORLD");
        titleTexts.Add("");
        contentTexts.Add("");
        titleTexts.Add("");
        contentTexts.Add("");
        titleTexts.Add("GAME  FACT");
        contentTexts.Add("MINES  DISAPPEAR  AFTER  18  SECONDS  ON  EASY,  12  ON  HARD");
        titleTexts.Add("");
        contentTexts.Add("");
        titleTexts.Add("");
        contentTexts.Add("");
        titleTexts.Add("");
        contentTexts.Add("");
        titleTexts.Add("GAME  FACT");
        contentTexts.Add("MINE  KILLS  ARE  NOT  COUNTED");
        titleTexts.Add("");
        contentTexts.Add("");
        titleTexts.Add("");
        contentTexts.Add("");
        titleTexts.Add("GAME  FACT");
        contentTexts.Add("CRATES  WERE  ABOUT  TO  BE  REMOVED  FROM  THE  GAME  DUE TO  BUGS");
        titleTexts.Add("");
        contentTexts.Add("");
    }
    public void showNotification()
    {
        int index = PlayerPrefs.GetInt("Deaths", 0);
        if (index < titleTexts.Count)
        {
            if (titleTexts[index] != "")
            {
                title.text = titleTexts[index];
                content.text = contentTexts[index];
                notificationAnimator.SetBool("Show", true);
            }
        }
        
    }

    public void hide()
    {
        print("hide");
        notificationAnimator.SetBool("Show", false);
    }
}
