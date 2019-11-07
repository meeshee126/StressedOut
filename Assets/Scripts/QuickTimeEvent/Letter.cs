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

    LetterEvent quickTimeEvent;

    float count;

    string character;

    void Start()
    {
        quickTimeEvent = GameObject.Find("QuickTimeEvent").GetComponent<LetterEvent>();

        //Get last letter from this gameobject name and it to lower case letter 
        character = this.gameObject.name.Substring(6).Remove(1).ToLower();
    }

    void Update()
    {
        Move();
        PressKey();
        Destroy();
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
                quickTimeEvent.correctInput++;
                Destroy(this.gameObject);
            }

            //if user input is wrong
            else
            {
                //QuickTimeEvent failed
                quickTimeEvent.Fail();
                Destroy(this.gameObject);
            }
        }
    }

    /// <summary>
    /// Destroy Gameobject if user gives not an input
    /// </summary>
    void Destroy()
    {
        count += Time.deltaTime;

        //Destroy this gameobject after a certain amount of time
        if (timeToDestroy < count)
        {
            Destroy(this.gameObject);
            quickTimeEvent.Fail();
        }
    }
}
