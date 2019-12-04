using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Michael Schmidt
public class FadeToBlack : MonoBehaviour
{
    public void Freeze()
    {
        // Frezze Plaer and Timer
        GameObject.Find("Player").GetComponent<Player>().characterRB.velocity = Vector2.zero;
        GameObject.Find("Player").GetComponent<Player>().enabled = false;
        GameObject.Find("GameManager").GetComponent<Timer>().enabled = false;
    }

    public void Defreeze()
    {
        // Defreeze Player and Timer
        GameObject.Find("Player").GetComponent<Player>().enabled = true;
        GameObject.Find("GameManager").GetComponent<Timer>().enabled = true;
    }
}
