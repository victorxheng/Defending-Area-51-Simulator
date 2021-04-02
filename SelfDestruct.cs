using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour {

    private void Start()
    {
        StartCoroutine(timer());
    }
    IEnumerator timer()
    {
        if (PlayerPrefs.GetInt("gameMode", 0) == 0)
            yield return new WaitForSeconds(18);
        else
            yield return new WaitForSeconds(12);
        SpriteRenderer Mine = gameObject.GetComponent<SpriteRenderer>();
        for (float x = 1; x > 0; x = x - Time.deltaTime)
        {
            Mine.color = new Color(Mine.color.r, Mine.color.g, Mine.color.b, x);
            yield return new WaitForSeconds(Time.deltaTime);
        }

        PlayerPrefs.SetInt("mines", PlayerPrefs.GetInt("mines", 5) + 1);
        Destroy(gameObject);
    }
}
