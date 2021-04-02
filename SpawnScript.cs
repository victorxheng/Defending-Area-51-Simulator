using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class SpawnScript : MonoBehaviour {
    public GameObject narutorunner;
    float time;
    public GameObject explosion;
    public GameObject ActualExplosion;
    public GameObject area51;
    public Text outputText;
    public Joystick joystick;

    public GameObject difficultyTexts;
    public Animator lbButton;
    public Animator ReviveButton;

    public GameObject playAudio;

	// Use this for initialization
	void Start () {
        outputText.text = "DEFEND  AREA  51";
        StartCoroutine("waitForStart");
        difficultyTexts.SetActive(true);
    }
    public void revive()
    {
        outputText.text = "";
        StartCoroutine(spawnLoop());
    }
    public void reviveSaved()
    {
        GameObject audio = Instantiate(playAudio);
        audio.GetComponent<AudioPlayer>().playSound = "flap";
        difficultyTexts.SetActive(false);
        outputText.text = "";
        StartCoroutine("spawnLoop");
        lbButton.SetBool("Show", false);
        ReviveButton.SetBool("Show", false);
        StartCoroutine(spawnLoop());
    }
    IEnumerator waitForStart()
    {
        while((joystick.Horizontal == 0 && joystick.Vertical == 0) || PlayerPrefs.GetInt("kills", 0) > 0 )
        {
            yield return new WaitForSeconds(Time.deltaTime);
        }
        GameObject audio = Instantiate(playAudio);
        audio.GetComponent<AudioPlayer>().playSound = "flap";
        difficultyTexts.SetActive(false);
        outputText.text = "";
        StartCoroutine("spawnLoop");
        lbButton.SetBool("Show", false);
        ReviveButton.SetBool("Show", false);
    }
    IEnumerator spawnLoop()
    {
        while (gameObject.active)
        {
            float spawnNumber;
            if (PlayerPrefs.GetInt("kills", 0) < 50) spawnNumber = 0.25f;
            else if (PlayerPrefs.GetInt("kills", 0) < 120) spawnNumber = 0.235f;
            else if (PlayerPrefs.GetInt("kills", 0) < 250) spawnNumber = 0.22f;
            else spawnNumber = 0.21f;
            if (PlayerPrefs.GetInt("gameMode", 0) == 1) spawnNumber -= 0.015f;
            
                float spawnX = Random.Range(-14f, 14f);
                float spawnY = Random.Range(-14f, 14f);
            int spawnLocation = Random.Range(1, 5);
            if(spawnLocation ==1)
                spawnArmy(spawnX, 14f);
            else if (spawnLocation == 2)
                spawnArmy(spawnX, -14);
            else if (spawnLocation == 3)
                spawnArmy(14, spawnY);
            else
                spawnArmy(-14, spawnY);
                yield return new WaitForSeconds(spawnNumber);
            
            
        }
        
    }
    private void spawnArmy(float x, float y)
    {
            var piece = Instantiate(narutorunner, new Vector2(x, y), Quaternion.identity);
            StartCoroutine(spawn(piece));

    }
    IEnumerator spawn(GameObject piece)
    {
        NarutoScript ns = piece.GetComponent<NarutoScript>();
        SpriteRenderer narutoSR = piece.GetComponent<SpriteRenderer>();
        if (piece.transform.position.x - area51.transform.position.x < 0)
        {
            narutoSR.flipX = true;
        }
        else
        {
            narutoSR.flipX = false;
        }
        while (piece)
        {
            if (piece.transform.position.x> 15 || piece.transform.position.x< -15 || piece.transform.position.y < -15 || piece.transform.position.y  > 15)
            {
                //Instantiate(explosion, piece.transform.position, Quaternion.identity);
                PlayerPrefs.SetInt("kills", PlayerPrefs.GetInt("kills", 0)+1);
                Destroy(piece);

            }
            else if (piece.transform.position.x < 1 && piece.transform.position.x > -1 && piece.transform.position.y > -1 && piece.transform.position.y < 1)
            {
                outputText.text = "AREA  51  RAIDED";
                Instantiate(ActualExplosion, piece.transform.position, Quaternion.identity);
                Destroy(piece);
                PlayerPrefs.SetInt("gameOver", 1);
                gameObject.SetActive(false);
            }
            else
            {
                float speed;
                if (PlayerPrefs.GetInt("kills", 0) < 50) speed = 3.8f;
                else if (PlayerPrefs.GetInt("kills", 0) < 120) speed = 4.1f;
                else if (PlayerPrefs.GetInt("kills", 0) < 250) speed = 4.35f;
                else speed = 4.5f;
                if (PlayerPrefs.GetInt("gameMode", 0) == 1) speed += 0.2f;
                    
                piece.transform.position = Vector3.MoveTowards(piece.transform.position, area51.transform.position, speed * Time.deltaTime);
            }
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
}
