using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarutoScript : MonoBehaviour {
    private GameObject area51;
    // Use this for initialization
    void Start () {
       // area51 = GameObject.FindGameObjectWithTag("Area51");
        //transform.position = new Vector2(Random.Range(-20, 20), Random.Range(-20, 20));
	}
    public GameObject explosion;
    public GameObject actualExplosion;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Crate")
        {
            crateProperty cp = collision.gameObject.GetComponent<crateProperty>();
            cp.touching++;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Crate")
        {
            crateProperty cp = collision.gameObject.GetComponent<crateProperty>();
            cp.touching--;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Mine")
        {
            Destroy(collision.gameObject);
            Instantiate(actualExplosion, gameObject.transform.position, Quaternion.identity);
            PlayerPrefs.SetInt("mines", PlayerPrefs.GetInt("mines", 5) + 1);
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Player")
        {
            if (PlayerPrefs.GetInt("gameMode", 0) == 1)
            {
                collision.gameObject.SetActive(false);
                Instantiate(explosion, collision.gameObject.transform.position, Quaternion.identity);
                PlayerPrefs.SetInt("gameOver", 1);
            }
        }

    }
    private void Update()
    {
        if (PlayerPrefs.GetInt("revive", 0) == 1)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
