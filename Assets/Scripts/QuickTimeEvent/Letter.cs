using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Michael Schmidt

public class Letter : MonoBehaviour
{
    [Header("Configurations")]
    [SerializeField]
    float speed;
    [SerializeField]
    float timeToDestroy;

    Transform player;

    Timer timer;

    LetterEvent letterEvent;

    float count;

    string character;

    void Start()
    {
        timer = GameObject.Find("GameManager").GetComponent<Timer>();

        player = GameObject.Find("Player").GetComponent<Transform>();

        letterEvent = GameObject.Find("QuickTimeEvent").GetComponent<LetterEvent>();

        //Get last letter from this gameobject name and it to lower case letter 
        character = this.gameObject.name.Substring(6).Remove(1).ToLower();
    }

    void Update()
    {
        CheckDayTime();
        Move();
        PressKey();
        Fail();
    }

    /// <summary>
    /// Chceck if daytime switch to panic mode while gathering
    /// </summary>
    void CheckDayTime()
    {
        //abort gathering when daytime switches to panic mode
        if(timer.currentDayTime != Timer.DayTime.Day)
        {
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// Mobve straight up on y axis
    /// </summary>
    void Move()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;
    }

    /// <summary>
    /// Check Player input
    /// </summary>
    void PressKey()
    {
        //get user input
        if (Input.anyKeyDown)
        {
            //Check if user input is equal with this gameobjects last letter

            //if user input is right
            if (Input.GetKeyDown(character))
            {
                //count up correct input
                letterEvent.correctInput++;

                //play sound when correct input
                if (letterEvent.gatherSound != null) Instantiate(letterEvent.gatherSound, player.position, Quaternion.identity);

                Destroy(this.gameObject);
            }

            //if user input is wrong
            else
            {
                //QuickTimeEvent failed
                letterEvent.Fail();
                Destroy(this.gameObject);
            }
        }
    }

    /// <summary>
    /// Destroy Gameobject if user gives not an input
    /// </summary>
    void Fail()
    {
        count += Time.deltaTime;

        //Destroy this gameobject after a certain amount of time
        if (timeToDestroy < count)
        {
            Destroy(this.gameObject);
            letterEvent.Fail();
        }
    }
}
