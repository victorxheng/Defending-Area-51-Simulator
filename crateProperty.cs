using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crateProperty : MonoBehaviour
{
    private GameObject player;

    private PlayerScript ms;

    private void Start()
    {
        StartCoroutine(timer());
    }
    IEnumerator timer()
    {
        if (PlayerPrefs.GetInt("gameMode", 0) == 0)
            health = 1.2f;
        else
            health = 0.8f;
        //onDestruction();
        while (health > 0)
        {
            if (touching>0)
                health -= Time.deltaTime;
            SpriteRenderer crateSR = gameObject.GetComponent<SpriteRenderer>();
            crateSR.color = new Color(crateSR.color.r, crateSR.color.g, crateSR.color.b, health);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        onDestruction();
    }
    public float health;
    public int touching = 0;

    public void onDestruction()
    {
        if(PlayerPrefs.GetInt("gameOver", 0) == 0)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            ms = player.GetComponent<PlayerScript>();
            int i = 0;
            foreach (float xValue in ms.crateX)
            {
                if (ms.crateY[i] == transform.position.y && ms.crateX[i] == transform.position.x)
                {
                    ms.crateY.RemoveAt(i);
                    ms.crateX.RemoveAt(i);
                    break;
                }
                i++;
            }
            //StartCoroutine("destroyAnimation");
            Destroy(gameObject);
        }

        
    }

    IEnumerator destroyAnimation()
    {
        SpriteRenderer crateSR = gameObject.GetComponent<SpriteRenderer>();
        for (float x = 1; x > 0; x = x - Time.deltaTime)
        {
            crateSR.color = new Color(crateSR.color.r, crateSR.color.g, crateSR.color.b, x);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        Destroy(gameObject);
    }
    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Naruto")
        {
            health -= Time.deltaTime * 50;
            if (health <= 0) onDestruction();
        }
    }*/
}
