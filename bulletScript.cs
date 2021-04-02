using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour {
    public GameObject explosion;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag != "Player")
        {
            Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
            //Destroy(collision.gameObject);
        }
    }
    private void Update()
    {
        if (PlayerPrefs.GetInt("gameOver", 0) == 1)
            Destroy(gameObject);
    }

}
