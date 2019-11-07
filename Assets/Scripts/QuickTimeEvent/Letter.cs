using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Letter : MonoBehaviour
{
    [Header("Configurations")]
    [SerializeField]
    float speed;
    [SerializeField]
    float timeToDestroy;

    float count;

    string character;

    QuickTimeEvent quickTimeEvent;

    void Start()
    {
        quickTimeEvent = GameObject.Find("QuickTimeEvent").GetComponent<QuickTimeEvent>();
        character = this.gameObject.name.Substring(6).Remove(1).ToLower();
    }

    void Update()
    {
        Move();
        PressKey();
        Destroy();
    }

    void Move()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;
    }

    void PressKey()
    {
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(character))
            {
                quickTimeEvent.correctInput++;
                Destroy(this.gameObject);
            }

            else
            {
                quickTimeEvent.Faile();
                Destroy(this.gameObject);
            }
        }
    }

  

    void Destroy()
    {
        count += Time.deltaTime;

        if(timeToDestroy < count)
        {
            Destroy(this.gameObject);
            quickTimeEvent.Faile();
        }
    }
}
