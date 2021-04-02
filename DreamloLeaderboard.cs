using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreamloLeaderboard : MonoBehaviour
{
    private string privateCode = "DdWpB3_3jEug2ztaNCIQ9wb4NKlZtZYEWvr-WVuaofXg";
    private string publicCode = "5d44cb7376827f1758d664f9";
    private string privateCode2 = "kI_5Ax0eGE2rLKgqDz5XXgFZj1lxNx2kykuTkvzyW_1w";
    private string publicCode2 = "5d46d6527682811758c4a53e";

    string dreamloWebserviceURL = "http://dreamlo.com/lb/";


    string highScore = "";
    string highScore2 = "";

    public LeaderBoardMenu lb;

    ////////////////////////////////////////////////////////////////////////////////////////////////

    // A player named Carmine got a score of 100. If the same name is added twice, we use the higher score.
    // http://dreamlo.com/lb/(your super secret very long code)/add/Carmine/100

    // A player named Carmine got a score of 1000 in 90 seconds.
    // http://dreamlo.com/lb/(your super secret very long code)/add/Carmine/1000/90

    // A player named Carmine got a score of 1000 in 90 seconds and is Awesome.
    // http://dreamlo.com/lb/(your super secret very long code)/add/Carmine/1000/90/Awesome

    ////////////////////////////////////////////////////////////////////////////////////////////////


    public struct Score
    {
        public string playerName;
        public ulong score;
        public int seconds;
        public string shortText;
        public string dateString;
    }


    void Start()
    {
        highScore = "";
    }


    public static double DateDiffInSeconds(System.DateTime now, System.DateTime olderdate)
    {
        var difference = now.Subtract(olderdate);
        return difference.TotalSeconds;
    }

    System.DateTime _lastRequest = System.DateTime.Now;
    int _requestTotal = 0;


    bool TooManyRequests()
    {
        var now = System.DateTime.Now;

        if (DateDiffInSeconds(now, _lastRequest) <= 2)
        {
            _lastRequest = now;
            _requestTotal++;
            if (_requestTotal > 3)
            {
                Debug.LogError("DREAMLO Too Many Requests. Am I inside an update loop?");
                return true;
            }

        }
        else
        {
            _lastRequest = now;
            _requestTotal = 0;
        }

        return false;
    }

    public void AddScore(string playerName)
    {
        if (TooManyRequests()) return;

        StartCoroutine(AddScoreWithPipe(playerName));

    }

    // This function saves a trip to the server. Adds the score and retrieves results in one trip.
    IEnumerator AddScoreWithPipe(string player)
    {
        player = Clean(player);

        WWW www = new WWW(dreamloWebserviceURL + privateCode + "/add-pipe/" + WWW.EscapeURL(player) + "/" + PlayerPrefs.GetInt("recordHard", 0).ToString());
        yield return www;
        WWW www2 = new WWW(dreamloWebserviceURL + privateCode2 + "/add-pipe/" + WWW.EscapeURL(player) + "/" + PlayerPrefs.GetInt("recordEasy", 0).ToString());
        yield return www2;
        if (lb.uploadSuccessfulText)
            lb.outputText.text = "UPLOAD SUCCESSFUL";
        LoadScores(true);
    }

    IEnumerator GetScores(bool refresh)
    {
        highScore = "";
        highScore2 = "";
        WWW www = new WWW(dreamloWebserviceURL + publicCode2 + "/pipe");
        yield return www;

        WWW www2 = new WWW(dreamloWebserviceURL + publicCode + "/pipe");
        yield return www2;

        highScore2 = www.text;
        highScore = www2.text;
        
        lb.formatScores(refresh);
    }
    public void DeletePrevious(string name)
    {
        StartCoroutine(DeleteScore(name));
        lb.UploadScores();
    }
    IEnumerator DeleteScore(string name)
    {
        WWW www = new WWW(dreamloWebserviceURL +privateCode + "/delete/" + WWW.EscapeURL(name));
        yield return www;
        WWW www2 = new WWW(dreamloWebserviceURL + privateCode2 + "/delete/" + WWW.EscapeURL(name));
        yield return www2;
    }
    /*
	IEnumerator GetSingleScore(string playerName)
	{
		highScores = "";
		WWW www = new WWW(dreamloWebserviceURL +  publicCode  + "/pipe-get/" + WWW.EscapeURL(playerName));
		yield return www;
		highScores = www.text;
	}
	*/
    public void LoadScores(bool refresh)
    {
        if (TooManyRequests()) return;
        StartCoroutine(GetScores(refresh));
    }


    public string[] ToStringArray()
    {
        if (this.highScore == null) return null;
        if (this.highScore == "") return null;

        string[] rows = this.highScore.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        return rows;


    }

    public string[] ToStringArray2()
    {
        if (this.highScore2 == null) return null;
        if (this.highScore2 == "") return null;

        string[] rows = this.highScore2.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        return rows;


    }

    public List<Score> ToListLowToHigh()
    {
        Score[] scoreList = this.ToScoreArray();

        if (scoreList == null) return new List<Score>();

        List<Score> genericList = new List<Score>(scoreList);

        genericList.Sort((x, y) => x.score.CompareTo(y.score));

        return genericList;
    }

    public List<Score> ToListHighToLow()
    {
        Score[] scoreList = this.ToScoreArray();

        if (scoreList == null) return new List<Score>();

        List<Score> genericList = new List<Score>(scoreList);

        genericList.Sort((x, y) => y.score.CompareTo(x.score));

        return genericList;
    }
    public List<Score> ToListHighToLow2()
    {
        Score[] scoreList = this.ToScoreArray2();

        if (scoreList == null) return new List<Score>();

        List<Score> genericList = new List<Score>(scoreList);

        genericList.Sort((x, y) => y.score.CompareTo(x.score));

        return genericList;
    }

    public Score[] ToScoreArray()
    {
        string[] rows = ToStringArray();
        if (rows == null) return null;

        int rowcount = rows.Length;

        if (rowcount <= 0) return null;

        Score[] scoreList = new Score[rowcount];

        for (int i = 0; i < rowcount; i++)
        {
            string[] values = rows[i].Split(new char[] { '|' }, System.StringSplitOptions.None);

            Score current = new Score();
            current.playerName = values[0];
            current.score = 0;
            current.seconds = 0;
            current.shortText = "";
            current.dateString = "";
            if (values.Length > 1) current.score = System.Convert.ToUInt64(values[1]);
            if (values.Length > 2) current.seconds = CheckInt(values[2]);
            if (values.Length > 3) current.shortText = values[3];
            if (values.Length > 4) current.dateString = values[4];
            scoreList[i] = current;
        }    

        return scoreList;
    }

    public Score[] ToScoreArray2()
    {
        string[] rows2 = ToStringArray2();
        if (rows2 == null) return null;
        
        int rowcount2 = rows2.Length;
        
        if (rowcount2 <= 0) return null;

        Score[] scoreList2 = new Score[rowcount2];

        for (int i = 0; i < rowcount2; i++)
        {
            string[] values = rows2[i].Split(new char[] { '|' }, System.StringSplitOptions.None);

            Score current = new Score();
            current.playerName = values[0];
            current.score = 0;
            current.seconds = 0;
            current.shortText = "";
            current.dateString = "";
            if (values.Length > 1) current.score = System.Convert.ToUInt64(values[1]);
            if (values.Length > 2) current.seconds = CheckInt(values[2]);
            if (values.Length > 3) current.shortText = values[3];
            if (values.Length > 4) current.dateString = values[4];
            scoreList2[i] = current;
        }


        return scoreList2;
    }


    // Keep pipe and slash out of names

    string Clean(string s)
    {
        s = s.Replace("/", "");
        s = s.Replace("|", "");
        s = s.Replace("+", " ");
        return s;

    }

    int CheckInt(string s)
    {
        int x = 0;

        int.TryParse(s, out x);
        return x;
    }
}
