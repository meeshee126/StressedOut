using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Michael Schmidt

public class GatheringManager : MonoBehaviour
{
    [Header("Sprite after been gathered")]
    [SerializeField]
    Sprite decomposed;

    [Header("Item Spawn Configuration")]
    [SerializeField]
    Vector3 offset;

    [Header("Collider Configuration")]
    [SerializeField]
    float radius;
    [SerializeField]
    LayerMask mask;
    [SerializeField]
    float spacing;

    [Header("Cooldowntimer after fail")]
    [SerializeField]
    float cooldownTimer;

    [Header("Item Drop configurations")]
    [SerializeField]
    [Range(0, 20)]
    int dropMin;
    [SerializeField]
    [Range(0, 20)]
    int dropMax;
    [SerializeField]
    GameObject ressource;

    [Header("Audio")]
    [SerializeField]
    GameObject gatherSFX;

    GeneratorManager generatorManager;
    Collider2D[] colliders;
    SpriteRenderer spriteRenderer;
    LetterEvent letterEvent;
    Player player;
    Timer timer;

    public bool cooldown;
    public float count;

    void Start()
    {
        generatorManager = new GeneratorManager(offset, radius, mask, 
                                                spacing, dropMin, dropMax, 
                                                ressource, colliders);

        spriteRenderer = GetComponent<SpriteRenderer>();
        letterEvent = GameObject.Find("QuickTimeEvent").GetComponent<LetterEvent>();
        player = GameObject.Find("Player").GetComponent<Player>();
        timer = GameObject.Find("GameManager").GetComponent<Timer>();
    }

    /// <summary>
    /// interacting with gatherable object
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerStay2D(Collider2D collision)
    {
        //only gatherable when day
        if (collision.gameObject.name == "Player" && timer.currentDayTime == Timer.DayTime.Day)
        {
            transform.GetChild(0).gameObject.SetActive(true);

            //start Event by pressing "f"
            if(Input.GetKeyDown(KeyCode.F))
            {
                letterEvent.gatherSound = gatherSFX;
                StartQuickTimeEvent();
            }

            if (letterEvent.won)
            {
                this.gameObject.GetComponent<CircleCollider2D>().enabled = false;

                letterEvent.won = false;

                //spawning resources
                generatorManager.SpawnObject(this.transform);

                ChangeSprite();
            }
        }
    }
    
    /// <summary>
    /// show "gather text"
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }

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

    /// <summary>
    /// Change sprite when quick time event won
    /// </summary>
    void ChangeSprite()
    {
        spriteRenderer.sprite = decomposed;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(this.transform.position, offset);
        Gizmos.DrawWireSphere(this.transform.position, radius);
    }
}