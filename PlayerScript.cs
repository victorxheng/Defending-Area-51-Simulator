using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour {

    bool crateDown = false;
    bool shootDown = false;
    public Text mineCount;
    public GameObject playAudio;

    public Rigidbody2D rb;
    private void Awake()
    {
        PlayerPrefs.SetInt("kills", 0);
        PlayerPrefs.SetInt("gameOver", 0);
        PlayerPrefs.SetInt("mines", 5);
        mineCount.text = PlayerPrefs.GetInt("mines", 5)+"";
        Application.targetFrameRate = 200;
        StartCoroutine(Loop());
    }
    public void revive()
    {
        StartCoroutine(Loop());
    }
    IEnumerator Loop()
    {
        while (gameObject.active)
        {
            if (crateDown) spawnCrate();
            if (shootDown) onFire();
            mineCount.text = PlayerPrefs.GetInt("mines", 5) + "";
            yield return new WaitForSeconds(0.05f);
        }
    }
    
    public void crateButtonDown()
    {
        crateDown = true;
    }
    public void crateButtonUp()
    {
        crateDown = false;
    }
    public void fireButtonDown()
    {
        shootDown = true;
    }
    public void fireButtonUp()
    {
        shootDown = false;
    }

    public GameObject bullet;
    public GameObject crate;
    public GameObject mine;
    public void onFire()
    {
        GameObject audio = Instantiate(playAudio);
        audio.GetComponent<AudioPlayer>().playSound = "shoot";

        var bulletObject = Instantiate(bullet, new Vector2(-Mathf.Sin(rb.rotation * (3.14f / 180f)) * 1f + transform.position.x, Mathf.Cos(rb.rotation * (3.14f / 180f)) * 1f + transform.position.y), Quaternion.Euler(new Vector3(0, 0, rb.rotation)));

        if (gameObject.active)
        StartCoroutine(bulletMove(bulletObject));
    }
    
    public void spawnCrate()
    {
        GameObject audio = Instantiate(playAudio);
        audio.GetComponent<AudioPlayer>().playSound = "place";
        var cratey = Instantiate(crate, new Vector2(-Mathf.Sin(rb.rotation * (3.14f / 180f)) * 2f + transform.position.x, Mathf.Cos(rb.rotation * (3.14f / 180f)) * 2f + transform.position.y), Quaternion.identity);
        crateSpawn(cratey);
    }
    
    public void spawnMine()
    {
        if (PlayerPrefs.GetInt("mines", 5) > 0)
        {
            GameObject audio = Instantiate(playAudio);
            audio.GetComponent<AudioPlayer>().playSound = "place";

            PlayerPrefs.SetInt("mines", PlayerPrefs.GetInt("mines", 5) - 1);
            Instantiate(mine, new Vector2(-Mathf.Sin(rb.rotation * (3.14f / 180f)) * 2f + transform.position.x, Mathf.Cos(rb.rotation * (3.14f / 180f)) * 2f + transform.position.y), Quaternion.identity);
        }
    }

    IEnumerator bulletMove(GameObject bulletObject)
    {
        Vector2 velocity;
        if (PlayerPrefs.GetInt("gameMode", 0) == 0)
            velocity = new Vector2(-Mathf.Sin(rb.rotation * (3.14f / 180f)) * Time.deltaTime * 32f + rb.velocity.x * Time.deltaTime * 1f, Mathf.Cos(rb.rotation * (3.14f / 180f)) * Time.deltaTime * 32f + rb.velocity.y * Time.deltaTime * 1f);

        else
            velocity = new Vector2(-Mathf.Sin(rb.rotation * (3.14f / 180f)) * Time.deltaTime * 50f + rb.velocity.x * Time.deltaTime * 1f, Mathf.Cos(rb.rotation * (3.14f / 180f)) * Time.deltaTime * 50f + rb.velocity.y * Time.deltaTime * 1f);

        float time2 = 0;
        while (bulletObject)
        {

            if (PlayerPrefs.GetInt("gameMode", 0) == 0)
            {
                if (time2 > 0.6f) Destroy(bulletObject);
            }
            else
            {
                if (time2 > 0.35f) Destroy(bulletObject);
            }
            bulletObject.transform.position += Vector3.up * velocity.y;
            bulletObject.transform.position += Vector3.right * velocity.x;
            time2 += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    private Rigidbody2D playerRB;
    private float rotation;

    public List<float> crateX = new List<float>();// { -1.6f, -0.8f, 0, 0.8f, 1.6f, -1.6f, -0.8f, 0, 0.8f, 1.6f, -1.6f, -0.8f, 0, 0.8f, 1.6f, -1.6f, -0.8f, 0, 0.8f, 1.6f, -1.6f, -0.8f, 0, 0.8f, 1.6f};
    public List<float> crateY = new List<float>(); //{ 1.6f, 1.6f, 1.6f, 1.6f, 1.6f, 0.8f, 0.8f, 0.8f, 0.8f, 0.8f, 0, 0, 0, 0, 0, -0.8f, -0.8f, -0.8f, -0.8f, -0.8f, -1.6f, -1.6f, -1.6f, -1.6f, -1.6f};

    private void crateSpawn(GameObject cratey)
    {
        cratey.transform.position = new Vector3((float)Math.Round(cratey.transform.position.x / 0.8, MidpointRounding.AwayFromZero) * 0.8f, (float)Math.Round(cratey.transform.position.y / 0.8, MidpointRounding.AwayFromZero) * 0.8f);
        checkForRange(cratey);
        samePosition(cratey);
    }
    void samePosition(GameObject cratey)
    {
        cratey.transform.position = new Vector3((float)Math.Round(cratey.transform.position.x / 0.8, MidpointRounding.AwayFromZero) * 0.8f, (float)Math.Round(cratey.transform.position.y / 0.8, MidpointRounding.AwayFromZero) * 0.8f);

        if (crateX.Contains(cratey.transform.position.x) && crateY.Contains(cratey.transform.position.y))
        {
            int i = 0;
            bool same = false;
            foreach (float xValue in crateX)
            {
                if (crateY[i] == cratey.transform.position.y && crateX[i] == cratey.transform.position.x)
                {
                    same = true;
                    break;
                }
                i++;
            }
            if (same)
            {
                shift(cratey);
            }
            else
            {
                checkForRange(cratey);
                crateX.Add(cratey.transform.position.x);
                crateY.Add(cratey.transform.position.y);
            }
        }
        else
        {
            checkForRange(cratey);
            crateX.Add(cratey.transform.position.x);
            crateY.Add(cratey.transform.position.y);
        }
    }
    private void shift(GameObject cratey)
    {

        playerRB = gameObject.GetComponent<Rigidbody2D>();

        rotation = playerRB.rotation;

        if (rotation >= 45 && rotation <= 135)
            cratey.transform.position = new Vector2(cratey.transform.position.x - 0.8f, cratey.transform.position.y);
        else if (rotation > 135 || rotation <= -135)
            cratey.transform.position = new Vector2(cratey.transform.position.x, cratey.transform.position.y - 0.8f);
        else if (rotation > -135 && rotation <= -45)
            cratey.transform.position = new Vector2(cratey.transform.position.x + 0.8f, cratey.transform.position.y);
        else
            cratey.transform.position = new Vector2(cratey.transform.position.x, cratey.transform.position.y + 0.8f);

        samePosition(cratey);
    }
    private void checkForRange(GameObject cratey)
    {

        if (cratey.transform.position.x < 2 && cratey.transform.position.x > -2 && cratey.transform.position.y > -2 && cratey.transform.position.y < 2)
        { 
            Destroy(cratey);
        }
        else if (cratey.transform.position.x > 10 || cratey.transform.position.x < -10 || cratey.transform.position.y < -10 || cratey.transform.position.y > 10)
        {
            Destroy(cratey);
        }
    }
}
