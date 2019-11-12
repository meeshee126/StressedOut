using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatheringManager : MonoBehaviour
{
    [SerializeField]
    Sprite decomposed;

    SpriteRenderer spriteRenderer;
    LetterEvent letterEvent;
    Player player;

    bool breakDown;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        letterEvent = GameObject.Find("QuickTimeEvent").GetComponent<LetterEvent>();
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    void ChangeSprite()
    {
        this.gameObject.GetComponent<CircleCollider2D>().enabled = false;
        spriteRenderer.sprite = decomposed;
        letterEvent.won = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            transform.GetChild(0).gameObject.SetActive(true);

            if(Input.GetKeyDown(KeyCode.F))
            {
                StartQuickTimeEvent();
            }

            if (letterEvent.won)
            {
                ChangeSprite();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    //Michael Schmidt
    /// <summary>
    /// Call QuickTimeEvent when start gathering
    /// </summary>
    void StartQuickTimeEvent()
    {
        //enable QuickTimeEvent script
        letterEvent.enabled = true;

        //disable player velocity 
        player.characterRB.velocity = Vector2.zero;

        //disable player script
        player.enabled = false;
    }
}
