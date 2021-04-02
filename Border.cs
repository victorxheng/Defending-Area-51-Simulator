using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour {
    public GameObject bloodyExplosion;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.SetActive(false);
            Instantiate(bloodyExplosion, collision.gameObject.transform.position, Quaternion.identity);
            PlayerPrefs.SetInt("gameOver", 1);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Naruto")
        {
            Destroy(collision.gameObject);
        }
    }
}
